using System.Collections.Generic;

namespace CompetitionGame.Models
{
    public class LeaderboardEntry
    {
        public string Name { get; set; }
        public IEnumerable<string> SolvedTasks { get; set; }
    }
}