
class Day1 {
    public static void Run() {
        string[] text = File.ReadAllLines(@"input/1");

        var max = 0;
        var tmp = 0;
        List<int> calories = new List<int>();

        foreach(var line in text){
            if (string.IsNullOrEmpty(line)) {
                if (tmp > max) {
                    max = tmp;
                }
                calories.Add(tmp);
                tmp = 0;
                continue;
            }
            tmp += Int32.Parse(line);
        }
        //last elf
        calories.Add(tmp);
        
        var orderedCalories = calories.OrderByDescending(i => i);

        Console.WriteLine($"Part 1: {orderedCalories.First()}");
        Console.WriteLine($"Part 2: {orderedCalories.Take(3).Sum()}");
    }
}