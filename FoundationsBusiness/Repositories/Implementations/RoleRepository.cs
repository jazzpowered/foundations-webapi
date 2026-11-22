using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

internal class RoleRepository(IDbConnectionFactory connectionFactory)
    : Repository<Role>(connectionFactory, "dbo.Roles", "Id", "Name"),
        IRoleRepository
{
}
