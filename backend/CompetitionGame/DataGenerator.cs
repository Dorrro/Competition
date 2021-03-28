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

            var sampleFibonacci = new SampleCode {Id = 1, CodingLanguage = cSharp, Code = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CompetitionGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var N = Console.In.ReadLine();

            //Your code goes here
            Console.WriteLine(""Hello, world!\"");
        }
    }
}" };
            context.SampleCodes.AddRange(new List<SampleCode>
            {
                sampleFibonacci
            });

            var fibonacciTask = new Task
            {
                Id = 1,
                Name = "Simple Fibonacci",
                Description = "Write a program that based on the given input number (N) returns the N-th number from the Fibonacci's sequence",
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