
// uses ReplaceAt method from Helper.cs

using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AocLibrary
{
    public class Day12
    {
        // --- Day 12: Hot Springs ---
        // https://adventofcode.com/2023/day/12
        //
        // Part 1: 7402
        // Runtime: 17536ms
        //
        // Part 2: using the brute force approach from part 1
        // I estimate will take ~10,000 x Age of Universe to compute... :(
        //
        // ~~~After memoization~~~
        // Part 1: 7402
        // Runtime: 63ms
        //
        // Part 2: 3384337640277
        // Runtime: 185ms



        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_12_input.txt";
        //public static string fileName = "day_12_test_input.txt";
        public static string[] inputs = File.ReadAllLines(fileDir + "\\" + fileName);

        private static Dictionary<Tuple<string, int>, bool> cache = new Dictionary<Tuple<string, int>, bool>();
        private static Dictionary<string, Int64> cachePermCnt2 = new Dictionary<string, Int64>();

        private static Int64 cacheHits = 0;
        private static Int64 cacheMiss = 0;
        public static void Part1()
        {

            Stopwatch sw = Stopwatch.StartNew();

            bool showDebug = false;
            Int64 sumOfCounts = 0;

            int inputsLen = inputs.Length;

            for (int i = 0; i < inputsLen; i++)
            {
                if (i == 375)
                {
                    bool debug = true;
                }

                string springs = GetSprings(inputs[i]);
                springs = "." + springs + ".";

                List<int> sizes = GetGroupSizes(inputs[i]);
                Int64 permutations = 1;

                string leftBound = LeftBound(springs, sizes);
                string rightBound = RightBound(springs, sizes);

                // List of group ranges
                List<Tuple<int, int>> groupRanges = GroupRanges(springs, sizes);

                List<int> subSizes = new List<int>();
                string subSprings = "";
                int startIndex = -1;
                int length = -1;

                // loop through each group
                for (int group = 0; group < groupRanges.Count; group++)
                {
                    bool isOverlapping = false;
                    if (group < groupRanges.Count - 1)
                    {
                        if (groupRanges[group].Item2 >= groupRanges[group + 1].Item1 - 1)
                        {
                            isOverlapping = true;
                        }
                    }

                    if (startIndex == -1) { startIndex = groupRanges[group].Item1; }
                    subSizes.Add(sizes[group]);

                    if (!isOverlapping)
                    {
                        length = groupRanges[group].Item2 - startIndex + 1; //do i need + 1?
                        //subSprings = "." + springs.Substring(startIndex, length) + ".";
                        subSprings = springs.Substring(startIndex, length);
                        Int64 countPerm = 0;
                        countPerm = CountPermutations(subSprings, subSizes, 0);
                        permutations *= countPerm;

                        startIndex = length = -1;
                        subSprings = "";
                        subSizes.Clear();
                    }

                }


                
                sumOfCounts += permutations;
                //Console.WriteLine($"{i + 1}: {permutations} {sumOfCounts}");

            }

            sw.Stop();

            Console.WriteLine($"Part 1: {sumOfCounts}");
            Console.WriteLine($"Runtime: {sw.Elapsed}");
        }
        public static void Part2()
        {

            Stopwatch sw = Stopwatch.StartNew();

            Int64 sumOfCounts = 0;

            int inputsLen = inputs.Length;

            for (int i = 0; i < inputsLen; i++)
            {
                string springs = GetSpringsExpanded(inputs[i]);
                springs = "." + springs + ".";

                List<int> sizes = GetGroupSizesExpanded(inputs[i]);
                Int64 permutations = 1;

                string leftBound = LeftBound(springs, sizes);
                string rightBound = RightBound(springs, sizes);

                // List of group ranges
                List<Tuple<int,int>> groupRanges = GroupRanges(springs, sizes);

                List<int> subSizes = new List<int>();
                string subSprings = "";
                int startIndex = -1;
                int length = -1;

                // loop through each group
                for (int group = 0; group < groupRanges.Count; group++)
                {
                    bool isOverlapping = false;
                    if (group < groupRanges.Count - 1)
                    {
                        if (groupRanges[group].Item2 >= groupRanges[group + 1].Item1 - 1)
                        {
                            isOverlapping = true;
                        }
                    }

                    if (startIndex == -1) { startIndex = groupRanges[group].Item1; }
                    subSizes.Add(sizes[group]);

                    if (!isOverlapping)
                    {
                        length = groupRanges[group].Item2 - startIndex + 1; //do i need + 1?
                        //subSprings = "." + springs.Substring(startIndex, length) + ".";
                        subSprings = springs.Substring(startIndex, length);
                        Int64 countPerm = 0;
                        countPerm = CountPermutations(subSprings, subSizes, 0);
                        permutations *= countPerm;

                        startIndex = length = -1;
                        subSprings = "";
                        subSizes.Clear();
                    }

                }

                sumOfCounts += permutations;

            }

            sw.Stop();

            Console.WriteLine($"Part 2: {sumOfCounts}");
            Console.WriteLine($"Runtime: {sw.Elapsed}");
        }



        private static string GetSprings(string line)
        {
            string springs = line.Split(' ')[0];
            return springs;
        }

        private static string GetSpringsExpanded(string line)
        {
            string springs = line.Split(' ')[0];
            for (int i = 0; i < 4; i++)
            {
                springs += "?" + line.Split(' ')[0];
            }
            return springs;
        }

        private static List<int> GetGroupSizes(string line)
        {
            List<int> sizes = new List<int>();
            string[] temp = line.Split(' ')[1].Split(',');
            foreach (string size in temp)
            {
                sizes.Add(int.Parse(size));
            }

            return sizes;
        }

        private static List<int> GetGroupSizesExpanded(string line)
        {
            List<int> sizes = new List<int>();


            string expanded = line.Split(' ')[1];

            for (int i = 0; i < 4; i++)
            {
                expanded += "," + line.Split(' ')[1];
            }


            string[] expandedArr = expanded.Split(',');

            foreach (string size in expandedArr)
            {
                sizes.Add(int.Parse(size));
            }

            return sizes;
        }

        private static bool CanFit(string substring, int size)
        {
            Tuple<string,int> key = Tuple.Create(substring, size);
            if (cache.ContainsKey(key))
            {
                cacheHits++;
                return cache[key];
            }
            else
            {
                if (substring[0] != '#' && substring[substring.Length - 1] != '#')
                {
                    for (int i = 1; i < substring.Length - 1; i++)
                    {
                        if (substring[i] == '.')
                        {
                            cache.Add(key, false);
                            cacheMiss++;
                            return false;
                        }
                    }
                    cache.Add(key,true);
                    cacheMiss++;
                    return true;
                }
                cache.Add(key, false);
                cacheMiss++;
                return false;
            }
            
        }

        private static string LeftBound(string springs, List<int> sizes)
        {
            string leftBound = springs;
            string original = leftBound;

            int index = 0;
            for (int i = 0; i < sizes.Count; i++)
            {

                for (int j = index; j <= leftBound.Length - sizes[i] - 2; j++)
                {
                    
                    if (CanFit(leftBound.Substring(j, sizes[i] + 2), sizes[i]))
                    {
                        
                        int n = i - 1;
                        int lastChar = n + 65; //represents 'A'-'Z'
                        if (lastChar > 90) { lastChar += 6; } //represents 'a'+
                        int lastIndex = (n >= 0) ? leftBound.IndexOf((char)lastChar) : 0;                        
                        
                        // If uncovered # found between last group and this group, recursively shift groups right
                        if (leftBound.Substring(lastIndex, j - lastIndex < 0 ? 0 : j - lastIndex).Contains('#'))
                        {
                            leftBound = leftBound.Substring(0, lastIndex) + original.Substring(lastIndex);
                            j = lastIndex;
                            i = i - 1;
                            if (i < 0)
                            {
                                return ""; // 0 permutations possible
                            }
                            continue;
                        }

                        // If uncovered # found at end of line, try shift last group right
                        int lastIndexofSizes = sizes.Count - 1;
                        int indexAfterGroup = j + sizes[i] + 1;
                        if (i == lastIndexofSizes && leftBound.Substring(indexAfterGroup).Contains('#'))
                        { 
                            continue;
                        }

                        // Found valid leftmost position for group
                        index = j + sizes[i] + 1;
                        int fillChar = i + 65; //represents 'A'-'Z'
                        if (fillChar > 90) { fillChar += 6; } //represents 'a'+

                        for (int k = j + 1; k < j + sizes[i] +1; k++)
                        {
                            leftBound = leftBound.ReplaceAt(k, (char)fillChar);
                        }
                        
                        break;
                    }
                }
            }




            return leftBound;
        }

        private static string RightBound(string springs, List<int> sizes)
        {
            string rightBound = ReverseStringOrder(springs);
            string original = rightBound;

            int index = 0;
            for (int i = sizes.Count -1; i>=0; i--)
            { 

                for (int j = index; j <= rightBound.Length - sizes[i] - 2; j++)
                {
                    if (CanFit(rightBound.Substring(j, sizes[i] + 2), sizes[i]))
                    {

                        int n = i + 1;
                        int firstN = sizes.Count;
                        int lastChar = n + 65; //represents 'A'-'Z'
                        if (lastChar > 90) { lastChar += 6; } //represents 'a'+
                        int lastIndex = (n < firstN) ? rightBound.IndexOf((char)lastChar) : 0;

                        // If uncovered # found between last group and this group, recursively shift groups right
                        if (rightBound.Substring(lastIndex, j - lastIndex < 0 ? 0 : j - lastIndex).Contains('#'))
                        {
                            rightBound = rightBound.Substring(0, lastIndex) + original.Substring(lastIndex);
                            j = lastIndex;
                            i = i + 1;
                            if (i > sizes.Count - 1)
                            {
                                return ""; // 0 permutations possible
                            }
                            continue;
                        }

                        // If uncovered # found at end of line, try shift last group right
                        int indexAfterGroup = j + sizes[i] + 1;
                        if (i == 0 && rightBound.Substring(indexAfterGroup).Contains('#'))
                        {
                            continue;
                        }


                        // Found valid rightmost position for group
                        index = j + sizes[i] + 1;
                        int fillChar = i + 65; //represents 'A'-'Z'
                        if (fillChar > 90) { fillChar += 6; } //represents 'a'+

                        for (int k = j + 1; k < j + sizes[i] + 1; k++)
                        {
                            rightBound = rightBound.ReplaceAt(k, (char)fillChar);
                            
                        }

                        break;
                    }
                }
            }

            rightBound = ReverseStringOrder(rightBound);
            return rightBound;
        }

        private static string ReverseStringOrder(string s)
        {
            string reversed = "";

            for (int i = s.Length - 1; i >= 0; i--)
            {
                reversed += s[i];
            }

            return reversed;
        }

        private static List<Tuple<int,int>> GroupRanges(string springs, List<int> sizes)
        {
            string leftBound = LeftBound(springs, sizes);
            string rightBound = RightBound(springs, sizes);
            List<Tuple<int,int>> groupRanges = new List<Tuple<int,int>>();
            
            for (int i = 0; i < sizes.Count; i++)
            {
                int charX = i + 65; //represents 'A'-'Z'
                if (charX > 90) { charX += 6; } //represents 'a'+
                int x = leftBound.IndexOf((char)charX);
                int y = rightBound.LastIndexOf((char)charX);

                groupRanges.Add(Tuple.Create(x, y));
            }

            return groupRanges;
        }

        private static Int64 CountPermutations(string springs, List<int> sizes, Int64 count)
        {
            

            if (String.IsNullOrEmpty(springs))
            {
                if (sizes.Count > 0)
                {
                    return count;
                }
                else
                {
                    return count + 1;
                }
            }
            else if (sizes.Count == 0)
            {
                if (springs.Contains('#'))
                {
                    return count;
                }
                else
                {
                    return count + 1;
                }
            }

            string key = springs;

            for (int i = 0; i < sizes.Count; i++)
            {
                key += "," + sizes[i].ToString();
            }

            if (cachePermCnt2.ContainsKey(key))
            {
                cacheHits++;
                return cachePermCnt2[key];
            }

            List<int> thisSizes = new List<int>();
            foreach (int size in sizes)
            {
                thisSizes.Add(size);
            }
            
            switch (springs[0])
            {
                case '.':
                    // discard the '.' and recursively check again
                    string shortersprings = springs.Substring(1);
                    count = CountPermutations(shortersprings, thisSizes, count);
                    break;
                case '?':
                    // replace the ? with a . and recursively check again,
                    // AND replace it with a # and recursively check again.
                    string damaged = "." + springs.Substring(1);
                    string good = "#" + springs.Substring(1);
                    count = CountPermutations(damaged, thisSizes, count) + CountPermutations(good, thisSizes,count);
                    break;
                case '#':
                    //check if it is long enough for the first group,
                    //check if all characters in the first [grouplength] characters are not '.',
                    //check if next char after first [grouplength] characters not '#',
                    //and then remove the first [grouplength] chars
                    //and the first group number, recursively check again.
                    if (springs.Length == thisSizes[0]) 
                    {
                        if (!springs.Substring(0, thisSizes[0]).Contains('.'))
                        {
                            string remainingSprings = springs.Substring(thisSizes[0]);
                            thisSizes.RemoveAt(0);
                            count = CountPermutations(remainingSprings, thisSizes, count);
                        }
                    }
                    else if (springs.Length >= thisSizes[0] + 1)
                    {
                        if (!springs.Substring(0, thisSizes[0]).Contains('.') && springs[thisSizes[0]] != '#')
                        {
                            string remainingSprings = springs.Substring(thisSizes[0] +1);
                            thisSizes.RemoveAt(0);
                            count = CountPermutations(remainingSprings, thisSizes, count);
                        }
                    }
                    break;
            }

            cacheMiss++;
            cachePermCnt2[key] = count;

            return count;
        }
    }
}
