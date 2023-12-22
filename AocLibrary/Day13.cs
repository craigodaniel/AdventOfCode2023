using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocLibrary
{
    public class Day13
    {
        // --- Day 13: Point of Incidence ---
        // https://adventofcode.com/2023/day/13
        //
        // Part 1: 35210
        // Runtime: 00:00:00.0005722


        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_13_input.txt";
        //public static string fileName = "day_13_test_input.txt";
        public static string[] inputs = File.ReadAllLines(fileDir + "\\" + fileName);



        public static void Part1()
        {

            Stopwatch sw = Stopwatch.StartNew();
            List<string[]> patterns = GetListOfPatterns(inputs);
            int answer = 0;

            foreach (string[] pattern in patterns)
            {
                answer += GetColCntLeftOfMirrorOfPattern(pattern) + (100 * GetLinesAboveReflection(pattern));
            }


            sw.Stop();
            
            Console.WriteLine($"Part 1: {answer}");
            Console.WriteLine($"Runtime: {sw.Elapsed}");
        }


        public static void Part2()
        {

        }

        private static List<string[]> GetListOfPatterns(string[] fullInput)
        {
            List<string> temp = new List<string>();
            List<string[]> patterns = new List<string[]>();


            foreach (string line in fullInput)
            {
                if (String.IsNullOrEmpty(line))
                {
                    string[] pattern = temp.ToArray();
                    
                    patterns.Add(pattern);
                    temp.Clear();
                }
                else
                {
                    temp.Add(line);
                }
            }

            // don't forget last pattern
            string[] patternLast = temp.ToArray();
            patterns.Add(patternLast);

            return patterns;
        }

        private static int GetColCntLeftOfMirrorOfPattern(string[] pattern)
        {
            List<List<int>> lists = new List<List<int>>();

            for (int i = 0; i < pattern.Length; i++)
            {
                string rowData = pattern[i];
                List<int> colCntLeftOfMirror = GetColListOfVerticalReflections(rowData);
                lists.Add(colCntLeftOfMirror);

                //string cols = "";
                //foreach (int c in colCntLeftOfMirror) { cols += c.ToString() + " "; }
                //Console.WriteLine($"{pattern[i]}    {cols}");

            }

            for (int checkIdx = 0; checkIdx < lists[0].Count; checkIdx++)
            {
                int checkCol = lists[0][checkIdx];

                for (int j = 1; j < lists.Count; j++)
                {
                    if (!lists[j].Contains(checkCol))
                    {
                        lists[0].RemoveAt(checkIdx);
                        checkIdx--;
                        break;
                    }
                }
            }

            //string colCnts = "";
            //foreach (int c in lists[0]) { colCnts += c.ToString() + " "; }
            //if (colCnts == "") { colCnts = "0"; }
            //Console.WriteLine($"cols left of vertical reflection: {colCnts}");
            //Console.WriteLine();
            
            return lists[0].Count > 0 ? lists[0].Last() : 0;            

        }

        private static List<int> GetColListOfVerticalReflections(string horizontalRow)
        {
            int firstCol = 0;
            int lastCol = horizontalRow.Length - 1;
            List<int> colCntLeftOfMirror = new List<int>();

            for (int col = 1; col <= lastCol; col++)
            {
                int leftOffset = col - 1;
                int rightOffset = col;
                bool isMirrored = false;

                while (leftOffset >= firstCol && rightOffset <= lastCol) 
                {
                    if (horizontalRow[leftOffset] == horizontalRow[rightOffset])
                    {
                        isMirrored = true;
                    }
                    else
                    {
                        isMirrored = false;
                        break;
                    }

                    leftOffset--;
                    rightOffset++;
                }

                if (isMirrored)
                {
                    colCntLeftOfMirror.Add(col);
                }

            }

            return colCntLeftOfMirror;
        }

        private static int GetLinesAboveReflection(string[] pattern)
        {
            int bottomRow = 0;
            int topRow = pattern.Length - 1;
            int numLinesAboveReflection = 0;
            
            for (int row = bottomRow; row <= topRow; row++)
            {
                int bottomOffset = row - 1;
                int topOffset = row;
                bool isMirrored = false;

                while ( bottomRow <= bottomOffset && topOffset <= topRow)
                {
                    if (pattern[bottomOffset] == pattern[topOffset]) { isMirrored = true;}
                    else { isMirrored=false; break; }

                    bottomOffset--;
                    topOffset++;
                }
                
                if (isMirrored)
                {
                    numLinesAboveReflection = row;
                }
            }

            return numLinesAboveReflection;
        }
    }
}
