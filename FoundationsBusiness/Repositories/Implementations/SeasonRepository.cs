using Dapper;
using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

internal class SeasonRepository(IDbConnectionFactory connectionFactory)
    : Repository<Season>(connectionFactory, "dbo.Seasons",
        "Id", "Year", "LanguageId", "ChurchId", "SemesterId", "Active"),
        ISeasonRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task<IEnumerable<Season>> GetByUserIdAsync(
        int userId, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Season>(new CommandDefinition(
            """
            SELECT s.Id, s.Year, s.LanguageId, s.ChurchId, s.SemesterId, s.Active
            FROM dbo.Seasons AS s
            INNER JOIN dbo.UsersSeasons AS us ON us.SeasonId = s.Id
            WHERE us.UserId = @UserId
            """,
            new { UserId = userId },
            cancellationToken: cancellationToken));
    }
}
