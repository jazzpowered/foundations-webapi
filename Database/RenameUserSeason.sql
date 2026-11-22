-- Run only on databases with the original dbo.UserSeason table.
SET XACT_ABORT ON;
BEGIN TRANSACTION;

ALTER TABLE dbo.UserSeason ADD Id int IDENTITY(1,1) NOT NULL;
ALTER TABLE dbo.UserSeason DROP CONSTRAINT PK_UserSeason;
ALTER TABLE dbo.UserSeason ADD
    CONSTRAINT PK_UsersSeasons PRIMARY KEY (Id),
    CONSTRAINT UQ_UsersSeasons_UserId_SeasonId UNIQUE (UserId, SeasonId);

EXEC sp_rename N'dbo.UserSeason', N'UsersSeasons';
EXEC sp_rename N'dbo.FK_UserSeason_Users', N'FK_UsersSeasons_Users', N'OBJECT';
EXEC sp_rename N'dbo.FK_UserSeason_Seasons', N'FK_UsersSeasons_Seasons', N'OBJECT';
EXEC sp_rename N'dbo.UsersSeasons.IX_UserSeason_SeasonId', N'IX_UsersSeasons_SeasonId', N'INDEX';

COMMIT TRANSACTION;
