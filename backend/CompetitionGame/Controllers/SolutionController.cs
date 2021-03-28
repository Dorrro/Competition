using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CompetitionGame.Data;
using CompetitionGame.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompetitionGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolutionController : ControllerBase
    {
        private readonly CompetitionGameContext _context;

        public SolutionController(CompetitionGameContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSolution(SolutionRequest request)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.TaskId);
            if (task == null)
            {
                ModelState.AddModelError(nameof(request.TaskId), $"Task with id {request.TaskId} does not exist");
                return BadRequest(ModelState);
            }

            var codingLanguage = await _context.CodingLanguages.FirstOrDefaultAsync(l => l.Id == request.CodingLanguageId);
            if (codingLanguage == null)
            {
                ModelState.AddModelError(nameof(request.CodingLanguageId), $"Coding language with id {request.CodingLanguageId} does not exist");
                return BadRequest(ModelState);
            }

            var solution = await _context.Solutions.FirstOrDefaultAsync(s => s.User == request.User && s.Task == task);
            if (solution != null)
            {
                ModelState.AddModelError(nameof(request.TaskId), $"User has already submitted a solution for task {request.TaskId}");
                return BadRequest(ModelState);
            }

            // TODO: request compiler

            var entry = await _context.Solutions.AddAsync(new Solution
            {
                User = request.User,
                Code = request.Code,
                CodingLanguage = codingLanguage,
                Task = task
            });

            await _context.SaveChangesAsync();

            return Ok(entry.Entity.Id);
        }
    }

    public class SolutionRequest
    {
        [Required]
        public string User { get; set; }

        [Required]
        public string Code { get; set; }

        public int CodingLanguageId { get; set; }

        [Required]
        public int TaskId { get; set; }
    }
}
