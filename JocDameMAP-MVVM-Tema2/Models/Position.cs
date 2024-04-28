using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocDameMAP_MVVM_Tema2
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; } 

        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
        public Player SquareColor()
        {
            if((Row + Column)%2 == 0)
            {
                return Player.Red;
            }
            else
            {
                return Player.White;
            }
        }

        public static Position operator+(Position pos, Direction dir)
        {
            return new Position(pos.Row + dir.RowDelta, pos.Column + dir.ColumnDelta);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }
    }
}
