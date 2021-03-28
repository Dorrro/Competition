using System.Threading.Tasks;
using CompetitionGame.Models;

namespace CompetitionGame.Services
{
    public interface ISolutionVerifier
    {
        Task<CodeVerificationResult> Verify(string code, string input, string expectedOutput);
    }
}