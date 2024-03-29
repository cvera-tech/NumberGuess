﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGuess
{
    class Program
    {
        const int MinimumValue = 1;
        const int MaximumValue = 100;
        const int NumberOfGuesses = 10;
        const string NumberPrompt = "Please enter a number between {0} and {1}, inclusive.";
        const string GuessPrompt = "What's your guess?";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Number Guess app!");
            string player1Name = GetPlayerName(1);
            string player2Name = GetPlayerName(2);
            bool playingGame = true;
            do
            {
                PlayGame(player1Name, player2Name);
                char replayInput;
                bool replayCharValid = false;
                do
                {
                    Console.Write("\nWould you like to switch places and play again? (y/n) ");
                    replayInput = char.ToLower(Console.ReadKey().KeyChar);
                    if (replayInput == 'n')
                    {
                        playingGame = false;
                        replayCharValid = true;
                    }
                    else if (replayInput == 'y')
                    {
                        string tempString = player1Name;
                        player1Name = player2Name;
                        player2Name = tempString;
                        Console.Clear();
                        replayCharValid = true;
                    }
                    else
                    {
                        Console.Write("\nInvalid input. ");
                    }
                } while (!replayCharValid);
            }
            while (playingGame);
        }

        /// <summary>
        /// Prompts the player to enter their name and returns the name input.
        /// </summary>
        /// <param name="playerNumber">The number associated with the name.</param>
        /// <returns>The player's name.</returns>
        static string GetPlayerName(int playerNumber)
        {
            string playerName = "";
            bool nameEntered = false;
            do
            {
                Console.Write("Player {0}, what is your name? ", playerNumber);
                playerName = Console.ReadLine();
                if (string.IsNullOrEmpty(playerName))
                {
                    Console.WriteLine("Please enter a name.");
                }
                else
                {
                    nameEntered = true;
                }
            }
            while (!nameEntered);
            return playerName;
        }

        /// <summary>
        /// Prompts the player to enter a number and returns the number input.
        /// </summary>
        /// <param name="playerName">The name of the player.</param>
        /// <param name="message">The prompt.</param>
        /// <returns>The valid number.</returns>
        static int GetNumber(string playerName, string message)
        {
            bool numberValid = false;
            int number = 0;
            do
            {
                Console.Write("{0}: {1} ", playerName, message);
                int tempNumber;
                if (int.TryParse(Console.ReadLine(), out tempNumber))
                {
                    if (tempNumber < MinimumValue || tempNumber > MaximumValue)
                    {
                        Console.WriteLine("That number is out of bounds.");
                    }
                    else
                    {
                        number = tempNumber;
                        numberValid = true;
                    }
                }
                else
                {
                    Console.WriteLine("Unable to parse input.");
                }
            }
            while (!numberValid);
            return number;
        }

        /// <summary>
        /// Starts the game by prompting one player to input a number to guess, then prompting the other player to guess.
        /// </summary>
        /// <param name="player1Name">The first player.</param>
        /// <param name="player2Name">The second player.</param>
        static void PlayGame(string player1Name, string player2Name)
        {
            int numberToGuess = GetNumber(player1Name, string.Format(NumberPrompt, MinimumValue, MaximumValue));
            Console.Clear();
            int incorrectGuesses = 0;
            bool gameWin = false;
            while (incorrectGuesses < NumberOfGuesses && !gameWin)
            {
                Console.WriteLine("You have {0} guesses remaining.", NumberOfGuesses - incorrectGuesses);
                int guess = GetNumber(player2Name, GuessPrompt);
                if (guess == numberToGuess)
                {
                    Console.WriteLine("Well done, {0}, you guessed {1}'s number!", player2Name, player1Name);
                    gameWin = true;
                }
                else if (guess < numberToGuess)
                {
                    Console.Write("Sorry, your guess is too low. ");
                }
                else
                {
                    Console.Write("Sorry, your guess is too high. ");
                }
                incorrectGuesses++;
            }
            if (!gameWin)
            {
                Console.WriteLine("Better luck next time, {0}! {1}'s number is {2}", player2Name, player1Name, numberToGuess);
            }
        }
    }
}
