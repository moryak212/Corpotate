using System;
using System.Collections.Generic;
using Corporate.Models;
public class CorporateEvent
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public DateTime EventDate { get; set; }

    public string? Description { get; set; }

    public int? VenueId { get; set; }
    public Venue? Venue { get; set; }

    public int? OrganizerId { get; set; }    // ← сделай nullable, если было int
    public Employee? Organizer { get; set; }

    public decimal? Budget { get; set; }

    public string? ImagePath { get; set; }

    public ICollection<EventParticipant> Participants { get; set; } = new List<EventParticipant>();
    public ICollection<EventTask> Tasks { get; set; } = new List<EventTask>();
    public ICollection<EventFile> Files { get; set; } = new List<EventFile>();
}
