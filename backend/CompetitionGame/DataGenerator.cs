using System.Collections.Generic;
using CompetitionGame.Data;
using CompetitionGame.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CompetitionGame
{
    public class DataGenerator
    {
        public static void AddSampleData(DbContextOptions<CompetitionGameContext>? options)
        {
            using var context = new CompetitionGameContext(options);
            var cSharp = new CodingLanguage
            {
                Id = 1, Name = "C#"
            };
            context.CodingLanguages.AddRange(new List<CodingLanguage>
            {
                cSharp
            });

            var sampleFibonacci = new SampleCode {Id = 1, CodingLanguage = cSharp, Code = "using System;"};
            context.SampleCodes.AddRange(new List<SampleCode>
            {
                sampleFibonacci
            });

            var fibonacciTask = new Task
            {
                Id = 1,
                Name = "Simple Fibonacci",
                Description = "Simple Fibonacci",
                ExpectedOutput = "6765",
                Input = "20",
                SampleCodes = new List<SampleCode>
                {
                    sampleFibonacci
                }
            };
            context.Tasks.AddRange(new List<Task>
            {
                fibonacciTask
            });

            context.SaveChanges();
        }
    }
}