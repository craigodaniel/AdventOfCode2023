
using System.Diagnostics;

namespace AocLibrary
{
    public class Day06
    {
        // --- Day 6: Wait For It ---
        // https://adventofcode.com/2023/day/6


        public static void Part1()
        {
            int[] raceTime = { 41, 96, 88, 94 };
            int[] raceDistRecord = { 214, 1789, 1127, 1055 };

            int winCntMultiplied = 1;

            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < raceTime.Length; i++)
            {
                int winCnt = 0;

                for (int t = 0;  t < raceTime[i]; t++)
                {
                    int distance = t * (raceTime[i] - t);
                    if (distance > raceDistRecord[i]) 
                    {
                        winCnt++;
                    }
                }

                winCntMultiplied = winCntMultiplied * winCnt;
            }

            sw.Stop();

            Console.WriteLine($"Part 1 Answer: {winCntMultiplied}");
            Console.WriteLine($"Runtime (ms): {sw.ElapsedMilliseconds}");
        }


        public static void Part2()
        {
            Int64 raceTime = 41968894;
            Int64 raceDistRecord = 214178911271055;

            Int64 winCnt = 0; ;

            Stopwatch sw = Stopwatch.StartNew();

            for (int t = 0; t < raceTime; t++)
            {
                Int64 distance = t * (raceTime - t);
                if (distance > raceDistRecord)
                {
                    winCnt++;
                }
            }

            sw.Stop();

            Console.WriteLine($"Part 2 Answer: {winCnt}");
            Console.WriteLine($"Runtime (ms): {sw.ElapsedMilliseconds}");
        }
    }
}
