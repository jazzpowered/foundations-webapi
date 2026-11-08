using Dapper;
using FoundationsBusiness.Models;

namespace FoundationsBusiness.Repositories;

internal class UserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<User>(
            "SELECT Id, ChurchId, CountryId, FirstName, LastName, Email, Gender, Area, ZipCode FROM Users");
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<User>(
            "SELECT Id, ChurchId, CountryId, FirstName, LastName, Email, Gender, Area, ZipCode " +
            "FROM Users WHERE Id = @Id",
            new { Id = id });
    }

    public async Task<int> AddAsync(User user)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "INSERT INTO Users (ChurchId, CountryId, FirstName, LastName, Email, Gender, Area, ZipCode) " +
            "OUTPUT INSERTED.Id VALUES (@ChurchId, @CountryId, @FirstName, @LastName, @Email, @Gender, @Area, @ZipCode)",
            user);
    }

    public async Task<bool> UpdateAsync(User user)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(
            "UPDATE Users SET ChurchId = @ChurchId, CountryId = @CountryId, FirstName = @FirstName, " +
            "LastName = @LastName, Email = @Email, Gender = @Gender, Area = @Area, ZipCode = @ZipCode " +
            "WHERE Id = @Id",
            user) > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id }) > 0;
    }
}
