using Microsoft.EntityFrameworkCore;
using MyGeneralNotes.Domain.Entities;

namespace MyGeneralNotes.Infrastructure.DataAccess;
public class MyGeneralNotesDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Routine> Routines { get; set; }
    public DbSet<Exercise> Exercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyGeneralNotesDbContext).Assembly);
    }
}
