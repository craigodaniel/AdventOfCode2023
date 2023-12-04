using System;
using System.IO;

namespace AocLibrary
{
    public class Day03
    {
        // --- Day 3: Gear Ratios ---
        // https://adventofcode.com/2023/day/3

        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_3_input.txt";
        public static string[] lines = File.ReadAllLines(fileDir + "\\" + fileName);
        //public static string[] lines = { "467..114..", "...*......", "..35..633.", "......#...", "617*......", ".....+.58.", "..592.....", "......755.", "...$.*....", ".664.598.."};

        public static void Part1()
        {
            // create matrix from lines
            int width = lines[0].Length;
            int height = lines.Length;
            char[,] schematic = new char[height, width];
            int sum = 0;

            for(int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    schematic[y,x] = lines[y][x];
                }
            }

            // walk through matrix and find numbers and neighbors
            for(int y = 0;y < height; y++)
            {
                string nums = "";
                bool hasNeighborSymbol = false;
                for (int x = 0;x < width; x++)
                {
                    if (Char.IsNumber(schematic[y,x]))
                    {
                        if (nums == "") //new number found
                        {                            
                            if (x -1 >= 0) //checks left side neighbors
                            {
                                hasNeighborSymbol = FindNeigborSymbols(schematic, y, x - 1);
                            }                          
                        }

                        if (!hasNeighborSymbol) // checks above and below neighbors
                        {
                            hasNeighborSymbol = FindNeigborSymbols(schematic, y, x);
                        }
                        
                        if (!hasNeighborSymbol) 
                        {
                            if (x + 1 < width) // checks right side neigbors
                            {
                                hasNeighborSymbol = FindNeigborSymbols(schematic, y, x + 1);
                            }
                        }

                        nums += schematic[y,x].ToString();

                        if (x == width - 1) // found end of number at end of row
                        {
                            if (hasNeighborSymbol)
                            {
                                Console.WriteLine(nums + " is VALID!");
                                sum += int.Parse(nums);
                            }
                            else
                            {
                                Console.WriteLine(nums + " is INVALID!");
                            }

                            //reset variables
                            hasNeighborSymbol = false;
                            nums = "";
                        }
                    }
                    else if (nums != "") //found end of number
                    {
                        if(hasNeighborSymbol)
                        {
                            Console.WriteLine(nums + " is VALID!");
                            sum += int.Parse(nums);
                        }
                        else
                        {
                            Console.WriteLine(nums + " is INVALID!");
                        }

                        //reset variables
                        hasNeighborSymbol = false;
                        nums = "";
                    }
                }
            }

