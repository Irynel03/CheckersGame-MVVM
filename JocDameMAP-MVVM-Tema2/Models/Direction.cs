using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JocDameMAP_MVVM_Tema2
{
    public class Direction
    {
        public readonly static Direction NorthEast = new Direction(-1, 1);
        public readonly static Direction NorthWest = new Direction(-1, -1);
        public readonly static Direction SouthEast = new Direction(1, 1);
        public readonly static Direction SouthWest = new Direction(1, -1);

        public int RowDelta { get; }
        public int ColumnDelta { get; }
        
        public Direction(int rowDelta, int columnDelta)
        {
            RowDelta = rowDelta;
            ColumnDelta = columnDelta;
        }
        public static Direction operator +(Direction dir1, Direction dir2)
        {
            return new Direction(dir1.RowDelta + dir2.RowDelta, dir1.ColumnDelta + dir2.ColumnDelta);
        }

    }
}
