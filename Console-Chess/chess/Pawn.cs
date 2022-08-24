using board;
using board.Enum;

namespace chess
{
    class Pawn : Piece
    {
        public Pawn(Board bo, Colors color) : base(bo, color) { }

        public override string ToString()
        {
            return "P";
        }


        private bool ExistEnemy(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool IsEmpty(Position pos)
        {
            return Board.Piece(pos) == null;
        }


        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Colors.White)
            {
                pos.DefineValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(pos) && IsEmpty(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(pos) && IsEmpty(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ExistEnemy(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ExistEnemy(pos))
                    mat[pos.Line, pos.Column] = true;

            }
            else
            {
                pos.DefineValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(pos) && IsEmpty(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(pos) && IsEmpty(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ExistEnemy(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ExistEnemy(pos))
                    mat[pos.Line, pos.Column] = true;
            }




            return mat;
        }


    }
}
