
using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var context = new FootballLeagueDBContext();

        // Insert
        //var newCoach = new Coach
        //{
        //    Name = "John Doe",
        //    DateCreated = DateTime.Now
        //};
        //await context.Coaches.AddAsync(newCoach);
        //await context.SaveChangesAsync();

        //Loop insert
        //var newCoach1 = new Coach
        //{
        //    Name = "Jane Smith",
        //    DateCreated = DateTime.Now
        //};

        //var listOfCoaches = new List<Coach>
        //{
        //    newCoach,
        //    newCoach1,
        //};

        //foreach (var coach in listOfCoaches)
        //{
        //    await context.Coaches.AddAsync(coach);
        //}
        //Console.WriteLine(context.ChangeTracker.DebugView.LongView);
        //await context.SaveChangesAsync();
        //Console.WriteLine(context.ChangeTracker.DebugView.LongView);

        // Batch insert
        //var newCoach = new Coach
        //{
        //    Name = "John Doe",
        //    DateCreated = DateTime.Now
        //};
        //var newCoach1 = new Coach
        //{
        //    Name = "Jane Smith",
        //    DateCreated = DateTime.Now
        //};
        //var listOfCoaches = new List<Coach>
        //{
        //    newCoach,
        //    newCoach1,
        //};
        //context.Coaches.AddRange(listOfCoaches);
        //await context.SaveChangesAsync();


        // Update
        //var coach = await context.Coaches.FindAsync(1);
        //coach.Name = "Updated Coach Name";
        //await context.SaveChangesAsync();

        // Update AsNoTracking
        //var coach1 = await context.Coaches.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
        //coach1.Name = "Updated Coach Name 2";
        //Console.WriteLine(context.ChangeTracker.DebugView.LongView);
        //context.Update(coach1);
        //Console.WriteLine(context.ChangeTracker.DebugView.LongView);
        //await context.SaveChangesAsync();
        //Console.WriteLine(context.ChangeTracker.DebugView.LongView);

        // Delete
        //var coachToDelete = await context.Coaches.FindAsync(1);
        //if (coachToDelete != null)
        //{
        //    context.Coaches.Remove(coachToDelete);
        //    await context.SaveChangesAsync();
        //}

        // Execute Delete
        //await context.Coaches
        //    .Where(c => c.Name == "John Doe")
        //    .ExecuteDeleteAsync();

        // Excute Update
        await context.Coaches
            .Where(c => c.Name == "Jane Smith")
            .ExecuteUpdateAsync(set => set.SetProperty(cc => cc.DateCreated, DateTime.Now)
                                        .SetProperty(cc => cc.Name, "Jane Smith 2"));
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
