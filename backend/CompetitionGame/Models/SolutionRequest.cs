using System.ComponentModel.DataAnnotations;

namespace CompetitionGame.Models
{
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