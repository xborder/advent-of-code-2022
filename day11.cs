
class Day11 {
    class Monkey {
        public Queue<long> items;
        public Func<long,long> operation;
        public Func<long, int> test;
        public int activity;
        public int prime;
    }
    /*
            var monkeys = new List<Monkey>();
        monkeys.Add(new Monkey() {
            items = new(new[]{79,98}),
            operation = (n) => (n * 19)/3,
            test = (n) => n % 23 ==0 ? 2 : 3
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{54, 65, 75, 74}),
            operation = (n) => (n + 6)/3,
            test = (n) => n % 19 == 0 ? 2 : 0
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{79, 60, 97}),
            operation = (n) => (n * n)/3,
            test = (n) => n % 13 == 0 ? 1 : 3
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{74}),
            operation = (n) => (n + 3)/3,
            test = (n) => n % 17 == 0 ? 0 : 1
        });
    */
    public static void Run() {
        string[] text = File.ReadAllLines(@"input/11");
        /*var monkeys = new List<Monkey>();
        monkeys.Add(new Monkey() {
            items = new(new[]{79L,98}),
            operation = (n) => (n * 19)% 96577,
            test = (n) => n % 23 == 0 ? 2 : 3,
            prime = 23
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{54L, 65, 75, 74}),
            operation = (n) => (n + 6) % 96577,
            test = (n) => n % 19 == 0 ? 2 : 0,
            prime = 19
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{79L, 60, 97}),
            operation = (n) => (n * n) % 96577,
            test = (n) => n % 13 == 0 ? 1 : 3,
            prime = 13
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{74L}),
            operation = (n) => (n + 3)% 96577,
            test = (n) => n % 17 == 0 ? 0 : 1,
            prime = 17
        });*/
        var monkeys = new List<Monkey>();
        monkeys.Add(new Monkey() {
            items = new(new[]{97L, 81, 57, 57, 91, 61}),
            operation = (n) => (n * 7),
            test = (n) => n % 11 == 0 ? 5 : 6
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{88L, 62, 68, 90}),
            operation = (n) => (n * 17),
            test = (n) => n % 19 == 0 ? 4 : 2
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{74L, 87}),
            operation = (n) => (n + 2),
            test = (n) => n % 5 == 0 ? 7 : 4
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{53L, 81, 60, 87, 90, 99, 75}),
            operation = (n) => (n + 1),
            test = (n) => n % 2 == 0 ? 2 : 1
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{57L}),
            operation = (n) => (n + 6),
            test = (n) => n % 13 == 0 ? 7 : 0
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{ 54L, 84, 91, 55, 59, 72, 75, 70}),
            operation = (n) => (n * n),
            test = (n) => n % 7 == 0 ? 6 : 3
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{95L, 79, 79, 68, 78}),
            operation = (n) => (n + 3),
            test = (n) => n % 3 == 0 ? 1 : 3
        });
        monkeys.Add(new Monkey() {
            items = new(new[]{61L, 97, 67}),
            operation = (n) => (n + 4),
            test = (n) => n % 17 == 0 ? 0 : 5
        });

        for(int i = 0; i < 10000; i++) {
            foreach(var monkey in monkeys) {
                while(monkey.items.Count() > 0) {
                    var item = monkey.items.Dequeue();
                    item = monkey.operation(item);
                    var monkeyToSend = monkey.test(item);
                    monkeys[monkeyToSend].items.Enqueue(item % 9699690);
                    monkey.activity++;
                }
            }
            if(new[]{0,19,999,1999,2999,3999,4999,5999,6999,7999,8999,9999}.Contains(i)) {
                int j = 0;
                Console.WriteLine($"== After round {i+1} ==");
                foreach(var monkey in monkeys) {
                    Console.WriteLine($"{j++} : inspected items {monkey.activity} times");
                }
            }
        }  
        var top = monkeys.OrderByDescending(m => m.activity).Take(2).Select(m => m.activity);
        Console.WriteLine(Math.BigMul(top.ElementAt(0),top.ElementAt(1)));
    }
}