using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.Attendances;

/// <summary>Payload for creating attendance.</summary>
public sealed record CreateAttendanceRequest
{
    [Range(1, int.MaxValue)]
    public required int ParticipantId { get; init; }

    [Range(1, int.MaxValue)]
    public required int WeekSessionId { get; init; }

    public bool? AttendedWeekSession { get; init; }

    [Range(1, int.MaxValue)]
    public int? ApproverId { get; init; }
}

/// <summary>Payload for replacing attendance.</summary>
public sealed record UpdateAttendanceRequest
{
    [Range(1, int.MaxValue)]
    public required int ParticipantId { get; init; }

    [Range(1, int.MaxValue)]
    public required int WeekSessionId { get; init; }

    public bool? AttendedWeekSession { get; init; }

    [Range(1, int.MaxValue)]
    public int? ApproverId { get; init; }
}

/// <summary>Attendance data returned by the API.</summary>
public sealed record AttendanceResponse(
    int Id,
    int ParticipantId,
    int WeekSessionId,
    bool? AttendedWeekSession,
    int? ApproverId);
