
abstract class Move {
    public abstract int score(Move adversary);
}
class Paper : Move {
    public override int score(Move adversary) {
        if (adversary is Paper) {
            return 3;
        } else if (adversary is Rock) {
            return 6;
        }
        return 0;
    }
}
class Scissor : Move {
    public override int score(Move adversary) {
        if (adversary is Scissor) {
            return 3;
        } else if (adversary is Paper) {
            return 6;
        }
        return 0;
    }
}
class Rock : Move {
    public override int score(Move adversary) {
        if (adversary is Rock) {
            return 3;
        } else if (adversary is Scissor) {
            return 6;
        }
        return 0;
    }
}

class DecryptPlay {

    private static Move convertToMove(string move) {
        if (new[]{"A", "X"}.Contains(move)){
            return new Rock();
        } else if (new[]{"B", "Y"}.Contains(move)) {
            return new Paper();
        } else if (new[]{"C", "Z"}.Contains(move)) {
            return new Scissor();
        }
        return null;
    }
    
     private static Move DecodeStratToMove(string strat, Move adversaryMove) {
        if ("X" == strat){  //lose
            return adversaryMove switch
        {
            Rock => new Scissor(),
            Paper => new Rock(),
            Scissor => new Paper(),
            _ => null
        };
        } else if ("Y" == strat) { //draw
            return adversaryMove switch
        {
            Rock => new Rock(),
            Paper => new Paper(),
            Scissor => new Scissor(),
            _ => null
        };
        } else if ("Z"== strat) { //win
            return adversaryMove switch
        {
            Rock => new Paper(),
            Paper => new Scissor(),
            Scissor => new Rock(),
            _ => null
        };
        }
        return null;
    }
    
    public static int countScore(string adversaryMove, string stratMove, bool realStrat) {
        var advsMove = convertToMove(adversaryMove);
        var myMove = realStrat ? DecodeStratToMove(stratMove, advsMove) : convertToMove(stratMove);
        return myMove switch
        {
            Rock =>  1 + myMove.score(advsMove),
            Paper => 2 + myMove.score(advsMove),
            Scissor => 3 + myMove.score(advsMove),
            _ => 0
        };
    }
}

class Day2 {
    public static void Run() {
        string[] strategy = File.ReadAllLines(@"input/2");

        var totalScore = 0;
        var totalScoreRealStrat = 0;
        foreach(var line in strategy){
            var play = line.Split(" ");
            totalScore += DecryptPlay.countScore(play[0], play[1],  realStrat: false);
            totalScoreRealStrat += DecryptPlay.countScore(play[0], play[1], realStrat: true);
        }

        Console.WriteLine($"Part 1: {totalScore}");
        Console.WriteLine($"Part 2: {totalScoreRealStrat}");
    }
}