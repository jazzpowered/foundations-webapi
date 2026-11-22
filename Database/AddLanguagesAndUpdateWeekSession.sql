SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF EXISTS (SELECT 1 FROM dbo.WeekSessions WITH (TABLOCKX, HOLDLOCK))
    THROW 50000, 'WeekSession contains rows; assign a language to existing rows before migrating.', 1;

CREATE TABLE dbo.Languages
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Languages PRIMARY KEY,
    Name nvarchar(50) NOT NULL
);

EXEC sp_rename N'dbo.WeekSessions.Session', N'Week', N'COLUMN';

ALTER TABLE dbo.WeekSessions ADD
    LanguageId int NOT NULL
        CONSTRAINT FK_WeekSessions_Languages REFERENCES dbo.Languages (Id);

COMMIT TRANSACTION;
