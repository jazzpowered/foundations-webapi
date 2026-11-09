using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

internal class ChurchRepository(IDbConnectionFactory connectionFactory)
    : Repository<Church>(connectionFactory, "dbo.Churches", "Id", "Name", "CountryId", "Area"),
        IChurchRepository
{
}
