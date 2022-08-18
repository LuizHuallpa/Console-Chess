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
            Board board = new Board(8, 8);


            board.putPiece(new Rook(board, Colors.Black), new Position(0, 0));
            board.putPiece(new Rook(board, Colors.Black), new Position(1, 3));
            board.putPiece(new King(board, Colors.Black), new Position(2, 4));


            View.printBoard(board);
            Console.ReadLine();

        }
    }
}
