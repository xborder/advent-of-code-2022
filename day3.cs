
using System.Collections.Specialized;

class Day3 {
    private static int CharPriority(char c) {
        var priority = 0;
        if ( c >= 'a' && c <= 'z') {
            var lowerCaseBase = 1;
            priority = lowerCaseBase + c - 'a';
        } else if ( c >= 'A' && c <= 'Z') {
            var upperCaseBase = ('z'-'a'+ 1) + 1;
            priority = upperCaseBase + c - 'A';
        }
        return priority;
    }

    private static char GetRepeatedChar(char[] array) {
        var firstHalf = array[..(array.Length/2)];
        var secondHalf = array[(array.Length/2)..];
        HashSet<char> visited = firstHalf.ToHashSet();
        foreach(char c in secondHalf) {
            if (visited.Contains(c)) {
                return c;
            }
        }
        return ' ';
    }

    private static void part1(string[] input) {
        var sum = 0;
        foreach(var line in input) {
            sum += CharPriority(GetRepeatedChar(line.ToCharArray()));
        }
        Console.WriteLine($"Part 1: {sum}");
    }

    private static void part2_v1(string[] input) {
        var sum = 0;
        
        input
        .Select(s => s.ToCharArray().AsEnumerable())
        .Chunk(3)
        .Select(s => s[0].Intersect(s[1]).Intersect(s[2]).First())
        .Select(c => CharPriority(c))
        .Sum();

        var intersection = input.First().ToCharArray().AsEnumerable();
        var elfNum = 1;
        foreach(var elf in input.Skip(1)) {
            if (elfNum == 3) {
                sum += CharPriority(intersection.First());
                
                intersection = elf.ToCharArray().AsEnumerable();
                elfNum = 1;
                continue;
            }
            intersection = intersection.Intersect(elf.ToCharArray());
            elfNum++;
        }
        //last group
        sum += CharPriority(intersection.First());
        Console.WriteLine($"Part 2 v1: {sum}");
    }

    private static void part2_v2(string[] input) {
        var sum = input
                    .Select(s => s.ToCharArray().AsEnumerable())
                    .Chunk(3)
                    .Select(s => s[0].Intersect(s[1]).Intersect(s[2]).First())
                    .Select(c => CharPriority(c))
                    .Sum();


        Console.WriteLine($"Part 2 v2: {sum}");
    }
    public static void Run() {
        string[] input = File.ReadAllLines(@"input/3");

        part1(input);
        part2_v1(input);
        part2_v2(input);
    }
}