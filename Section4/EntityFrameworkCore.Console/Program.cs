
using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var context = new FootballLeagueDBContext();

        //await GetAllTeams(context);

        //var team = await context.Teams.FindAsync(5);

        //var avg = await context.Teams.AverageAsync(t => t.TeamId);

        //var groupedTeams = context.Teams.GroupBy(t => t.DateCreated.Date)
        //    .Select(g => new
        //    {
        //        DateCreated = g.Key,
        //        NumerOfTeams = g.Count()
        //    })
        //    .Where(g => g.NumerOfTeams > 1)
        //    .ToList();

        //var teams = await context.Teams
        //    .Select(t => new {
        //        t.Name,
        //        t.DateCreated,
        //    })
        //    .ToListAsync();

        //foreach (var team in teams)
        //{
        //    Console.WriteLine($"{team}");
        //}

        var teams = await context.Teams
            .AsNoTracking()
            .ToListAsync();
    }

    private static async Task GetAllTeams(FootballLeagueDBContext context)
    {
        var teams = await context.Teams.ToListAsync();

        foreach (var team in teams)
        {
            Console.WriteLine($"Team ID: {team.TeamId}, Name: {team.Name}, Date Created: {team.DateCreated}");
        }
    }
}
