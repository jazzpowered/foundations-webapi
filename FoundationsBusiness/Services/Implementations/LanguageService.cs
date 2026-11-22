using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal class LanguageService(ILanguageRepository languageRepository)
    : Service<Language>(languageRepository), ILanguageService
{
}
