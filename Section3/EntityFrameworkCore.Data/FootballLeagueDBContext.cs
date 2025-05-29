using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Data;

public class FootballLeagueDBContext : DbContext
{
    public DbSet<Team> Teams { get; set; }

    public DbSet<Coach> Coaches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=FootballLeague.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().HasData(
            new Team { TeamId = 1, Name = "Team A", DateCreated = new DateTime(2025, 5, 29, 13, 34, 26, 346, DateTimeKind.Unspecified).AddTicks(2056) },
            new Team { TeamId = 2, Name = "Team B", DateCreated = new DateTime(2025, 5, 29, 13, 34, 26, 346, DateTimeKind.Unspecified).AddTicks(2056) }
        );
    }
}
