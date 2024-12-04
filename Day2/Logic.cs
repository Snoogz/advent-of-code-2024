using Microsoft.Extensions.Logging;

namespace AdventOfCode2024.Day2;

public class Logic(ILogger logger, string inputFilePath)
{
    private readonly string[] _input = File.ReadAllLines(inputFilePath);
    private readonly List<Report> _reports = [];

    public async Task ParseInputAsync()
    {
        logger.LogTrace("Entering Day2.ParseInputAsync");
        for (int i = 0; i < _input.Length; i += 1)
        {
            List<int> levels = _input[i].Split(' ').Select(int.Parse).ToList();
            logger.LogDebug("Parsing report [{Index}]: {Join}", i, string.Join(',', levels));
            _reports.Add(new Report
            {
                ID = i,
                Levels = levels,
                Trend = await CalculateTrend(levels),
                DiffRange = await CalculateDiffRand(levels)
            });
        }

        logger.LogInformation("Parsed {Count} reports", _reports.Count);
        logger.LogTrace("Exiting Day2.ParseInputAsync");
    }

    public async ValueTask<int> CalculateSafeReportsAsync(bool enableProblemDampener = false)
    {
        logger.LogTrace("Entering Day2.CalculateSafeReportsAsync");
        logger.LogDebug("Enable problem dampener is {Value}", enableProblemDampener);
        int safeReports = 0;
        foreach (Report report in _reports)
        {
            bool consistentTrend = await IsTrendConsistent(report.Trend);
            bool withinDiffRange = await IsWithinDiffRange(report.DiffRange);
            
            logger.LogDebug("Report ID {ID} {Trend} consistent trend and {Diff} within diff range", report.ID, consistentTrend ? "Is" : "Is Not", withinDiffRange ? "Is" : "Is Not");
            if (withinDiffRange && consistentTrend)
                safeReports += 1;
            
            if (!enableProblemDampener || (withinDiffRange && consistentTrend)) 
                continue;
            
            logger.LogDebug("Problem dampening Report {ID}", report.ID);
            if (!consistentTrend && withinDiffRange)
                safeReports += await TryProblemDampenReportAsync(report.Trend, true);

            if (!withinDiffRange && consistentTrend)
                safeReports += await TryProblemDampenReportAsync(report.DiffRange);
        }
        
        logger.LogInformation("Found {Count} safe reports", safeReports);
        logger.LogTrace("Exiting Day2.CalculateSafeReportsAsync");
        return safeReports;
    }

    private async ValueTask<int> TryProblemDampenReportAsync(List<int> values, bool dampenTrend = false)
    {
        logger.LogTrace("Entering Day2.TryProblemDampenReportAsync");
        int safeReports = 0;
        for (int i = 0; i < values.Count; i += 1)
        {
            List<int> copy = [..values];
            copy.RemoveAt(i);
            bool isSafeNow = dampenTrend ? await IsTrendConsistent(copy) : await IsWithinDiffRange(copy);
            if (!isSafeNow) continue;
            
            safeReports += 1;
            break;
        }
        
        logger.LogDebug("After dampening, found {Count}", safeReports);
        logger.LogTrace("Exiting Day2.TryProblemDampenReportAsync");
        return safeReports;
    }

    private ValueTask<List<int>> CalculateTrend(List<int> levels)
    {
        logger.LogTrace("Entering Day2.CalculateTrend");
        List<int> trend = [];
        for (int i = 0; i < levels.Count; i+=1)
        {
            if (i == levels.Count - 1) continue;
            if (levels.ElementAt(i) == levels.ElementAt(i + 1))
            {
                trend.Add(0);
            }
            else if (levels.ElementAt(i) < levels.ElementAt(i + 1))
            {
                trend.Add(1);
            }
            else if (levels.ElementAt(i) > levels.ElementAt(i + 1))
            {
                trend.Add(-1);
            }
        }

        logger.LogDebug("Calculated trend: [{Trend}]", string.Join(',', trend));
        logger.LogTrace("Exiting Day2.CalculateTrend");
        return new ValueTask<List<int>>(trend);
    }
    
    private ValueTask<List<int>> CalculateDiffRand(List<int> levels)
    {
        logger.LogTrace("Entering Day2.CalculateDiffRand");
        List<int> diff = [];
        for (int i = 0; i < levels.Count; i+=1)
        {
            if (i == levels.Count - 1) continue;
            diff.Add(Math.Abs(levels.ElementAt(i) - levels.ElementAt(i + 1)));
        }
        
        logger.LogDebug("Calculated diff: [{Diff}]", string.Join(',', diff));
        logger.LogTrace("Exiting Day2.CalculateDiffRand");
        return new ValueTask<List<int>>(diff);
    }
    
    private ValueTask<bool> IsTrendConsistent(List<int> trends)
    {
        logger.LogTrace("Entering Day2.IsTrendConsistent");
        bool result = trends.All(x => x != 0) && (trends.All(x => x == 1) || trends.All(x => x == -1));
        logger.LogDebug("Trend {Consistent}", result ? "is consistent" : "is not consistent");
        logger.LogTrace("Exiting Day2.IsTrendConsistent");
        return new ValueTask<bool>(result);
    }
    
    private ValueTask<bool> IsWithinDiffRange(List<int> diffs)
    {
        logger.LogTrace("Entering Day2.IsWithinDiffRange");
        bool result = diffs.All(x => x is >= 1 and <= 3);
        logger.LogDebug("Diff {Range}", result ? "is within range" : "is outside range");
        logger.LogTrace("Exiting Day2.IsWithinDiffRange");
        return new ValueTask<bool>(result);
    }
}