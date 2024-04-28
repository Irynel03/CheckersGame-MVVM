using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocDameMAP_MVVM_Tema2
{
    public class Move
    {
        public bool Jump { get; private set; } = false;
        public bool canJump { get; private set; } = false;
        public bool Jumped2Times { get; private set; } = false;
        public Position FromPos { get; }
        public Position ToPos { get; }

        public Move(Position fromPos, Position toPos)
        {
            FromPos = fromPos;
            ToPos = toPos;
        }
        public void Execute(Board board, bool multipleJump)
        {
            Piece piece = board[FromPos];
            if (piece != null)
            {
                if (piece.Type != PieceType.King)
                {
                    bool toKing = CheckForUpgrade(ToPos, piece.Color);
                    if (toKing)
                    {
                        board[ToPos] = new King(piece.Color);
                    }
                    else
                    {
                        board[ToPos] = piece;
                    }
                }
                else
                {
                    board[ToPos] = piece;
                }
                board[FromPos] = null;
                Position middlePos = new Position((FromPos.Row + ToPos.Row) / 2, (FromPos.Column + ToPos.Column) / 2);
                if (Board.IsInside(middlePos) && board[middlePos] != null && board[middlePos].Color != piece.Color)
                {
                    if (canJump && !Jumped2Times)
                    {
                        if(Jump)
                        {
                            Jumped2Times = true;
                        }
                        Jump = true;
                    }
                    if(multipleJump)
                        canJump = true;
                    board[middlePos] = null;
                }
            }
        }
        public bool CheckForUpgrade(Position pos, Player color)
        {
            if(color == Player.White)
            {
                if(pos.Row == 7 && pos.Column % 2 == 0)
                {
                    return true;
                }
            }
            else
            {
                if(pos.Row == 0 && pos.Column % 2 == 1) 
                {
                    return true;
                }
            }
            return false;
        }
        public virtual bool IsLegal(Board board, bool multipleJump) // probabil de verificat niste lucruri
        {
            Board boardCopy = board.Copy();
            Execute(boardCopy, multipleJump);
            return true;
        } 
    }
}
