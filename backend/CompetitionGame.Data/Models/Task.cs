using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CompetitionGame.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<SampleCode> SampleCodes { get; set; }

        [JsonIgnore]
        public string ExpectedOutput { get; set; }
        [JsonIgnore]
        public string Input { get; set; }
    }
}