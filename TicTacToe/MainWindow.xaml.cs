using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Holds the current value of the cell in the active game.
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if 'X' turn, False if 'O' turn.
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended, False if its still being played.
        /// </summary>
        private bool mGameEnded;
        

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        /// <summary>
        /// Starts a new game and clears all values back to the start.
        /// </summary>
        private void NewGame()
        {
            // Creates a new array of free cells
            mResults = new MarkType[9];

            for(var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            // Player 'X' starts the game.
            mPlayer1Turn = true;
            
            // Clear all the Text.
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event.
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Start a new game on the click after it finished.
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button)sender;

            //Find the buttons position in the array.
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Don't do anything if there is an 'X' or an 'O' in the cell
            if (mResults[index] != MarkType.Free)
            {
                return;
            }

            // Set the cell value based on turn.
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // See button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // Change color for each player
            button.Foreground = !mPlayer1Turn ? Brushes.Green : Brushes.Blue;

            // bitwise opperator, flips everytime its passed.
            mPlayer1Turn ^= true;

            // Check for winner
            CheckForWinner();
        }


        /// <summary>
        /// Checks if there is a winner with 3 in a row.
        /// </summary>
        private void CheckForWinner() 
        { 
            // Check top row
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in Pink
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Pink;
            }

            // Check middle row
            else if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in Pink
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Pink;
            }

            // Check bottom row
            else if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in Pin2
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Pink;
            }

            // Check left column
            else if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in Pink
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Pink;
            }

            // Check middle column
            else if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in Pink
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Pink;
            }

            // Check right column
            else if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in Pink
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Pink;
            }

            // Check diagonal down
            else if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in Pink
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Pink;
            }

            // Check diagonal up
            else if (mResults[6] != MarkType.Free && (mResults[6] & mResults[4] & mResults[2]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in Pink
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Pink;
            }

            // Check if Game is a draw
            else if (!mResults.Any(result => result == MarkType.Free) && mGameEnded == false)
            {
                // Games ended
                mGameEnded = true;

                // Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
        }
    }
}
