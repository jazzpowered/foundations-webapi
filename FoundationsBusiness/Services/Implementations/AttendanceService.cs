using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal class AttendanceService(IAttendanceRepository attendanceRepository)
    : Service<Attendance>(attendanceRepository), IAttendanceService
{
}
