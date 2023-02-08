class Day9 {
    class Knot {
        public (int x, int y) pos;
        public Knot next;
        public Knot previous;
    }

    private static Knot CreateRope(int nKnots) {
        var startPos = (0,0);
        var head = new Knot() {
            pos = startPos
        };

        var previous = head;
        nKnots--;
        while(nKnots > 0){
            var newKnot = new Knot() {
                pos = startPos,
                previous = previous
            };
            previous.next = newKnot;
            previous = newKnot;
            nKnots--;
        }
        return head;
    }
    
    private static int MoveRope(string[] moves, Knot head) {
        
        var visited = new HashSet<(int,int)>();
        visited.Add(head.pos);

        foreach(var move in moves) {
            var _move = move.Split(" ");
            var direction = _move[0];
            var amount = Int32.Parse(_move[1]);

            while(amount > 0) {
                switch(direction) {
                    case "R" : 
                        head.pos.y ++;
                        break;
                    case "L" : 
                        head.pos.y --;
                        break;
                    case "U" : 
                        head.pos.x ++;
                        break;
                    case "D" :
                        head.pos.x --;
                        break;
                    default: break;
                }

                var previous = head;
                var next = head.next;
                while(next != null) {
                    var diffy = previous.pos.y - next.pos.y;
                    var diffx = previous.pos.x - next.pos.x; 
                    if (Math.Abs(diffx) > 1 || Math.Abs(diffy) > 1) {
                        next.pos.x += Math.Sign(diffx);
                        next.pos.y += Math.Sign(diffy);
                        if (next.next == null) {
                            visited.Add(next.pos);
                        }
                    }
                    previous = next;
                    next = next.next;
                }
                amount--;
            }
        }
        return visited.Count();
    }
    private static void part2(string[] input) {
        var head = CreateRope(10);
        Console.WriteLine($"Part 2: {MoveRope(input, head)}");
    }

    private static void part1(string[] input) {
        var head = CreateRope(2);
        Console.WriteLine($"Part 1: {MoveRope(input, head)}");
    }
    public static void Run() {
        var input = File.ReadAllLines(@"input/9");
        part1(input);
        part2(input);
    }

}