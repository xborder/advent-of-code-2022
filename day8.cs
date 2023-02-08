
class Day8 {

    private static void part2(int[][] map) {
        int countVisibleTrees(IEnumerable<int> segment, int treeHeight) {
            var count = 0;
            foreach(var tree in segment) {
                if( tree < treeHeight) {
                    count ++;
                } else if (tree >= treeHeight) {
                    count++;
                    break;
                }
            }
            return count;
        }

        int scenicScore = 0;
        for(int i = 3; i < map.Length - 1; i++) {
            for(int j = 2; j < map[i].Length - 1; j++) {
                int treeHeight = map[i][j];
                var rigthSegment = map[i].Skip(j+1);
                var leftSegment = map[i].Take(j).Reverse();
                var topSegment = map.Take(i).Select(r => r[j]).Reverse();
                var bottomSegment = map.Skip(i+1).Select(r => r[j]);


                var visibleToTheBottom = countVisibleTrees(bottomSegment, treeHeight);
                var visibleToTheLeft = countVisibleTrees(leftSegment, treeHeight);
                var visibletoTheTop = countVisibleTrees(topSegment, treeHeight);
                var visibleToTheRight = countVisibleTrees(rigthSegment, treeHeight);
                
                var score = (visibleToTheBottom*visibleToTheLeft*visibleToTheRight*visibletoTheTop);

                scenicScore = Math.Max(scenicScore, score);
            }
        }
        Console.WriteLine($"Part 2: {scenicScore}");
    }

    private static void part1(int[][] map) {
        //every tree on the edges, minus 4 corners that are duplicates
        var visibleTrees = map.Length*2 - 2  + map[0].Length*2 - 2;

        for(int i = 1; i < map.Length - 1; i++) {
            for(int j = 1; j < map[i].Length - 1; j++) {
                int treeHeight = map[i][j];
                bool visibleToTheRight = map[i].Skip(j+1).All(v => v < treeHeight);
                bool visibleToTheLeft = map[i].Take(j).All(v => v < treeHeight);
                bool visibletoTheTop = map.Take(i).Select(r => r[j]).All(v => v < treeHeight);
                bool visibleToTheBottom = map.Skip(i+1).Select(r => r[j]).All(v => v < treeHeight);
                
                if (visibleToTheBottom || visibleToTheLeft || visibleToTheRight || visibletoTheTop) {
                    visibleTrees++;
                }
            }
        }
        Console.WriteLine($"Part 1: {visibleTrees}");
    }
    public static void Run() {
        var input = File.ReadAllLines(@"input/8");
        var map = input.Select(s => s.Select(c => c-48).ToArray()).ToArray();
        part1(map);
        part2(map);
    }

}