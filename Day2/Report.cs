namespace AdventOfCode2024.Day2;

public class Report
{
    public List<Level> Levels { get; set; } = [];
    public bool IsSafe => IsContinuingTrend && IsWithinDiffRange;
    public bool IsWithinDiffRange { get; set; } = false;
    public bool IsContinuingTrend { get; set; } = false;
}

public class Level
{
    public long Value { get; set; }
    
    public long? NextValue { get; set; }
    
    private TrendEnum Trend { get; set; } = TrendEnum.Unknown;
    public TrendEnum GetTrend()
    {
        if (Value > NextValue)
        {
            Trend = TrendEnum.Decreasing;
        } else if (Value < NextValue)
        {
            Trend = TrendEnum.Increasing;
        } else if (Value == NextValue)
        {
            Trend = TrendEnum.NoChange;
        }
        // Console.WriteLine($"Calculating {a} + {b} = {trend}");
        return Trend;
    }
    
    private long? DiffRange { get; set; } = null;
    public long? GetDiffRange()
    {
        DiffRange = !NextValue.HasValue ? DiffRange :
            // Console.WriteLine($"{a} vs. {b} = {diff is >= 1 and <= 3}");
            Math.Abs(Value - NextValue.Value);

        return DiffRange;
    }
}

public enum TrendEnum
{
    Unknown,
    Increasing,
    Decreasing,
    NoChange
}