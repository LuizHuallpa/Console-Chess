using board;
using board.Enum;

namespace chess
{
    class Knight : Piece
    {
        public Knight(Board bo, Colors color) : base(bo, color) { }

        public override string ToString()
        {
            return "k";
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

            //1
            pos.DefineValues(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //2
            pos.DefineValues(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //3
            pos.DefineValues(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //4
            pos.DefineValues(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //5
            pos.DefineValues(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //6
            pos.DefineValues(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //7
            pos.DefineValues(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //8
            pos.DefineValues(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            return mat;
        }



    }
}
