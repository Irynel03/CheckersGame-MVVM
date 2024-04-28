using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JocDameMAP_MVVM_Tema2
{
    public class GameState: INotifyPropertyChanged
    {
        CheckersGameVM cgvm {  get; set; }

        public Board Board { get; }
        public Player currentPlayer;
        public Result Result { get; private set; } = null;
        public string NumeJocSalvat { get; set; } = "";
        public bool canMultipleJump { get; set; }

        public GameState()
        {
        }
        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            set
            {
                if (currentPlayer != value)
                {
                    currentPlayer = value;
                    OnPropertyChanged("CurrentPlayer");
                }
            }
        }
        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
        }
        public GameState(Player player, Board board, bool isCheckedMultipleJumps)
        {
            CurrentPlayer = player;
            Board = board;
            canMultipleJump = isCheckedMultipleJumps;
        }
        public GameState(Player player, Board board, string name, bool isCheckedMultipleJumps)
        {
            CurrentPlayer = player;
            Board = board;
            NumeJocSalvat = name;
            canMultipleJump = isCheckedMultipleJumps;
        }
        public void CurrentPlayerSwitch()
        {
            if (CurrentPlayer == Player.White)
                CurrentPlayer = Player.Red;
            else
                CurrentPlayer = Player.White;
        }
        public void MakeMove(Move move)
        {
            move.Execute(Board, canMultipleJump);
            ChechForGameOver();
        }

        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => move.IsLegal(Board, canMultipleJump));
        }
        //public IEnumerable<Move> LegalMovesForPiece(Cell cell)
        //{
        //    Position pos = new Position(cell.X, cell.Y);
        //    if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
        //    {
        //        return Enumerable.Empty<Move>();
        //    }

        //    Piece piece = Board[pos];
        //    IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
        //    return moveCandidates.Where(move => move.IsLegal(Board, canMultipleJump));
        //}


        private void ChechForGameOver()
        {
            bool whiteWon = Board.AreAllPiecesOneColor(Player.White);
            if(whiteWon)
            {
                Result = new Result(Player.White);
            }
            else
            {
                bool redWon = Board.AreAllPiecesOneColor(Player.Red);
                if(redWon)
                {
                    Result = new Result(Player.Red);
                }
            }
        }
        public bool IsGameOver()
        {
            if (Result != null)
            {
                string filePath = "C:/Users/andre/Desktop/sem II/MAP/JocDameMAP-MVVM-Tema2/JocDameMAP-MVVM-Tema2/Models/Data/Statistics.json";

                string json = File.ReadAllText(filePath);
                Statistics stats = JsonConvert.DeserializeObject<Statistics>(json);

                if(Result.Winner == Player.Red)
                {
                    stats.redWins++;
                    int winDiff = Board.CountPiecesByColor(Player.Red);
                    if (stats.biggestWinDiff < winDiff)
                        stats.biggestWinDiff = winDiff;
                }
                else
                {
                    stats.whiteWins++;
                    int winDiff = Board.CountPiecesByColor(Player.White);
                    if (stats.biggestWinDiff < winDiff)
                        stats.biggestWinDiff = winDiff;
                }
                string updatedJsonStats = JsonConvert.SerializeObject(stats);
                File.WriteAllText(filePath, updatedJsonStats);

            }
            return Result != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
