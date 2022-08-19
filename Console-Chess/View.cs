using System;
using System.Collections.Generic;
using board;
using board.Enum;
using chess;

namespace Console_Chess
{
    class View
    {
        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.Piece(i, j) == null)
                        Console.Write("- ");
                    else
                    {
                        View.printPiece(board.Piece(i, j));
                        Console.Write(" ");
                    }
                       
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void printPiece(Piece p)
        {
            if (p.Color == Colors.White)
                Console.Write(p);
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(p);
                Console.ForegroundColor = aux;
            }


        }
    }
}
