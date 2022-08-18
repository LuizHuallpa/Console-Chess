using board.Enum;

namespace board
{
    class Piece
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
    }


}
