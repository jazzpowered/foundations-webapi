using System;

namespace FoundationsBusiness.Models;

public class WeekSession
{
    public required int Id { get; set; }
    public required int LanguageId { get; set; }
    public required int Week { get; set; }
    public required bool Active { get; set; }
    public required string Content { get; set; }
}
