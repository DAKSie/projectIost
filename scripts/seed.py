import mysql.connector
from mysql.connector import Error
from datetime import date, timedelta

# Database configuration - same as migration script
DB_CONFIG = {
    'host': 'localhost',
    'user': 'root',
    'password': '',
    'port': 3306,
    'database_name': 'iostdb'
}

def seed_database():
    """Seed the database with sample data"""
    
    connection = None
    cursor = None
    
    try:
        # Connect to database
        config = DB_CONFIG.copy()
        config['database'] = config.pop('database_name')
        connection = mysql.connector.connect(**config)
        cursor = connection.cursor()
        
        print(f"Seeding database: {DB_CONFIG['database_name']}")
        print("=" * 50)
        
        # Clear existing data (optional - comment out if you want to keep existing data)
        print("Clearing existing data...")
        tables_to_clear = ['Supply_Details', 'Order_Details', 'Supply', 'Orders', 'Item', 'User']
        for table in tables_to_clear:
            try:
                cursor.execute(f"DELETE FROM {table}")
                print(f"Cleared {table} table")
            except Error as e:
                print(f"Note: Could not clear {table}: {e}")
        
        # Reset auto-increment counters
        cursor.execute("ALTER TABLE User AUTO_INCREMENT = 1")
        cursor.execute("ALTER TABLE Item AUTO_INCREMENT = 1")
        cursor.execute("ALTER TABLE Orders AUTO_INCREMENT = 1")
        cursor.execute("ALTER TABLE Supply AUTO_INCREMENT = 1")
        
        # 1. Seed Users
        print("\nSeeding Users table...")
        users_data = [
            ('admin', 'admin123', True),
            ('manager', 'manager123', False),
            ('staff1', 'staff123', False),
            ('staff2', 'staff123', False),
            ('john_doe', 'password123', False)
        ]
        
        user_insert = "INSERT INTO User (user_name, user_password, user_isAdmin) VALUES (%s, %s, %s)"
        cursor.executemany(user_insert, users_data)
        print(f"Added {cursor.rowcount} users")
        
        # 2. Seed Items
        print("\nSeeding Items table...")
        items_data = [
            ('SKU001', 'Laptop Dell XPS 13', 25, 800.00, 1199.99),
            ('SKU002', 'iPhone 15 Pro', 50, 899.00, 1299.99),
            ('SKU003', 'Samsung Galaxy Tab S9', 30, 450.00, 699.99),
            ('SKU004', 'Wireless Mouse Logitech', 100, 15.00, 29.99),
            ('SKU005', 'Mechanical Keyboard', 75, 45.00, 89.99),
            ('SKU006', '27-inch Monitor 4K', 20, 250.00, 399.99),
            ('SKU007', 'Bluetooth Headphones', 60, 35.00, 79.99),
            ('SKU008', 'USB-C Hub 7-in-1', 40, 20.00, 49.99),
            ('SKU009', 'External SSD 1TB', 35, 60.00, 119.99),
            ('SKU010', 'Webcam 1080p', 45, 25.00, 59.99)
        ]
        
        item_insert = """
        INSERT INTO Item (item_SKU, item_name, item_quantity, item_cost, item_price) 
        VALUES (%s, %s, %s, %s, %s)
        """
        cursor.executemany(item_insert, items_data)
        print(f"Added {cursor.rowcount} items")
        
        # 3. Seed Orders
        print("\nSeeding Orders table...")
        today = date.today()
        orders_data = [
            (today - timedelta(days=10), 'Tech Solutions Inc.', 2399.98, 2),
            (today - timedelta(days=8), 'Global Enterprises', 1799.97, 3),
            (today - timedelta(days=5), 'Startup Innovations', 299.97, 1),
            (today - timedelta(days=3), 'University of Tech', 4899.95, 2),
            (today - timedelta(days=1), 'Government Office', 159.96, 4)
        ]
        
        order_insert = """
        INSERT INTO Orders (date, customer, total, user_id) 
        VALUES (%s, %s, %s, %s)
        """
        cursor.executemany(order_insert, orders_data)
        print(f"Added {cursor.rowcount} orders")
        
        # 4. Seed Order Details
        print("\nSeeding Order Details table...")
        order_details_data = [
            # Order 1: Tech Solutions Inc.
            (1, 1, 1, 1199.99, 1199.99),  # Laptop
            (1, 2, 1, 1199.99, 1199.99),  # iPhone
            
            # Order 2: Global Enterprises
            (2, 3, 2, 699.99, 1399.98),   # 2x Samsung Tablet
            (2, 6, 1, 399.99, 399.99),    # Monitor
            
            # Order 3: Startup Innovations
            (3, 4, 3, 29.99, 89.97),      # 3x Mouse
            (3, 5, 2, 89.99, 179.98),     # 2x Keyboard
            (3, 8, 1, 49.99, 49.99),      # USB-C Hub
            
            # Order 4: University of Tech
            (4, 1, 3, 1199.99, 3599.97),  # 3x Laptop
            (4, 6, 2, 399.99, 799.98),    # 2x Monitor
            (4, 9, 5, 119.99, 599.95),    # 5x External SSD
            
            # Order 5: Government Office
            (5, 4, 2, 29.99, 59.98),      # 2x Mouse
            (5, 7, 1, 79.99, 79.99),      # Headphones
            (5, 10, 1, 59.99, 59.99),     # Webcam
        ]
        
        order_details_insert = """
        INSERT INTO Order_Details (order_id, item_id, quantity, price, subtotal) 
        VALUES (%s, %s, %s, %s, %s)
        """
        cursor.executemany(order_details_insert, order_details_data)
        print(f"Added {cursor.rowcount} order details")
        
        # 5. Seed Supply
        print("\nSeeding Supply table...")
        supply_data = [
            (today - timedelta(days=15), 'Tech Distributors Co.', 16000.00, 1),
            (today - timedelta(days=12), 'Electronics Wholesale', 9000.00, 2),
            (today - timedelta(days=7), 'Global Components Ltd.', 5250.00, 3),
            (today - timedelta(days=2), 'Hardware Suppliers Inc.', 3000.00, 4)
        ]
        
        supply_insert = """
        INSERT INTO Supply (date, supplier, total, user_id) 
        VALUES (%s, %s, %s, %s)
        """
        cursor.executemany(supply_insert, supply_data)
        print(f"Added {cursor.rowcount} supply records")
        
        # 6. Seed Supply Details
        print("\nSeeding Supply Details table...")
        supply_details_data = [
            # Supply 1: Tech Distributors Co.
            (1, 1, 20, 800.00, 16000.00),  # 20x Laptops
            
            # Supply 2: Electronics Wholesale
            (2, 2, 10, 899.00, 8990.00),   # 10x iPhones
            
            # Supply 3: Global Components Ltd.
            (3, 3, 5, 450.00, 2250.00),    # 5x Samsung Tablets
            (3, 4, 50, 15.00, 750.00),     # 50x Mice
            (3, 5, 25, 45.00, 1125.00),    # 25x Keyboards
            (3, 6, 5, 250.00, 1250.00),    # 5x Monitors
            
            # Supply 4: Hardware Suppliers Inc.
            (4, 7, 30, 35.00, 1050.00),    # 30x Headphones
            (4, 8, 20, 20.00, 400.00),     # 20x USB-C Hubs
            (4, 9, 15, 60.00, 900.00),     # 15x External SSDs
            (4, 10, 25, 25.00, 625.00),    # 25x Webcams
        ]
        
        supply_details_insert = """
        INSERT INTO Supply_Details (supply_id, item_id, quantity, price, subtotal) 
        VALUES (%s, %s, %s, %s, %s)
        """
        cursor.executemany(supply_details_insert, supply_details_data)
        print(f"Added {cursor.rowcount} supply details")
        
        # Commit all changes
        connection.commit()
        print("\n" + "=" * 50)
        print("✅ Database seeded successfully!")
        
        # Display summary
        display_seed_summary(cursor)
        
    except Error as e:
        print(f"❌ Error seeding database: {e}")
        if connection:
            connection.rollback()
            
    finally:
        if cursor:
            cursor.close()
        if connection:
            connection.close()
            print("\nDatabase connection closed")

