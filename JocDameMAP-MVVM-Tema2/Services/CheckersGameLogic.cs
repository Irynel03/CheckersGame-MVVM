using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace JocDameMAP_MVVM_Tema2
{
    class CheckersGameLogic: INotifyPropertyChanged
    {
        public ObservableCollection<ObservableCollection<Cell>> cells {  get; set; }
        public Dictionary<Position, Move> moveCache { get; set; } = new Dictionary<Position, Move>();
        //public readonly Dictionary<Cell, Move> moveCache2 = new Dictionary<Cell, Move>();        // poate mai tarziu

        private int Jumped2Times {  get; set; } = 0;

        public Cell SelectedCell { get; set; }
        public GameState gameState;
        public GameState GameState
        {
            get { return gameState; }
            set
            {
                if (gameState != value)
                {
                    gameState = value;
                    OnPropertyChanged(nameof(GameState.CurrentPlayer));
                }
            }
        }


        public CheckersGameLogic(GameState gameState, ObservableCollection<ObservableCollection<Cell>> cells)
        {
            this.cells = cells;
            this.gameState = gameState;

        }
        public CheckersGameLogic(ObservableCollection<ObservableCollection<Cell>> cells, bool canMultipleJump)
        {
            this.cells = cells;
            gameState = new GameState(Player.Red, Board.Initial());
            gameState.canMultipleJump = canMultipleJump;
        }
        public void RedrawBoard()
        {
            cells.Clear();
            int x = -1, y = -1;
            if (SelectedCell != null)
            {
                x = SelectedCell.X;
                y = SelectedCell.Y;
            }
            for (int r = 0; r < 8; r++)
            {
                ObservableCollection<Cell> row = new ObservableCollection<Cell>();
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = gameState.Board.pieces[r, c];
                    SolidColorBrush cellColor = Brushes.Black;
                    if (x == r && y == c)
                    {
                        cellColor = Brushes.White;
                    }
                    if (piece != null)
                    {
                        if (piece.Color == Player.White && piece.Type == PieceType.King)
                        {
                            row.Add(new Cell(r, c, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\KingW.png", true, cellColor));
                        }
                        else if (piece.Color == Player.Red && piece.Type == PieceType.King)
                        {
                            row.Add(new Cell(r, c, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\KnightB.png", true, cellColor));
                        }
                        else if (piece.Color == Player.White && piece.Type == PieceType.Checker)
                        {
                            row.Add(new Cell(r, c, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\PawnW.png", true, cellColor));
                        }
                        else
                        {
                            row.Add(new Cell(r, c, "C:\\Users\\andre\\Desktop\\sem II\\MAP\\JocDameMAP-MVVM-Tema2\\JocDameMAP-MVVM-Tema2\\Models\\Assets\\PawnB.png", true, cellColor));
                        }
                    }
                    else
                    {
                        row.Add(new Cell(r, c));
                    }
                }
                cells.Add(row);
            }
            //ShowHighlights();
            OnRedrawBoardRequested(EventArgs.Empty);
        }
        //private void ShowHighlights()
        //{
        //    foreach (Position to in moveCache.Keys)
        //    {
        //        cells[to.Row][to.Column].CellColor = Brushes.Green;
        //    }
        //}

        public event EventHandler RedrawBoardRequested;
        protected virtual void OnRedrawBoardRequested(EventArgs e)
        {
            RedrawBoardRequested?.Invoke(this, e);
        }
        public void ClickAction(Cell obj)
        {
            Cell cell = obj;

            if (SelectedCell == null)
                OnFromCellSelected(cell);
            else
                OnToCellSelected(cell);
        }
        private void OnFromCellSelected(Cell cell)
        {
            //if (cell.hasPiece)
            //{
                cell.CellColor = Brushes.White;
                Position pos = new Position(cell.X, cell.Y);

                IEnumerable<Move> moves = gameState.LegalMovesForPiece(pos); // problem la LegalMovesForPiece

                if (moves.Count() > 0)
                {
                    SelectedCell = cell;
                    CacheMoves(moves);
                    if(moveCache.Count == 0)
                    {
                        cell.CellColor = Brushes.Black;
                        SelectedCell = null;
                    }
                }
                else 
                { 
                    cell.CellColor = Brushes.Black; 
                }
            //}
            //ShowHighlights();
            RedrawBoard();
        }
        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach (Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }
        }
        private void OnToCellSelected(Cell cell)
        {
            bool canJump = false;
            Position pos = new Position(cell.X, cell.Y);
            if (moveCache.TryGetValue(pos, out Move move))
            {
                gameState.MakeMove(move);
                
                if (move.Jump)
                {
                    IEnumerable<Move> moves = gameState.LegalMovesForPiece(move.ToPos);

                    canJump = moves.Any(m => m.canJump);

                    //Jumped2Times++;
                    //if (Jumped2Times>=2)
                    //{
                    //    Jumped2Times = 0;
                    //    gameState.CurrentPlayerSwitch();
                    //}
                    //else
                    if(!canJump)
                    {
                        gameState.CurrentPlayerSwitch();
                    }
                }
                else
                {
                    gameState.CurrentPlayerSwitch();
                }
            }
            moveCache.Clear();
            cell.CellColor = Brushes.Black;

            if(!canJump || !gameState.canMultipleJump)
                SelectedCell = null;
            else
            {
                RedrawBoard();
                cell = cells[move.ToPos.Row][move.ToPos.Column];
                OnFromCellSelected(cell);
                cells[move.ToPos.Row][move.ToPos.Column].CellColor = Brushes.Green;
            }
            //ShowHighlights();
            RedrawBoard();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
