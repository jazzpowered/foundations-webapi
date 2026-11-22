using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.WeekSessions;

/// <summary>Payload for creating a week session.</summary>
public sealed record CreateWeekSessionRequest
{
    [Range(1, int.MaxValue)]
    public required int LanguageId { get; init; }

    public required int Week { get; init; }

    public required bool Active { get; init; }

    [Required]
    public required string Content { get; init; }
}

/// <summary>Payload for replacing a week session.</summary>
public sealed record UpdateWeekSessionRequest
{
    [Range(1, int.MaxValue)]
    public required int LanguageId { get; init; }

    public required int Week { get; init; }

    public required bool Active { get; init; }

    [Required]
    public required string Content { get; init; }
}

/// <summary>Week session data returned by the API.</summary>
public sealed record WeekSessionResponse(int Id, int LanguageId, int Week, bool Active, string Content);
