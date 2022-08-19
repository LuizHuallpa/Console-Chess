using System;
using System.Collections.Generic;
using System.Text;
using board;
using board.Enum;

namespace chess
{
    class ChessMatch
    {
        public Board Bo { get; private set; }
        private int Turn;
        private Colors Player;
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Bo = new Board(8, 8);
            Turn = 1;
            Player = Colors.White;
            PutPieces();
        }

        public void ExecuteMovement(Position origin, Position destination)
        {
            Piece p = Bo.TakePiece(origin);
            p.IncrementMovement();
            Piece capturedPiece = Bo.TakePiece(destination);
            Finished = false;
            Bo.PutPiece(p, destination);
        }

        private void PutPieces()
        {
            Bo.PutPiece(new Rook(Bo, Colors.White), new ChessPosition('c', 1).toPosition());
            Bo.PutPiece(new Rook(Bo, Colors.White), new ChessPosition('c', 2).toPosition());
            Bo.PutPiece(new Rook(Bo, Colors.White), new ChessPosition('d', 2).toPosition());
            Bo.PutPiece(new Rook(Bo, Colors.White), new ChessPosition('e', 2).toPosition());
            Bo.PutPiece(new Rook(Bo, Colors.White), new ChessPosition('e', 1).toPosition());
            Bo.PutPiece(new King(Bo, Colors.White), new ChessPosition('d', 1).toPosition());

            Bo.PutPiece(new Rook(Bo, Colors.Black), new ChessPosition('c', 7).toPosition());
            Bo.PutPiece(new Rook(Bo, Colors.Black), new ChessPosition('c', 8).toPosition());
            Bo.PutPiece(new Rook(Bo, Colors.Black), new ChessPosition('d', 7).toPosition());
            Bo.PutPiece(new Rook(Bo, Colors.Black), new ChessPosition('e', 7).toPosition());
            Bo.PutPiece(new Rook(Bo, Colors.Black), new ChessPosition('e', 8).toPosition());
            Bo.PutPiece(new King(Bo, Colors.Black), new ChessPosition('d', 8).toPosition());


        }

    }
}
