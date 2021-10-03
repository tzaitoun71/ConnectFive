using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFive
{
    // Tariq Zaitoun
    // 18/11/2019
    // Assignment 3: ConnectFive
    // ConnectFive program is a game where one of 3 modes is selected: PvP, FileIo, PvC. the chips are placed and if 5 of them are corresponding either horizontally, vertically, or diagonally then the player is victorious 


    // Code that isn't usually used in class:
    // "this" refers to the current instance of the class, also used to differentiate between the method parameters
    // "continue" skips current iteration in a loop
    class Program
    {
        static void Main(string[] args)
        {
            bool init = false; // init ititialized as false 

            while (true)
            {
                if (!init)
                {
                    // Main menu telling user to select a game mode and one of three modes
                    Console.WriteLine("Select Gamemode");
                    Console.WriteLine("1. PvP");
                    Console.WriteLine("2. FileIO");
                    Console.WriteLine("3. PvC");

                    // init becomes true after one of the choices is picked 
                    init = true;
                }

                try // try catch to capture all error in input
                {
                    int Mode = int.Parse(Console.ReadLine()); // parses user input 
                    if (Mode == 1) // condition for PVP
                    {
                        PvpConnect5 pvp = new PvpConnect5(); // constructor for pvp 
                        pvp.startGame(); // starts the pvp game 
                        init = false; // makes init false after game is finished 
                        Console.Clear(); // clears the console 
                    }
                    else if (Mode == 2) // condition for FILEIO 
                    {
                        FileIO Fileio = new FileIO(); // constructor for fileio 
                        Fileio.startGame(); // starts the game and after the code is finished 
                        init = false; // init is initalized back to false 
                        Console.Clear(); // clears the console 
                    }
                    else if (Mode == 3) // condition for AI Match 
                    {
                        PvC pvc = new PvC(); // contructor for AI Mode  
                        pvc.startGame(); // starts the AI game
                        init = false; // after AI game is finished the init goes back to false 
                        Console.Clear(); // console clears 
                    }
                    else
                    {
                        Console.WriteLine("You must input a number between 1-3"); // if another input is entered then this message displays 
                        continue; // continue is written to skip current iteration and allows for user to enter again 
                    }
                }
                catch (Exception e) // catches exception 
                {
                    Console.WriteLine("You must input a number between 1-3");
                    continue;
                }
            }
        }
    }
}
