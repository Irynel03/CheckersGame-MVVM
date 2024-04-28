using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace JocDameMAP_MVVM_Tema2
{
    public class Checker: Piece
    {
        public override PieceType Type => PieceType.Checker;
        public override Player Color { get; }

        private readonly Direction[] dirs;

        public Checker(Player color)
        {
            Color = color;
            if(color == Player.White)
            {
                dirs =
                [
                    Direction.SouthEast,
                    Direction.SouthWest
                ];
            }
            else 
            {
                dirs =
                [
                    Direction.NorthEast,
                    Direction.NorthWest
                ];
            }
        }
        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;
                if (!Board.IsInside(to))
                {
                    continue;
                }
                if (board.IsEmpty(to))
                {
                    yield return to;
                    
                }
                else if(!board.IsEmpty(to) && board[to].Color != Color && Board.IsInside(to + dir))
                {
                    if (board.IsEmpty(to + dir))
                    {
                        yield return to + dir;
                    }
                }
            }
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach (Position to in MovePositions(from, board))
            {
                yield return new Move(from, to);
            }
        }
        public override Piece Copy()
        {
            Checker copy = new Checker(Color);
            return copy;
        }
    }
}
