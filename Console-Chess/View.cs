using System;
using System.Collections.Generic;
using board;
using board.Enum;
using chess;

namespace Console_Chess
{
    class View
    {

        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Bo);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine($"Turn {match.Turn}");
            if (!match.Finished)
            {
                Console.WriteLine($"Waiting for the play: {match.CurrentPlayer}");
                if (match.Check)
                {
                    Console.WriteLine("CHECK!!!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!!!!");
                Console.WriteLine($"WINNER {match.CurrentPlayer}");
            }
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine($"Captured pieces");
            Console.Write("White: ");
            PrintGroup(match.CapturedPieces(Colors.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            PrintGroup(match.CapturedPieces(Colors.Black));
            Console.ForegroundColor = aux;
           Console.WriteLine();
            Console.WriteLine();
        }
        
        public static void PrintGroup(HashSet<Piece> group)
        {
            Console.Write("[");
            foreach(Piece piece in group)
            {
                Console.Write($"{piece} ");
            }
            Console.Write("]");
        }


        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void PrintBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor background = Console.BackgroundColor;
            ConsoleColor alteredBackground = ConsoleColor.DarkGray;


            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {

                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = alteredBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = background;
                    }

                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = background;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = background;
        }


        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void PrintPiece(Piece p)
        {
            if (p == null)
                Console.Write("- ");
            else
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
                Console.Write(" ");
            }

        }
    }
}
