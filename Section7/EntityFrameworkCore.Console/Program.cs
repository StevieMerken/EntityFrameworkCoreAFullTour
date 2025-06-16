
using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var context = new FootballLeagueDBContext();

        //var match1 = new Match()
        //{
        //    AwayTeamId = 1,
        //    HomeTeamId = 2,

        //    HomeTeamScore = 0,
        //    AwayTeamScore = 0,

        //    Date = new DateTime(2025,10,1),
        //    TicketPrice = 10.00m,
        //};

        //await context.AddAsync(match1);
        //await context.SaveChangesAsync();

        // Incorrect data
        //var match2 = new Match()
        //{
        //    AwayTeamId = 10,
        //    HomeTeamId = 2,

        //    HomeTeamScore = 0,
        //    AwayTeamScore = 0,

        //    Date = new DateTime(2025, 10, 1),
        //    TicketPrice = 10.00m,
        //};

        //await context.AddAsync(match2);
        //await context.SaveChangesAsync();


        //var newTeam = new Team()
        //{
        //    Name = "Team C",
        //    LeagueId = 1,
        //    CoachId = 1,
        //    DateCreated = DateTime.Now,
        //    Coach = new Coach()
        //    {
        //        Name = "Coach 4",
        //    }
        //};

        //await context.AddAsync(newTeam);
        //await context.SaveChangesAsync();

        //var newLeague = new League
        //{
        //    Name = "League 2",

        //    Teams = new List<Team>
        //    {
        //        new Team
        //        {
        //            Name = "Team D",
        //            Coach = new Coach
        //            {
        //                Name = "Coach 4"
        //            }
        //        },
        //        new Team
        //        {
        //            Name = "Team E",
        //            Coach = new Coach
        //            {
        //                Name = "Coach 5"
        //            }
        //        }
        //    }
        //};

        //await context.AddAsync(newLeague);
        //await context.SaveChangesAsync();


        //var leagues = await context.Leagues
        //    .Include(l => l.Teams)
        //    .ThenInclude(t => t.Coach)
        //    .ToListAsync();

        //foreach (var league in leagues)
        //{
        //    Console.WriteLine($"League: {league.Name}");
        //    foreach (var team in league.Teams)
        //    {
        //        Console.WriteLine($"  Team: {team.Name}, Coach: {team.Coach?.Name}");
        //    }
        //}

        //var league = await context.FindAsync<League>(1);
        //if (!league.Teams.Any())
        //{
        //    Console.WriteLine("No teams found in the league.");
        //}

        //await context.Entry(league)
        //    .Collection(l => l.Teams)
        //    .LoadAsync();
        //if (league.Teams.Any())
        //{
        //    foreach (var team in league.Teams)
        //    {
        //        Console.WriteLine($"  Team: {team.Name}, Coach: {team.Coach?.Name}");
        //    }
        //}

        //await InsertMoreMatches();

        //var teams = await context.Teams
        //    .Include(t => t.Coach)
        //    .Include(m => m.HomeMatches.Where(q => q.HomeTeamScore > 0))
        //    .ToListAsync();

        //foreach (var team in teams)
        //{
        //    Console.WriteLine($"Team: {team.Name}, Coach: {team.Coach?.Name}");
        //    foreach(var match in team.HomeMatches)
        //    {
        //        Console.WriteLine($"  Match: {match.HomeTeamScore} - {match.AwayTeamScore}");
        //    }
        //}

        var teamDetails = await context.Teams
            .Select(t => new TeamDetails
            {
                TeamId = t.Id,
                TeamName = t.Name,
                CoachName = t.Coach.Name,
                TotalHomeGoals = t.HomeMatches.Sum(m => m.HomeTeamScore),
                TotalAwayGoals = t.AwayMatches.Sum(m => m.AwayTeamScore)
            })
            .ToListAsync();

        foreach (var team in teamDetails)
        {
            Console.WriteLine($"Team ID: {team.TeamId}, Team Name: {team.TeamName}, Coach Name: {team.CoachName}");
        }
    }

    static async Task InsertMoreMatches()
    {
        using var context = new FootballLeagueDBContext();

        var match1 = new Match
        {
            AwayTeamId = 2,
            HomeTeamId = 3,
            HomeTeamScore = 1,
            AwayTeamScore = 0,
            Date = new DateTime(2023, 01, 1),
            TicketPrice = 20,
        };
        var match2 = new Match
        {
            AwayTeamId = 2,
            HomeTeamId = 1,
            HomeTeamScore = 1,
            AwayTeamScore = 0,
            Date = new DateTime(2023, 01, 1),
            TicketPrice = 20,
        };
        var match3 = new Match
        {
            AwayTeamId = 1,
            HomeTeamId = 3,
            HomeTeamScore = 1,
            AwayTeamScore = 0,
            Date = new DateTime(2023, 01, 1),
            TicketPrice = 20,
        };
        var match4 = new Match
        {
            AwayTeamId = 4,
            HomeTeamId = 3,
            HomeTeamScore = 0,
            AwayTeamScore = 1,
            Date = new DateTime(2023, 01, 1),
            TicketPrice = 20,
        };
        await context.AddRangeAsync(match1, match2, match3, match4);
        await context.SaveChangesAsync();
    }
}

class TeamDetails
{
    public int TeamId { get; set; }
    public string TeamName { get; set; }
    public string CoachName { get; set; }

    public int TotalHomeGoals { get; set; }

    public int TotalAwayGoals { get; set; }
}
