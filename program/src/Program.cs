using System.Linq;

namespace PossiblePawnsMoves
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                System.Console.WriteLine("Program needs 2 parameters");
                System.Console.WriteLine("[1]: 'In' folder");
                System.Console.WriteLine("[2]: 'Out' folder");
                return;
            }


            System.Console.WriteLine("Loading Pawns from files...");
            var data = FileStreamer.ReadFile(args[0]);
            System.Console.WriteLine($"Wczytano {data.Count()} pionków");


            System.Console.WriteLine("Parsing data...");
            // get pawns without duplicates
            var pawns = data.Select(x => ChessPawnParser.Parse(x))
                            .Where(x => x != null)
                            .Distinct(new PawnComparer())
                            .ToList();

            System.Console.WriteLine($"{pawns.Count} pawns are correct");


            System.Console.WriteLine("Computing possible moves...");
            pawns = pawns.Select(x => new MovementEngine().GetPossibleMoves(x))
                         .ToList();


            System.Console.WriteLine("Drawing a chessboard...");
            var pawnsMoves = pawns.Select(x => new Board().GetBoard(x))
                                  .ToList();

            // create dictionary: input: output
            var dict = pawns.Select(x => x.ToString())
                            .Zip(pawnsMoves, (k, v) => new { k, v })
                            .ToDictionary(x => x.k, x => x.v);


            System.Console.WriteLine("Saving results...");
            FileStreamer.WriteFile(dict, args[1]);
        }
    }
}