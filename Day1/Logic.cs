using Microsoft.Extensions.Logging;

namespace AdventOfCode2024.Day1;

public class Logic(ILogger logger)
{
    private readonly string[] _input = File.ReadAllLines("Day1/input.txt");
    private List<long> _leftList = [];
    private List<long> _rightList = [];

    public async Task ParseInputIntoLists()
    {
        foreach (string line in _input)
        {
            string[] splitLine = line.Split(' ');
            
            long leftValue = long.Parse(splitLine[0]);
            long rightValue = long.Parse(splitLine[3]);
            
            await SortValueIntoList(_leftList, leftValue);
            await SortValueIntoList(_rightList, rightValue);
        }
        
        // Console.WriteLine($"Left list: {string.Join(',', leftList)}");
        // Console.WriteLine($"Right list: {string.Join(',', rightList)}");
    }
    
    public Task<long> GetTotalDistance()
    {
         return Task.FromResult(_leftList.Select((t, i) => Math.Abs(t - _rightList[i])).Sum());
    }

    public Task<long> GetSimilarityScore()
    {
        long similarityScore = 0; 
        foreach (long leftValue in _leftList)
        {
            int occurence = _rightList.Count(rl => rl.Equals(leftValue));
            // Console.WriteLine($"[{leftValue}] occured [{occurence}] times in right list");
            similarityScore += leftValue * occurence;
        }
        return Task.FromResult(similarityScore);
    }

    private static Task SortValueIntoList(List<long> list, long value)
    {
        List<long> copyList = list.ToList();
        if (copyList.Count == 0)
        {
            list.Add(value);
            return Task.CompletedTask;
        }
        
        for (int i = 0; i < copyList.Count; i += 1)
        {
            long currentValue = copyList[i];
            
            if (value <= currentValue)
            {
                list.Insert(i, value);
                return Task.CompletedTask;
            }

            if (value > currentValue && i == copyList.Count - 1)
            {
                list.Add(value);
                return Task.CompletedTask;
            }
        }

        return Task.CompletedTask;
    }
} 