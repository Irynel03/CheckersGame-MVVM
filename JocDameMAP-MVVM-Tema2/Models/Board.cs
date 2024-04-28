using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JocDameMAP_MVVM_Tema2
{
    public class Board
    {
        public readonly Piece[,] pieces = new Piece[8, 8];

        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }
        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }
        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPieces();
            return board;
        }
        public static Board InitialLoad()
        {
            Board board = new Board();
            board.AddStartPiecesLoad();
            return board;
        }
        private void AddStartPiecesLoad()
        {
            string filePath = "C:/Users/andre/Desktop/sem II/MAP/JocDameMAP-MVVM-Tema2/JocDameMAP-MVVM-Tema2/Models/Data/LoadGamesData.json";


            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<GameData> gameDataList = JsonConvert.DeserializeObject<List<GameData>>(json);
                List<List<int>> matrix;
                string currentToMove;
                foreach (var gameData in gameDataList)
                {
                    matrix = gameData.matrix;
                    currentToMove = gameData.currentToMove;

                    for (int r = 0; r < 8; r++)
                    {
                        for (int c = 0; c < 8; c++)
                        {
                            if (matrix[r][c] == 1)
                            {
                                pieces[r, c] = new Checker(Player.White); // Sau Player.Red, în funcție de necesități
                            } else if (matrix[r][c] == 2)
                            {
                                pieces[r, c] = new King(Player.White);
                            } else if (matrix[r][c] == 3)
                            {
                                pieces[r, c] = new Checker(Player.Red);
                            }
                            else if (matrix[r][c] == 4)
                            {
                                pieces[r, c] = new King(Player.Red);
                            }



                        }
                    }
                }
                //List<List<int>> matrix = gameData.matrix;
                //string currentToMove = gameData.currentToMove;
            }
        }
        private void AddStartPieces()
        {
            this[0, 1] = new Checker(Player.White);
            this[0, 3] = new Checker(Player.White);
            this[0, 5] = new Checker(Player.White);
            this[0, 7] = new Checker(Player.White);
            this[1, 0] = new Checker(Player.White);
            this[1, 2] = new Checker(Player.White);
            this[1, 4] = new Checker(Player.White);
            this[1, 6] = new Checker(Player.White);
            this[2, 1] = new Checker(Player.White);
            this[2, 3] = new Checker(Player.White);
            this[2, 5] = new Checker(Player.White);
            pieces[2, 7] = new Checker(Player.White);


            this[5, 0] = new Checker(Player.Red);
            this[5, 2] = new Checker(Player.Red);
            this[5, 4] = new Checker(Player.Red);
            this[5, 6] = new Checker(Player.Red);
            this[6, 1] = new Checker(Player.Red);
            this[6, 3] = new Checker(Player.Red);
            this[6, 5] = new Checker(Player.Red);
            this[6, 7] = new Checker(Player.Red);
            this[7, 0] = new Checker(Player.Red);
            this[7, 2] = new Checker(Player.Red);
            pieces[7, 4] = new Checker(Player.Red);
            pieces[7, 6] = new Checker(Player.Red);
        }

        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Column >= 0 && pos.Row <8 && pos.Column < 8;
        }
        public IEnumerable<Position> PiecePositions()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Position pos = new Position(r, c);
                    if (!IsEmpty(pos))
                    {
                        yield return pos;
                    }
                }
            }
        }
        public Board Copy()
        {
            Board copy = new Board();

            foreach (Position pos in PiecePositions())
            {
                copy[pos] = this[pos].Copy();
            }
            return copy;
        }

        public bool IsEmpty(Position pos)
        {
            if (this[pos] == null)
                return this[pos] == null;
            else 
                return false;
        }
        public bool AreAllPiecesOneColor(Player color)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = pieces[r, c];
                    if (piece != null && piece.Color != color)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public int CountPiecesByColor(Player color)
        {
            int count = 0;
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = pieces[r, c];
                    if (piece != null && piece.Color == color)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public Piece GetPieceAtPosition(Position pos)
        {
            return pieces[pos.Row, pos.Column];
        }

    }
}
