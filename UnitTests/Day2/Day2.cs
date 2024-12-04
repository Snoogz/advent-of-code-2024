using AdventOfCode2024.Day2;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Day2;

public class Day2
{
    [Test]
    public async Task Part1()
    {
        Logic day2 = new(Mock.Of<ILogger<Logic>>(), "Day2/test_input.txt");
        await day2.ParseInputAsync();
        long numberSafeReports = await day2.CalculateSafeReportsAsync();
        
        Assert.That(numberSafeReports, Is.EqualTo(2));
    }
    
    [Test]
    public async Task Part2()
    {
        Logic day2 = new(Mock.Of<ILogger<Logic>>(), "Day2/test_input.txt");
        await day2.ParseInputAsync();
        long numberSafeReports = await day2.CalculateSafeReportsAsync(true);
        
        Assert.That(numberSafeReports, Is.EqualTo(4));
    }
}