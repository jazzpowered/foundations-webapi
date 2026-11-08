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
        return await connection.QueryAsync<User>("SELECT Id, Name, Email FROM Users");
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<User>(
            "SELECT Id, Name, Email FROM Users WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> AddAsync(User user)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "INSERT INTO Users (Name, Email) OUTPUT INSERTED.Id VALUES (@Name, @Email)", user);
    }

    public async Task<bool> UpdateAsync(User user)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(
            "UPDATE Users SET Name = @Name, Email = @Email WHERE Id = @Id", user) > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id }) > 0;
    }
}
