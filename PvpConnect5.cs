using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFive
{
    class PvpConnect5 // Class for the PvP mode
    {
        private char Lastturn = 'B'; // private var to keep track of the last turn
        private char Turn = 'R'; // private var to keep track of current turn
        private char[,] board = new char[8, 8]; // char 2D Array 8x8 for the board
        private int NumMoves = 0; // keeps count of the number of moves to conclude if its a draw

        public void startGame() // method for the game in progress
        {
            // Clears the console after mode is selected, and board is initalized
            Console.Clear();
            Connect5Utils.initBoard(this.board);

            while (true) // while the condition is true
            {
                if (this.Lastturn != this.Turn) // if last turn doesnt equal to the current turn 
                {
                    Connect5Utils.displayBoard(this.board); // the board is displayed
                    Console.WriteLine(this.Turn + "'s turn"); // displays whos turn it is 
                    Console.WriteLine("Enter number between 1-8"); // asks user to enter number 
                    this.Lastturn = this.Turn; // this refers to the current instantiated object
                }

                try // try catch to catch any errors in the input of a number
                {
                    int Number = int.Parse(Console.ReadLine());
                    if (Number < 1 || Number > 8)
                    {
                        Console.WriteLine("The number you entered should be between 1-8");
                        continue; // Skips current iteration and loop starts again
                    }

                    int[] LastMove = Connect5Utils.PlaceChip(this.board, Number, this.Turn); // gives the lastmove array the value of the board, number, and turn
                    if (LastMove == null) // if there is nothing able to be placed in that column 
                    {
                        Console.WriteLine("The column you selected is full"); // this message is displayed
                        continue; // Player's turn is not taken away but loop starts again 
                    }

                    bool win = Connect5Utils.checkWin(this.board, this.Turn, LastMove); // Bool var for Win and setting it to check the conditions for a win

                    if (win) // if the condition is met 
                    {
                        Console.Clear(); // console is cleared
                        Connect5Utils.displayBoard(this.board); // current board is displayed
                        Console.WriteLine(this.Turn + " Won"); // victor is displayed
                        Console.WriteLine("Write M to go back to the main menu, or any key to exit"); // main menu 
                        string Command = Console.ReadLine().ToUpper();

                        if (Command == "M") break;
                        else Environment.Exit(0);
                    }

                    NumMoves++;
                    if (NumMoves == 64)
                    {
                        // explanation given throughout other modes for this condition
                        Console.Clear();
                        Connect5Utils.displayBoard(this.board);
                        Console.WriteLine("Its a draw hah");
                        Console.WriteLine("Write M to go back to the main menu, or any key to exit");
                        string Command = Console.ReadLine().ToUpper();
                        if (Command == "M") break;
                        else Environment.Exit(0);
                    }

                    if (this.Turn == 'R') this.Turn = 'B';
                    else this.Turn = 'R';
                    Console.Clear();
                }
                catch (Exception e)
                {
                    continue; // continue means restart the while loop, also does not take the current players turn away
                }
            }
        }
    }
}

