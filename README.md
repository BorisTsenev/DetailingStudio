# DetailingStudio

DetailingStudio is an ASP.NET Core MVC web application for managing the daily operations of an automotive detailing studio.

The application provides functionality for managing customers, vehicles, detailing services, orders, expenses, and business statistics through a centralized dashboard.

The project was developed as part of an ASP.NET Core Fundamentals course project.

---

## Features

### Dashboard

The dashboard provides an overview of the current business activity.

It includes:

* Completed orders for the current month
* Services performed during the current month
* Monthly revenue
* Monthly expenses
* Monthly profit
* Total number of customers
* Recent orders
* Pie chart showing completed services
* Daily Revenue vs Expenses line chart for the current month

Only completed orders are included when calculating realized revenue.

---

### Customer Management

The application provides full CRUD functionality for customers.

Features include:

* Create customers
* View customer details
* Edit customer information
* Delete customers
* Store phone number and email information
* Associate multiple vehicles with the same customer

---

### Vehicle Management

Vehicles can be registered and associated with customers.

Stored vehicle information includes:

* Make
* Model
* Registration number
* VIN
* Year
* Color
* Vehicle category
* Owner

The application also provides search functionality for customers and vehicles.

---

### Vehicle Categories

Vehicles are separated into categories that are used when determining service prices.

Default categories include:

* Small
* Medium
* Large
* SUV
* Van

Vehicle categories are automatically inserted into the database through Entity Framework Core seed data.

---

### Service Management

Detailing services can be created and managed from the application.

Each service can have a different price depending on the vehicle category.

Example:

| Vehicle Category | One Step Polish |
| ---------------- | --------------: |
| Small            |            €100 |
| Medium           |            €120 |
| Large            |            €140 |
| SUV              |            €160 |
| Van              |            €180 |

Services can also be classified as:

* Main
* Additional

Inactive services remain stored in the database but are not offered when creating new orders.

---

### Order Management

Orders connect customers, vehicles, services, appointments, and prices.

When creating an order, the user can:

* Select an existing vehicle
* Search existing vehicles
* Create a new vehicle
* Select an existing customer
* Search customers by name or phone number
* Create a new customer
* Select multiple detailing services
* View service prices based on the selected vehicle category
* View the automatically calculated order total
* Select an appointment date and time
* Add notes

The server retrieves service prices directly from the database.

When an order is created, the selected price is copied into the order. This preserves historical pricing even if the current price of a service is later changed.

Order statuses include:

* Pending
* In Progress
* Completed
* Cancelled

Historical orders are preserved instead of being deleted.

---

### Expense Tracker

The application contains an expense tracking module.

Expenses include:

* Description
* Amount
* Expense date
* Expense category
* Notes

Default expense categories include:

* Consumables
* Rent
* Utilities
* Equipment
* Marketing
* Other

Expense data is used together with completed orders to calculate revenue, expenses, and profit on the dashboard.

---

### Light and Dark Mode

The interface supports both light and dark themes.

The selected theme is stored locally and remains active after navigation or browser refresh.

---

## Technologies Used

* C#
* ASP.NET Core MVC
* Entity Framework Core
* Microsoft SQL Server
* Razor Views
* Bootstrap
* JavaScript
* Chart.js
* HTML
* CSS
* Git
* GitHub

---

## Architecture

The application follows the ASP.NET Core MVC architecture.

Main project areas include:

```text
Controllers/
Data/
    Configurations/
    Migrations/
    Models/
Models/
    ViewModels/
Views/
wwwroot/
```

Entity Framework Core is used for database access and relationship management.

Fluent API configurations are used for:

* Entity relationships
* Primary and foreign keys
* Decimal precision
* Delete behavior
* Unique indexes
* Seed data

---

## Main Database Entities

The main entities include:

* Customer
* Vehicle
* VehicleCategory
* Service
* ServicePrice
* DetailingOrder
* DetailingOrderService
* Expense
* ExpenseCategory

### Main Relationships

```text
Customer
   └── Vehicles

Vehicle
   └── DetailingOrders

VehicleCategory
   ├── Vehicles
   └── ServicePrices

Service
   ├── ServicePrices
   └── DetailingOrderServices

DetailingOrder
   └── DetailingOrderServices

ExpenseCategory
   └── Expenses
```

