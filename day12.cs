
class Day12 {
    public static void Run() {
        string[] input = File.ReadAllLines(@"input/12");
        
        var map = input.Select(s => s.ToCharArray().Select(c => c - 'a').ToArray()).ToArray();
        (int, int) nonExistent = (-1,-1);
        (int x, int y) start = nonExistent;
        (int x, int y) end = nonExistent;

        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map[i].Length; j++) {
                if (map[i][j] == 'S' - 'a') {
                    start = (i,j);
                    map[i][j] = 0;
                } else if (map[i][j] == 'E'- 'a') {
                    end = (i,j);
                    map[i][j] = 'z'-'a';
                }
            }
        }

        var visited = FindPath(map, end);
        Console.WriteLine($"Part 1: {visited[start]}");

        var lowestSteps = visited
            .Where(kvp => map[kvp.Key.Item1][kvp.Key.Item2] == 0)
            .OrderBy(kvp => kvp.Value)
            .First();

        Console.WriteLine($"Part 2: {lowestSteps.Value}");
    }

    static Dictionary<(int, int), int> FindPath(int[][] map, (int x, int y) end) {
        
        (int x, int y)[] adjancentPos = new[]{(-1,0), (0,1), (1,0), (0,-1)};

        Dictionary<(int, int), int> visited = new();
        Queue<(int, int, int)> toVisit = new();
        toVisit.Enqueue((end.x, end.y, 0));

        while(toVisit.TryDequeue(out (int x, int y, int dist) pos)) {
            if (visited.ContainsKey((pos.x,pos.y))) {
                continue;
            } else {
                visited.Add((pos.x,pos.y), pos.dist);
            }

            foreach (var adjacent in adjancentPos) {
                (int x, int y) nextPos = (adjacent.x + pos.x, adjacent.y + pos.y);
                
                if (nextPos.x < 0 || nextPos.x == map.Length || nextPos.y < 0 || nextPos.y == map[0].Length) {
                    continue;
                }
                
                if (map[nextPos.x][nextPos.y] >= map[pos.x][pos.y] - 1 ) {
                    toVisit.Enqueue((nextPos.x, nextPos.y, pos.dist + 1));
                }
            } 
        }
    	
        return visited;
    }
}