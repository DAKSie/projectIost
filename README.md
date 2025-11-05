# Project Iost

## Introduction

**Project Iost** is a C# Windows Forms inventory management system (IMS) designed for small businesses, warehouses, or anyone who needs robust inventory control with MySQL backend support. With Project Iost, you can create, view, edit, and delete inventory records, manage supply orders and purchases, and benefit from a comprehensive management interface built for real-world efficiency. The project also includes Python migration scripts for database setup and updates.

---

## Requirements

- **Windows OS** (tested on Windows 10/11)
- **.NET Framework 4.7.2 or higher**
- **Visual Studio 2019 or above** (for building/running the C# app)
- **XAMPP MySQL** (for local MySQL database server)
- **Python 3.7+** (for running database migration scripts)
- Python package:  
  ```
  pip install mysql-connector-python
  ```

---

## Database Setup & Migrations

1. **Install XAMPP:**
   - Download and install [XAMPP](https://www.apachefriends.org/index.html) and start the MySQL service.

2. **Configure MySQL:**
   - Set up a MySQL user and password (update credentials in your C# app and scripts as needed).

3. **Run Migration Scripts:**
   - Open a terminal and navigate to the `Migrations/` directory:
     ```
     cd Migrations
     ```
   - Execute the migration script using Python:
     ```
     python migrate.py
     ```
   - This will set up required tables and initial data in your MySQL database.

   > **Note:** You may need to update MySQL connection settings within `migrate.py` to match your environment.

---

## Building & Running the Program

1. **Clone the Repository:**
   ```
   git clone https://github.com/DAKSie/projectIost.git
   ```

2. **Open with Visual Studio:**
   - Open the solution file (`.sln`) in Visual Studio.

3. **Configure MySQL Connection:**
   - Update your app config (usually in `app.config` or `settings.cs`) with your MySQL server credentials.

4. **Build the Solution:**
   - In Visual Studio, build the project via `Build > Build Solution` or press `Ctrl+Shift+B`.

5. **Run the Application:**
   - Start the program with `F5` or from the Debug menu.
   - Use the UI to manage inventory, orders, and purchases.

---

## Contributors

- **DAKSie** ([GitHub Profile](https://github.com/DAKSie))
  - Main author, Bakend developer and maintainer.
- **DAKSie** ([GitHub Profile](https://github.com/joshuafrellte))
  - Co-author, Full-stack.
- **DAKSie** ([GitHub Profile](https://github.com/Hakari716))
  - Co-author, Front-end.
---

> For issues, suggestions, or new contributions, please open an issue or pull request in this repository!

