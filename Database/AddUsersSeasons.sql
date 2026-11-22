SET XACT_ABORT ON;
BEGIN TRANSACTION;

-- For new databases. Existing dbo.UserSeason tables use RenameUserSeason.sql instead.
CREATE TABLE dbo.UsersSeasons
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_UsersSeasons PRIMARY KEY,
    UserId int NOT NULL,
    SeasonId int NOT NULL,
    CONSTRAINT UQ_UsersSeasons_UserId_SeasonId UNIQUE (UserId, SeasonId),
    CONSTRAINT FK_UsersSeasons_Users FOREIGN KEY (UserId) REFERENCES dbo.Users (Id),
    CONSTRAINT FK_UsersSeasons_Seasons FOREIGN KEY (SeasonId) REFERENCES dbo.Seasons (Id)
);

CREATE INDEX IX_UsersSeasons_SeasonId ON dbo.UsersSeasons (SeasonId);

COMMIT TRANSACTION;
