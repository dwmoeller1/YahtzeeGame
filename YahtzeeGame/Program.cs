using System;
using System.Collections.Generic;
using System.Linq;

namespace YahtzeeGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int var = PlayerGame();
            Console.Write($"Your score is {var}");

            Console.ReadLine();
        }

        public static int PlayerGame()
        {
            int initialAmountOfDice = 5;

            //first round
            int[] fiveDiceFirstRound = { 4, 6, 5, 2, 2 };

            string output = String.Join(", ", fiveDiceFirstRound);
            Console.Write($"Choose dice you want to keep from following: {output} ");

            string chosenDice = Console.ReadLine();
            string[] firstArrayOfNumbers = chosenDice.Split(",");

            if (firstArrayOfNumbers.Length == initialAmountOfDice)
            {
                return getScore(firstArrayOfNumbers);
            }

            int secondRoundLength = initialAmountOfDice - firstArrayOfNumbers.Length;

            //second round

            if (secondRoundLength == 1)
            {
                int lastDiceNumber = new Random().Next(0, 7);
                Console.Write($"Your last dice number is: {lastDiceNumber} ");
                Console.Read();
                string[] lastArrayOfNumbers = new string[1];
                lastArrayOfNumbers[0] = lastDiceNumber.ToString();
                var arr = firstArrayOfNumbers.Concat(lastArrayOfNumbers).ToArray();
                return getScore(arr);
            }

            string output2 = RollDice(secondRoundLength);
            Console.Write($"Choose dice you want to keep from following {output2} numbers: ");

            // add check to make sure those numbers are in the array
            string chosenDiceTwo = Console.ReadLine();
            string[] secondArrayOfNumbers = chosenDiceTwo.Split(",");

            //third round
            int thirdRoundLength = secondRoundLength - secondArrayOfNumbers.Length;
            string output3 = RollDice(thirdRoundLength);

            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roundLength"></param>
        /// <returns></returns>
        public static string RollDice(int roundLength)
        {
            int[] nextRoundOfDice = new int[roundLength];
            int randomNumber = new Random().Next(0, 7);

            for (int i = 0; i < roundLength; i++)
            {
                int number = randomNumber;
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
            var groups = arrayOfNumbers.GroupBy(v => v);
            int[] maxAmount = new int[5];
            int i = 0;
            foreach (var group in groups)
            {
                maxAmount[i] = group.Count();
                i++;
            }

            return maxAmount.Max();
        }
    }
}