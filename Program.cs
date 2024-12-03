// See https://aka.ms/new-console-template for more information

using Day1Logic = AdventOfCode2024.Day1.Logic;
using Day2Logic = AdventOfCode2024.Day2.Logic;

// Console.WriteLine("Running Day 1...");
// Day1Logic day1 = new();
// await day1.ParseInputIntoLists();
// long totalDistance = await day1.GetTotalDistance();
// Console.WriteLine($"Total distance: {totalDistance}");
// long similarityScore = await day1.GetSimilarityScore();
// Console.WriteLine($"Similarity score: {similarityScore}");

Console.WriteLine("Running Day 2...");
Day2Logic day2 = new();
await day2.ParseInputAsync();
long numberSafeReports = await day2.CalculateSafeReportsAsync();
Console.WriteLine($"Number safe reports: {numberSafeReports}"); 