// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Logging;
using Day1Logic = AdventOfCode2024.Day1.Logic;
using Day2Logic = AdventOfCode2024.Day2.Logic;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddSimpleConsole(options =>
{
    options.IncludeScopes = false;
    options.SingleLine = true;
    options.TimestampFormat = "hh:mm:ss ";
}));
ILogger logger = factory.CreateLogger<Program>();

logger.LogInformation("Running Day 1...");
Day1Logic day1 = new(logger);
await day1.ParseInputIntoLists();
long totalDistance = await day1.GetTotalDistance();
Console.WriteLine($"Total distance: {totalDistance}");
long similarityScore = await day1.GetSimilarityScore();
Console.WriteLine($"Similarity score: {similarityScore}");

logger.LogInformation("Running Day 2...");
Day2Logic day2 = new(logger, "Day2/input.txt");
await day2.ParseInputAsync();
await day2.CalculateSafeReportsAsync();
await day2.CalculateSafeReportsAsync(true);