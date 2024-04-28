using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace JocDameMAP_MVVM_Tema2
{
    public class SavedGamesVM: INotifyPropertyChanged
    {
        CheckersGameLogic cgl {  get; set; }

        public ObservableCollection<GameState> SavedGamesList {  get; set; } = new ObservableCollection<GameState>();
        public SavedGamesVM()
        {
            SavedGamesList = SavedGamesManagement.LoadSavedGames();
            DeleteGameCommand = new RelayCommand<GameState>(DeleteGame);
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private ICommand enterGameCommand;
        public ICommand EnterGameCommand
        {
            get
            {
                if (enterGameCommand == null)
                {
                    enterGameCommand = new RelayCommand<Cell>(cgl.ClickAction);
                }
                return enterGameCommand;
            }
        }
        public ICommand DeleteGameCommand { get; }
        private void DeleteGame(object game)
        {
            if (game is GameState gameStateToDel)
            {
                for (int i = SavedGamesList.Count - 1; i >= 0; i--)
                {
                    if (SavedGamesList[i].NumeJocSalvat == gameStateToDel.NumeJocSalvat)
                    {
                        SavedGamesList.RemoveAt(i);
                        break;
                    }
                }
            }

            List<GameData> gameDataList = new List<GameData>();
            foreach (var gameState in SavedGamesList)
            {
                GameData gameData = new GameData();
                gameData.matrix = new List<List<int>>();
                for (int i = 0; i < 8; i++)
                {
                    List<int> row = new List<int>();
                    for (int j = 0; j < 8; j++)
                    {
                        Piece piece = gameState.Board.GetPieceAtPosition(new Position(i, j));
                        if (piece == null)
                        {
                            row.Add(0); // Celula este goală
                        }
                        else
                        {
                            if (piece.Color == Player.White)
                            {
                                if (piece.Type == PieceType.Checker)
                                    row.Add(1); // Piesă albă obișnuită
                                else
                                    row.Add(2); // Piesă albă rege
                            }
                            else
                            {
                                if (piece.Type == PieceType.Checker)
                                    row.Add(3); // Piesă roșie obișnuită
                                else
                                    row.Add(4); // Piesă roșie rege
                            }
                        }
                    }
                    gameData.matrix.Add(row);
                }
                gameData.currentToMove = gameState.CurrentPlayer.ToString();
                gameData.name = gameState.NumeJocSalvat;
                gameData.canMultipleJump = gameState.canMultipleJump;

                gameDataList.Add(gameData);
            }

            string json = JsonConvert.SerializeObject(gameDataList, Formatting.Indented);

            string filePath = "C:/Users/andre/Desktop/sem II/MAP/JocDameMAP-MVVM-Tema2/JocDameMAP-MVVM-Tema2/Models/Data/LoadGamesData.json";
            File.WriteAllText(filePath, json);
        }



    }
}