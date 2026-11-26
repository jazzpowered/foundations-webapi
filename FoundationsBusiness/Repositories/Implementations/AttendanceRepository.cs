using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

internal class AttendanceRepository(IDbConnectionFactory connectionFactory)
    : Repository<Attendance>(
        connectionFactory,
        "dbo.Attendances",
        "Id",
        "ParticipantId",
        "WeekSessionId",
        "AttendedWeekSession",
        "ApproverId"),
        IAttendanceRepository
{
}
