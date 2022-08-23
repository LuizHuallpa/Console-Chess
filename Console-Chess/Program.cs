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

                    try
                    {
                        Console.Clear();
                        View.PrintMatch(match);



                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = View.ReadChessPosition().toPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possiblePosition = match.Bo.Piece(origin).PossibleMovements();

                        Console.Clear();
                        View.PrintBoard(match.Bo, possiblePosition);

                        Console.WriteLine();
                        Console.Write("Destination: ");
                        Position destination = View.ReadChessPosition().toPosition();
                        match.ValidateDestination(origin, destination);


                        match.MakeThePlay(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                View.PrintMatch(match);

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
