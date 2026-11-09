using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.Countries;

/// <summary>Payload for creating a country.</summary>
public sealed record CreateCountryRequest
{
    [Required]
    public required string Name { get; init; }
}

/// <summary>Payload for replacing a country's editable fields.</summary>
public sealed record UpdateCountryRequest
{
    [Required]
    public required string Name { get; init; }
}

/// <summary>Country data returned by the API.</summary>
public sealed record CountryResponse(int Id, string Name);
