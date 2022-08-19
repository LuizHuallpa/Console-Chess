using System;
using System.Collections.Generic;
using System.Text;

namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }

        private Piece[,] Pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[Lines, Columns];
        }

        public Piece Piece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece Piece(Position pos)
        {
            return Pieces[pos.Line, pos.Column];
        }

        public bool PieceExists(Position pos)
        {
            ValidatePosition(pos);
            return Piece(pos) != null;
        }

        public void PutPiece(Piece p, Position pos)
        {
            if (PieceExists(pos))
                throw new BoardException("There is already a piece at that position");
            Pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public Piece TakePiece(Position pos)
        {
            if (Piece(pos) == null)
                return null;

            Piece aux = Piece(pos);
            aux.Position = null;
            Pieces[pos.Line, pos.Column] = null;
            return aux;
        }



        public bool ValidPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Columns)
                return false;

            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
                throw new BoardException("This position is invalid");
        }

    }
}
