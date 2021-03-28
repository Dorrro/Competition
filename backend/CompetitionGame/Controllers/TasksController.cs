using System.Collections.Generic;
using System.Threading.Tasks;
using CompetitionGame.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompetitionGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : Controller
    {
        private readonly CompetitionGameContext _context;

        public TasksController(CompetitionGameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Data.Models.Task>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Include(t => t.SampleCodes)
                .ThenInclude(s => s.CodingLanguage)
                .ToListAsync();
            return tasks;
        }
    }
}
