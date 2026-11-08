SET XACT_ABORT ON;
BEGIN TRANSACTION;

CREATE TABLE dbo.Countries
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Countries PRIMARY KEY,
    Name nvarchar(200) NOT NULL
);

CREATE TABLE dbo.Churches
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Churches PRIMARY KEY,
    Name nvarchar(200) NOT NULL,
    CountryId int NOT NULL,
    Area nvarchar(200) NOT NULL,
    CONSTRAINT FK_Churches_Countries FOREIGN KEY (CountryId) REFERENCES dbo.Countries (Id)
);

ALTER TABLE dbo.Users WITH CHECK ADD
    CONSTRAINT FK_Users_Churches FOREIGN KEY (ChurchId) REFERENCES dbo.Churches (Id),
    CONSTRAINT FK_Users_Countries FOREIGN KEY (CountryId) REFERENCES dbo.Countries (Id);

COMMIT TRANSACTION;
