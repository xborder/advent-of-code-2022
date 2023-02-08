using Newtonsoft.Json.Linq;

class Day13 {

    static bool? compare(JToken l, JToken r) {
        if (l.Type == JTokenType.Integer && r.Type == JTokenType.Integer) {
            var diff = l.Value<int>() - r.Value<int>();
            
            return diff != 0 ? diff < 0 : null;
        }

        if (l.Type == JTokenType.Array && r.Type == JTokenType.Array) {
            Queue<JToken> leftQ = new(l.Children().Select(t => t));
            Queue<JToken> rightQ = new(r.Children().Select(t => t));
            
            bool? inOrder = null;

            while(leftQ.Count > 0 && rightQ.Count > 0 && !inOrder.HasValue) {
                var left = leftQ.Dequeue();
                var right = rightQ.Dequeue();

                var tmp = compare(left,right);
                
                if (tmp.HasValue) {
                    inOrder = tmp;
                }
            }
            
            if (!inOrder.HasValue) {
                if (leftQ.Count < rightQ.Count) {
                    inOrder = true;
                } else if (leftQ.Count > rightQ.Count){
                    inOrder = false;
                }
            }
            return inOrder;
        }

        l = l.Type == JTokenType.Integer ? new JArray(l) : l;
        r = r.Type == JTokenType.Integer ? new JArray(r) : r;
        
        return compare(l, r);
    }

    class Comparer : IComparer<JToken>
    {
        public int Compare(JToken? x, JToken? y)
        {
            bool? res = compare(x, y);
            if (res.HasValue) {
                return res.Value ? -1 : 1;
            }
            return 0;
        }
    }
    static void part2(IEnumerable<IEnumerable<string>> input) {
        SortedSet<JToken> sorted = new(new Comparer());
        var key1 = JArray.Parse("[[2]]");
        var key2 = JArray.Parse("[[6]]");
        
        sorted.Add(key1);
        sorted.Add(key2);

        foreach(var pair in input) {
            var leftSide = pair.ElementAt(0);
            var rightSide = pair.ElementAt(1);

            JArray left = JArray.Parse(leftSide);
            JArray right = JArray.Parse(rightSide);

            sorted.Add(left);
            sorted.Add(right);
        }
        
        int index = 1;
        int res = 1;
        foreach(var x in sorted) {
            if (x == key1 || x == key2) {
                res *= index;
            }
            Console.WriteLine($"{index++, 2} " + x.ToString().Replace("\n","").Replace(" ",""));
        }
        Console.WriteLine($"decoder key: {res}");
    }

    static void part1(IEnumerable<IEnumerable<string>> input) {
        int pairNumber = 1;
        int sum = 0;
        foreach(var pair in input) {
            var leftSide = pair.ElementAt(0);
            var rightSide = pair.ElementAt(1);

            JArray left = JArray.Parse(leftSide);
            JArray right = JArray.Parse(rightSide);

            bool? rightOrder = compare(left, right);

            if (rightOrder.HasValue && rightOrder.Value) {
                Console.WriteLine($"== PAIR {pairNumber} ==");
                Console.WriteLine($"{leftSide} -> {rightSide}");
                Console.WriteLine($"{pairNumber} in the right order");
                sum += pairNumber;
            }
            pairNumber++;
        }
        Console.WriteLine($"Sum: {sum}");
    }

    public static void Run() {
        var input = File.ReadAllLines(@"input/13")
            .Chunk(3)
            .Select( sarr => sarr.Where( s => !string.IsNullOrEmpty(s)));
        part1(input);
        part2(input);
    }
}