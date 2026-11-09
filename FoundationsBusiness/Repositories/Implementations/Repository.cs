using Dapper;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

internal abstract class Repository<T>(
    IDbConnectionFactory connectionFactory,
    string tableName,
    params string[] columns) : IRepository<T>
{
    private const string IdColumn = "Id";

    private readonly SqlMetadata _metadata = CreateMetadata(tableName, columns);

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var connection = connectionFactory.CreateConnection();
        return await connection.QueryAsync<T>(
            $"SELECT {_metadata.SelectColumns} FROM {_metadata.TableName}");
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        using var connection = connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<T>(
            $"SELECT {_metadata.SelectColumns} FROM {_metadata.TableName} WHERE [{IdColumn}] = @Id",
            new { Id = id });
    }

    public async Task<int> AddAsync(T entity)
    {
        var columns = string.Join(", ", _metadata.WriteColumns.Select(QuoteIdentifier));
        var parameters = string.Join(", ", _metadata.WriteColumns.Select(column => $"@{column}"));
        using var connection = connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            $"INSERT INTO {_metadata.TableName} ({columns}) OUTPUT INSERTED.[{IdColumn}] VALUES ({parameters})",
            entity);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var assignments = string.Join(", ", _metadata.WriteColumns.Select(
            column => $"{QuoteIdentifier(column)} = @{column}"));
        using var connection = connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(
            $"UPDATE {_metadata.TableName} SET {assignments} WHERE [{IdColumn}] = @Id",
            entity) > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = connectionFactory.CreateConnection();
        return await connection.ExecuteAsync(
            $"DELETE FROM {_metadata.TableName} WHERE [{IdColumn}] = @Id",
            new { Id = id }) > 0;
    }

    private static SqlMetadata CreateMetadata(string tableName, string[] columns)
    {
        var tableParts = tableName.Split('.');
        var qualifiedTableName = string.Join(".", tableParts.Select(QuoteIdentifier));

        if (!columns.Contains(IdColumn, StringComparer.Ordinal)
            || columns.Length < 2
            || columns.Distinct(StringComparer.Ordinal).Count() != columns.Length)
        {
            throw new ArgumentException(
                $"Columns must contain '{IdColumn}' exactly once and at least one other column.",
                nameof(columns));
        }

        var selectColumns = string.Join(", ", columns.Select(QuoteIdentifier));
        var writeColumns = columns.Where(column => column != IdColumn).ToArray();
        return new SqlMetadata(qualifiedTableName, selectColumns, writeColumns);
    }

    private static string QuoteIdentifier(string identifier)
    {
        if (string.IsNullOrEmpty(identifier)
            || (!char.IsLetter(identifier[0]) && identifier[0] != '_')
            || identifier.Any(character => !char.IsLetterOrDigit(character) && character != '_'))
        {
            throw new ArgumentException($"'{identifier}' is not a valid SQL identifier.", nameof(identifier));
        }

        return $"[{identifier}]";
    }

    private sealed record SqlMetadata(string TableName, string SelectColumns, string[] WriteColumns);
}
