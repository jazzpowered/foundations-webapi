using Dapper;
using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

internal class UserRepository(IDbConnectionFactory connectionFactory)
    : Repository<User>(
        connectionFactory,
        "dbo.Users",
        "Id",
        "ChurchId",
        "CountryId",
        "RoleId",
        "FirstName",
        "LastName",
        "Email",
        "Gender",
        "Area",
        "ZipCode"),
        IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task<IEnumerable<User>> GetBySeasonIdAsync(
        int seasonId, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<User>(new CommandDefinition(
            """
            SELECT u.Id, u.ChurchId, u.CountryId, u.RoleId, u.FirstName, u.LastName,
                   u.Email, u.Gender, u.Area, u.ZipCode
            FROM dbo.Users AS u
            INNER JOIN dbo.UsersSeasons AS us ON us.UserId = u.Id
            WHERE us.SeasonId = @SeasonId
            """,
            new { SeasonId = seasonId },
            cancellationToken: cancellationToken));
    }
}
