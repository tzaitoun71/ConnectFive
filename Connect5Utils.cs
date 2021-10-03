using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFive
{
    class Connect5Utils // Class for the utilities of Connect5 as a whole, check for wins, display boards, initalizing.
    {
        public static void initBoard(char[,] board) // Initalizing board method that takes in the 2D array of the board
        {
            for (int x = 0; x < 8; x++) // loops through column of board
            {
                for (int y = 0; y < 8; y++) // loops through row of column
                {
                    board[x, y] = '*'; // gives each coordinate the *
                }
            }
        }

        public static void displayBoard(char[,] board) // method for displaying board
        {
            for (int x = 0; x < 8; x++) // self explanatory
            {
                Console.Write("|"); // Surrounds the left and ride walls of the board with |
                for (int y = 0; y < 8; y++)
                {
                    Console.Write(board[x, y]); // displays the specified chip in its coordinate
                }

                Console.Write("| \n");
            }
        }

        public static int[] PlaceChip(char[,] board, int Number, char Turn) // method for placing chip
        {
            int Position = Number - 1; // Int var equaling to the passed Number minus 1 

            int x = 0; // initalizing x to 0 

            // looping through in order to make the chip land on the bottom 
            for (x = 7; x >= 0; x--)
            {
                if (board[x, Position] == '*')
                {
                    board[x, Position] = Turn;
                    break;
                }
            }
            // if x is less than 0 then it will return a null and if its greater than =  it will return a coordinate
            if (x < 0) return null;
            else return new int[2] { x, Position };
        }

        public static bool checkWin(char[,] board, char Turn, int[] LastMove) // Method for checking win 
        {
            // calls for other check methods including (Vertical, Horizontal, and Diagonal)
            return checkVertical(board, Turn, LastMove)
                || checkHorizontal(board, Turn, LastMove)
                || checkDiagonal(board, Turn, LastMove);
        }

        public static bool checkVertical(char[,] board, char Turn, int[] LastMove) // Method for checking vertical win
        {
            // initalizing new int Vars with the coordinates of the Lastmove Array
            int LastmoveRow = LastMove[0];
            int LastmoveColumn = LastMove[1];
            int Count = 0; // counter

            for (int x = LastmoveRow; x < 8; x++) // Takes the row that that the last move was in and looks for corresponding chips beneath it
            {
                if (board[x, LastmoveColumn] == Turn) Count++; // if beneath the current chip is the same then it will add to the counter
                else Count = 0; // if not it'll set the count to 0
            }

            if (Count == 5) return true; // if it counts 5 chips in total then it will return a true 
            else return false; // if not it will return a false
        }

        public static bool checkHorizontal(char[,] board, char Turn, int[] LastMove) // Method for checking horizontal
        {
            // initalizing the new int vars  from the last move rows and columns
            int LastmoveRow = LastMove[0];
            int LastmoveColumn = LastMove[1];
            int LeftCount = 0; // counter for the left
            int RightCount = 0; // counter for the right 

            for (int y = LastmoveColumn - 1; y >= 0; y--) // a loop that subtracts 1 during each iteration to go left from the current position
            {
                if (board[LastmoveRow, y] == Turn) LeftCount++; // if the chip is the same as the one before it the count iterates
                else break; // if not it breaks the loop
            }

            for (int y = LastmoveColumn + 1; y < 8; y++) // a loop that adds 1 during each iteration to go right from the current position
            {
                if (board[LastmoveRow, y] == Turn) RightCount++; // if the chip is the same as the one before it the counter increases
                else break;
            }

            if (LeftCount + RightCount + 1 == 5) return true; // if the addition of both left and right count is = to 5 then true is returned
            else return false; // if not false is 
        }

        public static bool checkDiagonal(char[,] board, char Turn, int[] LastMove) // Method for checking diagonal 
        {
            return checkDiagonal1(board, Turn, LastMove) || checkDiagonal2(board, Turn, LastMove); // returning one of the other methods
        }

        public static bool checkDiagonal1(char[,] board, char Turn, int[] LastMove) // method for first case diagonal
        {
            //Self explanatory
            int LastmoveRow = LastMove[0];
            int LastmoveColumn = LastMove[1];
            int LeftCount = 0;
            int RightCount = 0;
            int x = LastmoveRow - 1;
            int y = LastmoveColumn - 1;

            while (x >= 0 && y >= 0) // while the x value and y value are increasing which means going top left
            {
                if (board[x, y] == Turn) LeftCount++; // if the chips correspond the counter will add to the counter 
                else break;

                x--; // X values decreasing during each iteration 
                y--; // Y value decreasing during each iteration 
            }

            // reinitalizing of both x,y
            x = LastmoveRow + 1;
            y = LastmoveColumn + 1;

            while (x < 8 && y < 8) // while both are less than 8
            {
                if (board[x, y] == Turn) RightCount++; // if they correspond right counter iterates
                else break;

                x++; // X iterates making it go right
                y++; // Y iterates making it go down
            }

            if (LeftCount + RightCount + 1 == 5) return true; // if both counters add up to 5, true is returned
            else return false;
        }

        public static bool checkDiagonal2(char[,] board, char Turn, int[] LastMove) // method for second case diagonal 
        {
            // self explanatory
            int LastmoveRow = LastMove[0];
            int LastmoveColumn = LastMove[1];
            int LeftCount = 0;
            int RightCount = 0;
            int x = LastmoveRow + 1;
            int y = LastmoveColumn - 1;

            while (x < 8 && y >= 0) // while X is less than 8 and Y is greater than or equal to 0
            {
                if (board[x, y] == Turn) LeftCount++; // if the points correspond it iterates
                else break;

                x++; // X values go to the right
                y--; // Y values go up, in other words the check diagonal is going top right
            }

            //reinitializing of the coordinates
            x = LastmoveRow - 1;
            y = LastmoveColumn + 1;

            while (x >= 0 && y < 8) // opposite case 
            {
                if (board[x, y] == Turn) RightCount++; // if the corresponding points are the same it iterates
                else break;

                x--; // at the current chip x goes to the left
                y++; // at the current chip y goes down, other words chip is going bottom left
            }

            if (LeftCount + RightCount + 1 == 5) return true; // if the addition of both counters are equal to 5, true is returned
            else return false;
        }

        public static int winner(char[,] board) // winner method used for AI
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (checkWin(board, 'R', new int[2] { i, j })) return 1; ; // returns 1 if R wins
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (checkWin(board, 'B', new int[2] { i, j })) return 2; ; // returns 2 if B wins 
                }
            }

            return 0; // returns 0 if no one wins
        }

        public static void removeChip(char[,] board, int Number) // a method for when the AI is performing different combinations, it is used to undo them
        {
            int Position = Number - 1;
            for (int i = 0; i < 8; i++)
            {
                if (board[i, Position] != '*') // self explanatory 
                {
                    board[i, Position] = '*';
                    return;
                }
            }
        }
        public static bool canPlay(char[,] board, int Number) // method for a turn surpassing
        {
            int Position = Number; // Int var equaling to the passed Number minus 1 

            int x = 0; // initalizing x to 0 

            // looping through in order to make the chip land on the bottom 
            for (x = 7; x >= 0; x--)
            {
                if (board[x, Position] == '*') return true;
            }

            return false;
        }

        public static bool isBoardFull(char[,] board) // method for checking if board is full
        {
            for (int i = 0; i < 8; i++)
            {
                if (canPlay(board, i)) return false;
            }

            return true;
        }

        public static int MinMax(char[,] board, int Depth, bool MaximizingPlayer, int[] LastMove) // MinMax theory Method AKA pain in the ass
        {
            // Checks the depth for determing where to place chip against Player
            if (Depth <= 0) return 0;
            int win = winner(board);
            if (win == 2) return Depth;
            if (win == 1) return -Depth;
            if (isBoardFull(board)) return 0;

            int bestValue; // Int var for determing the best value 
            char currentPlayer; // char var for current player 
            if (MaximizingPlayer) // if the MaximizingPlayer which is other known as the AI
            {
                bestValue = -1; // best value reduces 
                currentPlayer = 'B'; // current player is B
            }
            else
            {
                bestValue = 1; // best value is made into 1 
                currentPlayer = 'R'; // current player is R
            }

            for (int i = 0; i < 8; i++)
            {
                int[] recLastMove = Connect5Utils.PlaceChip(board, i + 1, currentPlayer); // an array for recording the last move of the current player
                if (recLastMove == null) continue; // if the last move is null then it skips this current iteration and goes to the next one 
                int v = MinMax(board, Depth - 1, !MaximizingPlayer, recLastMove); // an int var called v is assigned  the value of the MinMax method 
                if (MaximizingPlayer) bestValue = Math.Max(bestValue, v); // if its the AI the best value takes the greater value of best value or v 
                else bestValue = Math.Min(bestValue, v); // if its not then the minimum value is taken from the two 
                removeChip(board, i + 1);
            }

            return bestValue; // best value is returned 
        }
    }
}
