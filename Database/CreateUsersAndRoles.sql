-- Run before AddChurchesAndCountries.sql, which adds the remaining User foreign keys.
SET XACT_ABORT ON;
BEGIN TRANSACTION;

CREATE TABLE dbo.Roles
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Roles PRIMARY KEY,
    Name nvarchar(50) NOT NULL
);

CREATE TABLE dbo.Users
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
    ChurchId int NOT NULL,
    CountryId int NOT NULL,
    RoleId int NOT NULL,
    FirstName nvarchar(100) NOT NULL,
    LastName nvarchar(100) NOT NULL,
    Email nvarchar(320) NOT NULL,
    Gender nvarchar(50) NOT NULL,
    Area nvarchar(200) NOT NULL,
    ZipCode nvarchar(20) NOT NULL,
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES dbo.Roles (Id)
);

COMMIT TRANSACTION;
