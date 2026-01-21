using System;

namespace Corporate.Models
{
    public class EventTask
    {
        public int Id { get; set; }

        public int CorporateEventId { get; set; }
        public CorporateEvent? CorporateEvent { get; set; }


        public int? EmployeeId { get; set; }
        public Employee? Executor { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime Deadline { get; set; } = DateTime.Today;

        public string Status { get; set; } = "Новая";

        public string? Comment { get; set; }
    }
}
