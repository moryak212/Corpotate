namespace Corporate.Models;

public class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Position { get; set; }

    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public ICollection<CorporateEvent> OrganizedEvents { get; set; } = new List<CorporateEvent>();

    public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();

    public ICollection<EventTask> TasksResponsibleFor { get; set; } = new List<EventTask>();
}
