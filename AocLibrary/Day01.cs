namespace AocLibrary
{
    public class Day01
    {
        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_1_input.txt";
        public static string[] lines = File.ReadAllLines(fileDir + "\\" + fileName);
        public static void Part1() 
        {
            int sum = 0;

            //read input into array
            string[] fileString = lines;

            foreach (string line in fileString)
            {
                bool isFirstInt = true;
                int firstInt = 0;
                int secondInt = 0;
                foreach (char c in line)
                {
                    bool isNumeric = int.TryParse(c.ToString(), out int x);
                    if (isNumeric)
                    {
                        if(isFirstInt)
                        {
                            //store first int
                            firstInt = x;
                            isFirstInt = false;
                        }

                        //store last int
                        secondInt = x;
                    }
                }

                //store 2-digit number from first and last ints
                int lineNumber = (firstInt * 10) + secondInt;
                sum += lineNumber;
            }

            //Output sum
            Console.WriteLine("Day 1, Part 1 Answer: " + sum.ToString());
        }

        public static void Part2()
        {
            /*
             Edge Case breaks?
                line: 2ninehnsnnvj21fkeightwodmz
                formatted: 29hnsnnvj21fk8wodmz
                digits: 28

            should it be 22? or 28?

            when reading "eightwo"...

            if last digit is supposed to be read left to right... you get 2. If right to left you get 8...
            this is unclear from the test examples provided.

            !!!!!!!!!!!!!!!!
            TIP FROM REDDIT:
            "The right calibration values for string "eighthree" is 83 and for "sevenine" is 79.
            The examples do not cover such cases.

            new idea: get value and index of all numbers (digits and words)
            list of pairs (index, value)
            first digit = value with lowest index
            last digit = value with highest index
            combine into single two-digit number
            sum all the two-digit numbers

            */


            int sum = 0;

            //read input into array
            string[] fileString = lines;
            foreach (string line in fileString)
            {
                bool isFirstInt = true;
                int firstInt = 0;
                int secondInt = 0;


                int len = line.Length;

                for (int i = 0; i < len; i++)
                {
                    int num = 0;

                    // if number is digit
                    bool isNumeric = int.TryParse(line[i].ToString(), out int x);
                    if (isNumeric)
                    {
                        if (isFirstInt)
                        {
                            //store first int
                            firstInt = x;
                            isFirstInt = false;
                        }

                        //store last int
                        secondInt = x;
                        continue;
                    }

                    // Find number words with 3 chars
                    if (i + 3 > len) { continue; }
                    string word = line.Substring(i, 3);
                    switch (word)
                    {
                        case "one":
                            num = 1;
                            goto SAVE_PAIR;
                        case "two":
                            num = 2;
                            goto SAVE_PAIR;
                        case "six":
                            num = 6;
                            goto SAVE_PAIR;
                    }

                    // Find number words with 4 chars
                    if (i + 4 > len) { continue; }
                    word = line.Substring(i, 4);
                    switch (word)
                    {
                        case "four":
                            num = 4;
                            goto SAVE_PAIR;
                        case "five":
                            num = 5;
                            goto SAVE_PAIR;
                        case "nine":
                            num = 9;
                            goto SAVE_PAIR;
                    }

                    // Find number words with 5 chars
                    if (i + 5 > len) { continue; }
                    word = line.Substring(i, 5);
                    switch (word)
                    {
                        case "three":
                            num = 3;
                            goto SAVE_PAIR;
                        case "seven":
                            num = 7;
                            goto SAVE_PAIR;
                        case "eight":
                            num = 8;
                            goto SAVE_PAIR;
                    }


                    // Update String and loop to start
                SAVE_PAIR:
                    if (num != 0) //if number is word
                    {
                        if (isFirstInt)
                        {
                            //store first int
                            firstInt = num;
                            isFirstInt = false;
                        }

                        //store last int
                        secondInt = num;
                    }

                }
                
                

                //store 2-digit number from first and last ints
                int lineNumber = (firstInt * 10) + secondInt;
                sum += lineNumber;
                Console.WriteLine("line: " + line);
                Console.WriteLine("digits: " + lineNumber.ToString());
            }

            //Output sum
            Console.WriteLine("/nDay 1, Part 2 Answer: " + sum.ToString());
        }

        
    }
        
}
