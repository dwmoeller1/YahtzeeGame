using System;
using System.Collections.Generic;
using System.Linq;

namespace YahtzeeGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int playerScore = PlayerGameScore();
            int computerScore = ComputerGameScore();

            string winner;

            if (playerScore > computerScore || playerScore == computerScore)
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

        public static int PlayerGameScore()
        {
            int initialAmountOfDice = 5;

            int finalScore = 0;

            //first round
            int[] fiveDiceFirstRound = { new Random().Next(1, 7), new Random().Next(1, 7), new Random().Next(1, 7), new Random().Next(1, 7), new Random().Next(1, 7) };

            string output = String.Join(", ", fiveDiceFirstRound);
            Console.Write($"Choose dice you want to keep from following: {output} numbers: ");

            string chosenDice = Console.ReadLine();
            string[] firstArrayOfNumbers = chosenDice.Split(",");

            if (firstArrayOfNumbers.Length == initialAmountOfDice)
            {
                getScore(firstArrayOfNumbers);
            }

            int secondRoundLength = initialAmountOfDice - firstArrayOfNumbers.Length;

            //second round

            if (secondRoundLength == 1)
            {
                finalScore = CollectResults(firstArrayOfNumbers);
            }

            string output2 = RollDice(secondRoundLength);
            Console.Write($"Choose dice you want to keep from following {output2} numbers: ");

            // add check to make sure those numbers are in the array
            string chosenDiceTwo = Console.ReadLine();
            string[] secondArrayOfNumbers = chosenDiceTwo.Split(",");

            //third round
            int thirdRoundLength = secondRoundLength - secondArrayOfNumbers.Length;

            if (thirdRoundLength == 1)
            {
                finalScore = CollectResults(firstArrayOfNumbers, secondArrayOfNumbers);
            }

            if (thirdRoundLength == 0)
            {
                finalScore = getScore(secondArrayOfNumbers);
            }

            return finalScore;
        }

        /// <summary>
        ///
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
        ///
        /// </summary>
        /// <param name="arrayOfNumbers"></param>
        /// <returns></returns>
        public static int getScore(string[] arrayOfNumbers)
        {
            var groups = arrayOfNumbers.GroupBy(i => i).Select(i => new { Word = i.Key, Count = i.Count() });

            List<int> maxAmount = new List<int>();
            foreach (var group in groups)
            {
                // Console.WriteLine($"Number: {group.Key} Count:{group.Count()}");
                maxAmount.Add(group.Count);
            }

            Console.Write($"Your score is {maxAmount.Max()}  \n");

            Console.ReadLine();

            return maxAmount.Max();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="firstArrayOfNumbers"></param>
        /// <param name="secondArrayOfNumbers"></param>
        /// <returns></returns>
        public static int CollectResults(string[] firstArrayOfNumbers, string[] secondArrayOfNumbers = null)
        {
            int lastDiceNumber = new Random().Next(1, 7);
            Console.Write($"Your last dice number is: {lastDiceNumber}");
            Console.Read();

            string[] lastArrayOfNumbers = new string[1];
            lastArrayOfNumbers[0] = lastDiceNumber.ToString();

            if (secondArrayOfNumbers != null)
            {
                var arr = secondArrayOfNumbers.Concat(lastArrayOfNumbers).ToArray();
                var arr2 = arr.Concat(firstArrayOfNumbers).ToArray();

                return getScore(arr2);
            }

            var arr3 = firstArrayOfNumbers.Concat(lastArrayOfNumbers).ToArray();

            return getScore(arr3);
        }

        /// <summary>
        ///
        /// </summary>
        public static int ComputerGameScore()
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

        public static int GetComputerTotalScore(string rollNumbers)
        {
            Console.Write($"Computers first roll of dice:  {rollNumbers} \n");
            Console.Read();
            string[] computerFirstDice = rollNumbers.Split(",");

            return getScore(computerFirstDice);
        }
    }
}