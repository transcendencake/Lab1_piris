using Lab1_piris.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_piris.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<City> Cities { get; set; }
    public DbSet<Disability> Disabilities { get; set; }
    public DbSet<FamilyState> FamilyStates { get; set; }
    public DbSet<Client> Clients { get; set; }
}