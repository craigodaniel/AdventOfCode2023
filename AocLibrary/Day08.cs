
using System.Diagnostics;

namespace AocLibrary
{
    public class Day08
    {
        // --- Day 8: Haunted Wasteland ---
        // https://adventofcode.com/2023/day/8
        //
        // Part 1:
        // Total steps: 19667
        // Runtime(ms) : 3
        //
        // Part 2:
        // Total steps: 19185263738117
        // Runtime (ms): 9



        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_8_input.txt";
        //public static string fileName = "day_8_test_input.txt";
        public static string[] inputs = File.ReadAllLines(fileDir + "\\" + fileName);


        public static void Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            Dictionary<string, string[]> nodes = new Dictionary<string, string[]>();
            string instructions = inputs[0];
            string currentNode = "AAA"; //start at node "AAA".
            string endNode = "ZZZ"; //end at node "ZZZ".
            int stepCnt = 0;

            // Load nodes
            for (int i = 2; i < inputs.Length; i++)
            {
                string node = inputs[i].Split(" = ")[0].Trim();
                string[] directions = inputs[i].Split(" = ")[1].Split(",");
                directions[0] = directions[0].Trim().Remove(0, 1);
                directions[1] = directions[1].Trim().Remove(3, 1);
                nodes[node] = directions;
            }


            int j = 0;
            while (currentNode != endNode)
            {
                if (instructions[j] == 'L')
                {
                    currentNode = nodes[currentNode][0];
                }
                else if (instructions[j] == 'R')
                {
                    currentNode = nodes[currentNode][1];
                }
                else
                {
                    Console.WriteLine($"Could not read instruction at j: {j}");
                }

                j++;

                if (j > instructions.Length - 1)
                {
                    stepCnt += j;
                    j = 0;
                }
            }

            stepCnt += j;

            sw.Stop();

            Console.WriteLine($"Total steps: {stepCnt}");
            Console.WriteLine($"Runtime (ms): {sw.ElapsedMilliseconds}");

        }


        public static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            Dictionary<string, string[]> nodes = new Dictionary<string, string[]>();
            string instructions = inputs[0];
            List<string> currentNodes = new List<string>();
            int[] pathLens = new int[6];

            // Load nodes
            for (int i = 2; i < inputs.Length; i++)
            {
                string node = inputs[i].Split(" = ")[0].Trim();
                string[] directions = inputs[i].Split(" = ")[1].Split(",");
                directions[0] = directions[0].Trim().Remove(0, 1);
                directions[1] = directions[1].Trim().Remove(3, 1);
                nodes[node] = directions;

                if (node[2] == 'A') { currentNodes.Add(node); }

            }


            for (int i = 0; i < currentNodes.Count; i++)
            {
                pathLens[i] = (GetPathLength(nodes, currentNodes[i]));
            }

            long stepCnt = lcm_of_array_elements(pathLens);

            sw.Stop();

            Console.WriteLine($"Total steps: {stepCnt}");
            Console.WriteLine($"Runtime (ms): {sw.ElapsedMilliseconds}");

        }

        private static int GetPathLength(Dictionary<string, string[]> nodes, string startNode)
        {
            string instructions = inputs[0];
            string currentNode = startNode;
            int j = 0;
            int pathLen = 0;
            while (currentNode[2] != 'Z')
            {
                if (instructions[j] == 'L')
                {
                    currentNode = nodes[currentNode][0];
                }
                else if (instructions[j] == 'R')
                {
                    currentNode = nodes[currentNode][1];
                }
                else
                {
                    Console.WriteLine($"Could not read instruction at j: {j}");
                }

                j++;

                if (j > instructions.Length - 1)
                {
                    pathLen += j;
                    j = 0;
                }
            }

            pathLen += j;
            return pathLen;
        }

        public static long lcm_of_array_elements(int[] element_array)
        {
            long lcm_of_array_elements = 1;
            int divisor = 2;

            while (true)
            {

                int counter = 0;
                bool divisible = false;
                for (int i = 0; i < element_array.Length; i++)
                {

                    // lcm_of_array_elements (n1, n2, ... 0) = 0.
                    // For negative number we convert into
                    // positive and calculate lcm_of_array_elements.
                    if (element_array[i] == 0)
                    {
                        return 0;
                    }
                    else if (element_array[i] < 0)
                    {
                        element_array[i] = element_array[i] * (-1);
                    }
                    if (element_array[i] == 1)
                    {
                        counter++;
                    }

                    // Divide element_array by devisor if complete
                    // division i.e. without remainder then replace
                    // number with quotient; used for find next factor
                    if (element_array[i] % divisor == 0)
                    {
                        divisible = true;
                        element_array[i] = element_array[i] / divisor;
                    }
                }

                // If divisor able to completely divide any number
                // from array multiply with lcm_of_array_elements
                // and store into lcm_of_array_elements and continue
                // to same divisor for next factor finding.
                // else increment divisor
                if (divisible)
                {
                    lcm_of_array_elements = lcm_of_array_elements * divisor;
                }
                else
                {
                    divisor++;
                }

                // Check if all element_array is 1 indicate 
                // we found all factors and terminate while loop.
                if (counter == element_array.Length)
                {
                    return lcm_of_array_elements;
                }
            }
        }
    }
}
