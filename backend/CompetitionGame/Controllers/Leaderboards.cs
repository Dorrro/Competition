using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompetitionGame.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompetitionGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaderboardController : Controller
    {
        private readonly CompetitionGameContext _context;

        public LeaderboardController(CompetitionGameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<LeaderboardEntry>> Top()
        {
            // the below is a result of a EF bug
            var entries = await _context.Solutions
                .Select(s => new {s.User, TaskName = s.Task.Name})
                .ToListAsync();

            return entries
                .GroupBy(s => s.User)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => new LeaderboardEntry
                {
                    Name = g.Key,
                    SolvedTasks = g.Select(s => s.TaskName)
                });
        }
    }

    public class LeaderboardEntry
    {
        public string Name { get; set; }
        public IEnumerable<string> SolvedTasks { get; set; }
    }
}