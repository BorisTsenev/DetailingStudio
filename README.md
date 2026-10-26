# DetailingStudio

DetailingStudio is an ASP.NET Core MVC web application designed for managing the daily operations of an automotive detailing studio.

The application provides functionality for managing customers, vehicles, detailing services, orders, expenses, and business statistics through a centralized dashboard.

The project was developed as part of the ASP.NET Core Fundamentals course project.

---

## Features

### Dashboard

The dashboard provides an overview of the detailing studio's activity.

It includes:

* Completed orders for the current month
* Number of services performed
* Monthly revenue
* Monthly expenses
* Profit overview
* Total customers
* Recent orders
* Pie chart showing performed services
* Daily Revenue vs Expenses line chart for the current month

Only completed orders are included when calculating revenue.

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

The application supports searching for existing customers when creating a vehicle.

---

### Vehicle Categories

Vehicles are separated into categories that are used when determining service prices.

Examples include:

* Small
* Medium
* Large
* SUV
* Van

Vehicle categories are seeded automatically into the database.

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

Inactive services are preserved in the database but are not offered when creating new orders.

---

### Order Management

Orders connect customers, vehicles, services, appointments, and prices.

When creating an order, the user can:

* Select an existing vehicle
* Search vehicles by make, model, registration number, customer name, or phone number
* Create a new vehicle
* Select an existing customer
* Search customers by name or phone number
* Create a new customer
* Select multiple services
* See service prices based on the selected vehicle category
* See the calculated order total
* Select an appointment date and time
* Add notes

Service prices are retrieved from the database on the server.

The selected price is stored in the order when it is created. This preserves historical prices even if the service price is changed later.

Orders can have statuses such as:

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
* Category
* Notes

Expense categories include examples such as:

* Consumables
* Rent
* Utilities
* Equipment
* Marketing
* Other

The expense data is used together with completed orders to calculate revenue, expenses, and profit on the dashboard.

---

### Authentication

ASP.NET Core Identity is included in the application.

Users can:

* Register
* Log in
* Log out

---

### Light and Dark Mode

The interface supports both light and dark themes.

The selected theme is saved locally so the preference is preserved when navigating through the application or reopening it.

---

## Technologies Used

* C#
* ASP.NET Core MVC
* Entity Framework Core
* Microsoft SQL Server
* ASP.NET Core Identity
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

Main parts of the project include:

```text
Controllers
    DashboardController
    CustomersController
    VehiclesController
    ServicesController
    DetailingOrdersController
    ExpensesController

Data
    Models
    Configurations
    Migrations
    DetailingStudioDbContext

Models
    ViewModels

Views
    Dashboard
    Customers
    Vehicles
    Services
    DetailingOrders
    Expenses
    Home
```

Entity Framework Core is used for database access and relationship management.

Fluent API configurations are used to configure entity relationships, constraints, decimal precision, delete behavior, and seed data.

---

## Main Database Entities

The project contains entities including:

* Customer
* Vehicle
* VehicleCategory
* Service
* ServicePrice
* DetailingOrder
* DetailingOrderService
* Expense
* ExpenseCategory

### Important Relationships

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

* .NET SDK compatible with the project
* SQL Server LocalDB or Microsoft SQL Server
* Git

Visual Studio is recommended but not required.

---

## 1. Clone the repository

```bash
git clone YOUR_REPOSITORY_URL
```

Open the cloned directory:

```bash
cd DetailingStudio
```

Navigate to the directory containing the `.csproj` file if necessary.

---

## 2. Restore dependencies

Run:

```bash
dotnet restore
```

This restores the required NuGet packages.

---

## 3. Database Configuration

The project uses Microsoft SQL Server.

For local development, the recommended connection string is based on SQL Server LocalDB:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DetailingStudioDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

If another SQL Server instance is used, configure the `DefaultConnection` connection string in the appropriate local configuration file.

Do not commit passwords or other sensitive credentials to the repository.

---

## 4. Install Entity Framework CLI tools

If `dotnet ef` is not already installed:

```bash
dotnet tool install --global dotnet-ef
```

If it is already installed, this step can be skipped.

---

## 5. Create the database

Apply the existing Entity Framework Core migrations:

```bash
dotnet ef database update
```

This creates the database schema and inserts the configured seed data.

---

## 6. Run the application

Run:

```bash
dotnet run
```

Alternatively, open the solution in Visual Studio and start the project from there.

The application will display the local URL in the console.

For example:

```text
https://localhost:xxxx
```

Open the displayed address in a web browser.

---

# Testing the Installation

After starting the application on a new environment, verify the following:

1. The application starts without code changes.
2. The database is created successfully.
3. Vehicle categories are available.
4. Expense categories are available.
5. A user can register and log in.
6. Customers can be created.
7. Vehicles can be created.
8. Services and category-specific prices can be created.
9. Orders can be created.
10. Expenses can be created.
11. Dashboard statistics and charts are displayed.

---

## Business Logic

Some important business rules implemented in the project include:

* One customer can own multiple vehicles.
* Services have different prices depending on the vehicle category.
* Only active services are available when creating new orders.
* Order prices are calculated using prices stored in the database.
* The price of a service is copied into the order when the order is created, preserving historical pricing.
* Existing customers can be reused when registering additional vehicles.
* Completed orders are used to calculate realized revenue.
* Expenses are tracked separately.
* Profit is calculated using revenue and expenses.
* Historical order information is preserved.

---

## Version Control

The project uses Git for version control.

Development was performed using separate feature branches for parts of the application and merged into the main branch after completion.

The repository contains the project development history through multiple commits.

---

## AI Usage

AI tools were used only as assistance during development, primarily for UI/UX improvements, styling, debugging support, and development guidance.

The application's architecture, database design, entity relationships, controllers, CRUD functionality, validation, and core business logic were implemented and understood by the author.

---

## Future Improvements

Possible future improvements include:

* More advanced financial reports
* Revenue and expense filtering by custom date range
* Service dependency rules
* Invoice generation
* Customer appointment notifications
* More detailed reporting and statistics
* Role-based authorization
* Exporting reports to PDF or Excel

---

## Author

**Boris Tsenev**

ASP.NET Core MVC course project.
