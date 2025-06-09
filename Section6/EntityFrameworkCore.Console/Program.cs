
using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var context = new FootballLeagueDBContext();

        await context.Database.MigrateAsync();
    }    
}
