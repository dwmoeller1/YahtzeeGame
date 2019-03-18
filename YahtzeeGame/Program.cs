using System;
using System.Collections.Generic;
using System.Linq;

namespace YahtzeeGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int playerScore = GetPlayerGameScore();
            int computerScore = GetComputerGameScore();

            string winner;

            if (playerScore >= computerScore)
            {
                winner = "player";
            }
            else
            {
                winner = "computer";
            }

            Console.Write($"Winner is {winner}");

            Console.ReadLine();
        }

        /// <summary>
        /// method that does all calculations for player and returns final score
        /// </summary>
        /// <returns></returns>
        public static int GetPlayerGameScore()
        {
            int initialAmountOfDice = 5;
            int finalScore = 0;

            int[] fiveDiceFirstRound = { new Random().Next(1, 7), new Random().Next(1, 7), new Random().Next(1, 7), new Random().Next(1, 7), new Random().Next(1, 7) };

            string output = String.Join(", ", fiveDiceFirstRound);
            Console.Write($"Choose dice you want to keep from following: {output} numbers: ");

            string chosenDice = Console.ReadLine();
            string[] firstArrayOfNumbers = chosenDice.Split(", ");

            if (firstArrayOfNumbers.Length == initialAmountOfDice)
            {
                GetScore(firstArrayOfNumbers);
            }

            int secondRoundLength = initialAmountOfDice - firstArrayOfNumbers.Length;

            if (secondRoundLength == 1)
            {
                finalScore = CollectResults(firstArrayOfNumbers);
            }

            string output2 = RollDice(secondRoundLength);
            Console.Write($"Choose dice you want to keep from following {output2} numbers: ");

            string chosenDiceTwo = Console.ReadLine();
            string[] secondArrayOfNumbers = chosenDiceTwo.Split(", ");

            int thirdRoundLength = secondRoundLength - secondArrayOfNumbers.Length;

            if (thirdRoundLength == 0)
            {
                finalScore = GetScore(secondArrayOfNumbers);
                return finalScore;
            }

            if (thirdRoundLength == 1)
            {
                finalScore = CollectResults(firstArrayOfNumbers, secondArrayOfNumbers);
                return finalScore;
            }

            string output4 = RollDice(thirdRoundLength);
            Console.Write($"Choose dice you want to keep from following {output4} numbers: ");

            string chosenDiceThree = Console.ReadLine();
            string[] thirdArrayOfNumbers = chosenDiceThree.Split(", ");

            int fourthRoundLength = thirdRoundLength - thirdArrayOfNumbers.Length;

            if (fourthRoundLength == 0)
            {
                finalScore = GetScore(secondArrayOfNumbers);
                return finalScore;
            }

            if (fourthRoundLength == 1)
            {
                finalScore = CollectResults(firstArrayOfNumbers, secondArrayOfNumbers, thirdArrayOfNumbers);
                return finalScore;
            }

            return finalScore;
        }

        /// <summary>
        /// method that rolls dice and create  next round of available dice options
        /// </summary>
        /// <param name="roundLength"></param>
        /// <returns></returns>
        public static string RollDice(int roundLength)
        {
            int[] nextRoundOfDice = new int[roundLength];
            int randomNumber = new Random().Next(1, 7);

            for (int i = 0; i < roundLength; i++)
            {
                int number = new Random().Next(1, 7);
                nextRoundOfDice[i] = number;
            }
            return String.Join(", ", nextRoundOfDice);
        }

        /// <summary>
        /// returns score for specific dice round
        /// </summary>
        /// <param name="arrayOfNumbers"></param>
        /// <returns></returns>
        public static int GetScore(string[] arrayOfNumbers)
        {
            int maxAmount = arrayOfNumbers.GroupBy(i => i).Max(group => group.Count());

            Console.Write($"Your score is {maxAmount}  \n");
            Console.ReadLine();

            return maxAmount;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="firstArrayOfNumbers"></param>
        /// <param name="secondArrayOfNumbers"></param>
        /// <param name="thirdArrayOfNumbers"></param>
        /// <returns></returns>
        public static int CollectResults(string[] firstArrayOfNumbers, string[] secondArrayOfNumbers = null, string[] thirdArrayOfNumbers = null)
        {
            int lastDiceNumber = new Random().Next(1, 7);
            Console.Write($"Your last dice number is: {lastDiceNumber}");
            Console.Read();

            string[] lastArrayOfNumbers = new string[1];
            lastArrayOfNumbers[0] = lastDiceNumber.ToString();

            if (secondArrayOfNumbers != null && thirdArrayOfNumbers == null)
            {
                var arr = secondArrayOfNumbers.Concat(lastArrayOfNumbers).ToArray();
                var arr2 = arr.Concat(firstArrayOfNumbers).ToArray();

                return GetScore(arr2);
            }

            if (thirdArrayOfNumbers != null)
            {
                var arr = secondArrayOfNumbers.Concat(lastArrayOfNumbers).ToArray();
                var arr2 = arr.Concat(firstArrayOfNumbers).ToArray();
                var arr4 = arr2.Concat(thirdArrayOfNumbers).ToArray();

                return GetScore(arr4);
            }

            var arr3 = firstArrayOfNumbers.Concat(lastArrayOfNumbers).ToArray();

            return GetScore(arr3);
        }

        /// <summary>
        /// method to generate three round of dices and get final score
        /// </summary>
        public static int GetComputerGameScore()
        {
            int[] totalScore = new int[3];

            for (int i = 0; i < totalScore.Length; i++)
            {
                string roll = RollDice(5);
                totalScore[i] = GetComputerTotalScore(roll);
            }

            Console.Write($"Computer's score is {totalScore.Max()}  \n");

            Console.ReadLine();

            return totalScore.Max();
        }

        /// <summary>
        /// roll numbers three times and return score for specific round of dice
        /// </summary>
        /// <param name="rollNumbers"></param>
        /// <returns></returns>
        public static int GetComputerTotalScore(string rollNumbers)
        {
            Console.Write($"Computers first roll of dice:  {rollNumbers} \n");
            Console.Read();
            string[] computerFirstDice = rollNumbers.Split(", ");

            return GetScore(computerFirstDice);
        }
    }
}