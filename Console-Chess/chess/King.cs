using board;
using board.Enum;

namespace chess
{
    class King : Piece
    {
        public King (Board bo, Colors color) : base(bo, color) { }

        public override string ToString()
        {
            return "K";
        }


    }
}
