
namespace CompetitionGame.Data.Models
{
    public class Solution
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Code { get; set; }
        public virtual CodingLanguage CodingLanguage { get; set; }
        public virtual Task Task { get; set; }
    }
}
