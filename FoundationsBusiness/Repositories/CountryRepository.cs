using Dapper;
using FoundationsBusiness.Models;

namespace FoundationsBusiness.Repositories;

internal class CountryRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CountryRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Country>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Country>("SELECT Id, Name FROM dbo.Countries");
    }

    public async Task<Country?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Country>(
            "SELECT Id, Name FROM dbo.Countries WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> AddAsync(Country country)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "INSERT INTO dbo.Countries (Name) OUTPUT INSERTED.Id VALUES (@Name)", country);
    }

    public async Task<bool> UpdateAsync(Country country)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(
            "UPDATE dbo.Countries SET Name = @Name WHERE Id = @Id", country) > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(
            "DELETE FROM dbo.Countries WHERE Id = @Id", new { Id = id }) > 0;
    }
}
