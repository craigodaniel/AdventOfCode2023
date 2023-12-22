using AocLibrary;

namespace AocProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("--------ADVENT OF CODE 2023--------");
            Console.WriteLine("-----------------------------------");

            while (!endApp)
            {
                // Declare variables.
                string? numInput1;
                string? numInput2;
                string? quitInput;
                double day;

                // Ask the user to type the first number.
                Console.Write("Type DAY # and then press Enter: ");
                numInput1 = Console.ReadLine();
                
                while (!double.TryParse(numInput1, out day))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("For PART ONE enter '1', for PART TWO enter '2': ");
                numInput2 = Console.ReadLine();

                double part;
                while (!double.TryParse(numInput2, out part))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                switch (day)
                {
                    case 1:
                        if (part == 1) {Day01.Part1();}
                        else if(part == 2){Day01.Part2();}                        
                        break;

                    case 2:
                        if (part == 1) { Day02.Part1(); }
                        else if (part == 2){ Day02.Part2(); }
                        break;

                    case 3:
                        if (part == 1) { Day03.Part1(); }
                        else if (part == 2) { Day03.Part2(); }
                        break;

                    case 4:
                        if (part == 1) { Day04.Part1(); }
                        else if (part == 2) { Day04.Part2(); }
                        break;

                    case 5:
                        if (part == 1) { Day05.Part1(); }
                        else if (part == 2) { Day05.Part2(); }
                        break;

                    case 6:
                        if (part == 1) { Day06.Part1(); }
                        else if (part == 2) { Day06.Part2(); }
                        break;

                    case 7:
                        if (part == 1) { Day07.Part1(); }
                        else if (part == 2) { Day07.Part2(); }
                        break;

                    case 8:
                        if (part == 1) { Day08.Part1(); }
                        else if (part == 2) { Day08.Part2(); }
                        break;

                    case 9:
                        if (part == 1) { Day09.Part1(); }
                        else if (part == 2) { Day09.Part2(); }
                        break;

                    case 10:
                        if (part == 1) { Day10.Part1(); }
                        else if (part == 2) { Day10.Part2(); }
                        break;

                    case 11:
                        if (part == 1) { Day11.Part1(); }
                        else if (part == 2) { Day11.Part2(); }
                        break;

                    case 12:
                        if (part == 1) { Day12.Part1(); }
                        else if (part == 2) { Day12.Part2(); }
                        break;

                    case 13:
                        if (part == 1) { Day13.Part1(); }
                        else if (part == 2) { Day13.Part2(); }
                        break;

                    default:
                        break;
                }


                // Ask the user if they want to end program
                Console.Write("Quit? Y/N: ");
                quitInput = Console.ReadLine();
                if (quitInput == "Y" || quitInput == "y") { endApp = true; }

            }

        }
    }
}