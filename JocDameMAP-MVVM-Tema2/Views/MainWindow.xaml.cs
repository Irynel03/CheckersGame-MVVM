using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JocDameMAP_MVVM_Tema2
{
    public partial class MainWindow : Window
    {
        public bool isCheckedMultipleJumps = false;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnEnterNewGame_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new CheckersGameVM(isCheckedMultipleJumps);

            BoardGrid.DataContext = viewModel;

            MainMenugrid.Visibility = Visibility.Collapsed;
            BoardGrid.Visibility = Visibility.Visible;
        }
        private void btnLoadGames_Click(object sender, RoutedEventArgs e)
        {
            SavedGamesSelectorGrid.Visibility = Visibility.Visible;
            MainMenugrid.Visibility = Visibility.Collapsed;
        }
        private void btn_LoadGame(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            GameState gameState = button.DataContext as GameState;

            var viewModel = new CheckersGameVM(gameState);
            BoardGrid.DataContext = viewModel;

            SavedGamesSelectorGrid.Visibility = Visibility.Collapsed;
            BoardGrid.Visibility = Visibility.Visible;
        }
        private void btnStatistics_Click(object sender, RoutedEventArgs e)
        {
            MainMenugrid.Visibility = Visibility.Collapsed;
            StatsGrid.Visibility = Visibility.Visible;
        }
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            MainMenugrid.Visibility = Visibility.Collapsed;
            HelpGrid.Visibility = Visibility.Visible;
        }
        public void CheckBoxMultipleJumps_Checked(object sender, RoutedEventArgs e)
        {
            isCheckedMultipleJumps = true;
        }
        public void CheckBoxMultipleJumps_UnChecked(object sender, RoutedEventArgs e)
        {
            isCheckedMultipleJumps = false;
        }

    }
}