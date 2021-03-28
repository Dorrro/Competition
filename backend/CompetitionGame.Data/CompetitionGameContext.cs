using CompetitionGame.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CompetitionGame.Data
{
    public class CompetitionGameContext : DbContext
    {
        public CompetitionGameContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<CodingLanguage> CodingLanguages { get; set; }
        public DbSet<SampleCode> SampleCodes { get; set; }
    }
}