using System.Threading.Tasks;
using CompetitionGame.Data;
using CompetitionGame.Data.Models;
using CompetitionGame.Models;
using CompetitionGame.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompetitionGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolutionController : ControllerBase
    {
        private readonly CompetitionGameContext _context;
        private readonly ISolutionVerifier _solutionVerifier;

        public SolutionController(CompetitionGameContext context, ISolutionVerifier solutionVerifier)
        {
            _context = context;
            _solutionVerifier = solutionVerifier;
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

            var verificationResult = await _solutionVerifier.Verify(request.Code, task.Input, task.ExpectedOutput);
            if (!verificationResult.IsSuccessful)
            {
                ModelState.AddModelError(nameof(request.Code), verificationResult.Error);
                return BadRequest(ModelState);
            }

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
}
