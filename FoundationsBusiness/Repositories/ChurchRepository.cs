using Dapper;
using FoundationsBusiness.Models;

namespace FoundationsBusiness.Repositories;

internal class ChurchRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ChurchRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Church>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Church>("SELECT Id, Name, CountryId, Area FROM dbo.Churches");
    }

    public async Task<Church?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Church>(
            "SELECT Id, Name, CountryId, Area FROM dbo.Churches WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> AddAsync(Church church)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "INSERT INTO dbo.Churches (Name, CountryId, Area) OUTPUT INSERTED.Id VALUES (@Name, @CountryId, @Area)",
            church);
    }

    public async Task<bool> UpdateAsync(Church church)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(
            "UPDATE dbo.Churches SET Name = @Name, CountryId = @CountryId, Area = @Area WHERE Id = @Id",
            church) > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(
            "DELETE FROM dbo.Churches WHERE Id = @Id", new { Id = id }) > 0;
    }
}
