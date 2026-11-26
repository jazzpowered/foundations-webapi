using System;

namespace FoundationsBusiness.Models;

public class Attendance
{
    public required int Id { get; set; }
    public required int ParticipantId { get; set; }
    public required int WeekSessionId { get; set; }
    public bool? AttendedWeekSession { get; set; }
    public int? ApproverId { get; set; }
}
