using System.Collections.Generic;

namespace CompetitionGame.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExpectedOutput { get; set; }
        public string Input { get; set; }
        public virtual IEnumerable<SampleCode> SampleCodes { get; set; }
    }
}