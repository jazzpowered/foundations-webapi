SET XACT_ABORT ON;
BEGIN TRANSACTION;

CREATE TABLE dbo.Semesters
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Semesters PRIMARY KEY,
    Name nvarchar(200) NOT NULL
);

CREATE TABLE dbo.Seasons
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Seasons PRIMARY KEY,
    Year int NOT NULL,
    LanguageId int NOT NULL,
    ChurchId int NOT NULL,
    SemesterId int NOT NULL,
    Active bit NOT NULL,
    CONSTRAINT FK_Seasons_Languages FOREIGN KEY (LanguageId) REFERENCES dbo.Languages (Id),
    CONSTRAINT FK_Seasons_Churches FOREIGN KEY (ChurchId) REFERENCES dbo.Churches (Id),
    CONSTRAINT FK_Seasons_Semesters FOREIGN KEY (SemesterId) REFERENCES dbo.Semesters (Id)
);

COMMIT TRANSACTION;
