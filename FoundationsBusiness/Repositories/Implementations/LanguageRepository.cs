using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

internal class LanguageRepository(IDbConnectionFactory connectionFactory)
    : Repository<Language>(connectionFactory, "dbo.Languages", "Id", "Name"),
        ILanguageRepository
{
}
