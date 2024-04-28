using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace JocDameMAP_MVVM_Tema2
{
    class CheckersGameVM
    {
        public CheckersGameLogic cgl {  get; set; }
        public ObservableCollection<ObservableCollection<CellVM>> Pieces { get; set; }


        //          Intrebari
        // buttonul se apasa doar daca apas pe imagine, nu pe toata casuta acestuia
        // probleme la binding cand am o imagine null


        public CheckersGameVM(bool canMultipleJump)
        {
            ObservableCollection<ObservableCollection<Cell>> board = InitGameBoard();
            cgl = new CheckersGameLogic(board, canMultipleJump);

            cgl.RedrawBoardRequested += OnRedrawBoardRequested;
            Pieces = CheckerGameBoardToChechersGameVMBoard(board);
        }
        public CheckersGameVM(GameState gameState)
        {
            ObservableCollection<ObservableCollection<Cell>> board = InitGameBoard2(gameState);
            cgl = new CheckersGameLogic(gameState, board);

            cgl.RedrawBoardRequested += OnRedrawBoardRequested;
            Pieces = CheckerGameBoardToChechersGameVMBoard(board);
        }

        private ObservableCollection<ObservableCollection<CellVM>> CheckerGameBoardToChechersGameVMBoard(ObservableCollection<ObservableCollection<Cell>> board)
        {
            ObservableCollection<ObservableCollection<CellVM>> result = new ObservableCollection<ObservableCollection<CellVM>>();

            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<CellVM> line = new ObservableCollection<CellVM>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    Cell c = board[i][j];

                    CellVM cellVM = new CellVM(c.X, c.Y, c.Image, c.CellColor, c.hasPiece, cgl);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }
        public static ObservableCollection<ObservableCollection<Cell>> InitGameBoard()
        {
            var gameBoard = new ObservableCollection<ObservableCollection<Cell>>();
            Board board = Board.Initial();

            for(int i = 0; i < board.pieces.GetLength(0); i++)
            {
                var row = new ObservableCollection<Cell>();
                for( int j = 0;j<board.pieces.GetLength(1); j++)
                {
                    if (board.pieces[i,j] != null)
                    {
                        if (board.pieces[i, j].Color == Player.White && board.pieces[i, j].Type == PieceType.King)
                        {
                            row.Add(new Cell(i, j, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\KingW.png", true));
                        }
                        else if (board.pieces[i, j].Color == Player.Red && board.pieces[i, j].Type == PieceType.King)
                        {
                            row.Add(new Cell(i, j, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\KnightB.png", true));
                        }
                        else if (board.pieces[i, j].Color == Player.White && board.pieces[i, j].Type == PieceType.Checker)
                        {
                            row.Add(new Cell(i, j, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\PawnW.png", true));
                        }
                        else
                        {
                            row.Add(new Cell(i, j, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\PawnB.png", true));
                        }
                    }
                    else
                    {
                        row.Add(new Cell(i, j));
                    }  
                }
                gameBoard.Add(row);
            }
            return gameBoard;
        }
        public static ObservableCollection<ObservableCollection<Cell>> InitGameBoard2(GameState gameState)
        {
            var gameBoard = new ObservableCollection<ObservableCollection<Cell>>();
            Board board = gameState.Board;

            for (int i = 0; i < board.pieces.GetLength(0); i++)
            {
                var row = new ObservableCollection<Cell>();
                for (int j = 0; j < board.pieces.GetLength(1); j++)
                {
                    if (board.pieces[i, j] != null)
                    {
                        if (board.pieces[i, j].Color == Player.White && board.pieces[i, j].Type == PieceType.King)
                        {
                            row.Add(new Cell(i, j, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\KingW.png", true));
                        }
                        else if (board.pieces[i, j].Color == Player.Red && board.pieces[i, j].Type == PieceType.King)
                        {
                            row.Add(new Cell(i, j, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\KnightB.png", true));
                        }
                        else if (board.pieces[i, j].Color == Player.White && board.pieces[i, j].Type == PieceType.Checker)
                        {
                            row.Add(new Cell(i, j, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\PawnW.png", true));
                        }
                        else
                        {
                            row.Add(new Cell(i, j, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\PawnB.png", true));
                        }
                    }
                    else
                    {
                        row.Add(new Cell(i, j));
                    }
                }
                gameBoard.Add(row);
            }
            return gameBoard;
        }
        private void OnRedrawBoardRequested(object sender, EventArgs e)
        {
            RedrawBoard();
        }
        public void RedrawBoard()
        {
            Pieces.Clear();

            ObservableCollection<ObservableCollection<Cell>> board = cgl.cells;

            ObservableCollection<ObservableCollection<CellVM>>  newPieces = CheckerGameBoardToChechersGameVMBoard(board);
            foreach (var row in newPieces)
            {
                Pieces.Add(row);
            }
            ShowHighlights();
            if (cgl.gameState.IsGameOver())
            {
                ShowGameOver();
            }
        }
        private void ShowHighlights()
        {
            foreach (Position to in cgl.moveCache.Keys)
            {
                Pieces[to.Row][to.Column].SimpleCell.CellColor = Brushes.Green;
            }
        }
        public void ShowGameOver()
        {
            MessageBox.Show("A castigat: " + cgl.gameState.Result.Winner);
        }

        private ICommand saveGameCommand;
        public ICommand SaveGameCommand
        {
            get
            {
                if (saveGameCommand == null)
                {
                    saveGameCommand = new RelayCommand<GameState>(SaveCurrentGame);
                }
                return saveGameCommand;
            }
        }
        public void SaveCurrentGame(GameState currentGameState)
        {
            currentGameState = cgl.gameState;

            ObservableCollection<GameState> SavedGamesList = new ObservableCollection<GameState>();
            SavedGamesList = SavedGamesManagement.LoadSavedGames();
            int gameNumber = SavedGamesList.Count + 1;
            currentGameState.NumeJocSalvat = "Game " + gameNumber;
            SavedGamesList.Add(currentGameState);

            List<GameData> gameDataList = new List<GameData>();

            
            foreach (var gameState in SavedGamesList)
            {
                GameData gameData = new GameData();
                // Creează o matrice de GameData pe baza informațiilor din GameState
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
