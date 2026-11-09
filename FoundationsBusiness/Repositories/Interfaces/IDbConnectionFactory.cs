using System.Data;

namespace FoundationsBusiness.Repositories.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
