
namespace AocLibrary
{
    public class Day09
    {
        // --- Day 9: Mirage Maintenance ---
        // https://adventofcode.com/2023/day/9
        //
        // Part 1:
        // 2043677056
        //
        // Part 2:
        // 1062



        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_9_input.txt";
        //public static string fileName = "day_9_test_input.txt";
        public static string[] inputs = File.ReadAllLines(fileDir + "\\" + fileName);

        public static void Part1()
        {
            Stack<int> stack = new Stack<int>();
            int sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                Console.WriteLine(inputs[i]);
                string[] line = inputs[i].Trim().Split(" ");
                int[] numbers = new int[line.Length];
                for (int j = 0; j < line.Length; j++)
                {
                    numbers[j] = int.Parse(line[j]);
                }

                bool isAllZeroes = false;

                while (!isAllZeroes)
                {
                    stack.Push(numbers.Last());

                    bool isNotZero = false;
                    foreach (int n in numbers)
                    {
                        if (n != 0) { isNotZero = true; break; }
                    }
                    isAllZeroes = !isNotZero;
                    numbers = GetNextSequence(numbers);

                    string sequence = "";
                    foreach (int n in numbers)
                    {
                        sequence += n.ToString() + " ";
                    }
                }

                int nextValue = stack.Pop();

                while (stack.Count > 0)
                {
                    nextValue += stack.Pop();
                }

                Console.WriteLine($"Next: {nextValue}");
                sum += nextValue;
                

            }

            Console.WriteLine($"Part 1 Answer: {sum}");
        }


        public static void Part2()
        {
            Stack<int> stack = new Stack<int>();
            int sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                Console.WriteLine(inputs[i]);
                string[] line = inputs[i].Trim().Split(" ");
                int[] numbers = new int[line.Length];
                for (int j = 0; j < line.Length; j++)
                {
                    numbers[j] = int.Parse(line[j]);
                }

                bool isAllZeroes = false;

                while (!isAllZeroes)
                {
                    stack.Push(numbers.First());

                    bool isNotZero = false;
                    foreach (int n in numbers)
                    {
                        if (n != 0) { isNotZero = true; break; }
                    }
                    isAllZeroes = !isNotZero;
                    numbers = GetNextSequence(numbers);

                    string sequence = "";
                    foreach (int n in numbers)
                    {
                        sequence += n.ToString() + " ";
                    }
                }

                int prevValue = stack.Pop();

                while (stack.Count > 0)
                {
                    prevValue = stack.Pop() - prevValue;
                }

                Console.WriteLine($"Next: {prevValue}");
                sum += prevValue;


            }

            Console.WriteLine($"Part 2 Answer: {sum}");
        }

        private static int[] GetNextSequence(int[] sequence)
        {
            int[] nextSequence = new int[sequence.Length - 1];
            
            for (int i = 0; i < sequence.Length - 1; i++)
            {
                nextSequence[i] = sequence[i + 1] - sequence[i];
            }

            return nextSequence;
        }
    }
}
