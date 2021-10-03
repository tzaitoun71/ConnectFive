using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConnectFive
{
    class FileIO // Class for the FileIO mode
    {
        private char Turn = 'R'; // private char var that keeps track of current turn
        private char[,] board = new char[8, 8]; // private char 2D Array initialized for the conventions of the board 8x8

        public void startGame() // Startgame method
        {
            Console.Clear(); // clears the console when 1-3 is pressed

            try  // try catch to capture any errors in the document 
            {
                StreamReader file = new StreamReader("Input.txt"); // looks for a file called Input.txt
                string Line; // string var named Line to read each line

                while ((Line = file.ReadLine()) != null) // while loop to read each line until it is null
                {
                    string[] StringInput = Line.Split(' '); // transforms every text that is seperated by a space into a string
                    int[] Input = new int[StringInput.Length]; // Input array is given the value of the length of the stringinput array

                    for (int i = 0; i < Input.Length; i++) // loops through each input converting it into an int
                    {
                        Input[i] = int.Parse(StringInput[i]);
                    }
                    Connect5Utils.initBoard(this.board); // board is initalized

                    for (int i = 0; i < Input.Length; i++)
                    {
                        int Number = Input[i]; // a new int var called Number is given the value of each iteration
                        if (Number < 1 || Number > 8) continue; // if its less than 1 and greater than 8 it will skip the iteration

                        int[] LastMove = Connect5Utils.PlaceChip(this.board, Number, this.Turn); // the chip is placed 
                        if (LastMove == null) continue; // if the last move is unable to be placed it will skip the iteration

                        bool win = Connect5Utils.checkWin(this.board, this.Turn, LastMove); // calls for checkwin method from connect5utils class

                        if (win) // condition if win
                        {
                            Connect5Utils.displayBoard(this.board); // displays the current board
                            Console.WriteLine(this.Turn + " Won"); // displays victor  
                            break;
                        }

                        if (i == Input.Length - 1) // if the iteration is equal to the length of the array - 1 then the board is displayed and no one wins
                        {
                            Connect5Utils.displayBoard(this.board);
                            Console.WriteLine("Its a draw hah");
                        }

                        if (this.Turn == 'R') this.Turn = 'B'; // if its Red's turn then it becomes blue's turn
                        else this.Turn = 'R'; // if its not Red's turn, it is red's turn
                    }
                }

                file.Close(); // closes the file after its read
                Console.WriteLine("Write M to go back to the main menu, or any key to exit"); // main menu 
                string Command = Console.ReadLine().ToUpper(); // makes it capital 

                if (Command != "M") Environment.Exit(0);      // exits if M is not entered          
            }
            catch (Exception e) // catches the exception
            {
                Console.WriteLine("An error has occured while parsing the file"); // displays error to user
                Console.ReadKey();
            }
        }
    }
}
