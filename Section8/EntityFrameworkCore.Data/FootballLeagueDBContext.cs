using EntityFrameworkCore.Data.Configurations;
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

    public DbSet<League> Leagues { get; set; }

    public DbSet<Match> Matches { get; set; }

    public DbSet<TeamsAndLeaguesView> TeamsAndLeaguesView { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}")
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FootballLeagueDBContext).Assembly);

        modelBuilder.Entity<TeamsAndLeaguesView>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("vw_TeamsAndLeagues");
            entity.Property(e => e.Name).HasColumnName("Name");
            entity.Property(e => e.LeagueName).HasColumnName("LeagueName");
        });
    }
}
