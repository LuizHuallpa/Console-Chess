using board;
using board.Enum;

namespace chess
{
    class Pawn : Piece
    {
        private ChessMatch Match;
        public Pawn(Board bo, Colors color, ChessMatch match) : base(bo, color)
        {
            Match = match;
        }

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

                //#Special play en passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && ExistEnemy(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                        mat[left.Line - 1, left.Column] = true;

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && ExistEnemy(right) && Board.Piece(right) == Match.VulnerableEnPassant)
                        mat[right.Line - 1, right.Column] = true;
                }

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
                    mat[pos.Line + 1, pos.Column] = true;

                //#Special play en passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && ExistEnemy(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                        mat[left.Line + 1, left.Column] = true;

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && ExistEnemy(right) && Board.Piece(right) == Match.VulnerableEnPassant)
                        mat[right.Line + 1, right.Column] = true;
                }

            }

            return mat;
        }


    }
}
