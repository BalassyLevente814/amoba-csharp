using System.Windows;
using System.Windows.Controls;

namespace Amoba3x3
{
    public partial class MainWindow : Window
    {
        private char currentPlayer = 'X';
        private char[] board = new char[9]; // 'X', 'O' vagy '\0'
        private int counter;

        public MainWindow()
        {
            InitializeComponent();
            ResetBoard();
        }

        private void ResetBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                board[i] = '\0';
                ((Button)GameGrid.Children[i]).Content = "";
                ((Button)GameGrid.Children[i]).IsEnabled = true;
            }
            currentPlayer = 'X';
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int index = GameGrid.Children.IndexOf(clickedButton);

            if (board[index] == '\0')
            {
                board[index] = currentPlayer;
                clickedButton.Content = currentPlayer.ToString();
                clickedButton.IsEnabled = false;

                if (CheckWin())
                {
                    MessageBox.Show($"A(z) {currentPlayer} játékos nyert!");
                    ResetBoard();
                    counter++;
                    MessageBox.Show($"Ennyiszer játszottál: {counter}");
                }
                else if (IsDraw())
                {
                    MessageBox.Show("Döntetlen!");
                    ResetBoard();
                    counter++;
                    MessageBox.Show($"Ennyiszer játszottál: {counter}");
                }
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
            }
        }

        private bool CheckWin()
        {
            int[][] winningCombinations = new int[][]
            {
                new int[]{0,1,2},
                new int[]{3,4,5},
                new int[]{6,7,8},
                new int[]{0,3,6},
                new int[]{1,4,7},
                new int[]{2,5,8},
                new int[]{0,4,8},
                new int[]{2,4,6}
            };

            foreach (var combo in winningCombinations)
            {
                if (board[combo[0]] != '\0' &&
                    board[combo[0]] == board[combo[1]] &&
                    board[combo[1]] == board[combo[2]])
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsDraw()
        {
            foreach (var cell in board)
            {
                if (cell == '\0')
                    return false;
            }
            return true;
        }
    }
}
