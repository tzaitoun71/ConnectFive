using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFive
{
    class PvC
    {
        private char[,] board = new char[8, 8]; // char 2D Array 8x8 for the board
        private int NumMoves = 0; // keeps count of the number of moves to conclude if its a draw

        public void startGame()  // Method for starting game
        {
            Console.Clear(); // clearing console 
            Connect5Utils.initBoard(this.board); // initalizing the board

            while (true)
            {
                bool TurnInit = false; // initalizing turn as false 
                if (!TurnInit) // if not turninit
                {
                    Console.Clear(); // console clears 
                    Connect5Utils.displayBoard(this.board); // the board is displayed
                    Console.WriteLine("Your turn"); // displays whos turn it is 
                    Console.WriteLine("Enter number between 1-8"); // asks user to enter number 
                }

                try // try catch to catch any errors in the input of a number
                {
                    TurnInit = true; // turninit initialized as true 
                    int Number = int.Parse(Console.ReadLine()); // takes in input 
                    if (Number < 1 || Number > 8) // condition self explanatory 
                    {
                        Console.WriteLine("The number you entered should be between 1-8");
                        TurnInit = false; // makes the turninit false  if the condition is true 
                        continue; // Skips current iteration and loop starts again
                    }

                    int[] LastMove = Connect5Utils.PlaceChip(this.board, Number, 'R'); // gives the lastmove array the value of the board, number, and Red
                    if (LastMove == null) // if there is nothing able to be placed in that column 
                    {
                        Console.WriteLine("The column you selected is full"); // this message is displayed
                        TurnInit = false;
                        continue; // Player's turn is not taken away but loop starts again 
                    }

                    bool PlayerWin = Connect5Utils.checkWin(this.board, 'R', LastMove); // Bool var for Win and setting it to check the conditions for a win

                    if (PlayerWin) // if the condition is met 
                    {
                        Console.Clear(); // console is cleared
                        Connect5Utils.displayBoard(this.board); // current board is displayed
                        Console.WriteLine("You Won"); // victor is displayed
                        Console.WriteLine("Write M to go back to the main menu, or any key to exit"); // main menu 
                        string Command = Console.ReadLine().ToUpper(); // makes input capital 

                        if (Command == "M") break; // breaks if M is entered
                        else Environment.Exit(0);
                    }

                    NumMoves++; // number of moves iterates 
                    if (NumMoves == 64) // condition if 64 
                    {
                        Console.Clear(); // clears console 
                        Connect5Utils.displayBoard(this.board); // displays current board 
                        Console.WriteLine("Its a draw hah");// displays messages 
                        Console.WriteLine("Write M to go back to the main menu, or any key to exit");
                        string Command = Console.ReadLine().ToUpper();  // self explanatory 
                        if (Command == "M") break;
                        else Environment.Exit(0);
                    }

                    int[][] Moves = new int[8][]; // Moves array

                    for (int i = 0; i < 8; i++)
                    {
                        int[] AILastMove = Connect5Utils.PlaceChip(this.board, i + 1, 'B'); // AIlastmove array for keeping track of last move of AI
                        if (AILastMove == null) continue; // if its Null it skips the current iteration 

                        Moves[i] = new int[2] { i, Connect5Utils.MinMax(this.board, 6, false, AILastMove) }; // moves array with size of 2 storing the index and the minmax method
                        Connect5Utils.removeChip(this.board, i + 1);
                    }

                    int maxMoveScore = -1000; // setting max move score to 1000
                    int maxMoveIndex = 0; // the index to 0 
                    for (int i = 0; i < 8; i++)
                    {
                        if (Moves[i][1] >= maxMoveScore) // checks the columns 
                        {
                            maxMoveScore = Moves[i][1]; // sets maxmove score to the values of the moves array of the columns 
                            maxMoveIndex = i; // the index iterates
                        }
                    }

                    int[] actualAILastMove = Connect5Utils.PlaceChip(this.board, maxMoveIndex + 1, 'B');

                    bool AiWin = Connect5Utils.checkWin(this.board, 'B', actualAILastMove); // Bool var for Win and setting it to check the conditions for a win

                    if (AiWin) // if the condition is met 
                    {
                        Console.Clear(); // console is cleared
                        Connect5Utils.displayBoard(this.board); // current board is displayed
                        Console.WriteLine("You Lose"); // victor is displayed
                        Console.WriteLine("Write M to go back to the main menu, or any key to exit"); // main menu 
                        string Command = Console.ReadLine().ToUpper();

                        if (Command == "M") break;
                        else Environment.Exit(0);
                    }

                    NumMoves++; // num moves iterate 
                    if (NumMoves == 64) // condition if its 64
                    {
                        Console.Clear(); // console clears 
                        Connect5Utils.displayBoard(this.board); // current board displays
                        Console.WriteLine("Its a draw hah"); // draw message
                        Console.WriteLine("Write M to go back to the main menu, or any key to exit"); // standard message 
                        string Command = Console.ReadLine().ToUpper(); // input capital 
                        if (Command == "M") break;
                        else Environment.Exit(0);
                    }
                }
                catch (Exception e)
                {
                    continue; // continue means restart the while loop, also does not take the current players turn away
                }
            }
        }
    }
}

