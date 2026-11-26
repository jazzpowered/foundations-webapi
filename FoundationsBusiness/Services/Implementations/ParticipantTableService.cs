using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal class ParticipantTableService(IParticipantTableRepository repository)
    : Service<ParticipantTable>(repository), IParticipantTableService
{
}
