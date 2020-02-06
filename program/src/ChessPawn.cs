using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PossiblePawnsMoves
{
    public class ChessPawn
    {
        public char side { get; private set; }

        // (1,1) is down left corner
        // values must be positive
        public (int x, int y) coords { get; private set; }

        public ChessPawnType type { get; private set; }

        public IEnumerable<(int x, int y)> possibleMoves { get; set; }

        public ChessPawn(char _side, (int x, int y) _coords, ChessPawnType _type)
        {
            side = _side;

            // top left corner in chess i 8, A
            // so we need to reverse position Y
            // to get top left corner 1, A
            coords = (x: _coords.x, y: _coords.y);;
            type = _type;
        }

        public override string ToString() 
        {
            var strPawn = new Collection<string>(){
                side.ToString(),
                (coords.x).ToString(),
                (coords.y).ToString(),
                ((char)type).ToString()
            };

            return String.Join(";", strPawn);
        }
    }
}