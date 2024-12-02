// See https://aka.ms/new-console-template for more information

using AdventOfCode2024.Day1;

Console.WriteLine("Running Day 1...");
Day1Logic day1 = new();
await day1.ParseInputIntoLists();
long totalDistance = await day1.GetTotalDistance();
Console.WriteLine($"Total distance: {totalDistance}");
long similarityScore = await day1.GetSimilarityScore();
Console.WriteLine($"Similarity score: {similarityScore}");
