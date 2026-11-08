using System.Data;

namespace FoundationsBusiness.Repositories;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
