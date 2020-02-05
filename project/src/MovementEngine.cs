using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace PossiblePawnsMoves
{
    // i used a normal class instead of static class 
    // because of problem with reflection
    public class MovementEngine
    {
        public ChessPawn GetPossibleMoves(ChessPawn pawn)
        {
            if (pawn.type == ChessPawnType.NormalPawn && pawn.side == 'w')
            {
                pawn.possibleMoves = NormalWhitePawnMoves(pawn.coords);

                return pawn;
            }

            string methodName = pawn.type + "Moves";

            // get method by using reflection
            var method = typeof(MovementEngine).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

            // invoke method, then assign results into current pawn
            pawn.possibleMoves = (IEnumerable<(int x, int y)>)method.Invoke(this, new object[] { pawn.coords });
            
            return pawn;
        }

        // Normal pawn can move only 1 field ahead
        private ICollection<(int x, int y)> NormalPawnMoves((int x, int y) pawnCoords)
        {
            if (pawnCoords.y < 8)
            {
                return new Collection<(int x, int y)> { (x: pawnCoords.x, y: (pawnCoords.y + 1)) };
            }

            return new Collection<(int x, int y)>();
        }

        // Normal pawn can move only 1 field ahead
        private ICollection<(int x, int y)> NormalWhitePawnMoves((int x, int y) pawnCoords)
        {
            if (pawnCoords.y > 1)
            {
                return new Collection<(int x, int y)> { (x: pawnCoords.x, y: (pawnCoords.y - 1)) };
            }

            return new Collection<(int x, int y)>();
        }

        // Rook moves only in 4 directions (top, right, down, left)
        private ICollection<(int x, int y)> RookMoves((int x, int y) pawnCoords)
        {
            var possibleMoves = new Collection<(int x, int y)>();

            // every x position at pawn's y position
            // left <-> right
            for (int possibleX = 1; possibleX <= 8; possibleX++)
            {
                if (possibleX != pawnCoords.x)
                {
                    possibleMoves.Add((x: possibleX, y: pawnCoords.y));
                }
            }

            // every y position at pawn's x position
            // top <-> down
            for (int possibleY = 1; possibleY <= 8; possibleY++)
            {
                if (possibleY != pawnCoords.y)
                {
                    possibleMoves.Add((x: pawnCoords.x, y: possibleY));
                }
            }

            return possibleMoves;
        }

        // every possibly rotation of 'L' letter
        private ICollection<(int x, int y)> KnightMoves((int x, int y) pawnCoords)
        {
            var possibleMoves = new Collection<(int x, int y)>();

            // o: start, x: route, e: end

            // e
            // x
            // x o
            if (pawnCoords.x > 1 && pawnCoords.y < 7)
            {
                possibleMoves.Add((x: pawnCoords.x - 1, y: pawnCoords.y + 2));
            }

            //   e
            //   x
            // o x
            if (pawnCoords.x < 8 && pawnCoords.y < 7)
            {
                possibleMoves.Add((x: pawnCoords.x + 1, y: pawnCoords.y + 2));
            }

            // e x x
            //     o
            if (pawnCoords.x > 2 && pawnCoords.y < 8)
            {
                possibleMoves.Add((x: pawnCoords.x - 2, y: pawnCoords.y + 1));
            }

            // x x e
            // o
            if (pawnCoords.x < 7 && pawnCoords.y < 8)
            {
                possibleMoves.Add((x: pawnCoords.x + 2, y: pawnCoords.y + 1));
            }

            // x o
            // x
            // e
            if (pawnCoords.x > 1 && pawnCoords.y > 2)
            {
                possibleMoves.Add((x: pawnCoords.x - 1, y: pawnCoords.y - 2));
            }

            // o x
            //   x
            //   e
            if (pawnCoords.x < 8 && pawnCoords.y > 2)
            {
                possibleMoves.Add((x: pawnCoords.x + 1, y: pawnCoords.y - 2));
            }

            //     o
            // e x x
            if (pawnCoords.x > 2 && pawnCoords.y > 8)
            {
                possibleMoves.Add((x: pawnCoords.x - 2, y: pawnCoords.y - 1));
            }

            // o
            // x x e
            if (pawnCoords.x < 7 && pawnCoords.y > 1)
            {
                possibleMoves.Add((x: pawnCoords.x + 2, y: pawnCoords.y - 1));
            }

            return possibleMoves;
        }

        // bishop moves diagonally in 4 directions
        private ICollection<(int x, int y)> BishopMoves((int x, int y) pawnCoords)
        {
            var possibleMoves = new Collection<(int x, int y)>();

            // going to top right
            for (int px = pawnCoords.x + 1, py = pawnCoords.y + 1; px <= 8 && py <= 8; px++, py++)
            {
                possibleMoves.Add((x: px, y: py));
            }

            // going to top left
            for (int px = pawnCoords.x - 1, py = pawnCoords.y + 1; px >= 1 && py <= 8; px--, py++)
            {
                possibleMoves.Add((x: px, y: py));
            }

            // going to down left
            for (int px = pawnCoords.x - 1, py = pawnCoords.y - 1; px >= 1 && py >= 1; px--, py--)
            {
                possibleMoves.Add((x: px, y: py));
            }

            // goint to down right
            for (int px = pawnCoords.x + 1, py = pawnCoords.y - 1; px <= 8 && py >= 1; px++, py--)
            {
                possibleMoves.Add((x: px, y: py));
            }

            return possibleMoves;
        }

        // queen moves like rook and bishop combined into one super pawn
        private ICollection<(int x, int y)> QueenMoves((int x, int y) pawnCoords)
        {
            // concat method returns IEnumerable container
            // so we can just return two concatenated methods 
            return BishopMoves(pawnCoords).Concat(RookMoves(pawnCoords)).ToList();
        }

        // king is probably old,
        // so he can moves only one field each direction (8 possible directions)

        // o: start, e: end
        // e e e
        // e o e
        // e e e  
        private ICollection<(int x, int y)> KingMoves((int x, int y) pawnCoords)
        {
            var possibleMoves = new Collection<(int x, int y)>();

            // top
            if (pawnCoords.y < 8)
            {
                possibleMoves.Add((x: pawnCoords.x, y: pawnCoords.y + 1));
            }
            // top-right
            if (pawnCoords.x < 8 && pawnCoords.y < 8)
            {
                possibleMoves.Add((x: pawnCoords.x + 1, y: pawnCoords.y + 1));
            }
            // right
            if (pawnCoords.x < 8)
            {
                possibleMoves.Add((x: pawnCoords.x + 1, y: pawnCoords.y));
            }
            // down-right
            if (pawnCoords.x < 8 && pawnCoords.y > 1)
            {
                possibleMoves.Add((x: pawnCoords.x + 1, y: pawnCoords.y - 1));
            }
            // down
            if (pawnCoords.y > 1)
            {
                possibleMoves.Add((x: pawnCoords.x, y: pawnCoords.y - 1));
            }
            // down-left
            if (pawnCoords.x > 1 && pawnCoords.y > 1)
            {
                possibleMoves.Add((x: pawnCoords.x - 1, y: pawnCoords.y - 1));
            }
            // left
            if (pawnCoords.x > 1)
            {
                possibleMoves.Add((x: pawnCoords.x - 1, y: pawnCoords.y));
            }
            // top-left
            if (pawnCoords.x > 1 && pawnCoords.y < 8)
            {
                possibleMoves.Add((x: pawnCoords.x - 1, y: pawnCoords.y + 1));
            }

            return possibleMoves;
        }
    }
}