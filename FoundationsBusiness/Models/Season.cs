using System;

namespace FoundationsBusiness.Models;

public class Season
{
    public required int Id { get; set; }
    public required int Year { get; set; }
    public required int LanguageId { get; set; }
    public required int ChurchId { get; set; }
    public required int SemesterId { get; set; }
    public required bool Active { get; set; }
}
