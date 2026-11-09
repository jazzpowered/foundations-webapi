using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal class ChurchService(IChurchRepository churchRepository)
    : Service<Church>(churchRepository), IChurchService
{
}
