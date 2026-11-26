using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.ParticipantTables;

/// <summary>Payload for creating a participant table.</summary>
public sealed record CreateParticipantTableRequest
{
    [Required]
    [StringLength(200)]
    public required string Name { get; init; }

    [Range(1, int.MaxValue)]
    public required int SeasonId { get; init; }

    [Required]
    [StringLength(50)]
    public required string Gender { get; init; }

    [Required]
    [StringLength(50)]
    public required string Day { get; init; }

    public required DateTime Time { get; init; }

    public required IEnumerable<int> Leaders { get; init; }

    public required IEnumerable<int> Participants { get; init; }
}

/// <summary>Payload for replacing a participant table.</summary>
public sealed record UpdateParticipantTableRequest
{
    [Required]
    [StringLength(200)]
    public required string Name { get; init; }

    [Range(1, int.MaxValue)]
    public required int SeasonId { get; init; }

    [Required]
    [StringLength(50)]
    public required string Gender { get; init; }

    [Required]
    [StringLength(50)]
    public required string Day { get; init; }

    public required DateTime Time { get; init; }

    public required IEnumerable<int> Leaders { get; init; }

    public required IEnumerable<int> Participants { get; init; }
}

/// <summary>Participant table data returned by the API.</summary>
public sealed record ParticipantTableResponse(
    int Id,
    string Name,
    int SeasonId,
    string Gender,
    string Day,
    DateTime Time,
    IEnumerable<int> Leaders,
    IEnumerable<int> Participants);
