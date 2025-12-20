using Corporate.Models;
using Microsoft.EntityFrameworkCore;

namespace Corporate.Data;

public class CorporateDbContext : DbContext
{
    public CorporateDbContext(DbContextOptions<CorporateDbContext> options)
        : base(options)
    {
    }

    public DbSet<CorporateEvent> CorporateEvents => Set<CorporateEvent>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Venue> Venues => Set<Venue>();
    public DbSet<EventParticipant> EventParticipants => Set<EventParticipant>();
    public DbSet<EventTask> EventTasks => Set<EventTask>();
    public DbSet<EventFile> EventFiles => Set<EventFile>();
}