using board.Enum;

namespace board
{
    class Piece
    {
        public Position Position { get; set; }
        public  Colors Color { get; set; }
        public int Moves { get; set; }
        public Board Tab { get; set; }

        public Piece(Position position, Colors color, int moves, Board tab)
        {
            Position = position;
            Color = color;
            Moves = 0;
            Tab = tab;
        }
    }


}
