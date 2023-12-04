using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AocLibrary
{
    public class Day04
    {
        // --- Day 4: Scratchcards ---
        // https://adventofcode.com/2023/day/4
        //
        // Try 1: 9211307 too high. Possible winning # occurs more than once?
        // Bug: matches spaces Bugfix: check if value is number first.
        // 
        // Try 2: 21088 Correct!
        //
        // Part 2:
        // Try 1: 898 too low. forgot to push reward cards to stack.
        //
        // Try 2: 6874754 Correct!
        //
        // Refactor: created int array with all card results...
        // Mitigated electric bill >90%+, room temp decreased 3.14159 C.
        //
        //
        // Refactor: Mitigated electric bill >90%+, room temp decreased 3.14159 C.

        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_4_input.txt";
        //public static string fileName = "day_4_input_test.txt";
        public static string[] lines = File.ReadAllLines(fileDir + "\\" + fileName);

        public static void Part1()
        {
            int pointsTotal = 0;

            foreach (string line in lines)
            {
                int points = 0;
                string matches = "";
                string[] winNums = line.Split(":")[1].Split("|")[0].Trim().Split(" ");
                string[] playNums = line.Split(":")[1].Split("|")[1].Trim().Split(" ");

                Console.WriteLine(line);               

                foreach (string playNum in playNums) 
                {
                    foreach (string winNum in winNums)
                    {
                        if (winNum.Trim() ==  playNum.Trim() && int.TryParse(winNum,out int _))
                        {
                            if (points > 0)
                            {
                                points = points * 2;
                                matches += " " + winNum;
                            }
                            else if (points == 0)
                            {
                                points = 1;
                                matches = winNum;
                            }
                            else
                            {
                                Console.Write($"ERROR! Points less than 0?? points: {points}");
                            }
                        }
                    }
                }

                Console.WriteLine($"Matches: {matches}");
                Console.WriteLine($"Line points: {points}");
                pointsTotal += points;
            }

            Console.WriteLine($"Points total: {pointsTotal}");
        }

        public static void Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int cardsTotal = 0;
            Stack<int> cardStack = new Stack<int>();
            int[] cardResults = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                cardStack.Push(i);
                string[] winNums = lines[i].Split(":")[1].Split("|")[0].Trim().Split(" ");
                string[] playNums = lines[i].Split(":")[1].Split("|")[1].Trim().Split(" ");
                cardResults[i] = GetMatchCnt(winNums, playNums);
            }

            while (cardStack.Count > 0)
            {
                int cardIndex = cardStack.Pop();
                int matchCnt = cardResults[cardIndex];

                if (matchCnt > 0)
                {
                    if (cardIndex + matchCnt > lines.Length - 1) // protect from pushing index outside of lines array
                    {
                        matchCnt = lines.Length - 1 - cardIndex;
                    }
                    for (int i = 1;i <= matchCnt; i++)
                    {
                        cardStack.Push(cardIndex + i);
                    }
                }

                cardsTotal++;
                //Console.WriteLine($"Stack size: {cardStack.Count}    Cards Played: {cardsTotal}");
            }

            sw.Stop();
            Console.WriteLine($"Cards Played: {cardsTotal}");
            Console.WriteLine($"Run time (ms): {sw.ElapsedMilliseconds}");
        }

        private static int GetMatchCnt(string[] winNums, string[] playNums)
        {
            int matchCnt = 0;

            foreach (string playNum in playNums)
            {
                foreach (string winNum in winNums)
                {
                    if (winNum.Trim() == playNum.Trim() && int.TryParse(winNum, out int _))
                    {
                        matchCnt++;
                    }
                }
            }

            return matchCnt;
        }
    }
}
