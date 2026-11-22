using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.Seasons;

/// <summary>Payload for creating a season.</summary>
public sealed record CreateSeasonRequest
{
    public required int Year { get; init; }

    [Range(1, int.MaxValue)]
    public required int LanguageId { get; init; }

    [Range(1, int.MaxValue)]
    public required int ChurchId { get; init; }

    [Range(1, int.MaxValue)]
    public required int SemesterId { get; init; }

    public required bool Active { get; init; }
}

/// <summary>Payload for replacing a season.</summary>
public sealed record UpdateSeasonRequest
{
    public required int Year { get; init; }

    [Range(1, int.MaxValue)]
    public required int LanguageId { get; init; }

    [Range(1, int.MaxValue)]
    public required int ChurchId { get; init; }

    [Range(1, int.MaxValue)]
    public required int SemesterId { get; init; }

    public required bool Active { get; init; }
}

/// <summary>Season data returned by the API.</summary>
public sealed record SeasonResponse(int Id, int Year, int LanguageId, int ChurchId, int SemesterId, bool Active);
