using System.Collections.Generic;

namespace PossiblePawnsMoves
{
    public class PawnComparer : EqualityComparer<ChessPawn>
    {
        public override bool Equals(ChessPawn x, ChessPawn y)
            => x.ToString().Equals(y.ToString());

        //  only above function determinate about equality
        public override int GetHashCode(ChessPawn obj)
            => 0;
    }
}