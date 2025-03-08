CREATE DATABASE CarSalesDB;
USE CarSalesDB;

CREATE TABLE CarModels (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Brand NVARCHAR(50) NOT NULL,
    Class NVARCHAR(50) NOT NULL,
    ModelName NVARCHAR(100) NOT NULL,
    ModelCode NVARCHAR(10) NOT NULL CHECK (ModelCode LIKE '[A-Za-z0-9]%'),
    Description NVARCHAR(MAX) NOT NULL,
    Features NVARCHAR(MAX) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    DateOfManufacturing DATETIME NOT NULL,
    Active BIT NOT NULL,
    SortOrder INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Sales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Salesman NVARCHAR(100) NOT NULL,
    Class NVARCHAR(10) NOT NULL,
    Brand NVARCHAR(50) NOT NULL,
    CarsSold INT NOT NULL,
    TotalSaleAmount DECIMAL(18,2) NOT NULL
);

CREATE TABLE Salesman (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    LastYearTotalSales DECIMAL(18,2) NOT NULL
);