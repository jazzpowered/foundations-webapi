using System.Data;
using Microsoft.Data.SqlClient;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

public class SqlConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection() => new SqlConnection(connectionString);
}
