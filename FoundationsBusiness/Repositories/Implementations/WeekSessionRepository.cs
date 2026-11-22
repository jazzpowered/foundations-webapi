using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

internal class WeekSessionRepository(IDbConnectionFactory connectionFactory)
    : Repository<WeekSession>(connectionFactory, "dbo.WeekSessions", "Id", "LanguageId", "Week", "Active", "Content"),
        IWeekSessionRepository
{
}