---

# Installation and Setup

## Requirements

Before running the project, make sure the following are installed:

* Git
* .NET SDK compatible with the project
* Microsoft SQL Server LocalDB
* Entity Framework Core CLI tools

Visual Studio is recommended but not required.

---

## 1. Clone the Repository

Clone the project from GitHub:

```bash
git clone YOUR_REPOSITORY_URL
```

You can also choose a different local folder name:

```bash
git clone YOUR_REPOSITORY_URL DetailingStudio-Test
```

Then open the project directory:

```bash
cd DetailingStudio-Test
```

Make sure you are in the directory containing:

```text
DetailingStudio.csproj
```

You can check with:

```bash
dir *.csproj
```

---

## 2. Restore NuGet Packages

Run:

```bash
dotnet restore
```

This restores the required NuGet packages and generates the necessary build assets.

---

## 3. Verify SQL Server LocalDB

Run:

```bash
sqllocaldb info
```

You should see:

```text
MSSQLLocalDB
```

The project uses the following default connection:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DetailingStudioDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

No machine-specific SQL Server name is required.

---

## 4. Install Entity Framework Core CLI

Check whether Entity Framework CLI is available:

```bash
dotnet ef --version
```

If the command is not found, install it:

```bash
dotnet tool install --global dotnet-ef
```

After installation, restart the terminal if necessary.

---

## 5. Create the Database

Apply all existing migrations:

```bash
dotnet ef database update
```

This will:

* Create the `DetailingStudioDB` database
* Create all application tables
* Create all configured relationships
* Insert the seeded vehicle categories
* Insert the seeded expense categories

No manual database creation or SQL script execution is required.

---

## 6. Trust the Development HTTPS Certificate

Run:

```bash
dotnet dev-certs https --trust
```

Accept the certificate confirmation if prompted.

---

## 7. Run the Application

Start the project using the HTTPS launch profile:

```bash
dotnet run --launch-profile https
```

The console should display addresses similar to:

```text
Now listening on: https://localhost:7217
Now listening on: http://localhost:5031
```

Open the HTTPS address in your browser.

Example:

```text
https://localhost:7217
```

The exact port may differ depending on the local launch settings.

---

## Clean Installation Test

A clean installation can be tested by cloning the repository into a new folder:

```bash
git clone YOUR_REPOSITORY_URL DetailingStudio-CleanTest
cd DetailingStudio-CleanTest

dotnet restore
dotnet ef database update
dotnet dev-certs https --trust
dotnet run --launch-profile https
```

If the project starts successfully without modifying the source code, the installation is complete.

---

## Business Logic

Important business rules implemented in the application include:

* One customer can own multiple vehicles.
* Existing customers can be reused when adding new vehicles.
* Services can have different prices depending on vehicle category.
* Only active services are available when creating new orders.
* Service prices are retrieved from the database on the server.
* The price of a service is stored in the order at the moment the order is created.
* Historical order prices therefore remain unchanged when current service prices are updated.
* Orders can be marked as Pending, In Progress, Completed, or Cancelled.
* Completed orders are used when calculating realized revenue.
* Expenses are tracked separately.
* Monthly profit is calculated using revenue minus expenses.
* Dashboard statistics are generated from the stored order and expense data.

---

## Dashboard Statistics

The dashboard includes:

* Completed orders this month
* Services performed this month
* Revenue this month
* Expenses this month
* Profit this month
* Total customers
* Recent orders
* Services distribution chart
* Daily revenue and expenses chart

---

## Version Control

Git is used for version control.

Development was performed using feature branches for different parts of the application and changes were merged into the main branch after completion.

The repository contains the development history through multiple commits.

---

## AI Usage

AI tools were used as development assistance, mainly for:

* UI/UX improvements
* Styling
* Debugging support
* Development guidance

The project architecture, database design, entity relationships, controllers, CRUD functionality, validation, and core business logic were implemented and understood by the author.

---

## Possible Future Improvements

Possible future improvements include:

* Advanced financial reports
* Custom date range filtering
* Invoice generation
* Service dependency rules
* Customer notifications
* Export to PDF or Excel
* More advanced dashboard analytics
* Additional reporting functionality

---

## Author

**Boris Tsenev**

ASP.NET Core MVC course project.
