using System;
using board;
using board.Enum;
using chess;


namespace Console_Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Board board = new Board(8, 8);


                //board.PutPiece(new Rook(board, Colors.Black), new Position(0, 0));
                //board.PutPiece(new King(board, Colors.Black), new Position(0, 14));
                //board.PutPiece(new Rook(board, Colors.Black), new Position(1, 3));
                //board.PutPiece(new King(board, Colors.Black), new Position(2, 4));


                //View.printBoard(board);
                //Console.ReadLine();

                ChessPosition pos = new ChessPosition('c',7);
                Console.WriteLine(pos);

                Console.WriteLine(pos.toPosition());

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
