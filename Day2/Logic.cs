namespace AdventOfCode2024.Day2;

public class Logic
{
    private readonly string[] _input = File.ReadAllLines("Day2/input.txt");
    private List<Report> _reports = [];

    public ValueTask ParseInputAsync()
    {
        foreach (string line in _input)
        {
            Report report = new();
            string[] levels = line.Split(' ');
            for (int i = 0; i < levels.Length; i+=1)
            {
                report.Levels.Add(new Level
                {
                    Value = long.Parse(levels[i]),
                    NextValue = i == levels.Length - 1 ? null : long.Parse(levels[i + 1])
                });
            }
            _reports.Add(report);
        }

        // Console.WriteLine($"Parsed {_reports.Count} reports");
        return ValueTask.CompletedTask;
    }

    public async Task<int> CalculateSafeReportsAsync()
    {
        foreach (Report report in _reports)
        {
            report.IsContinuingTrend = report.Levels.All(l => l.GetTrend() == report.Levels.First().GetTrend());
            report.IsWithinDiffRange = report.Levels.All(l => l.GetDiffRange() is >= 1 and <= 3);
        }

        return _reports.Count(r => r.IsSafe);
    }
}