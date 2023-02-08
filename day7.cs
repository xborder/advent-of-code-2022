
class Day7 {
    class Node {
        public string name;
        public Node parent;
        public List<Node> children = new List<Node>();
        public int size;
        public bool isDir;
    }

    private static Node BuildFilesystem(string[] input) {

        Node root = null;
        Node currentNode = null;
        foreach(var line in input) {
            var tokens = line.Split(" ");
            if (tokens.First().Equals("$")) { // is a command
                var command = tokens.ElementAt(1);
                var args = tokens.Skip(2);

                switch (command) {
                    case "cd": 
                    var dir = args.First();
                    if (dir == "/") {
                        root = new Node() {
                            isDir = true,
                            name = dir
                        };
                        currentNode = root;
                    } else if (dir == "..") {
                        currentNode = currentNode.parent;
                    } else {
                        currentNode = currentNode.children.FirstOrDefault(n => n.name == dir && n.isDir);
                    }
                    break;
                    case "ls": break;
                    default: Console.Error.WriteLine("unknown command"); break;
                } 
            } else {
                var token1 = tokens.ElementAt(0);
                var token2 = tokens.ElementAt(1);
                if (token1 == "dir") {
                    currentNode.children.Add(new Node() {
                        isDir = true,
                        name = token2,
                        parent = currentNode
                    });
                } else {
                    Int32.TryParse(token1, out int filesize);
                    currentNode.children.Add(new Node() {
                        name = token2,
                        size = filesize,
                        parent = currentNode
                    });
                    

                    var node = currentNode;
                    do {
                        node.size += filesize;
                        node = node.parent;
                    } while(node != null);
                    
                }
            }
        }
        return root;
    }


    private static int GetDirectoriesSum(Node dir, int atMostSize) {
        var acc = dir.size <= atMostSize ? dir.size : 0; 
        return acc + dir.children.Where(c => c.isDir).Select(d => GetDirectoriesSum(d, atMostSize)).Sum();
    }

    private static void part1(string[] input) {
        Node root = BuildFilesystem(input);
        Console.WriteLine($"Part 1: {GetDirectoriesSum(root, 100000)}");
    }

    private static void part2(string[] input) {
        Node root = BuildFilesystem(input);
        Queue<Node> toVisit = new Queue<Node>();
        toVisit.Enqueue(root);

        List<Node> elligebleNodes = new List<Node>();

        var freespace = 70000000 - root.size;
        var neededSize = 30000000;
        while(toVisit.Count() > 0) {
            var node = toVisit.Dequeue();
            if(node.size + freespace >= neededSize) {
                elligebleNodes.Add(node);
                var elligebleChildren = node.children.Where(n => n.isDir);
                foreach(var x in elligebleChildren){
                    toVisit.Enqueue(x);
                }
            }
        }
        Console.WriteLine($"Part 2: {elligebleNodes.OrderBy(n => n.size).First().size}");
    }

    public static void Run() {
        string[] input = File.ReadAllLines(@"input/7");
        
        part1(input);
        part2(input);
    }

}