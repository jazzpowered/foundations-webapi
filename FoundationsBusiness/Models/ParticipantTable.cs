using System;

namespace FoundationsBusiness.Models;

public class ParticipantTable
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int SeasonId { get; set; }
    public required string Gender { get; set; }
    public required string Day { get; set; }
    public required DateTime Time { get; set; }
    public required IEnumerable<int> Leaders { get; set; } = [];
    public required IEnumerable<int> Participants { get; set; } = [];  
}
