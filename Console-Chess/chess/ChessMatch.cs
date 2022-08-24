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
        public Piece VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Bo = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Colors.White;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            PutPieces();
            VulnerableEnPassant = null;
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

            //#SPecia Move small castle
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinationR = new Position(origin.Line, origin.Column + 1);
                Piece T = Bo.TakePiece(originR);
                T.IncrementMovement();
                Bo.PutPiece(T, destinationR);
            }

            //#Special Move big castle
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinationR = new Position(origin.Line, origin.Column - 1);
                Piece T = Bo.TakePiece(originR);
                T.IncrementMovement();
                Bo.PutPiece(T, destinationR);
            }

            //#Special move en passant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == null)
                {
                    Position posP;
                    if (p.Color == Colors.White)
                        posP = new Position(destination.Line + 1, destination.Column);
                    else
                        posP = new Position(destination.Line - 1, destination.Column);

                    capturedPiece = Bo.TakePiece(posP);
                    captured.Add(capturedPiece);
                }
            }


            return capturedPiece;
        }

        public void UnmakeTheMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = Bo.TakePiece(destination);
            p.DecreaseMovement();

            if (capturedPiece != null)
            {
                Bo.PutPiece(capturedPiece, destination);
                captured.Remove(capturedPiece);
            }
            Bo.PutPiece(p, origin);


            //#SPecia Move small castle
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Line, origin.Column + 3);
                Position destinationR = new Position(origin.Line, origin.Column + 1);
                Piece T = Bo.TakePiece(destinationR);
                T.DecreaseMovement();
                Bo.PutPiece(T, originR);
            }

            //#SPecia Move big castle
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Line, origin.Column - 4);
                Position destinationR = new Position(origin.Line, origin.Column - 1);
                Piece T = Bo.TakePiece(destinationR);
                T.DecreaseMovement();
                Bo.PutPiece(T, originR);
            }

            //#Special play en passant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Bo.TakePiece(destination);
                    Position posP;
                    if (p.Color == Colors.White)
                    {
                        posP = new Position(3, destination.Column);

                    }
                    else
                    {
                        posP = new Position(4, destination.Column);
                    }

                    Bo.PutPiece(pawn, posP);

                }
            }


        }


        public void MakeThePlay(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMovement(origin, destination);
            Piece p = Bo.Piece(destination);

            if (IsinCheck(CurrentPlayer))
            {
                UnmakeTheMove(origin, destination, capturedPiece);
                throw new BoardException("You cannot put yourself in check");
            }

            


            //#Special play Queening
            if(p is Pawn)
            {
                if (p.Color == Colors.White && destination.Line == 0 || p.Color == Colors.Black && destination.Line == 0)
                {
                    p = Bo.TakePiece(destination);
                    pieces.Remove(p);
                    Piece queen = new Queen(Bo, p.Color);
                    Bo.PutPiece(queen, destination);
                    pieces.Add(queen);
                }
            }


            if (IsinCheck(Adversary(CurrentPlayer)))
                Check = true;
            else
                Check = false;


            if (IsInCheckMate(Adversary(CurrentPlayer)))
                Finished = true;
            else
            {
                Turn++;
                ChangePlayer();
            }

      

            //#Special play Een passant
            if (p is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
                VulnerableEnPassant = p;
            else
                VulnerableEnPassant = null;

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
            if (!Bo.Piece(origin).PossibleMovement(destination))
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

        public bool IsInCheckMate(Colors color)
        {
            if (!IsinCheck(color))
                return false;

            foreach (Piece piece in PiecesInPlay(color))
            {
                bool[,] mat = piece.PossibleMovements();
                for (int i = 0; i < Bo.Lines; i++)
                {
                    for (int j = 0; j < Bo.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMovement(origin, destination);
                            bool isInCheck = IsinCheck(color);
                            UnmakeTheMove(origin, destination, capturedPiece);
                            if (!isInCheck)
                                return false;

                        }
                    }
                }
            }

            return true;
        }

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Bo.PutPiece(piece, new ChessPosition(column, line).toPosition());
            pieces.Add(piece);
        }
        private void PutPieces()
        {


            PutNewPiece('a', 1, new Rook(Bo, Colors.White));
            PutNewPiece('b', 1, new Knight(Bo, Colors.White));
            PutNewPiece('c', 1, new Bishop(Bo, Colors.White));
            PutNewPiece('d', 1, new Queen(Bo, Colors.White));
            PutNewPiece('e', 1, new King(Bo, Colors.White, this));
            PutNewPiece('f', 1, new Bishop(Bo, Colors.White));
            PutNewPiece('g', 1, new Knight(Bo, Colors.White));
            PutNewPiece('h', 1, new Rook(Bo, Colors.White));
            PutNewPiece('a', 2, new Pawn(Bo, Colors.White, this));
            PutNewPiece('b', 2, new Pawn(Bo, Colors.White, this));
            PutNewPiece('c', 2, new Pawn(Bo, Colors.White, this));
            PutNewPiece('d', 2, new Pawn(Bo, Colors.White, this));
            PutNewPiece('e', 2, new Pawn(Bo, Colors.White, this));
            PutNewPiece('f', 2, new Pawn(Bo, Colors.White, this));
            PutNewPiece('g', 2, new Pawn(Bo, Colors.White, this));
            PutNewPiece('h', 2, new Pawn(Bo, Colors.White, this));


            PutNewPiece('a', 8, new Rook(Bo, Colors.Black));
            PutNewPiece('b', 8, new Knight(Bo, Colors.Black));
            PutNewPiece('c', 8, new Bishop(Bo, Colors.Black));
            PutNewPiece('d', 8, new Queen(Bo, Colors.Black));
            PutNewPiece('e', 8, new King(Bo, Colors.Black, this));
            PutNewPiece('f', 8, new Bishop(Bo, Colors.Black));
            PutNewPiece('g', 8, new Knight(Bo, Colors.Black));
            PutNewPiece('h', 8, new Rook(Bo, Colors.Black));
            PutNewPiece('a', 7, new Pawn(Bo, Colors.Black, this));
            PutNewPiece('b', 7, new Pawn(Bo, Colors.Black, this));
            PutNewPiece('c', 7, new Pawn(Bo, Colors.Black, this));
            PutNewPiece('d', 7, new Pawn(Bo, Colors.Black, this));
            PutNewPiece('e', 7, new Pawn(Bo, Colors.Black, this));
            PutNewPiece('f', 7, new Pawn(Bo, Colors.Black, this));
            PutNewPiece('g', 7, new Pawn(Bo, Colors.Black, this));
            PutNewPiece('h', 7, new Pawn(Bo, Colors.Black, this));






        }

    }
}
