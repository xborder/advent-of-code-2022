using System.Numerics;

class Day15 {

    public static void part1(string[] input) {
        var coords = input
        .Select(s => 
            s.Split(" ")
            .Select(ss => ss.Replace("x=", "").Replace("y=", "").Replace(",", "").Replace(":",""))
            .Where(ss => int.TryParse(ss, out _))
            .Select(ss => int.Parse(ss))
        );

        var pairs = coords.Select(c => (s : (c: c.ElementAt(0), r: c.ElementAt(1)), b: (c: c.ElementAt(2), r: c.ElementAt(3))));
        var sensors = new HashSet<(int,int)>(pairs.Select(p => p.s));
        var beacons = new HashSet<(int,int)>(pairs.Select(p => p.b));
        
        var filled = new HashSet<(int r, int c)>();

        var row = 2000000;

        foreach (var pair in pairs) {
            var dist = Math.Abs(pair.b.c - pair.s.c) + Math.Abs(pair.b.r - pair.s.r);
            Console.WriteLine($"sensor: {pair.s} beacon: {pair.b} dist: {dist}");

            var cols = Enumerable.Range(pair.s.c - dist, dist * 2 + 1);

            if (pair.s.r + dist < row) {
                continue;
            }

            foreach (var col in cols) {
                var inRange = Math.Abs(col - pair.s.c) + Math.Abs(row - pair.s.r);

                if (inRange <= dist) {
                    if (!filled.Contains((col, row))) {
                        filled.Add((col, row));
                    }
                }
            }
        }
        Console.WriteLine($"Part 1: {filled.Except(sensors.Union(beacons)).Count()}");
    }

    static void part2(string[] input)  {
        var coords = input
        .Select(s => 
            s.Split(" ")
            .Select(ss => ss.Replace("x=", "").Replace("y=", "").Replace(",", "").Replace(":",""))
            .Where(ss => int.TryParse(ss, out _))
            .Select(ss => int.Parse(ss))
        );

        var pairs = coords.Select(c => (s : (c: c.ElementAt(0), r: c.ElementAt(1)), b: (c: c.ElementAt(2), r: c.ElementAt(3))));
        
        var limit = 4000000;

        var filled = new Dictionary<int, int[]>();
        var list = new Dictionary<int, List<Range>>();

        foreach (var pair in pairs) {
            var dist = Math.Abs(pair.b.c - pair.s.c) + Math.Abs(pair.b.r - pair.s.r);
            Console.WriteLine($"sensor: {pair.s} beacon: {pair.b} dist: {dist}");

            var i = -1;
            for (int j = pair.s.c; j <= pair.s.c + dist && j <= limit; j++) {
                i++;
                
                if (j < 0) continue;

                var min_r = pair.s.r - (dist - i) < 0 ? 0 : pair.s.r - (dist - i);
                var max_r = pair.s.r + (dist - i) > limit ? limit : pair.s.r + (dist - i);

                if (!list.ContainsKey(j)) {
                    list.Add(j, new List<Range>());
                }

                list[j].Add(new Range(min_r,max_r));
            }

            i = 0;
            for (int j = pair.s.c - 1; j >= pair.s.c - dist && j >= 0; j--) {
                i++;
                
                if (j > limit) continue;

                var min_r = pair.s.r - (dist - i) < 0 ? 0 : pair.s.r - (dist - i);
                var max_r = pair.s.r + (dist - i) > limit ? limit : pair.s.r + (dist - i);

                if (!list.ContainsKey(j)) {
                    list.Add(j, new List<Range>());
                }

                list[j].Add(new Range(min_r,max_r));
            }
        }

        foreach (var kvp in list) {
            var ordered = kvp.Value.OrderBy(r => r.Start.Value);
            var range = ordered.First();

            foreach (var r in ordered.Skip(1)) {
                if ((range.Start.Value <= r.End.Value && range.End.Value >= r.Start.Value) || r.Start.Value - range.End.Value == 1) {
                    range = new Range(Math.Min(r.Start.Value,range.Start.Value), Math.Max(r.End.Value, range.End.Value));
                } else {
                    var x = new BigInteger(kvp.Key);
                    var mul = new BigInteger(4000000);
                    var y = new BigInteger(range.End.Value + 1);
                    Console.WriteLine($"Part 2:{kvp.Key} {range.End.Value + 1} {x*mul+y}");
                    return;
                }
            }
        }
    }

    public static void Run() {
        string[] input = File.ReadAllLines(@"input/15");

        part1(input);
        part2(input);
    }
}