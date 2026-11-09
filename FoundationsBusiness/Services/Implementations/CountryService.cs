using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal class CountryService(ICountryRepository countryRepository)
    : Service<Country>(countryRepository), ICountryService
{
}
