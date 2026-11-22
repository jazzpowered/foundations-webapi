using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal class WeekSessionService(IWeekSessionRepository weekSessionRepository)
    : Service<WeekSession>(weekSessionRepository), IWeekSessionService
{
}
