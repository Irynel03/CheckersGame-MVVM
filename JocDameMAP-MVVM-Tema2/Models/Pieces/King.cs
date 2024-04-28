using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace JocDameMAP_MVVM_Tema2
{
    public class King: Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }
        private readonly Direction[] dirs = new Direction[]
        {
            Direction.NorthEast,
            Direction.NorthWest,
            Direction.SouthEast,
            Direction.SouthWest
        };

        public King(Player color)
        {
            Color = color;
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
                else if (!board.IsEmpty(to) && board[to].Color != Color && Board.IsInside(to + dir))
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
            King copy = new King(Color);
            return copy;
        }
    }
}
