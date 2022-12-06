class Day6 {

    private static void GetStartOfMessage(int markerSize, string[] input) {
        foreach (var datastream in input) {
            var signals = datastream.ToArray();
            for(int i = 0; i < signals.Length; i++) {
                var marker = signals.Skip(i).Take(markerSize); 
                if (marker.Distinct().Count() == markerSize) {
                    Console.WriteLine(i+markerSize);
                    break;
                }
            }
        } 
    }

    private static void part1(string[] input) {
        Console.Write("Part 1: ");
        GetStartOfMessage(4, input);
    }
    
    private static void part2(string[] input) {
        Console.Write("Part 2: ");
        GetStartOfMessage(14, input);
    }

    public static void Run() {
        string[] text = File.ReadAllLines(@"input/6");
        part1(text);
        part2(text);
    }
}