using board;
using board.Enum;

namespace chess
{
    class Rook : Piece
    {
        public Rook(Board bo, Colors color) : base(bo, color) { }

        public override string ToString()
        {
            return "R";
        }


    }
}
