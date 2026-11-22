using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.Users;

/// <summary>Payload for creating a user.</summary>
public sealed record CreateUserRequest
{
    [Range(1, int.MaxValue)]
    public required int ChurchId { get; init; }

    [Range(1, int.MaxValue)]
    public required int CountryId { get; init; }

    [Range(1, int.MaxValue)]
    public required int RoleId { get; init; }

    [Required, MaxLength(100)]
    public required string FirstName { get; init; }

    [Required, MaxLength(100)]
    public required string LastName { get; init; }

    [Required, EmailAddress, MaxLength(320)]
    public required string Email { get; init; }

    [Required, MaxLength(50)]
    public required string Gender { get; init; }

    [MaxLength(200)]
    public string Area { get; init; } = string.Empty;

    [MaxLength(20)]
    public string ZipCode { get; init; } = string.Empty;
}

/// <summary>Payload for replacing a user's editable fields.</summary>
public sealed record UpdateUserRequest
{
    [Range(1, int.MaxValue)]
    public required int ChurchId { get; init; }

    [Range(1, int.MaxValue)]
    public required int CountryId { get; init; }

    [Range(1, int.MaxValue)]
    public required int RoleId { get; init; }

    [Required, MaxLength(100)]
    public required string FirstName { get; init; }

    [Required, MaxLength(100)]
    public required string LastName { get; init; }

    [Required, EmailAddress, MaxLength(320)]
    public required string Email { get; init; }

    [Required, MaxLength(50)]
    public required string Gender { get; init; }

    [MaxLength(200)]
    public string Area { get; init; } = string.Empty;

    [MaxLength(20)]
    public string ZipCode { get; init; } = string.Empty;
}

/// <summary>User data returned by the API.</summary>
public sealed record UserResponse(
    int Id,
    int ChurchId,
    int CountryId,
    int RoleId,
    string FirstName,
    string LastName,
    string Email,
    string Gender,
    string Area,
    string ZipCode);
