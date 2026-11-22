CREATE TABLE dbo.WeekSessions
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_WeekSession PRIMARY KEY,
    [Session] int NOT NULL,
    Active bit NOT NULL,
    Content nvarchar(max) NOT NULL
);
