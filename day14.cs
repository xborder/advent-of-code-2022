using System.Diagnostics;

class Day14 {

    static HashSet<(int r,int c)> GetPaths(string[] input) {

        var paths = input.Select(s => s.Split(" -> "));

        var allPaths = new HashSet<(int r,int c)>();

        foreach(var path in paths) {
            (int r, int c) start = default;
            foreach(var coord in path) {
                var parsed = coord.Split(",").Select(i => int.Parse(i));
                var r = parsed.ElementAt(1);
                var c = parsed.ElementAt(0);

                if (start == default) {
                    start = (r,c);
                    continue;
                } 
                
                var r_diff = r - start.r;
                var c_diff = c - start.c;
                allPaths.UnionWith(
                        Enumerable.Range(0, Math.Abs(r_diff) + 1)
                            .Select(i => (start.r + i * Math.Sign(r_diff), c))
                    );

                allPaths.UnionWith(
                        Enumerable.Range(0, Math.Abs(c_diff)  + 1)
                            .Select(i => (r, start.c + i * Math.Sign(c_diff)))
                    );

                start = (r,c);
            }
        }

        return allPaths;
    }

    static void print(HashSet<(int r,int c)> paths, HashSet<(int r,int c)> grains) {
        var max_r = paths.MaxBy(p => p.r);
        var min_r = paths.MinBy(p => p.r);
        var max_c = paths.MaxBy(p => p.c);
        var min_c = paths.MinBy(p => p.c);
        var row_s = max_r.r + 2;
        var col_s = max_c.c - min_c.c;

        var map = Enumerable.Range(1, row_s).Select(i => Enumerable.Repeat('.', col_s).ToArray()).ToArray();

        foreach(var path in paths) {
            map[path.r % row_s][(path.c - min_c.c) % col_s] = '#';
        }
        foreach(var path in grains) {
            map[path.r % row_s][(path.c - min_c.c) % col_s] = 'o';
        }
        Console.Write(string.Join("\n", map.Select( r => string.Join("", r))));
        Thread.Sleep(50);
        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
    }
    
    static void part2(string[] input) {

        var osbtacles = GetPaths(input);
        
        var grains = new HashSet<(int r,int c)>();

        //print(osbtacles, grains);

        var sand = (r: 0, c: 500);

        var settledGrains = 0;
        var grainToSettle = sand;
        var max_r = osbtacles.MaxBy(p => p.r).r + 2;
        
        while(true) {
            var grain = grainToSettle;
            var closestObstacle = osbtacles.Where(p => p.c == grain.c && p.r >= grain.r);

            if (closestObstacle.Count() == 0) {
                closestObstacle = closestObstacle.Append((max_r, grain.c));
            }
            var closest = closestObstacle.MinBy(p => p.r);

            var down_left = (r: closest.r, c: closest.c - 1);
            var down_right = (r: closest.r, c: closest.c + 1); 

            if (!osbtacles.Contains(down_left) && down_left.r != max_r) {
                grainToSettle = down_left;
                continue;
            } 

            if (!osbtacles.Contains(down_right) && down_right.r != max_r) {
                grainToSettle = down_right;
                continue;
            }

            grains.Add((closest.r - 1, closest.c));
            osbtacles.Add((closest.r - 1, closest.c));
            
            //print(osbtacles,grains);
            settledGrains++;
            if ((closest.r - 1, closest.c) == sand) {
                break;
            }
            grainToSettle = sand;
        }

        Console.WriteLine($"Part 2: {settledGrains}");
    }

    static void part2_bfs(string[] input) {
        
        var osbtacles = GetPaths(input);
        
        var grains = new Queue<(int r,int c)>();
        var settled = new HashSet<(int t, int c)>();

        var max_r = osbtacles.MaxBy(p => p.r).r + 2;

        var sand = (r: 0, c: 500);
        grains.Enqueue(sand);

        while(grains.TryDequeue(out (int r, int c) grain)) {

            if (settled.Contains(grain) || grain.r == max_r || osbtacles.Contains(grain)) {
                continue;
            }

            settled.Add(grain);
            osbtacles.Add(grain);
            
            //print(osbtacles, settled);

            grains.Enqueue((grain.r + 1, grain.c));
            grains.Enqueue((grain.r + 1, grain.c - 1));
            grains.Enqueue((grain.r + 1, grain.c + 1));
        }

        
        Console.WriteLine($"Part 2: {settled.Count()}");
    }

    static void part1(string[] input) {

        var osbtacles = GetPaths(input);
        
        var grains = new HashSet<(int r,int c)>();

        print(osbtacles, grains);

        var sand = (r: 0, c: 500);

        var settledGrains = 0;
        var grainToSettle = sand;

        while(true) {
            var grain = grainToSettle;
            var closestObstacle = osbtacles.Where(p => p.c == grain.c && p.r >= grain.r);

            if (closestObstacle.Count() == 0) {
                //falling into the abyss
                break;
            }

            var closest = closestObstacle.MinBy(p => p.r);

            var down_left = (r: closest.r, c: closest.c - 1);
            var down_right = (r: closest.r, c: closest.c + 1); 

            if (!osbtacles.Contains(down_left)) {
                grainToSettle = down_left;
                continue;
            } 

            if (!osbtacles.Contains(down_right)) {
                grainToSettle = down_right;
                continue;
            }

            grains.Add((closest.r - 1, closest.c));
            osbtacles.Add((closest.r - 1, closest.c));
            
            print(osbtacles, grains);
            settledGrains++;
            grainToSettle = sand;
        }

        Console.WriteLine($"Part 1: {settledGrains}");
    }
    public static void Run() {
        string[] input = File.ReadAllLines(@"input/14");

        part1(input);
        
        var watch = Stopwatch.StartNew();
        part2(input);
        watch.Stop();
        Console.WriteLine($"Total: {TimeSpan.FromTicks(watch.Elapsed.Ticks).TotalSeconds}s");

        watch.Reset();
        watch.Start();
        part2_bfs(input);
        watch.Stop();
        Console.WriteLine($"Total: {TimeSpan.FromTicks(watch.Elapsed.Ticks).TotalSeconds}s");
    }
}