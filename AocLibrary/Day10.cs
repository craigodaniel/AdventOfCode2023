
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AocLibrary
{
    public class Day10
    {
        // --- Day 10: Pipe Maze ---
        // https://adventofcode.com/2023/day/10
        //
        // Part 1:
        // 6875
        //
        // Part 2:
        // 471
        //
        // | is a vertical pipe connecting north and south.
        // - is a horizontal pipe connecting east and west.
        // L is a 90-degree bend connecting north and east.
        // J is a 90-degree bend connecting north and west.
        // 7 is a 90-degree bend connecting south and west.
        // F is a 90-degree bend connecting south and east.
        // . is ground; there is no pipe in this tile.
        // S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.



        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_10_input.txt";
        //public static string fileName = "day_10_test_input.txt";
        public static string[] inputs = File.ReadAllLines(fileDir + "\\" + fileName);

        public static void Part1()
        {
            int startRow = -1;
            int startCol = -1;
            int currRow = -1;
            int currCol = -1;
            int pathDistance = 0;
            char prevPipe = '\0';
            char currPipe = '\0';
            char[,] pipes = new char[inputs.Length +2 , inputs[0].Length + 2];
            


            // Convert input file into char array pipes
            for (int row = 0; row < inputs.Length; row++)
            {
                for (int col = 0; col < inputs[0].Length; col++)
                {
                    pipes[row +1, col +1] = inputs[row][col];
                    if (pipes[row + 1, col + 1] == 'S')
                    {
                        startRow = row + 1;
                        startCol = col + 1;
                        currRow = startRow;
                        currCol = startCol;

                        Console.WriteLine($"Starting @ ({startRow},{startCol})...");
                    }
                }
            }


            // Get Valid Starting Path
            List<char> validNeighbors = GetValidNeighbors(startRow, startCol, pipes, prevPipe);
            switch (validNeighbors[0])
            {
                case 'N':
                    currRow --;
                    prevPipe = 'S';
                    break;
                case 'E':
                    currCol++;
                    prevPipe = 'W';
                    break;
                case 'S':
                    currRow++;
                    prevPipe = 'N';
                    break;
                case 'W':
                    currCol--;
                    prevPipe = 'E';
                    break;
            }

            
            // Walk the pipe and count steps
            while (currPipe != 'S')
            {
                currPipe = pipes[currRow, currCol];
                switch (currPipe)
                {
                    case 'S':
                        break;
                    case '|':
                        if (prevPipe == 'N')
                        {
                            currRow++;
                            prevPipe = 'N';
                        }
                        else if (prevPipe == 'S')
                        {
                            currRow--;
                            prevPipe = 'S';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case '-':
                        if (prevPipe == 'W')
                        {
                            currCol++;
                            prevPipe = 'W';
                        }
                        else if (prevPipe == 'E')
                        {
                            currCol--;
                            prevPipe = 'E';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case 'L':
                        if (prevPipe == 'N')
                        {
                            currCol++;
                            prevPipe = 'W';
                        }
                        else if (prevPipe == 'E')
                        {
                            currRow--;
                            prevPipe = 'S';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case 'J':
                        if (prevPipe == 'N')
                        {
                            currCol--;
                            prevPipe = 'E';
                        }
                        else if (prevPipe == 'W')
                        {
                            currRow--;
                            prevPipe = 'S';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case '7':
                        if (prevPipe == 'S')
                        {
                            currCol--;
                            prevPipe = 'E';
                        }
                        else if (prevPipe == 'W')
                        {
                            currRow++;
                            prevPipe = 'N';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case 'F':
                        if (prevPipe == 'S')
                        {
                            currCol++;
                            prevPipe = 'W';
                        }
                        else if (prevPipe == 'E')
                        {
                            currRow++;
                            prevPipe = 'N';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case '.':
                        throw new Exception("Error! Pipe path not valid!");
                    case '\0':
                        throw new Exception("Error! Pipe path not valid!");
                    default: throw new Exception($"Error! {currPipe} is unkown pipe char.");
                }

                pathDistance++;

            }

            // Output
            Console.WriteLine($"Part 1: {pathDistance / 2}");
        }


        public static void Part2()
        {
            int innerArea = 0;
            List<Tuple<int, int>> pipeLoop = GetPipeLoop();
            char[,] pipes = GetPipes();



            // Walk the array and sum up inner area
            for (int row = 1; row < pipes.GetUpperBound(0); row++)
            {
                bool isInner = false;
                int leftCol = -1;

                // Find left-most column of main pipe loop in this row
                foreach (Tuple<int, int> pair in pipeLoop)
                {
                    if (pair.Item2 > leftCol && pair.Item1 == row)
                    {
                        leftCol = pair.Item2;
                    }
                }

                // Walk left to right through row
                for (int col = 1; col < pipes.GetUpperBound(1); col++)
                {
                    Tuple<int,int> coord = Tuple.Create(row, col);
                    
                    if (pipeLoop.Contains(coord))
                    {
                        if (pipes[row,col] == '|' || pipes[row,col] == 'L' || pipes[row, col] == 'J') // Flip isInner on edge detected
                        {
                            isInner = !isInner;
                        }
                    }
                    else if (isInner && col < leftCol) // Inner area check
                    {
                        pipes[row, col] = 'I';
                        innerArea++;
                    }
                    else
                    {
                        pipes[row, col] = 'O'; // Mark outer areas
                    }
                }
            }


            //Show pipes in console
            for (int row = 0; row <= pipes.GetUpperBound(0); row++)
            {
                string line = "";

                for (int col = 0; col <= pipes.GetUpperBound(1); col++)
                {
                    line += pipes[row, col].ToString();
                }
                Console.WriteLine(line);
            }


            // Output
            Console.WriteLine($"Part 2: {innerArea}");
        }


        private static List<char> GetValidNeighbors(int row, int col, char[,] pipes, char prevDirection)
        {
            List<char> validNbrs = new List<char>();

            //North
            char north = pipes[row - 1,col];
            if (north == '|' || north == '7' || north == 'F')
            {
                validNbrs.Add('N');
            }

            // East
            char east = pipes[row,col + 1];
            if (east == '-' || east == 'J' || east == '7')
            {
                validNbrs.Add('E');
            }

            // South
            char south = pipes[row + 1, col];
            if (south == '|' || south == 'L' || south == 'J')
            {
                validNbrs.Add('S');
            }

            // West
            char west = pipes[row,col - 1];
            if (west == '-' || west == 'L' || west == 'F')
            {
                validNbrs.Add('W');
            }

            if (validNbrs.Contains(prevDirection))
            {
                validNbrs.Remove(prevDirection);
            }

            return validNbrs;
        }


        private static List<Tuple<int,int>> GetPipeLoop()
        {
            List<Tuple<int,int>> pipeLoop = new List<Tuple<int,int>>();
            int startRow = -1;
            int startCol = -1;
            int currRow = -1;
            int currCol = -1;
            char prevPipe = '\0';
            char currPipe = '\0';
            char[,] pipes = new char[inputs.Length + 2, inputs[0].Length + 2];



            // Convert input file into char array pipes
            for (int row = 0; row < inputs.Length; row++)
            {
                for (int col = 0; col < inputs[0].Length; col++)
                {
                    pipes[row + 1, col + 1] = inputs[row][col];
                    if (pipes[row + 1, col + 1] == 'S')
                    {
                        startRow = row + 1;
                        startCol = col + 1;
                        currRow = startRow;
                        currCol = startCol;
                    }
                }
            }


            // Get Valid Starting Path
            List<char> validNeighbors = GetValidNeighbors(startRow, startCol, pipes, prevPipe);
            switch (validNeighbors[0])
            {
                case 'N':
                    currRow--;
                    prevPipe = 'S';
                    break;
                case 'E':
                    currCol++;
                    prevPipe = 'W';
                    break;
                case 'S':
                    currRow++;
                    prevPipe = 'N';
                    break;
                case 'W':
                    currCol--;
                    prevPipe = 'E';
                    break;
            }


            // Add first pipe
            pipeLoop.Add(Tuple.Create(currRow, currCol));


            // Walk the pipe and add coord's to list
            while (currPipe != 'S')
            {
                currPipe = pipes[currRow, currCol];
                switch (currPipe)
                {
                    case 'S':
                        break;
                    case '|':
                        if (prevPipe == 'N')
                        {
                            currRow++;
                            prevPipe = 'N';
                        }
                        else if (prevPipe == 'S')
                        {
                            currRow--;
                            prevPipe = 'S';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case '-':
                        if (prevPipe == 'W')
                        {
                            currCol++;
                            prevPipe = 'W';
                        }
                        else if (prevPipe == 'E')
                        {
                            currCol--;
                            prevPipe = 'E';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case 'L':
                        if (prevPipe == 'N')
                        {
                            currCol++;
                            prevPipe = 'W';
                        }
                        else if (prevPipe == 'E')
                        {
                            currRow--;
                            prevPipe = 'S';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case 'J':
                        if (prevPipe == 'N')
                        {
                            currCol--;
                            prevPipe = 'E';
                        }
                        else if (prevPipe == 'W')
                        {
                            currRow--;
                            prevPipe = 'S';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case '7':
                        if (prevPipe == 'S')
                        {
                            currCol--;
                            prevPipe = 'E';
                        }
                        else if (prevPipe == 'W')
                        {
                            currRow++;
                            prevPipe = 'N';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case 'F':
                        if (prevPipe == 'S')
                        {
                            currCol++;
                            prevPipe = 'W';
                        }
                        else if (prevPipe == 'E')
                        {
                            currRow++;
                            prevPipe = 'N';
                        }
                        else
                        {
                            throw new Exception("Error! Pipe path not valid!");
                        }
                        break;
                    case '.':
                        throw new Exception("Error! Pipe path not valid!");
                    case '\0':
                        throw new Exception("Error! Pipe path not valid!");
                    default: throw new Exception($"Error! {currPipe} is unkown pipe char.");
                }

                Tuple<int,int> coord = Tuple.Create(currRow, currCol);
                pipeLoop.Add(coord);

            }

            // Output
            return pipeLoop;
        }

        private static char[,] GetPipes()
        {
            char[,] pipes = new char[inputs.Length + 2, inputs[0].Length + 2];

            // Convert input file into char array pipes
            for (int row = 0; row < inputs.Length; row++)
            {
                for (int col = 0; col < inputs[0].Length; col++)
                {
                    pipes[row + 1, col + 1] = inputs[row][col];
                }
            }

            return pipes;
        }


    }
}