def display_seed_summary(cursor):
    """Display a summary of the seeded data"""
    
    print("\n📊 SEED DATA SUMMARY")
    print("=" * 50)
    
    # Count records in each table
    tables = ['User', 'Item', 'Orders', 'Order_Details', 'Supply', 'Supply_Details']
    
    for table in tables:
        cursor.execute(f"SELECT COUNT(*) FROM {table}")
        count = cursor.fetchone()[0]
        print(f"{table:<15}: {count:>3} records")
    
    # Display some sample data
    print("\n📋 SAMPLE DATA")
    print("=" * 50)
    
    # Sample users
    cursor.execute("SELECT user_id, user_name, user_isAdmin FROM User LIMIT 3")
    users = cursor.fetchall()
    print("\n👥 Sample Users:")
    for user in users:
        admin_status = "Admin" if user[2] else "Staff"
        print(f"  ID: {user[0]}, Name: {user[1]}, Role: {admin_status}")
    
    # Sample items
    cursor.execute("SELECT item_id, item_SKU, item_name, item_quantity FROM Item LIMIT 3")
    items = cursor.fetchall()
    print("\n📦 Sample Items:")
    for item in items:
        print(f"  ID: {item[0]}, SKU: {item[1]}, Name: {item[2]}, Qty: {item[3]}")
    
    # Recent orders
    cursor.execute("""
        SELECT o.order_number, o.customer, o.total, o.date 
        FROM Orders o 
        ORDER BY o.date DESC 
        LIMIT 2
    """)
    orders = cursor.fetchall()
    print("\n🛒 Recent Orders:")
    for order in orders:
        print(f"  Order #{order[0]}, Customer: {order[1]}, Total: ${order[2]}, Date: {order[3]}")

