using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JocDameMAP_MVVM_Tema2
{
    public static class Images
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            {PieceType.Checker, LoadImage("C://Users//andre//Desktop//sem II//MAP//JocDameMAP-MVVM-Tema2//JocDameMAP-MVVM-Tema2//Models//Assets//PawnW.png") },
            {PieceType.King, LoadImage("C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\KingW.png") }
        };
        private static readonly Dictionary<PieceType, ImageSource> redSources = new()
        {
            {PieceType.Checker, LoadImage("C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\PawnB.png") },
            {PieceType.King, LoadImage("C://Users//andre//Desktop//sem II//MAP//JocDameMAP-MVVM-Tema2//JocDameMAP-MVVM-Tema2//Models//Assets//KnightB.png") }
        };


        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Absolute));
        }

        public static ImageSource GetImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.White => whiteSources[type],
                Player.Red => redSources[type],
                _ => throw new Exception()
            };
        }
        public static ImageSource GetImage(Piece piece) // cu sau fara "?"
        {
            if (piece == null)
                return null;
            return GetImage(piece.Color,  piece.Type);
        }


    }
}
