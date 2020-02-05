using System;
using System.Linq;

namespace PossiblePawnsMoves
{
    public static class ChessPawnParser
    {
        // Parse data into collection of pawns object
        public static ChessPawn Parse(string data)
        {
            string[] tbData = data
                .Split(';')
                .Select(x => x.Trim().ToLower())
                .ToArray();

            ChessPawn pawn;

            try
            {
                var side = ParseSide(tbData[0].ToLower());
                var xPos = ParseCoordinate(tbData[1]);
                var yPos = ParseCoordinate(tbData[2]);
                var type = ParseType(tbData[3]);
                pawn = new ChessPawn(side, (x: xPos, y: yPos), type);

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Oops... :C");
                System.Console.WriteLine($"Type of error: {e.GetType()}");
                System.Console.WriteLine($"Input data: {data}");
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine();
                pawn = null;
            }

            return pawn;
        }

        private static char ParseSide(string side)
        {
            if (String.IsNullOrWhiteSpace(side))
            {
                throw new ArgumentException("Color of side can't be empty.");
            }

            if (side != "b" && side != "w")
            {
                throw new ArgumentOutOfRangeException($"Color of side should be marked by 'w' or 'b'. It can't be '[{side}]'.");
            }

            return side[0];
        }

        private static int ParseCoordinate(string coord)
        {
            int temp = int.Parse(coord);

            if (temp > 8 || temp < 1)
            {
                throw new ArgumentOutOfRangeException($"Coordinate of pawn [{coord}] must be in range (1,8).");
            }

            return temp;
        }

        private static ChessPawnType ParseType(string type)
        {
            if (String.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Pawn's type can't be empty.");
            }

            try
            {
                // parse by name of enum
                // like bishop, king, normalpawn etc.
                if (type.Length > 1)
                {
                    return (ChessPawnType)Enum.Parse(typeof(ChessPawnType), type, true);
                }

                // parse by value of enum
                // like 'p', 'q', 'k' etc.
                var suspectedType = (ChessPawnType)(type[0]);

                if (!Enum.IsDefined(typeof(ChessPawnType), suspectedType))
                {
                    throw new Exception();
                }
                
                return suspectedType;
            }
            catch
            {
                String err = $"Pawn's type [{type}] is uknown.\n";
                err += "Availavle types for inupt: p, r, n, b, q, k\n";
                err += " or NormalPawn, Rook, Knight, Bishop, Queen, King";

                throw new ArgumentOutOfRangeException(err);
            }
        }
    }
}