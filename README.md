ProjectIost

Inventory, Order, and Supply Tracking Application

1. Overview

This project is a C# Windows Forms Application using Entity Framework Core and MySQL.
It follows the MVC architecture (Model-View-Controller) for cleaner code organization.

The system includes:

Inventory management

Order and supply tracking

Analytics (e.g., Best Sellers, Profit)

Entity Framework Core with MySQL integration

Dependency Injection for managing services and forms

2. Requirements

Before starting, make sure the following are installed:

Visual Studio 2022

.NET 8 SDK

MySQL Server (can be through XAMPP)

Git (for version control)

Optional:

MySQL Workbench for database management

3. Getting Started
Step 1. Clone the Repository

If the repository is on GitHub, open a terminal and run:

git clone https://github.com/<your-username>/projectIost.git
cd projectIost


Replace <your-username> with your actual GitHub username or organization name.

Step 2. Open in Visual Studio

Open Visual Studio 2022.

Select File → Open → Project/Solution.

Choose the folder you cloned (projectIost).

Make sure the target framework is .NET 8.0.

Step 3. Configure Database Connection

Create or open the file named appsettings.json in your project root folder.
Add the following content:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1;Database=iostdb;User=root;Password=;"
  }
}


Then, right-click appsettings.json → Properties →
Set Copy to Output Directory to Copy always.

Step 4. Install Dependencies

Open the terminal in Visual Studio (View → Terminal) and run:

dotnet restore


Then install the required packages:

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.Extensions.Hosting
dotnet add package Microsoft.Extensions.DependencyInjection

Step 5. Setup the Folder Structure

Inside the project, create these folders:

/Data
/Models
/Services
/Controllers
/Forms


Example contents:

Data/IostDbContext.cs

using Microsoft.EntityFrameworkCore;
using projectIost.Models;

namespace projectIost.Data
{
    public class IostDbContext : DbContext
    {
        public IostDbContext(DbContextOptions<IostDbContext> options)
            : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}


Models/Item.cs

namespace projectIost.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

Step 6. Build and Run

Build and run the project to make sure it works:

dotnet build
dotnet run


Or press F5 inside Visual Studio.

4. Git Workflow (For Teams)

This section explains how your team should use Git for collaboration.

Step 1. Clone or Fork the Repository

Each member should clone or fork the repository once:

git clone https://github.com/<main-repo>/projectIost.git

Step 2. Create a Branch for Each Task

Do not work directly on the main branch.
Create a new branch for every new feature or fix:

git checkout -b feature/add-inventory-module

Step 3. Work on Your Code

Write your code in the appropriate folders (Models, Controllers, etc.).

Test it locally by running the project.

When ready to save:

git add .
git commit -m "Added inventory module with CRUD operations"

Step 4. Update Your Branch

Before pushing, make sure your branch is up to date:

git fetch origin
git pull origin main


If there are conflicts, resolve them before pushing.

Step 5. Push Your Changes
git push origin feature/add-inventory-module

Step 6. Create a Pull Request

Go to your GitHub repository.

Click Compare & pull request.

Choose your branch and describe your changes.

Submit it for review.

Step 7. Review and Merge

The team lead reviews the pull request.

If approved, it is merged into the main branch.

After merging, everyone should update their local main branch:

git checkout main
git pull origin main

Step 8. Clean Up

Once your feature is merged, you can delete the branch locally:

git branch -d feature/add-inventory-module

5. Workflow for Working on the Project

Pull the latest version of main.

Create a new branch for your feature or fix.

Implement and test your changes.

Commit your changes.

Push your branch to GitHub.

Create a pull request.

Wait for approval and merge.

Pull the latest main branch again.

6. Recommended Folder Structure
projectIost/
│
├── appsettings.json
├── Program.cs
│
├── Data/
│   └── IostDbContext.cs
│
├── Models/
│   └── Item.cs
│
├── Services/
│   └── IostService.cs
│
├── Controllers/
│   └── IostController.cs
│
├── Forms/
│   └── MainForm.cs
│
└── projectIost.csproj

7. Common Errors and Fixes
Problem	Fix
UseMySql not found	Install Pomelo.EntityFrameworkCore.MySql
ServerVersion not found	Add using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
ToListAsync not found	Add using Microsoft.EntityFrameworkCore;
Unable to resolve service for type ...	Register it in builder.Services in Program.cs
8. Best Practices

Use meaningful branch names (e.g., feature/add-orders, fix/login-bug).

Write clear commit messages.

Don’t push directly to main.

Always pull before starting new work.

Keep your code modular: put logic in services or controllers, not forms.

Test changes before creating a pull request.
