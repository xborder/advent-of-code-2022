class Day10 {

    private static string GetCRTPixel(int cycle, int X) {
        if (cycle - 1 <= X && X <= cycle + 1) {
            return "#";
        } else {
            return ".";
        }
    }
    private static void part2(string[] instructions) {
        var cycles = 0;
        var carryOver = 0;
        var X = 1;

        var crt = "";
        var opCycles = new Dictionary<string,int>() { 
            {"noop", 1},
            {"addx", 2}
        };
        var lastOperation = "";

        foreach(var instruction in instructions) {
            var args = instruction.Split(" ");
            var operation = args[0];
            

            X += carryOver;

            Console.WriteLine($"{string.Join("\n",crt.Chunk(40).Select(c => string.Join("", c)))}");

            carryOver = operation switch {
                "noop" => 0,
                "addx" => Int32.Parse(args[1]),
                _ => 0
            };
            
            cycles += opCycles[operation];
            
            var crtCycle = (cycles - 1) % 40; 
            if(operation == "noop") {
                crt += GetCRTPixel(crtCycle, X);
            } else if (operation == "addx") {
                crt += GetCRTPixel(crtCycle - 1, X);
                crt += GetCRTPixel(crtCycle, X);
            }
        }

        Console.WriteLine($"Part 2: \n{string.Join("\n",crt.Chunk(40).Select(c => string.Join("", c)))}");
    }
    private static void part1(string[] instructions) {
        var cycles = 0;
        var carryOver = 0;
        var X = 1;
        var signal = 0;
        HashSet<int> specialCycles = new() {20,60,100,140,180,220};

        int GetSignalForCycle(Func<int, bool> cycleInterval) {
            int specialCycle = specialCycles.FirstOrDefault(n => cycleInterval(n));
            if (specialCycle != 0) {
                specialCycles.Remove(specialCycle);
                return specialCycle;
            }
            return 0;
        }

        foreach(var instruction in instructions) {
            var args = instruction.Split(" ");
            var operation = args[0];
            var opCycles = 0;

            X += carryOver;
            
            if (operation == "noop") {
                opCycles = 1;
                carryOver = 0;
            } else if (operation == "addx") {
                opCycles = 2;
                carryOver = Int32.Parse(args[1]);
            }

            cycles += opCycles;
            signal += X * GetSignalForCycle((n) =>  (cycles-opCycles <= n && n <= cycles));
            Console.WriteLine($"{instruction}    \t- cycles {cycles} - X {X}");
        }
        Console.WriteLine($"Part 1: {signal}");
    }
    public static void Run() {
        var input = File.ReadAllLines(@"input/10");

        //part1(input);
        part2(input);
    }

}