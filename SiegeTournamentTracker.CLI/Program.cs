using System.Threading.Tasks;

namespace SiegeTournamentTracker.CLI
{
    using Api;
    using System;
    using System.Linq;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            ITournamentApi api = new TournamentApi();

            var matches = await api.Matches();

            var upcoming = matches.Where(t => t.Status == MatchStatus.Upcoming)
                                  .OrderBy(t => t.Offset)
                                  .Take(5);

            foreach(var match in upcoming)
            {
                Console.WriteLine($"{match.TeamOne.FullName} vs {match.TeamTwo.FullName} - {match.LocalTime:HH:mm}");
            }

            Console.WriteLine("Finished");
        }
    }
}
