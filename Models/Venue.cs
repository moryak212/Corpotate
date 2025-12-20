namespace Corporate.Models;

public class Venue
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public int? Capacity { get; set; }

    public string? ContactPhone { get; set; }

    public ICollection<CorporateEvent> Events { get; set; } = new List<CorporateEvent>();
}
