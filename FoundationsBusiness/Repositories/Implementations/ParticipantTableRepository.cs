using Dapper;
using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;
using System.Data;

namespace FoundationsBusiness.Repositories.Implementations;

internal class ParticipantTableRepository(IDbConnectionFactory connectionFactory)
    : IParticipantTableRepository
{
    private const string SelectColumns = "Id, Name, SeasonId, Gender, Day, Time";

    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task<IEnumerable<ParticipantTable>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        var participantTables = (await connection.QueryAsync<ParticipantTable>(
            new CommandDefinition(
                $"SELECT {SelectColumns} FROM dbo.ParticipantTables",
                cancellationToken: cancellationToken))).ToList();

        await LoadUsersAsync(connection, participantTables, cancellationToken);
        return participantTables;
    }

    public async Task<ParticipantTable?> GetByIdAsync(
        int id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        var participantTable = await connection.QuerySingleOrDefaultAsync<ParticipantTable>(
            new CommandDefinition(
                $"SELECT {SelectColumns} FROM dbo.ParticipantTables WHERE Id = @Id",
                new { Id = id },
                cancellationToken: cancellationToken));

        if (participantTable is not null)
        {
            await LoadUsersAsync(connection, [participantTable], cancellationToken);
        }

        return participantTable;
    }

    public async Task<int> AddAsync(
        ParticipantTable entity, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        var id = await connection.ExecuteScalarAsync<int>(new CommandDefinition(
            """
            INSERT INTO dbo.ParticipantTables (Name, SeasonId, Gender, Day, Time)
            OUTPUT INSERTED.Id
            VALUES (@Name, @SeasonId, @Gender, @Day, @Time)
            """,
            entity,
            transaction,
            cancellationToken: cancellationToken));

        await SaveUsersAsync(connection, transaction, id, entity, cancellationToken);
        transaction.Commit();
        return id;
    }

    public async Task<bool> UpdateAsync(
        ParticipantTable entity, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        var updated = await connection.ExecuteAsync(new CommandDefinition(
            """
            UPDATE dbo.ParticipantTables
            SET Name = @Name, SeasonId = @SeasonId, Gender = @Gender, Day = @Day, Time = @Time
            WHERE Id = @Id
            """,
            entity,
            transaction,
            cancellationToken: cancellationToken));

        if (updated == 0)
        {
            transaction.Rollback();
            return false;
        }

        await connection.ExecuteAsync(new CommandDefinition(
            """
            DELETE FROM dbo.ParticipantTableLeaders WHERE ParticipantTableId = @Id;
            DELETE FROM dbo.ParticipantTableParticipants WHERE ParticipantTableId = @Id;
            """,
            new { entity.Id },
            transaction,
            cancellationToken: cancellationToken));
        await SaveUsersAsync(connection, transaction, entity.Id, entity, cancellationToken);
        transaction.Commit();
        return true;
    }

    public async Task<bool> DeleteAsync(
        int id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        await connection.ExecuteAsync(new CommandDefinition(
            """
            DELETE FROM dbo.ParticipantTableLeaders WHERE ParticipantTableId = @Id;
            DELETE FROM dbo.ParticipantTableParticipants WHERE ParticipantTableId = @Id;
            """,
            new { Id = id },
            transaction,
            cancellationToken: cancellationToken));

        var deleted = await connection.ExecuteAsync(new CommandDefinition(
            "DELETE FROM dbo.ParticipantTables WHERE Id = @Id",
            new { Id = id },
            transaction,
            cancellationToken: cancellationToken));

        transaction.Commit();
        return deleted > 0;
    }

    private static async Task LoadUsersAsync(
        IDbConnection connection,
        IReadOnlyCollection<ParticipantTable> participantTables,
        CancellationToken cancellationToken)
    {
        if (participantTables.Count == 0)
        {
            return;
        }

        var parameters = new { ParticipantTableIds = participantTables.Select(table => table.Id).ToArray() };
        var leaders = await connection.QueryAsync<UserAssignment>(new CommandDefinition(
            """
            SELECT ParticipantTableId, UserId
            FROM dbo.ParticipantTableLeaders
            WHERE ParticipantTableId IN @ParticipantTableIds
            """,
            parameters,
            cancellationToken: cancellationToken));
        var participants = await connection.QueryAsync<UserAssignment>(new CommandDefinition(
            """
            SELECT ParticipantTableId, UserId
            FROM dbo.ParticipantTableParticipants
            WHERE ParticipantTableId IN @ParticipantTableIds
            """,
            parameters,
            cancellationToken: cancellationToken));

        var leadersByTable = leaders
            .GroupBy(assignment => assignment.ParticipantTableId)
            .ToDictionary(group => group.Key, group => group.Select(assignment => assignment.UserId).ToArray());
        var participantsByTable = participants
            .GroupBy(assignment => assignment.ParticipantTableId)
            .ToDictionary(group => group.Key, group => group.Select(assignment => assignment.UserId).ToArray());

        foreach (var table in participantTables)
        {
            table.Leaders = leadersByTable.GetValueOrDefault(table.Id) ?? [];
            table.Participants = participantsByTable.GetValueOrDefault(table.Id) ?? [];
        }
    }

    private static async Task SaveUsersAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        int participantTableId,
        ParticipantTable entity,
        CancellationToken cancellationToken)
    {
        var leaders = entity.Leaders.Distinct()
            .Select(userId => new { ParticipantTableId = participantTableId, UserId = userId });
        var participants = entity.Participants.Distinct()
            .Select(userId => new { ParticipantTableId = participantTableId, UserId = userId });

        await connection.ExecuteAsync(new CommandDefinition(
            """
            INSERT INTO dbo.ParticipantTableLeaders (ParticipantTableId, UserId)
            VALUES (@ParticipantTableId, @UserId)
            """,
            leaders,
            transaction,
            cancellationToken: cancellationToken));
        await connection.ExecuteAsync(new CommandDefinition(
            """
            INSERT INTO dbo.ParticipantTableParticipants (ParticipantTableId, UserId)
            VALUES (@ParticipantTableId, @UserId)
            """,
            participants,
            transaction,
            cancellationToken: cancellationToken));
    }

    private sealed class UserAssignment
    {
        public int ParticipantTableId { get; set; }
        public int UserId { get; set; }
    }
}
