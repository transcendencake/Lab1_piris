using Lab1_piris.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_piris.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasIndex(p => new {p.PassportSeries, p.PassportNumber})
            .IsUnique();
        modelBuilder.Entity<Client>()
            .HasIndex(p => p.PassportId)
            .IsUnique();
    }

    public DbSet<City> Cities { get; set; }
    public DbSet<Disability> Disabilities { get; set; }
    public DbSet<FamilyState> FamilyStates { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Citizenship> Citizenship { get; set; }
}