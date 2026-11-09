using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.Churches;

/// <summary>Payload for creating a church.</summary>
public sealed record CreateChurchRequest
{
    [Required]
    public required string Name { get; init; }

    [Range(1, int.MaxValue)]
    public required int CountryId { get; init; }

    [Required]
    public required string Area { get; init; }
}

/// <summary>Payload for replacing a church's editable fields.</summary>
public sealed record UpdateChurchRequest
{
    [Required]
    public required string Name { get; init; }

    [Range(1, int.MaxValue)]
    public required int CountryId { get; init; }

    [Required]
    public required string Area { get; init; }
}

/// <summary>Church data returned by the API.</summary>
public sealed record ChurchResponse(int Id, string Name, int CountryId, string Area);
