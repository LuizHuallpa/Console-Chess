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
        public int Turn { get; private set; }
        public Colors CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Bo = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Colors.White;
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



        public void MakeThePlay(Position origin, Position destination)
        {
            ExecuteMovement(origin, destination);
            Turn++;
            ChangePlayer();
        }


        public void ValidateOriginPosition(Position pos)
        {
            if (Bo.Piece(pos) == null)
                throw new BoardException("There is no piece in the chosen position");
            if(CurrentPlayer != Bo.Piece(pos).Color)
                throw new BoardException("The chosen piece is not yours");
            if(!Bo.Piece(pos).CanMove())
                throw new BoardException("There are no possible movements for this piece");
        }

        public void ValidateDestination(Position origin, Position destination)
        {
            if (!Bo.Piece(origin).CanMoveTo(destination))
                throw new BoardException("Destination is not valid");
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Colors.White)
                CurrentPlayer = Colors.Black;
            else
                CurrentPlayer = Colors.White;
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
