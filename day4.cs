
class Day4 {
    private static void part1_v1(string[] input) {
        var count = 0;
        foreach(var pair in input) {
            var assignments = pair.Split(",");
            var elf1_assignment = assignments[0].Split('-').Select(s => Int32.Parse(s));
            var elf1_interval = elf1_assignment.ElementAt(1)-elf1_assignment.ElementAt(0);
            
            var elf2_assignment = assignments[1].Split('-').Select(s => Int32.Parse(s)); 
            var elf2_interval = elf2_assignment.ElementAt(1)-elf2_assignment.ElementAt(0);

            if (elf1_interval == elf2_interval) {
                if(elf1_assignment.ElementAt(0) == elf2_assignment.ElementAt(0)){
                    Console.WriteLine(pair);
                    count++;
                }
            }

            IEnumerable<int> bigger_assignment, smaller_assignment;
            if (elf1_interval > elf2_interval){
                bigger_assignment = elf1_assignment;
                smaller_assignment = elf2_assignment;
            } else {
                bigger_assignment = elf2_assignment;
                smaller_assignment = elf1_assignment;
            }
            
            if (bigger_assignment.ElementAt(0) <= smaller_assignment.ElementAt(0) 
                && bigger_assignment.ElementAt(1) >= smaller_assignment.ElementAt(1)) {
                count++;
            }
        }

        Console.WriteLine($"Part 1: {count}");
    }
    
    private static HashSet<int> GetElfSection(string assignment) {
        var elf1_assignment = assignment.Split('-').Select(s => Int32.Parse(s));
        var elf1_interval = elf1_assignment.ElementAt(1)-elf1_assignment.ElementAt(0);

        return new HashSet<int>(Enumerable.Range(elf1_assignment.ElementAt(0), elf1_interval+1));
    }
        
    private static void part2(string[] input) {
        int count = 0;

        foreach(var pair in input) {
        
            var assignments = pair.Split(",");
            var elf1_section = GetElfSection(assignments[0]);
            var elf2_section = GetElfSection(assignments[1]);

            var intersection = elf1_section.Intersect(elf2_section);
            if (intersection.Count() > 0) { 
                count++;
                continue;
            }
        }
        Console.WriteLine($"Part 2: {count}");
    }
    
    private static void part1_v2(string[] input) {
        int count = 0;

        foreach(var pair in input) {
        
            var assignments = pair.Split(",");
            var elf1_section = GetElfSection(assignments[0]);
            var elf2_section = GetElfSection(assignments[1]);

            var intersection = elf1_section.Except(elf2_section);
            if (intersection.Count() == 0) { 
                count++;
                continue;
            }
            intersection = elf2_section.Except(elf1_section);
            if (intersection.Count() == 0) {
                count++;
                continue;
            }
        }
        Console.WriteLine($"Part 1: {count}");
    }
    public static void Run() {
        string[] text = File.ReadAllLines(@"input/4");

        part1_v2(text);
        part2(text);
    }
}