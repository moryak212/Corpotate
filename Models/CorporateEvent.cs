using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Corporate.Models
{
    public class CorporateEvent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Дата обязательна")]
        public DateTime EventDate { get; set; }

        public string? Description { get; set; }

        public int? VenueId { get; set; }
        public Venue? Venue { get; set; }

        public int? OrganizerId { get; set; }
        public Employee? Organizer { get; set; }

        public decimal? Budget { get; set; }

        public string? ImagePath { get; set; }

        public ICollection<EventParticipant> Participants { get; set; } = new List<EventParticipant>();
        public ICollection<EventTask> Tasks { get; set; } = new List<EventTask>();
        public ICollection<EventFile> Files { get; set; } = new List<EventFile>();
    }
}
