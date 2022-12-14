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


        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != this.Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //above
            pos.DefineValues(Position.Line -1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Line = pos.Line - 1;
            }


            //Below
            pos.DefineValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Line = pos.Line + 1;
            }

            //Right
            pos.DefineValues(Position.Line , Position.Column +1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Column = pos.Column + 1;
            }

            //Right
            pos.DefineValues(Position.Line, Position.Column -1 );
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Column = pos.Column - 1;
            }

            return mat;
        }



    }
}
