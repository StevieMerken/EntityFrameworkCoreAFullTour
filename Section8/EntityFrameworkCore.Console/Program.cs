
using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var context = new FootballLeagueDBContext();

        var details = await context.TeamsAndLeaguesView.ToListAsync();

        var teamName= "Team A";
        var teamNameParam = new SqliteParameter("teamName", teamName);
        
        // Raw sql
        var team = await context.Teams.FromSqlRaw($"SELECT * FROM TEAMS WHERE Name = @teamName", teamNameParam).ToListAsync();


        // FromSql
        team = await context.Teams.FromSql($"SELECT * FROM TEAMS WHERE Name = {teamName}").ToListAsync();


        // From sql interpolated
        team = await context.Teams.FromSqlInterpolated($"SELECT * FROM TEAMS WHERE Name = {teamName}").ToListAsync();
    }
}