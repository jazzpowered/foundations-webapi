using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.Languages;

/// <summary>Payload for creating a language.</summary>
public sealed record CreateLanguageRequest
{
    [Required, MaxLength(50)]
    public required string Name { get; init; }
}

/// <summary>Payload for replacing a language.</summary>
public sealed record UpdateLanguageRequest
{
    [Required, MaxLength(50)]
    public required string Name { get; init; }
}

/// <summary>Language data returned by the API.</summary>
public sealed record LanguageResponse(int Id, string Name);
