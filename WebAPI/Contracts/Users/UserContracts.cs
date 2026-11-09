using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.Users;

/// <summary>Payload for creating a user.</summary>
public sealed record CreateUserRequest
{
    [Range(1, int.MaxValue)]
    public required int ChurchId { get; init; }

    [Range(1, int.MaxValue)]
    public required int CountryId { get; init; }

    [Required]
    public required string FirstName { get; init; }

    [Required]
    public required string LastName { get; init; }

    [Required, EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Gender { get; init; }

    public string Area { get; init; } = string.Empty;

    public string ZipCode { get; init; } = string.Empty;
}

/// <summary>Payload for replacing a user's editable fields.</summary>
public sealed record UpdateUserRequest
{
    [Range(1, int.MaxValue)]
    public required int ChurchId { get; init; }

    [Range(1, int.MaxValue)]
    public required int CountryId { get; init; }

    [Required]
    public required string FirstName { get; init; }

    [Required]
    public required string LastName { get; init; }

    [Required, EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Gender { get; init; }

    public string Area { get; init; } = string.Empty;

    public string ZipCode { get; init; } = string.Empty;
}

/// <summary>User data returned by the API.</summary>
public sealed record UserResponse(
    int Id,
    int ChurchId,
    int CountryId,
    string FirstName,
    string LastName,
    string Email,
    string Gender,
    string Area,
    string ZipCode);
