namespace FoundationsBusiness.Models;

public class User
{
    public required int Id { get; set; }
    public required int ChurchId { get; set; }
    public required int CountryId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Gender { get; set; }
    public string Area { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}