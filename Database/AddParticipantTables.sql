SET XACT_ABORT ON;
BEGIN TRANSACTION;

CREATE TABLE dbo.ParticipantTables
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ParticipantTables PRIMARY KEY,
    Name nvarchar(200) NOT NULL,
    SeasonId int NOT NULL,
    Gender nvarchar(50) NOT NULL,
    Day nvarchar(50) NOT NULL,
    Time datetime2 NOT NULL,
    CONSTRAINT FK_ParticipantTables_Seasons FOREIGN KEY (SeasonId) REFERENCES dbo.Seasons (Id)
);

CREATE TABLE dbo.ParticipantTableLeaders
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ParticipantTableLeaders PRIMARY KEY,
    ParticipantTableId int NOT NULL,
    UserId int NOT NULL,
    CONSTRAINT UQ_ParticipantTableLeaders_ParticipantTableId_UserId UNIQUE (ParticipantTableId, UserId),
    CONSTRAINT FK_ParticipantTableLeaders_ParticipantTables FOREIGN KEY (ParticipantTableId)
        REFERENCES dbo.ParticipantTables (Id) ON DELETE CASCADE,
    CONSTRAINT FK_ParticipantTableLeaders_Users FOREIGN KEY (UserId)
        REFERENCES dbo.Users (Id) ON DELETE CASCADE
);

CREATE INDEX IX_ParticipantTableLeaders_UserId
    ON dbo.ParticipantTableLeaders (UserId);

CREATE TABLE dbo.ParticipantTableParticipants
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ParticipantTableParticipants PRIMARY KEY,
    ParticipantTableId int NOT NULL,
    UserId int NOT NULL,
    CONSTRAINT UQ_ParticipantTableParticipants_ParticipantTableId_UserId UNIQUE (ParticipantTableId, UserId),
    CONSTRAINT FK_ParticipantTableParticipants_ParticipantTables FOREIGN KEY (ParticipantTableId)
        REFERENCES dbo.ParticipantTables (Id) ON DELETE CASCADE,
    CONSTRAINT FK_ParticipantTableParticipants_Users FOREIGN KEY (UserId)
        REFERENCES dbo.Users (Id) ON DELETE CASCADE
);

CREATE INDEX IX_ParticipantTableParticipants_UserId
    ON dbo.ParticipantTableParticipants (UserId);

COMMIT TRANSACTION;
