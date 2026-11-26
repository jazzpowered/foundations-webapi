SET XACT_ABORT ON;
BEGIN TRANSACTION;

CREATE TABLE dbo.Attendances
(
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Attendances PRIMARY KEY,
    ParticipantId int NOT NULL,
    WeekSessionId int NOT NULL,
    AttendedWeekSession bit NULL,
    ApproverId int NULL,
    CONSTRAINT UQ_Attendances_ParticipantId_WeekSessionId
        UNIQUE (ParticipantId, WeekSessionId),
    CONSTRAINT FK_Attendances_Users_Participant FOREIGN KEY (ParticipantId)
        REFERENCES dbo.Users (Id),
    CONSTRAINT FK_Attendances_WeekSessions FOREIGN KEY (WeekSessionId)
        REFERENCES dbo.WeekSessions (Id),
    CONSTRAINT FK_Attendances_Users_Approver FOREIGN KEY (ApproverId)
        REFERENCES dbo.Users (Id)
);

CREATE INDEX IX_Attendances_WeekSessionId
    ON dbo.Attendances (WeekSessionId);

CREATE INDEX IX_Attendances_ApproverId
    ON dbo.Attendances (ApproverId);

COMMIT TRANSACTION;
