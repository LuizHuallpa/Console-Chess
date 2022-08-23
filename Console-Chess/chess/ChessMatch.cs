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
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Bo = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Colors.White;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            PutPieces();
        }

        public Piece ExecuteMovement(Position origin, Position destination)
        {
            Piece p = Bo.TakePiece(origin);
            p.IncrementMovement();
            Piece capturedPiece = Bo.TakePiece(destination);
            Finished = false;
            Bo.PutPiece(p, destination);
            if (capturedPiece != null)
                captured.Add(capturedPiece);

            return capturedPiece;
        }

        public void UnmakeTheMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = Bo.TakePiece(destination);
            p.DecreaseMovement();

            if(capturedPiece != null)
            {
                Bo.PutPiece(capturedPiece, destination);
                captured.Remove(capturedPiece);
            }
            Bo.PutPiece(p, origin);

        }


        public void MakeThePlay(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMovement(origin, destination);

            if (IsinCheck(CurrentPlayer))
            {
                UnmakeTheMove(origin, destination, capturedPiece);
                throw new BoardException("You cannot put yourself in check");
            }

            if (IsinCheck(Adversary(CurrentPlayer)))
                Check = true;
            else
                Check = false;

            Turn++;
            ChangePlayer();
        }


        public void ValidateOriginPosition(Position pos)
        {
            if (Bo.Piece(pos) == null)
                throw new BoardException("There is no piece in the chosen position");
            if (CurrentPlayer != Bo.Piece(pos).Color)
                throw new BoardException("The chosen piece is not yours");
            if (!Bo.Piece(pos).CanMove())
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

        public HashSet<Piece> CapturedPieces(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in captured)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }
            return aux;
        }

        public HashSet<Piece> PiecesInPlay(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in pieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;

        }

        private Colors Adversary(Colors color)
        {
            if (color == Colors.White)
                return Colors.Black;
            else
                return Colors.White;
        }

        private Piece King(Colors color)
        {
            foreach (Piece piece in PiecesInPlay(color))
            {
                if (piece is King)
                    return piece;
            }
            return null;
        }

        public bool IsinCheck(Colors color)
        {
            foreach (Piece piece in PiecesInPlay(Adversary(color)))
            {
                Piece k = King(color);
                if (k == null)
                    throw new BoardException($"There is no King {color} in the board!");

                bool[,] mat = piece.PossibleMovements();
                if (mat[k.Position.Line, k.Position.Column])
                    return true;

            }

            return false;
        }

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Bo.PutPiece(piece, new ChessPosition(column, line).toPosition());
            pieces.Add(piece);
        }
        private void PutPieces()
        {

            PutNewPiece('c', 1, new Rook(Bo, Colors.White));
            PutNewPiece('c', 2, new Rook(Bo, Colors.White));
            PutNewPiece('d', 2, new Rook(Bo, Colors.White));
            PutNewPiece('e', 2, new Rook(Bo, Colors.White));
            PutNewPiece('e', 1, new Rook(Bo, Colors.White));
            PutNewPiece('d', 1, new King(Bo, Colors.White));

            PutNewPiece('c', 7, new Rook(Bo, Colors.Black));
            PutNewPiece('c', 8, new Rook(Bo, Colors.Black));
            PutNewPiece('d', 7, new Rook(Bo, Colors.Black));
            PutNewPiece('e', 7, new Rook(Bo, Colors.Black));
            PutNewPiece('e', 8, new Rook(Bo, Colors.Black));
            PutNewPiece('d', 8, new King(Bo, Colors.Black));



        }

    }
}
