using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocDameMAP_MVVM_Tema2
{
    class SavedGamesManagement
    {
        public ObservableCollection<GameState> SavedGamesList { get; set; } = new ObservableCollection<GameState>();

        public SavedGamesManagement(ObservableCollection<GameState> SavedGamesList)
        {
            this.SavedGamesList = SavedGamesList;
        }

        public static ObservableCollection<GameState> LoadSavedGames()
        {
            ObservableCollection<GameState> gameStates = new ObservableCollection<GameState>();
            string filePath = "C:/Users/andre/Desktop/sem II/MAP/JocDameMAP-MVVM-Tema2/JocDameMAP-MVVM-Tema2/Models/Data/LoadGamesData.json";

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<GameData> gameDataList = JsonConvert.DeserializeObject<List<GameData>>(json);
                List<List<int>> matrix;
                string currentToMove;
                foreach (var gameData in gameDataList)
                {
                    Board board = new Board();
                    matrix = gameData.matrix;
                    currentToMove = gameData.currentToMove;

                    for (int r = 0; r < 8; r++)
                    {
                        for (int c = 0; c < 8; c++)
                        {
                            if (matrix[r][c] == 1)
                            {
                                board.pieces[r, c] = new Checker(Player.White); // Sau Player.Red, în funcție de necesități
                            }
                            else if (matrix[r][c] == 2)
                            {
                                board.pieces[r, c] = new King(Player.White);
                            }
                            else if (matrix[r][c] == 3)
                            {
                                board.pieces[r, c] = new Checker(Player.Red);
                            }
                            else if (matrix[r][c] == 4)
                            {
                                board.pieces[r, c] = new King(Player.Red);
                            }
                        }
                    }
                    bool canMultipleJump = (gameData.canMultipleJump == true);
                    Player currentPlayer = new Player();
                    if (currentToMove == "White")
                        currentPlayer = Player.White;
                    else
                        currentPlayer = Player.Red;
                    GameState gameState = new GameState(currentPlayer, board, gameData.name, canMultipleJump);
                    gameStates.Add(gameState);
                }
            }
            return gameStates;
        }
    }
}
