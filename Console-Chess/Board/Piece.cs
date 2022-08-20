using board.Enum;

namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Colors Color { get; set; }
        public int Moves { get; set; }
        public Board Board { get; set; }

        public Piece(Board bo, Colors color)
        {
            Position = null;
            Color = color;
            Moves = 0;
            Board = bo;
        }


        public void IncrementMovement()
        {
            Moves++;
        }

        public bool CanMove()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                        return true;

                }
            }

            return false;
        }


        public bool CanMoveTo(Position pos)
        {
            return PossibleMovements()[pos.Line, pos.Column];
        }

        public abstract bool[,] PossibleMovements();


    }


}