def verify_data_integrity():
    """Verify that the seeded data maintains referential integrity"""
    
    try:
        config = DB_CONFIG.copy()
        config['database'] = config.pop('database_name')
        connection = mysql.connector.connect(**config)
        cursor = connection.cursor()
        
        print("\n🔍 DATA INTEGRITY CHECK")
        print("=" * 50)
        
        # Check for orphaned records
        checks = [
            ("Order_Details with invalid order_id", 
             "SELECT COUNT(*) FROM Order_Details od LEFT JOIN Orders o ON od.order_id = o.order_number WHERE o.order_number IS NULL"),
            
            ("Order_Details with invalid item_id", 
             "SELECT COUNT(*) FROM Order_Details od LEFT JOIN Item i ON od.item_id = i.item_id WHERE i.item_id IS NULL"),
            
            ("Supply_Details with invalid supply_id", 
             "SELECT COUNT(*) FROM Supply_Details sd LEFT JOIN Supply s ON sd.supply_id = s.supply_id WHERE s.supply_id IS NULL"),
            
            ("Supply_Details with invalid item_id", 
             "SELECT COUNT(*) FROM Supply_Details sd LEFT JOIN Item i ON sd.item_id = i.item_id WHERE i.item_id IS NULL"),
            
            ("Orders with invalid user_id", 
             "SELECT COUNT(*) FROM Orders o LEFT JOIN User u ON o.user_id = u.user_id WHERE u.user_id IS NULL"),
            
            ("Supply with invalid user_id", 
             "SELECT COUNT(*) FROM Supply s LEFT JOIN User u ON s.user_id = u.user_id WHERE u.user_id IS NULL")
        ]
        
        all_valid = True
        for check_name, query in checks:
            cursor.execute(query)
            count = cursor.fetchone()[0]
            status = "✅ PASS" if count == 0 else "❌ FAIL"
            print(f"{check_name}: {status} ({count} issues)")
            if count > 0:
                all_valid = False
        
        if all_valid:
            print("\n🎉 All data integrity checks passed!")
        else:
            print("\n⚠️  Some data integrity issues found.")
            
        cursor.close()
        connection.close()
        
    except Error as e:
        print(f"Error during integrity check: {e}")

if __name__ == "__main__":
    print("Starting database seeding...")
    print("Make sure XAMPP MySQL is running and database exists!")
    print("-" * 50)
    
    seed_database()
    
    print("\n" + "=" * 50)
    print("Running data integrity verification...")
    verify_data_integrity()
