using EntityFrameworkCore.Data.Configurations;
using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

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

    //public FootballLeagueDBContext(DbContextOptions<FootballLeagueDBContext> options) : base(options)
    //{

    //}

    public string DbPath { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<Coach> Coaches { get; set; }

    public DbSet<League> Leagues { get; set; }

    public DbSet<Match> Matches { get; set; }

    public DbSet<TeamsAndLeaguesView> TeamsAndLeaguesView { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}", o =>
        {
            o.CommandTimeout(30); // Set command timeout to 30 seconds
        })
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

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(100);
        configurationBuilder.Properties<Decimal>().HavePrecision(16, 2);
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        // Custom logic before saving changes can be added here
        var entries = ChangeTracker.Entries<BaseDomainModel>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach(var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;
                entry.Entity.CreatedBy = "Sample user 1";
            }
            entry.Entity.DateModified = DateTime.UtcNow;
            entry.Entity.ModifiedBy = "Sample user 1";
            entry.Entity.Version = Guid.NewGuid();
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}

//public class FootballDbContextFactory: IDesignTimeDbContextFactory<FootballLeagueDBContext>
//{
//    public FootballLeagueDBContext CreateDbContext(string[] args)
//    {
//        var folder = Environment.SpecialFolder.LocalApplicationData;
//        var path = Path.Combine(Environment.GetFolderPath(folder), "EntityFrameworkCore");

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//            .Build();
//        var dbPath = Path.Combine(path, configuration.GetConnectionString("SqliteDatabaseConnectionString"));

//        var optionsBuilder = new DbContextOptionsBuilder<FootballLeagueDBContext>();
//        optionsBuilder.UseSqlite($"Data Source={dbPath}");
        
//        return new FootballLeagueDBContext(optionsBuilder.Options);
//    }
//}