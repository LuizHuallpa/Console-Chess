using board;
using board.Enum;

namespace chess
{
    class King : Piece
    {

        private ChessMatch Match;
        public King(Board bo, Colors color, ChessMatch match) : base(bo, color)
        {
            Match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != this.Color;
        }

        private bool CanCastleTest(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p is Rook && p.Color == Color && p.Moves == 0;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //above
            pos.DefineValues(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //NE
            pos.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //Right
            pos.DefineValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //SE
            pos.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //Below
            pos.DefineValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //SW
            pos.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //Left
            pos.DefineValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //NW
            pos.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;


            //##Special Move Castling

            if (Moves == 0 && !Match.Check)
            {
                //small castle
                Position posT1 = new Position(Position.Line, Position.Column + 3);
                if (CanCastleTest(posT1))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if(Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }
                //Big castle
                Position posT2 = new Position(Position.Line, Position.Column - 4);
                if (CanCastleTest(posT2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }


            }

                return mat;
        }


    }
}
