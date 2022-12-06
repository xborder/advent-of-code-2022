
class Day5 {

    private static void ParseInput(Action<int, IEnumerable<string>> addToShip, Action<int, int, int> moveCrates, string[] input) {
        var cratesInitPos = new List<IEnumerable<string>>();
        var initBoard = true;
        foreach(var line in input){
            if (string.IsNullOrEmpty(line)) {
                initBoard = false;
                continue;
            }

            if (initBoard) {
                if (!line.Contains('[')) { //parse stacks numbers
                    var numStacks = line.Split(' ').Where(s => !string.IsNullOrEmpty(s));
                    foreach(var s in numStacks) {
                        var stackNum = Int32.Parse(s);
                        var initialStack = cratesInitPos.Select(c => c.ElementAt(stackNum - 1)).Where(s => !string.IsNullOrEmpty(s)).Reverse();
                        addToShip(stackNum, initialStack);
                    }
                } else {
                    var crates = line.Chunk(4).Select(array => string.Join("", array).Trim(' ').Trim('[').Trim(']'));
                    cratesInitPos.Add(crates);
                }
            } else {
                var move = line.Split(' ').Where(s => !new[]{"move", "from", "to"}.Contains(s)).Select(s => Int32.Parse(s));
                var nCrates = move.ElementAt(0);
                var startStack = move.ElementAt(1);
                var endStack = move.ElementAt(2);
                
                moveCrates(nCrates, startStack, endStack);
            }
        }
    }
    private static void part2(string[] input) {
        var ship = new Dictionary<int, List<string>>();

        ParseInput(
            (stackNum, initialStack) => ship.Add(stackNum, new List<string>(initialStack)), 
            (nCrates, startStack, endStack) => {
                var crates = ship[startStack].TakeLast(nCrates);
                ship[startStack] = ship[startStack].SkipLast(nCrates).ToList();
                ship[endStack].AddRange(crates);
            },
            input
        );

        Console.Write("Part 2: ");
        foreach(var stack in ship.ToList()) {
            Console.Write(stack.Value.Last());
        } 
        Console.WriteLine();
    }

    private static void part1(string[] input) {
        var ship = new Dictionary<int, Stack<string>>();

        ParseInput(
            (stackNum, initialStack) => ship.Add(stackNum, new Stack<string>(initialStack)), 
            (nCrates, startStack, endStack) => {
                for(int i = 0; i < nCrates; i++) {
                    var crate = ship[startStack].Pop();
                    ship[endStack].Push(crate);
                }
            },
            input
        );

        Console.Write("Part 1: ");
        foreach(var stack in ship.ToList()) {
            Console.Write(stack.Value.Peek());
        } 
        Console.WriteLine();
    }
    public static void Run() {
        string[] text = File.ReadAllLines(@"input/5");
        
        part1(text);
        part2(text);
    }
}