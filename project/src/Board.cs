using System.Collections.Generic;

namespace PossiblePawnsMoves
{
    public class Board
    {
        private IList<string> _board;

        public Board()
        {
            _board = new List<string>();

            _board.Add("    A   B   C   D   E   F   G   H");
            _board.Add("  ┌───┬───┬───┬───┬───┬───┬───┬───┐");

            for (int i = 1; i < 8; i++)
            {
                _board.Add("  ├───┼───┼───┼───┼───┼───┼───┼───┤");
            }

            _board.Add("  └───┴───┴───┴───┴───┴───┴───┴───┘");

            for (int i = 8, j = 0; i > 0; i--, j++)
            {
                _board.Insert((j + 1) * 2, $"{i} │   │   │   │   │   │   │   │   │");
            }
        }

        public string GetBoard(ChessPawn pawn)
        {
            // convert enum value (not name like "NormalPawn") to string
            string pawnType = ((char)(pawn.type)).ToString();

            if (pawn.side == 'w')
            {
                pawnType = pawnType.ToUpper();
            }

            // set pawn character
            ReplaceCharInBoard(pawn.coords.x * 4, (9 - pawn.coords.y) * 2, pawnType);
            
            // set posibble pawn moves
            foreach (var move in pawn.possibleMoves)
            {
                ReplaceCharInBoard(move.x * 4, (9 - move.y) * 2, "X");
            }

            return getStringOfBoard();
        }

        private void ReplaceCharInBoard(int x, int y, string character)
        {
            _board[y] = _board[y].Remove(x, 1).Insert(x, character);
        }

        private string getStringOfBoard()
        {
            string temp = "";
            foreach (var line in _board)
            {
                temp += line + "\n";
            }
            return temp + "\n";
        }
    }
}