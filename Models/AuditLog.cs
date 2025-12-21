namespace Corporate.Models;

public class AuditLog
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;       // Create / Update / Delete
    public string EntityName { get; set; } = string.Empty;   // CorporateEvent, EventTask и т.д.
    public string EntityId { get; set; } = string.Empty;     // Id объекта

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
