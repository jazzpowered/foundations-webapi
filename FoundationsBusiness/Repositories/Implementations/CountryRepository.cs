using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

internal class CountryRepository(IDbConnectionFactory connectionFactory)
    : Repository<Country>(connectionFactory, "dbo.Countries", "Id", "Name"),
        ICountryRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;
}
