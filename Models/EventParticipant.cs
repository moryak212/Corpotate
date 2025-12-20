namespace Corporate.Models;

public class EventParticipant
{
    public int Id { get; set; }

    public int CorporateEventId { get; set; }
    public CorporateEvent? CorporateEvent { get; set; }

    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public string Role { get; set; } = "Участник";

    public string Status { get; set; } = "Приглашён";

    public string? Comment { get; set; }
}
