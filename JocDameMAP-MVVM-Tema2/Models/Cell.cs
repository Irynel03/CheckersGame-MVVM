using System.ComponentModel;
using System.Windows.Media;

namespace JocDameMAP_MVVM_Tema2
{
    public class Cell : BaseNotification, INotifyPropertyChanged
    {
        private int x;
        private int y;
        private string image;
        public bool hasPiece {  get; set; }

        public Cell(int x, int y, Piece piece)  // incercam asa
        {
            this.X = x;
            this.Y = y;
            //this.piece = piece;
        }

        public Cell(int x, int y, string image)
        {
            this.X = x;
            this.Y = y;
            this.Image = image;
        }
        public Cell(int x, int y, string image, bool hasPiece)
        {
            this.X = x;
            this.Y = y;
            this.Image = image;
            this.hasPiece = hasPiece;
        }
        public Cell(int x, int y, string image, bool hasPiece, SolidColorBrush cellColor)
        {
            this.X = x;
            this.Y = y;
            this.Image = image;
            this.hasPiece = hasPiece;
            this.cellColor = cellColor;
        }
        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Image = "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\advTime.png";
            //Image = null;
            this.hasPiece = false;
        }

        /* Am optat sa fac proprietati notificabile aici; o alta varianta ar fi fost sa lucrez in Services cu obiecte ViewModel
        care sunt notificabile, cum am abordat anterior */


        public int X
        {
            get { return x; }
            set
            {
                x = value;
                NotifyPropertyChanged("X");
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                NotifyPropertyChanged("Y");
            }
        }

        public string? Image
        {
            get { return image; }
            set
            {
                image = value;
                NotifyPropertyChanged("Image");
            }
        }
        private SolidColorBrush cellColor = Brushes.Black;

        public SolidColorBrush CellColor
        {
            get { return cellColor; }
            set
            {
                if (cellColor != value)
                {
                    cellColor = value;
                    OnPropertyChanged(nameof(CellColor));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
