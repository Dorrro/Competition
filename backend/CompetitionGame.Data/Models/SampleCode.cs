namespace CompetitionGame.Data.Models
{
    public class SampleCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public virtual CodingLanguage CodingLanguage { get; set; }
    }
}