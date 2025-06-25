
using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var context = new FootballLeagueDBContext();

        // Audit columns
        //var newCoach = new Coach
        //{
        //    Name = "John Doe",
        //};
        //await context.Coaches.AddAsync(newCoach);
        //await context.SaveChangesAsync();



        // Concurrency
        //var team = context.Teams.Find(1);
        //team.Name = "Updated Team Name";
        //try
        //{
        //    await context.SaveChangesAsync();
        //}
        //catch (DbUpdateConcurrencyException ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}



        //Global query filters
        //var leagues = context.Leagues.ToList();
        //foreach (var league in leagues)
        //{
        //    Console.WriteLine($"League: {league.Name}, IsDeleted: {league.IsDeleted}");
        //}

        //var league3 = context.Leagues.Find(3);

        //league3.IsDeleted = true;

        //await context.SaveChangesAsync();

        //leagues = context.Leagues.ToList();
        //foreach (var league in leagues)
        //{
        //    Console.WriteLine($"League: {league.Name}, IsDeleted: {league.IsDeleted}");
        //}
        var leagues = context.Leagues.IgnoreQueryFilters().ToList();
        foreach (var league in leagues)
        {
            Console.WriteLine($"League: {league.Name}, IsDeleted: {league.IsDeleted}");
        }
    }
}