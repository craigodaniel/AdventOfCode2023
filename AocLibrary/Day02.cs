using System.Diagnostics.CodeAnalysis;

namespace AocLibrary
{
    public class Day02
    {
        // --- Day 2: Cube Conundrum ---
        // https://adventofcode.com/2023/day/2
        // 
        //The Elf would first like to know which games would have been possible
        // if the bag contained only 12 red cubes, 13 green cubes, and 14 blue cubes?
        //
        // Try 1: 2095 is too low...FAIL because I compared >= max. should be > max.
        // Try 2: 2439  CORRECT!
        //
        // https://adventofcode.com/2023/day/2#part2
        //
        // Try 1: 63711 CORRECT!

        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_2_input.txt";
        public static string[] lines = File.ReadAllLines(fileDir + "\\" + fileName);
        public static void Part1()
        {
            /* // Test 
            string[] games = ["Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"];
            */

            // Real Input file
            string[] games = lines;

            int sum = 0;
            int legitCnt = 0;
            int maxRed = 12;
            int maxGreen = 13;
            int maxBlue = 14;

            foreach (string game in games)
            {
                int gameNbr = 0;      
                bool isLegit = true;

                bool isNumeric = int.TryParse(game.Split(":")[0].Split(" ")[1], out gameNbr);
                if (!isNumeric) { Console.WriteLine("Could not get Game # from: " + game); }

                Console.WriteLine("Game #: " + gameNbr.ToString());

                string[] handfulls = game.Split(":")[1].Split(";");
                foreach (string handfull in handfulls)
                {
                    if (!isLegit) { break; }

                    string[] results = handfull.Split(",");

                    foreach (string result in results)
                    {
                        if (!isLegit) { break; }

                        string trimmedResult = result.Trim();
                        int marbleCnt = 0;
                        isNumeric = int.TryParse(trimmedResult.Split(" ")[0], out marbleCnt);
                        if (!isNumeric) { Console.WriteLine("Could not get marble count from: " + result); }
                        string color = trimmedResult.Split(" ")[1];
                        //Console.WriteLine("color: " + color + ", marble count: " + marbleCnt.ToString());
                        
                        switch (color)
                        {
                            case "red":
                                if (marbleCnt > maxRed) 
                                { 
                                    isLegit = false;
                                    Console.WriteLine(result + " > maxRed " + maxRed.ToString());
                                }
                                else
                                {
                                    Console.WriteLine(result + " is less than maxRed " + maxRed.ToString());
                                }
                                break;

                            case "blue":
                                if (marbleCnt > maxBlue)
                                {
                                    isLegit = false;
                                    Console.WriteLine(result + " > maxBlue" + maxBlue.ToString());
                                }
                                else
                                {
                                    Console.WriteLine(result + " is less than maxBlue " + maxBlue.ToString());
                                }
                                break;
                            case "green":
                                if (marbleCnt > maxGreen)
                                {
                                    isLegit = false;
                                    Console.WriteLine(result + " > maxGreen " + maxGreen.ToString());
                                }
                                else
                                {
                                    Console.WriteLine(result + " is less than maxGreen " + maxGreen.ToString());
                                }
                                break;                           
                        }
                    }
                }
                
                if (isLegit)
                {
                    Console.WriteLine("Game # " + gameNbr + " is legit!");
                    sum += gameNbr;
                    legitCnt++;
                }
                else
                {
                    Console.WriteLine("Game # " + gameNbr + " is NOT legit!");
                }
            }

            Console.WriteLine("Legit Games: " + legitCnt.ToString());
            Console.WriteLine("Sum of Legit Game Nbrs: " + sum.ToString());

        }

        public static void Part2() 
        {
             // Test 
            /*
            string[] games = ["Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"];
            */

            // Real Input file
            string[] games = lines;

            int sumOfPowers= 0;

            foreach (string game in games)
            {
                int maxRed = 0;
                int maxBlue = 0;
                int maxGreen = 0;
                int power = 0;


                string[] handfulls = game.Split(":")[1].Split(";");
                foreach (string handfull in handfulls)
                {
                    string[] results = handfull.Split(",");
                    foreach (string result in results)
                    {
                        string trimmedResult = result.Trim();
                        int marbleCnt = 0;
                        bool isNumeric = int.TryParse(trimmedResult.Split(" ")[0], out marbleCnt);
                        if (!isNumeric) { Console.WriteLine("Could not get marble count from: " + result); }
                        string color = trimmedResult.Split(" ")[1];
                        //Console.WriteLine("color: " + color + ", marble count: " + marbleCnt.ToString());

                        switch (color)
                        {
                            case "red":
                                if (marbleCnt > maxRed)
                                {
                                    maxRed = marbleCnt;
                                }
                                break;

                            case "blue":
                                if (marbleCnt > maxBlue)
                                {
                                    maxBlue = marbleCnt;
                                }
                                break;
                            case "green":
                                if (marbleCnt > maxGreen)
                                {
                                    maxGreen = marbleCnt;
                                }
                                break;
                        }
                    }
                }

                power = maxRed * maxBlue * maxGreen;
                sumOfPowers += power;
                Console.WriteLine(game.Split(":")[0] + " : POWER = " + power.ToString());
            }

            
            Console.WriteLine("Sum of POWER: " + sumOfPowers.ToString());

        }
    }
}
