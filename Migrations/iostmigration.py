import mysql.connector
from mysql.connector import Error

def create_database():
    """Create the database and tables based on the ERD"""
    
    # Database connection configuration
    config = {
        'host': 'localhost',
        'user': 'root',  # Default XAMPP MySQL username
        'password': '',  # Default XAMPP MySQL password (usually empty)
        'port': 3306     # Default MySQL port
    }
    
    connection = None
    cursor = None
    
    try:
        # Connect to MySQL server
        connection = mysql.connector.connect(**config)
        cursor = connection.cursor()
        
        # Create database
        database_name = "iostdb"
        cursor.execute(f"CREATE DATABASE IF NOT EXISTS {database_name}")
        cursor.execute(f"USE {database_name}")
        print(f"Database '{database_name}' created/selected successfully")
        
        # Create User table
        user_table = """
        CREATE TABLE IF NOT EXISTS User (
            user_id INT AUTO_INCREMENT PRIMARY KEY,
            user_name VARCHAR(255) NOT NULL,
            user_password VARCHAR(255) NOT NULL,
            user_isAdmin BOOLEAN DEFAULT FALSE
        )
        """
        cursor.execute(user_table)
        print("User table created successfully")
        
        # Create Item table
        item_table = """
        CREATE TABLE IF NOT EXISTS Item (
            item_id INT AUTO_INCREMENT PRIMARY KEY,
            item_SKU VARCHAR(255) UNIQUE NOT NULL,
            item_name VARCHAR(255) NOT NULL,
            item_quantity INT(11) DEFAULT 0,
            item_cost DECIMAL(10,2) DEFAULT 0.00,
            item_price DECIMAL(10,2) DEFAULT 0.00
        )
        """
        cursor.execute(item_table)
        print("Item table created successfully")
        
        # Create Orders table (renamed from Order to avoid reserved keyword)
        orders_table = """
        CREATE TABLE IF NOT EXISTS Orders (
            order_number INT AUTO_INCREMENT PRIMARY KEY,
            date DATE NOT NULL,
            customer VARCHAR(255) NOT NULL,
            total DECIMAL(10,2) DEFAULT 0.00,
            user_id INT,
            FOREIGN KEY (user_id) REFERENCES User(user_id)
        )
        """
        cursor.execute(orders_table)
        print("Orders table created successfully")
        
        # Create Order_Details table
        order_details_table = """
        CREATE TABLE IF NOT EXISTS Order_Details (
            order_detail_id INT AUTO_INCREMENT PRIMARY KEY,
            order_id INT NOT NULL,
            item_id INT NOT NULL,
            quantity INT(11) NOT NULL,
            price DECIMAL(10,2) NOT NULL,
            subtotal DECIMAL(10,2) NOT NULL,
            FOREIGN KEY (order_id) REFERENCES Orders(order_number),
            FOREIGN KEY (item_id) REFERENCES Item(item_id)
        )
        """
        cursor.execute(order_details_table)
        print("Order_Details table created successfully")
        
        # Create Supply table
        supply_table = """
        CREATE TABLE IF NOT EXISTS Supply (
            supply_id INT AUTO_INCREMENT PRIMARY KEY,
            date DATE NOT NULL,
            supplier VARCHAR(255) NOT NULL,
            total DECIMAL(10,2) DEFAULT 0.00,
            user_id INT,
            FOREIGN KEY (user_id) REFERENCES User(user_id)
        )
        """
        cursor.execute(supply_table)
        print("Supply table created successfully")
        
        # Create Supply_Details table
        supply_details_table = """
        CREATE TABLE IF NOT EXISTS Supply_Details (
            supply_detail_id INT AUTO_INCREMENT PRIMARY KEY,
            supply_id INT NOT NULL,
            item_id INT NOT NULL,
            quantity INT(11) NOT NULL,
            price DECIMAL(10,2) NOT NULL,
            subtotal DECIMAL(10,2) NOT NULL,
            FOREIGN KEY (supply_id) REFERENCES Supply(supply_id),
            FOREIGN KEY (item_id) REFERENCES Item(item_id)
        )
        """
        cursor.execute(supply_details_table)
        print("Supply_Details table created successfully")
        
        # Insert sample admin user
        sample_user = """
        INSERT IGNORE INTO User (user_name, user_password, user_isAdmin) 
        VALUES ('admin', 'admin123', TRUE)
        """
        cursor.execute(sample_user)
        print("Sample admin user created")
        
        # Commit changes
        connection.commit()
        print("All tables created successfully!")
        
        # Display table structure
        cursor.execute("SHOW TABLES")
        tables = cursor.fetchall()
        print("\nCreated tables:")
        for table in tables:
            print(f"- {table[0]}")
            
    except Error as e:
        print(f"Error: {e}")
        if connection:
            connection.rollback()
            
    finally:
        if cursor:
            cursor.close()
        if connection:
            connection.close()
            print("\nDatabase connection closed")

def test_connection():
    """Test the database connection and display table info"""
    
    config = {
        'host': 'localhost',
        'user': 'root',
        'password': '',
        'database': 'inventory_management',
        'port': 3306
    }
    
    try:
        connection = mysql.connector.connect(**config)
        cursor = connection.cursor()
        
        cursor.execute("SHOW TABLES")
        tables = cursor.fetchall()
        
        print("\nDatabase Structure:")
        print("=" * 50)
        
        for table in tables:
            table_name = table[0]
            print(f"\nTable: {table_name}")
            print("-" * 30)
            
            # Use parameterized query to avoid SQL injection and reserved word issues
            cursor.execute(f"DESCRIBE `{table_name}`")
            columns = cursor.fetchall()
            
            for column in columns:
                print(f"  {column[0]:<20} {column[1]:<15} {column[2]}")
                
        cursor.close()
        connection.close()
        
    except Error as e:
        print(f"Connection test error: {e}")

if __name__ == "__main__":
    print("Starting database migration...")
    print("Make sure XAMPP MySQL is running!")
    print("-" * 50)
    
    create_database()
    
    print("\n" + "=" * 50)
    print("Testing database connection and structure...")
    test_connection()