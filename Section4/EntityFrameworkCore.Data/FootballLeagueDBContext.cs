using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Data;

public class FootballLeagueDBContext : DbContext
{
    public FootballLeagueDBContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Path.Combine(Environment.GetFolderPath(folder), "EntityFrameworkCore");
        Directory.CreateDirectory(path);
        DbPath = Path.Combine(path, "FootballLeague.db");
    }

    public string DbPath { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<Coach> Coaches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}")
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().HasData(
            new Team { TeamId = 1, Name = "Team A", DateCreated = new DateTime(2025, 5, 29, 13, 34, 26, 346, DateTimeKind.Unspecified).AddTicks(2056) },
            new Team { TeamId = 2, Name = "Team B", DateCreated = new DateTime(2025, 5, 29, 13, 34, 26, 346, DateTimeKind.Unspecified).AddTicks(2056) }
        );
    }
}