            // print answer
            Console.WriteLine("Sum of valid part numbers: " + sum.ToString());
        }
        public static void Part2() 
        {
            //Find numbers with neighbor '*'
            //add number to dictionary with '*' coord's as key
            // if dictionary already contains '*', multiply it's value with new number and add to total
            // this assumes each '*' has a max of 2 number neighbors!
            //
            // Try 1: 8603825 is too low!...check if any gears has more than 2 neighbors?
            // or...just update dict value with running power ratio ( dict val * new num)
            // but then...lose the info on which gears have 1 vs 2+ number neighbors...hmmm
            // nah silly goose...FindNeighborGearCoord method had errors!
            //
            // Try 2: 89471771 is CORRECT! Christmas is saved...



            // create matrix from lines
            int width = lines[0].Length;
            int height = lines.Length;
            char[,] schematic = new char[height, width];
            int sum = 0;

            //Dictionary of grid coord's and nums
            Dictionary<string,int> gears = new Dictionary<string,int>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    schematic[y, x] = lines[y][x];
                }
            }

            // walk through matrix and find numbers and neighbors
            for (int y = 0; y < height; y++)
            {
                string nums = "";
                string gearCoords = "";
                string temp = "";
                for (int x = 0; x < width; x++)
                {
                    if (Char.IsNumber(schematic[y, x]))
                    {
                        if (nums == "") //new number found
                        {
                            if (x - 1 >= 0) //checks left side neighbors
                            {
                                temp = FindNeigborGearCoord(schematic, y, x - 1);
                                if (temp != "")
                                {
                                    gearCoords += ";" + temp;
                                }
                                
                            }
                        }

                        temp = FindNeigborGearCoord(schematic, y, x); // checks above and below neighbors
                        if (temp != "")
                        {
                            gearCoords += ";" + temp;
                        }

                        if (x + 1 < width) // checks right side neigbors
                        {
                            temp = FindNeigborGearCoord(schematic, y, x + 1);
                            if (temp != "")
                            {
                                gearCoords += ";" + temp;
                            }
                        }

                        nums += schematic[y, x].ToString();

                        if (x == width - 1) // found end of number at end of row
                        {
                            if (gearCoords != "")
                            {
                                gearCoords = gearCoords.Remove(0, 1); // remove leading ";"
                                string[] gcArr = gearCoords.Split(';').Distinct().ToArray(); // remove duplicates
                                
                                foreach (string gc in gcArr)
                                {
                                    if (gears.ContainsKey(gc))
                                    {
                                        sum += gears[gc] * int.Parse(nums); //adds gear ratio to sum
                                    }
                                    else
                                    {
                                        gears.Add(gc, int.Parse(nums)); // adds k,v: gearCoord, number to dict
                                    }
                                }

                                Console.WriteLine(nums + " is VALID! gearCoords: " + gcArr);
                            }
                            else
                            {
                                Console.WriteLine(nums + " is INVALID!");
                            }

                            //reset variables
                            gearCoords = "";
                            nums = "";
                        }
                    }
                    else if (nums != "") //found end of number
                    {
                        if (gearCoords != "")
                        {
                            gearCoords = gearCoords.Remove(0, 1); // remove leading ";"
                            string[] gcArr = gearCoords.Split(';').Distinct().ToArray(); // remove duplicates

                            foreach (string gc in gcArr)
                            {
                                if (gears.ContainsKey(gc))
                                {
                                    sum += gears[gc] * int.Parse(nums); //adds gear ratio to sum
                                }
                                else
                                {
                                    gears.Add(gc, int.Parse(nums)); // adds k,v: gearCoord, number to dict
                                }
                            }

                            Console.WriteLine(nums + " is VALID! gearCoords:");
                            foreach (string gc in gcArr)
                            {
                                Console.WriteLine(gc);
                            }
                        }
                        else
                        {
                            Console.WriteLine(nums + " is INVALID!");
                        }

                        //reset variables
                        gearCoords = "";
                        nums = "";
                    }
                }
            }

            // print answer
            Console.WriteLine("Sum of valid part numbers: " + sum.ToString());

        }

        public static bool FindNeigborSymbols(char[,] schematic, int y, int x)
        {
            if (y - 1 >= schematic.GetLowerBound(1))
            {
                if (!Char.IsNumber(schematic[y - 1, x]) && !Char.Equals(schematic[y - 1, x], '.')) //checks top left neighbor
                {
                    return true;
                }
            }

            if (!Char.IsNumber(schematic[y, x]) && !Char.Equals(schematic[y, x], '.')) //checks center left neighbor
            {
                return true;
            }

            if (y + 1 <= schematic.GetUpperBound(1))
            {
                if (!Char.IsNumber(schematic[y + 1, x]) && !Char.Equals(schematic[y + 1, x], '.')) //checks bottom left neighbor
                {
                    return true;
                }
            }

            return false;
        }

        public static string FindNeigborGearCoord(char[,] schematic, int y, int x)
        {
            string gearCoords = "";

            if (y - 1 >= schematic.GetLowerBound(1))
            {
                if (Char.Equals(schematic[y - 1, x], '*')) //checks top left neighbor
                {
                    gearCoords += ";" + (y-1).ToString() + "," + x.ToString();
                }
            }

            if (Char.Equals(schematic[y, x], '*')) //checks center left neighbor
            {
                gearCoords += ";" + (y).ToString() + "," + x.ToString();
            }

            if (y + 1 <= schematic.GetUpperBound(1))
            {
                if (Char.Equals(schematic[y + 1, x], '*')) //checks bottom left neighbor
                {
                    gearCoords += ";" + (y + 1).ToString() + "," + x.ToString();
                }
            }

            if (gearCoords != "") //remove leading ";"
            {
                gearCoords = gearCoords.Remove(0, 1);
            }

            return gearCoords;
        }
    }
}
