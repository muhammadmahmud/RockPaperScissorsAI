using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissorsAI
{
    enum Move
    {
        Rock,
        Paper,
        Scissors
    }

    class Program
    {
        static List<Move> playerHistory = new List<Move>();
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Rock-Paper-Scissors AI ===");
            Console.WriteLine("Type 'rock', 'paper', or 'scissors'. Type 'exit' to quit.");

            while (true)
            {
                Console.Write("\nYour move: ");
                string input = Console.ReadLine().ToLower();

                if (input == "exit") break;

                if (!TryParseMove(input, out Move playerMove))
                {
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
                }

                Move aiMove = GetAIMove();
                Console.WriteLine($"Computer played: {aiMove}");

                string result = GetResult(playerMove, aiMove);
                Console.WriteLine(result);

                playerHistory.Add(playerMove);
            }

            Console.WriteLine("\nGame over. Thanks for playing!");
        }

        static bool TryParseMove(string input, out Move move)
        {
            switch (input)
            {
                case "rock":
                    move = Move.Rock;
                    return true;
                case "paper":
                    move = Move.Paper;
                    return true;
                case "scissors":
                    move = Move.Scissors;
                    return true;
                default:
                    move = Move.Rock;
                    return false;
            }
        }

        static Move GetAIMove()
        {
            if (playerHistory.Count < 3)
            {
                // Use random strategy for first few moves
                return (Move)random.Next(3);
            }

            // Analyze player's most common move
            var mostCommonPlayerMove = playerHistory
                .GroupBy(m => m)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;

            // Return the move that beats the most common player move
            return mostCommonPlayerMove switch
            {
                Move.Rock => Move.Paper,       // Paper beats Rock
                Move.Paper => Move.Scissors,   // Scissors beats Paper
                Move.Scissors => Move.Rock,    // Rock beats Scissors
                _ => (Move)random.Next(3)
            };
        }

        static string GetResult(Move player, Move ai)
        {
            if (player == ai) return "It's a draw!";
            if ((player == Move.Rock && ai == Move.Scissors) ||
                (player == Move.Paper && ai == Move.Rock) ||
                (player == Move.Scissors && ai == Move.Paper))
                return "You win!";
            return "Computer wins!";
        }
    }
}
