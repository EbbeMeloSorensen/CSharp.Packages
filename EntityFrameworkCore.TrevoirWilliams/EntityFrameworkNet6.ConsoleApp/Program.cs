using EntityFrameworkNet6.Data;
using EntityFrameworkNet6.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkNet6.ConsoleApp;

class Program
{
    private static FootballLeagueDbContext context = new FootballLeagueDbContext();

    static async Task Main(string[] args)
    {
        Console.WriteLine("Attempting to access database..");

        //await AddNewLeague();
        //await AddNewTeamsWithLeague();

        //await SimpleSelectQuery();
        //await QueryFilters();
        await AdditionalExecutionMethods();

        Console.WriteLine("done");
    }

    static async Task AddNewLeague()
    {
        //// Adding a new League Object
        var league = new League { Name = "Seria A" };
        await context.Leagues.AddAsync(league);
        await context.SaveChangesAsync();

        //// Function To add new teams related to the new league object. 
        await AddTeamsWithLeague(league);
        await context.SaveChangesAsync();
    }

    static async Task AddTeamsWithLeague(League league)
    {
        var teams = new List<Team>
        {
            new Team
            {
                Name = "Juventus",
                LeagueId = league.Id
            },
            new Team
            {
                Name = "AC Milan",
                LeagueId = league.Id
            },
            new Team
            {
                Name = "AS Roma",
                League = league
            }
        };

        //// Operation to add multiple objects to database in one call.
        await context.AddRangeAsync(teams);
    }

    static async Task AddNewTeamsWithLeague()
    {
        var league = new League { Name = "Bundesliga" };
        var team = new Team { Name = "Bayern Munich", League = league };
        await context.AddAsync(team);
        await context.SaveChangesAsync();
    }

    static async Task AddNewTeamWithLeagueId()
    {
        // Her indsætter vi et team og angiver dens liga med et id
        var team = new Team { Name = "Fiorentina", LeagueId = 8 };
        await context.AddAsync(team);
        await context.SaveChangesAsync();
    }

    static async Task AddNewLeagueWithTeams()
    {
        // Her indsætter vi en liga med to teams i et enkelt hug
        var teams = new List<Team> {
            new Team
            {
                Name = "Rivoli United"
            },
            new Team
            {
                Name = "Waterhouse FC"
            },
        };
        var league = new League { Name = "CIFA", Teams = teams };
        await context.AddAsync(league);
        await context.SaveChangesAsync();
    }

    static async Task AddNewMatches()
    {
        var matches = new List<Match>
        {
            new Match
            {
                AwayTeamId = 1, HomeTeamId = 2, Date = new DateTime(2021, 10, 28)
            },
            new Match
            {
                AwayTeamId = 8, HomeTeamId = 7, Date = DateTime.Now
            },
            new Match
            {
                AwayTeamId = 8, HomeTeamId = 7, Date = DateTime.Now
            }
        };
        await context.AddRangeAsync(matches);
        await context.SaveChangesAsync();
    }

    private static async Task AddNewCoach()
    {
        var coach1 = new Coach { Name = "Jose Mourinho", TeamId = 3 };

        await context.AddAsync(coach1);

        var coach2 = new Coach { Name = "Antonio Conte" };

        await context.AddAsync(coach2);
        await context.SaveChangesAsync();
    }
    
    static async Task SimpleSelectQuery()
    {
        //// Smartest most efficient way to get results
        var leagues = await context.Leagues.ToListAsync();
        foreach (var league in leagues)
        {
            Console.WriteLine($"{league.Id} - {league.Name}");
        }

        //// Inefficient way to get results. Keeps connection open until completed and might create lock on table
        ////foreach (var league in context.Leagues)
        ////{
        ////    Console.WriteLine($"{league.Id} - {league.Name}");
        ////}
    }

    static async Task QueryFilters()
    {
        Console.Write($"Enter League Name (Or Part Of): ");
        var leagueName = Console.ReadLine();
        var exactMatches = await context.Leagues.Where(q => q.Name.Equals(leagueName)).ToListAsync();
        foreach (var league in exactMatches)
        {
            Console.WriteLine($"{league.Id} - {league.Name}");
        }

        // Bemærk, hvordan han bruger en EF function her for at opnå noget, der svarer til contains, sm er udkommenteret

        ////var partialMatches = await context.Leagues.Where(q => q.Name.Contains(leagueName)).ToListAsync();
        var partialMatches = await context.Leagues.Where(q => EF.Functions.Like(q.Name, $"%{leagueName}%")).ToListAsync();
        foreach (var league in partialMatches)
        {
            Console.WriteLine($"{league.Id} - {league.Name}");
        }
    }

    static async Task AdditionalExecutionMethods()
    {
        //// These methods also have non-async
        var leagues = context.Leagues;
        var list = await leagues.ToListAsync();
        var first = await leagues.FirstAsync();
        var firstOrDefault = await leagues.FirstOrDefaultAsync();
        var single = await leagues.SingleAsync();
        var singleOrDefault = await leagues.SingleOrDefaultAsync();

        var count = await leagues.CountAsync();
        var longCount = await leagues.LongCountAsync();
        var min = await leagues.MinAsync();
        var max = await leagues.MaxAsync();

        //// DbSet Method that will execute
        var league = await leagues.FindAsync(1);
    }

    static async Task QueryRelatedRecords()
    {
        //// Get Many Related Records - Leagues -> Teams
        var leagues = await context.Leagues.Include(q => q.Teams).ToListAsync();

        //// Get One Related Record - Team -> Coach
        var team = await context.Teams
            .Include(q => q.Coach)
            .FirstOrDefaultAsync(q => q.Id == 3);

        //// Get 'Grand Children' Related Record - Team -> Matches -> Home/Away Team
        var teamsWithMatchesAndOpponents = await context.Teams
            .Include(q => q.AwayMatches).ThenInclude(q => q.HomeTeam).ThenInclude(q => q.Coach)
            .Include(q => q.HomeMatches).ThenInclude(q => q.AwayTeam).ThenInclude(q => q.Coach)
            .FirstOrDefaultAsync(q => q.Id == 1);

        //// Get Includes with filters
        var teams = await context.Teams
            .Where(q => q.HomeMatches.Count > 0)
            .Include(q => q.Coach)
            .ToListAsync();
    }
}
