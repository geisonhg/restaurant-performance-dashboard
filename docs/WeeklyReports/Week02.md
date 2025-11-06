# 📅 Week 02 – Project Progress Report  
**Project:** Restaurant Performance Dashboard  
**Team Members:** Geison Herrera & Daniel Vega  
**Date:** 04–10 November 2025  
**Status:** 🟢 On Track  


## Weekly Summary  
During this second week, we focused on the **core backend foundation** of our project.  
Our goal was to design the main **entities**, set up the **database context (DbContext)** and successfully configure **Entity Framework Core** to handle our data model.  

Even though we encountered several issues with namespaces, migrations, and dependencies, we managed to fix all of them and establish a fully functional backend structure that connects correctly with SQL Server.



## Technical Work Completed  

### 1. Entity Design  
We created the main domain entities that represent the restaurant’s operational data:

- **Sale:** records total sales, taxes, and net income per day.  
- **Tip:** stores daily tip amounts distributed among staff.  
- **Expense:** tracks spending with category, amount, notes, and recurring flag.  
- **Staff:** represents employees involved in operations and tip distribution.  
- **TipRule:** defines FOH and BOH percentage rules for tips.  
- **Import:** created for uploading or importing sales/tips reports.  
- **Stock:** will be developed later for inventory control.  

Each class now has a unique ID, relevant fields, and a clear structure ready for CRUD operations.

---

### 🗃️ 2. Database Context Setup  
We developed the **DashboardDbContext** class inside the *Infrastructure* layer.  
This context manages the link between the application and the database.  
It includes `DbSet<>` definitions for all entities and the `OnModelCreating` method to specify decimal precision and avoid SQL truncation issues.  

Example:
c#
modelBuilder.Entity<Expense>(e =>
{
    e.Property(p => p.Amount).HasColumnType("decimal(18,2)");
});

This ensures consistent handling of all monetary data such as sales, tips, and expenses.

3. Migrations and Database Configuration
We successfully generated and applied two migrations:

InitialCreate → Created the base database structure.

SetDecimalPrecision → Adjusted the precision for financial fields.

Commands used:

bash
Copy code
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet ef migrations add SetDecimalPrecision
dotnet ef database update
After installing the missing Microsoft.EntityFrameworkCore.SqlServer package in the Infrastructure layer and rebuilding the solution, the database was fully updated and functional.

4. Tools and Architecture
Framework: .NET 9

ORM: Entity Framework Core

Database: SQL Server

Architecture: Clean separation between Domain, Infrastructure, API, and Reporting.

Version Control: GitHub with protected main branch and individual update/report branches.

The architecture is now stable and ready for CRUD implementations and controller setup in the following weeks.

Challenges & Solutions
Challenge	Solution
EF couldn’t find entity references (DbSet<>)	Fixed namespaces and project references.
Build failed during migration	Installed Microsoft.EntityFrameworkCore.SqlServer in Infrastructure.
“Pending model changes” error	Created the SetDecimalPrecision migration.
Missing decimal precision warnings	Added OnModelCreating configuration for all decimal fields.

Each problem was addressed systematically, improving our understanding of how Entity Framework handles model synchronization and project layering.

Next Steps (Week 03)
Implement CRUD operations for Sales, Tips, and Expenses.

Create API controllers and connect them to the database.

Seed sample data for testing.

Begin preparing the mock frontend connection.

Reflection
This week was both challenging and rewarding.
We spent time debugging build and migration issues, but this process helped us understand how EF Core works under the hood.
Now, the backend feels solid and structured — a real foundation for our project’s next features.
Next week, we’re excited to finally start connecting the API with real data and building endpoints.

Summary:

Week 02 focused on backend structure, EF Core configuration, and database setup.
All objectives were met, and the project is ready to progress toward CRUD and data interaction.

Overall Progress: 🟢 On Track and Performing Well