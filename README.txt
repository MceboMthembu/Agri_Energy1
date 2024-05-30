# Agri-Energy Connect Platform

## Overview
Agri-Energy Connect is a web-based platform designed to streamline agricultural operations and product management for farmers and employees. This prototype demonstrates a functional model of the intended final product.

## Table of Contents
1. [Setting Up the Development Environment](#setting-up-the-development-environment)
2. [Database Setup and Configuration](#database-setup-and-configuration)
3. [Building and Running the Prototype](#building-and-running-the-prototype)
4. [System Functionalities and User Roles](#system-functionalities-and-user-roles)
5. [SQL Queries to Verify Data Population](#sql-queries-to-verify-data-population)

## Setting Up the Development Environment

### Prerequisites
- Visual Studio 2019 or later
- SQL Server Management Studio (SSMS)
- .NET Core SDK 3.1 or later

### Development Machine
- **Processor**: Intel Core i5 or AMD Ryzen 5 (or equivalent) or higher
- **RAM**: Minimum 8 GB (16 GB recommended)
- **Storage**: SSD with at least 100 GB of free space
- **Operating System**: Windows 10 or later, or macOS Mojave (10.14) or later
- **Display**: 1920 x 1080 resolution (Full HD) or higher



### Steps
1. **Clone the Repository**:
    ```sh
    git clone https://github.com/your-repo/agri-energy-connect.git
    cd agri-energy-connect
    ```

2. **Open the Project in Visual Studio**:
    - Launch Visual Studio.
    - Open the `Agri_Energy1.sln` solution file.

3. **Install Dependencies**:
    - Open the Package Manager Console in Visual Studio.
    - Run the following command to install all necessary packages:
      ```sh
      Update-Package
      ```

## Database Setup and Configuration

### Setting Up AgriDB Database in SSMS
1. **Create the Database**:
    - Open SSMS and connect to your SQL Server instance.
    - Execute the following SQL script to create the `AgriDB` database:
      ```sql
      CREATE DATABASE AgriDB;
      ```
    - Or Use 

### Configuring Database Connection in Visual Studio
1. **Update `appsettings.json`**:
    - Open `appsettings.json`.
    - Update the `ConnectionStrings` section with your SQL Server details:
      ```json
      "ConnectionStrings": {
         "DefaultConnection": "Server=[YOUR PC NAME];Database=AgriDB;TrustServerCertificate=true;Trusted_Connection=True;MultipleActiveResultSets=true",

      }
      ```

2. **Apply Migrations**: Making sure there are no conflicting migrations with any others
    - Open the Package Manager Console in Visual Studio.
    - Run the following commands to apply migrations and update and create the tables in the database:
      ```sh
      Add-Migration InitialCreate
      Update-Database
      ```

3. **Verify if tables have been added to the database**:
     -```Run these queries on a sql queries to verif databse table creation 
USE AgriDB;
SELECT * FROM Farmers;
SELECT * FROM AspNetRoles;
SELECT * FROM AspNetUsers;
SELECT * FROM AspNetUserRoles;
SELECT * FROM FarmerProducts;


## Building and Running the Prototype

### Build the Project
1. In Visual Studio, select `Build > Build Solution` or press `Ctrl+Shift+B`.
2. Ensure there are no build errors before proceeding.

### Run the Project
1. Set the startup project to `Agri_Energy1`.
2. Click on the `IIS Express` button or press `F5` to run the project.

## System Functionalities and User Roles

### User Roles
1. **Farmer**:
    - Can add new products to their profile.
    - Can view their own product listings.

2. **Employee**:
    - Can add new farmer profiles.
    - Can view and filter products from specific farmers.
    - Use filters to search products by date range and category.

### Functionalities
- **Farmers**: Can log in once their account is created by an Employee
    - Log in to manage their product inventory.
    - Add, edit, and delete product listings.

- **Employees**: An employee account by the name of dave@gmail.com has been created already with the password Pass@1234 to ensure easier accessibity into the system without having to register
    - Register new farmer profiles.
    - View all farmers and their products.
    - Apply filters to search for specific products.

## SQL Queries to Verify Data Population

Execute again the following queries in SSMS to verify that the tables and data are correctly populated:

```sql
USE AgriDB;
SELECT * FROM Farmers;
SELECT * FROM AspNetRoles;
SELECT * FROM AspNetUsers;
SELECT * FROM AspNetUserRoles;
SELECT * FROM FarmerProducts;
