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
                ChessMatch match = new ChessMatch();
                while (!match.Finished)
                {
                    Console.Clear();
                    View.printBoard(match.Bo);
                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = View.ReadChessPosition().toPosition();
                    Console.Write("Destination: ");
                    Position destination = View.ReadChessPosition().toPosition();

                    match.ExecuteMovement(origin, destination);
                }

            


                Console.ReadLine();

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
