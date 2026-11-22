using System.ComponentModel.DataAnnotations;

namespace WebAPI.Contracts.Semesters;

/// <summary>Payload for creating a semester.</summary>
public sealed record CreateSemesterRequest
{
    [Required, MaxLength(200)]
    public required string Name { get; init; }
}

/// <summary>Payload for replacing a semester.</summary>
public sealed record UpdateSemesterRequest
{
    [Required, MaxLength(200)]
    public required string Name { get; init; }
}

/// <summary>Semester data returned by the API.</summary>
public sealed record SemesterResponse(int Id, string Name);
