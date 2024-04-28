using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace JocDameMAP_MVVM_Tema2
{
    class CellVM
    {
        CheckersGameLogic cgl;
        public Cell SimpleCell {  get; set; }


        public CellVM(int x, int y, string image, bool hasPiece, CheckersGameLogic cgl)
        {
            if (hasPiece)
                SimpleCell = new Cell(x, y, image, hasPiece);
            else
                SimpleCell = new Cell(x, y);
            this.cgl = cgl;
        }
        public CellVM(int x, int y, string image, SolidColorBrush cellColor, bool hasPiece, CheckersGameLogic cgl)
        {
            if (hasPiece)
            {
                SimpleCell = new Cell(x, y, image, hasPiece);
            }
            else
            {
                SimpleCell = new Cell(x, y);
            }
            SimpleCell.CellColor = cellColor;
            this.cgl = cgl;
        }



        private ICommand clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                if (clickCommand == null)
                {
                    clickCommand = new RelayCommand<Cell>(cgl.ClickAction);
                }
                return clickCommand;
            }
        }





    }
}
