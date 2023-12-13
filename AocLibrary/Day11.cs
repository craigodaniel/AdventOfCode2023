
using System.Diagnostics;

namespace AocLibrary
{
    public class Day11
    {
        // --- Day 11: Cosmic Expansion ---
        // https://adventofcode.com/2023/day/11
        //
        // Part 1: 9681886
        // Runtime: 9ms
        //
        // Part 2: 791134099634
        // Runtime: 10ms


        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_11_input.txt";
        //public static string fileName = "day_11_test_input.txt";
        public static string[] inputs = File.ReadAllLines(fileDir + "\\" + fileName);


        public static void Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            Int64 expansionFactor = 2;
            List<List<char>> universe = GetUniverseStart();
            List<Tuple<Int64, Int64>> galaxies = GetGalaxies(universe, GetRowExpansions(universe), GetColExpansions(universe), expansionFactor);
            Int64 sumLengths = 0;

            // Sum lengths between every galaxy
            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = 0; j < galaxies.Count; j++)
                {
                    if (i != j)
                    {
                        Int64 length = Math.Abs(galaxies[i].Item1 - galaxies[j].Item1) + Math.Abs(galaxies[i].Item2 - galaxies[j].Item2);
                        sumLengths += length;
                        //Console.WriteLine($"Between galaxy {i} and galaxy {j}: {length}");
                    }
                }
            }
            sumLengths = sumLengths / 2; // Because both i->j and j->i are counted

            sw.Stop();

            // Output
            Console.WriteLine($"Part 1: {sumLengths}");
            Console.WriteLine($"Runtime: {sw.ElapsedMilliseconds}ms");
        }


        public static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            Int64 expansionFactor = 1000000;
            List<List<char>> universe = GetUniverseStart();
            List<Tuple<Int64, Int64>> galaxies = GetGalaxies(universe,GetRowExpansions(universe),GetColExpansions(universe), expansionFactor);
            Int64 sumLengths = 0;

            // Sum lengths between every galaxy
            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = 0; j < galaxies.Count; j++)
                {
                    if (i != j)
                    {
                        Int64 length = Math.Abs(galaxies[i].Item1 - galaxies[j].Item1) + Math.Abs(galaxies[i].Item2 - galaxies[j].Item2);
                        sumLengths += length;
                        //Console.WriteLine($"Between galaxy {i} and galaxy {j}: {length}");
                    }
                }
            }
            sumLengths = sumLengths / 2; // Because both i->j and j->i are counted

            sw.Stop();

            // Output
            Console.WriteLine($"Part 2: {sumLengths}");
            Console.WriteLine($"Runtime: {sw.ElapsedMilliseconds}ms");
        }

        private static List<List<char>> GetUniverseStart()
        {
            List<List<char>> universe = new List<List<char>>();

            for (int row = 0; row < inputs.Length; row++)
            {
                List<char> chars = new List<char>();

                for (int col = 0; col < inputs[0].Length; col++)
                {
                    chars.Add(inputs[row][col]);
                }

                universe.Add(chars);
            }

            return universe;
        }

        private static List<Int64> GetRowExpansions(List<List<char>> universe)
        {
            List<Int64> rowExpansions = new List<Int64>();
            
            for (int row = 0; row < universe.Count; row++)
            {
                bool isEmpty = true;
                for (int col = 0; col < universe[row].Count; col++)
                {
                    if (universe[row][col] != '.')
                    {
                        isEmpty = false;
                    }
                }

                if (isEmpty)
                {
                    rowExpansions.Add(row);
                }
            }

            return rowExpansions;
        }

        private static List<Int64> GetColExpansions(List<List<char>> universe)
        {
            List<Int64> colExpansions = new List<Int64>();

            for (int col = 0; col < universe[0].Count; col++)
            {
                bool isEmpty = true;
                for (int row = 0; row < universe.Count; row++)
                {
                    if (universe[row][col] != '.')
                    {
                        isEmpty = false;
                    }
                }

                if (isEmpty)
                {
                    colExpansions.Add(col);
                }
            }

            return colExpansions;
        }

        private static List<Tuple<Int64, Int64>> GetGalaxies(List<List<char>> universe, List<Int64> rowExpansions, List<Int64> colExpansions, Int64 expansionFactor)
        {
            List<Tuple<Int64, Int64>> galaxies = new List<Tuple<Int64, Int64>>();

            for (int row = 0; row < universe.Count; row++)
            {
                for (int col = 0; col < universe[row].Count; col++)
                {
                    if (universe[row][col] == '#')
                    {
                        Int64 rowBuffer = 0;
                        Int64 colBuffer = 0;

                        foreach (Int64 rowExpansion in rowExpansions)
                        {
                            if (rowExpansion < row) { rowBuffer += expansionFactor - 1; }
                        }

                        foreach (Int64 colExpansion in colExpansions)
                        {
                            if (colExpansion < col) { colBuffer += expansionFactor - 1; }
                        }                        
                        
                        galaxies.Add(Tuple.Create(row + rowBuffer, col + colBuffer));
                    }
                }
            }

            return galaxies;
        }

        private static void PrintUniverse(List<List<char>> universe)
        {
            // Print universe
            foreach (List<char> chars in universe)
            {
                string line = "";
                foreach (char c in chars)
                {
                    line += c;
                }
                Console.WriteLine(line);
            }
        }
    }
}
