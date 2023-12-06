
using System.Diagnostics;

namespace AocLibrary
{
    public class Day05
    {
        // --- Day 5: If You Give A Seed A Fertilizer ---
        // https://adventofcode.com/2023/day/5
        //
        // Part 1:
        // Try 1: 26497990 too low... fixed dest = source (not seed) if no map overide.
        // Try 2: 107430936 Correct!
        //
        // Part 2:
        // Try 1: 23738616 Correct! Only took 75 minutes...


        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_5_input.txt";
        //public static string fileName = "day_5_test_input.txt";
        public static string inputs = File.ReadAllText(fileDir + "\\" + fileName);


        public static void Part1()
        {
            List<Int64> seedList = new List<Int64>();
            
            // Real inputs
            Int64[,] seed_to_soil = new long[33,3];
            Int64[,] soil_to_fertilizer = new long[41, 3];
            Int64[,] fertilizer_to_water = new long[42, 3];
            Int64[,] water_to_light = new long[27, 3];
            Int64[,] light_to_temperature = new long[38, 3];
            Int64[,] temperature_to_humidity = new long[13, 3];
            Int64[,] humidity_to_location = new long[26, 3];
            


            // Test inputs
            /*
            Int64[,] seed_to_soil = new long[2, 3];
            Int64[,] soil_to_fertilizer = new long[3, 3];
            Int64[,] fertilizer_to_water = new long[4, 3];
            Int64[,] water_to_light = new long[2, 3];
            Int64[,] light_to_temperature = new long[33, 3];
            Int64[,] temperature_to_humidity = new long[2, 3];
            Int64[,] humidity_to_location = new long[2, 3];
            */

            // Map input data to seedList and Int64 arrays
            string[] maps = inputs.Split("\r\n\r\n");
            foreach (string map in maps)
            {
                string[] contents = map.Split(":");
                string key = contents[0].Split(" ")[0].Trim();

                if (key == "seeds")
                {
                    string[] data = contents[1].Trim().Split(" ");
                    foreach (string value in data)
                    {
                        bool isNumber = Int64.TryParse(value, out Int64 number);
                        if (!isNumber) { Console.WriteLine($"value: {value} in key: {key} is not a number!"); }
                        else { seedList.Add(number); }
                    }
                }
                else
                {
                    string[] rows = contents[1].Trim().Split("\r\n");
                    for (int row = 0; row < rows.Length; row++)
                    {
                        string[] values = rows[row].Split(" ");

                        for (int col = 0; col < 3; col++)
                        {
                            bool isNumber = Int64.TryParse(values[col], out Int64 number);
                            if (!isNumber) { Console.WriteLine($"value: {values[col]} in key: {key} is not a number!"); }
                            else 
                            {
                                switch (key)
                                {
                                    case "seed-to-soil":
                                        seed_to_soil[row, col] = number;
                                        break;

                                    case "soil-to-fertilizer":
                                        soil_to_fertilizer[row, col] = number;
                                        break;

                                    case "fertilizer-to-water":
                                        fertilizer_to_water[row, col] = number;
                                        break;

                                    case "water-to-light":
                                        water_to_light[row, col] = number;
                                        break;

                                    case "light-to-temperature":
                                        light_to_temperature[row, col] = number;
                                        break;

                                    case "temperature-to-humidity":
                                        temperature_to_humidity[row, col] = number;
                                        break;

                                    case "humidity-to-location":
                                        humidity_to_location[row, col] = number;
                                        break;
                                }
                                
                            }
                        }

                    }
                }

            }


            // Boogie Woogie
            Int64 minDistance = Int64.MaxValue;
            foreach (Int64 seed in seedList)
            {
                Int64 soil = GetDestination(seed_to_soil, seed);
                Int64 fertilizer = GetDestination(soil_to_fertilizer, soil);
                Int64 water = GetDestination(fertilizer_to_water, fertilizer);
                Int64 light = GetDestination(water_to_light, water);
                Int64 temperature = GetDestination(light_to_temperature, light);
                Int64 humidity = GetDestination(temperature_to_humidity, temperature);
                Int64 location = GetDestination(humidity_to_location, humidity);

                Console.WriteLine($"{seed} -> {soil} -> {fertilizer} -> {water} -> {light} -> {temperature} -> {humidity} -> {location}");

                if (location < minDistance) { minDistance = location; Console.WriteLine("NEW MIN DISTANCE"); }

            }

            Console.WriteLine($"closest distance is: {minDistance}");


            // Debug Tool: toggle console messages
            bool debugInputMapping = false;
            if (debugInputMapping)
            {
                Console.WriteLine("seedList:");
                for (int i = 0; i < seedList.Count; i++)
                {
                    Console.WriteLine(seedList[i]);
                }

                Console.WriteLine();
                Console.WriteLine("seed_to_soil:");
                for (int i = 0; i <= seed_to_soil.GetUpperBound(0); i++)
                {
                    Console.WriteLine($"{seed_to_soil[i, 0]} {seed_to_soil[i, 1]} {seed_to_soil[i, 2]}");
                }

                Console.WriteLine();
                Console.WriteLine("soil_to_fertilizer:");
                for (int i = 0; i <= soil_to_fertilizer.GetUpperBound(0); i++)
                {
                    Console.WriteLine($"{soil_to_fertilizer[i, 0]} {soil_to_fertilizer[i, 1]} {soil_to_fertilizer[i, 2]}");
                }

                Console.WriteLine();
                Console.WriteLine("fertilizer_to_water:");
                for (int i = 0; i <= fertilizer_to_water.GetUpperBound(0); i++)
                {
                    Console.WriteLine($"{fertilizer_to_water[i, 0]} {fertilizer_to_water[i, 1]} {fertilizer_to_water[i, 2]}");
                }

                Console.WriteLine();
                Console.WriteLine("water_to_light:");
                for (int i = 0; i <= water_to_light.GetUpperBound(0); i++)
                {
                    Console.WriteLine($"{water_to_light[i, 0]} {water_to_light[i, 1]} {water_to_light[i, 2]}");
                }

                Console.WriteLine();
                Console.WriteLine("light_to_temperature:");
                for (int i = 0; i <= light_to_temperature.GetUpperBound(0); i++)
                {
                    Console.WriteLine($"{light_to_temperature[i, 0]} {light_to_temperature[i, 1]} {light_to_temperature[i, 2]}");
                }

                Console.WriteLine();
                Console.WriteLine("temperature_to_humidity:");
                for (int i = 0; i <= temperature_to_humidity.GetUpperBound(0); i++)
                {
                    Console.WriteLine($"{temperature_to_humidity[i, 0]} {temperature_to_humidity[i, 1]} {temperature_to_humidity[i, 2]}");
                }

                Console.WriteLine();
                Console.WriteLine("humidity_to_location:");
                for (int i = 0; i <= humidity_to_location.GetUpperBound(0); i++)
                {
                    Console.WriteLine($"{humidity_to_location[i, 0]} {humidity_to_location[i, 1]} {humidity_to_location[i, 2]}");
                }
            }

            
        }

        public static void Part2()
        {
            Stopwatch sw = new Stopwatch();

            Int64 seedCnt = 0;
            List<Int64> seedList = new List<Int64>();

            // Real inputs
            
            Int64[,] seed_to_soil = new long[33, 3];
            Int64[,] soil_to_fertilizer = new long[41, 3];
            Int64[,] fertilizer_to_water = new long[42, 3];
            Int64[,] water_to_light = new long[27, 3];
            Int64[,] light_to_temperature = new long[38, 3];
            Int64[,] temperature_to_humidity = new long[13, 3];
            Int64[,] humidity_to_location = new long[26, 3];
            


            // Test inputs
            /*
            Int64[,] seed_to_soil = new long[2, 3];
            Int64[,] soil_to_fertilizer = new long[3, 3];
            Int64[,] fertilizer_to_water = new long[4, 3];
            Int64[,] water_to_light = new long[2, 3];
            Int64[,] light_to_temperature = new long[33, 3];
            Int64[,] temperature_to_humidity = new long[2, 3];
            Int64[,] humidity_to_location = new long[2, 3];
            */

            // Map input data to seedList and Int64 arrays
            string[] maps = inputs.Split("\r\n\r\n");
            foreach (string map in maps)
            {
                string[] contents = map.Split(":");
                string key = contents[0].Split(" ")[0].Trim();

                if (key == "seeds")
                {
                    string[] data = contents[1].Trim().Split(" ");
                    foreach (string value in data)
                    { 
                        bool isNumber = Int64.TryParse(value, out Int64 number);
                        if (!isNumber) { Console.WriteLine($"value: {value} in key: {key} is not a number!"); }
                        else { seedList.Add(number); }
                    }
                }
                else
                {
                    string[] rows = contents[1].Trim().Split("\r\n");
                    for (int row = 0; row < rows.Length; row++)
                    {
                        string[] values = rows[row].Split(" ");

                        for (int col = 0; col < 3; col++)
                        {
                            bool isNumber = Int64.TryParse(values[col], out Int64 number);
                            if (!isNumber) { Console.WriteLine($"value: {values[col]} in key: {key} is not a number!"); }
                            else
                            {
                                switch (key)
                                {
                                    case "seed-to-soil":
                                        seed_to_soil[row, col] = number;
                                        break;

                                    case "soil-to-fertilizer":
                                        soil_to_fertilizer[row, col] = number;
                                        break;

                                    case "fertilizer-to-water":
                                        fertilizer_to_water[row, col] = number;
                                        break;

                                    case "water-to-light":
                                        water_to_light[row, col] = number;
                                        break;

                                    case "light-to-temperature":
                                        light_to_temperature[row, col] = number;
                                        break;

                                    case "temperature-to-humidity":
                                        temperature_to_humidity[row, col] = number;
                                        break;

                                    case "humidity-to-location":
                                        humidity_to_location[row, col] = number;
                                        break;
                                }

                            }
                        }

                    }
                }
            }

            // Boogie Woogie Round 2
            Int64 minDistance = Int64.MaxValue;
            for (int i = 0; i < seedList.Count; i = i + 2)
            {
                Int64 seedEndRange = seedList[i] + seedList[i + 1] - 1;
                Int128 bigNum = seedList[i] + seedList[i + 1] - 1;
                Console.WriteLine($"Analyzing seeds {seedList[i]} thru {seedEndRange}");
                if (seedEndRange != bigNum) { Console.WriteLine("Seed bigger than Int64!"); }
                for (Int64 j = seedList[i]; j < seedList[i] + seedList[i + 1]; j++)
                {
                    Int64 seed = j;
                    Int64 soil = GetDestination(seed_to_soil, seed);
                    Int64 fertilizer = GetDestination(soil_to_fertilizer, soil);
                    Int64 water = GetDestination(fertilizer_to_water, fertilizer);
                    Int64 light = GetDestination(water_to_light, water);
                    Int64 temperature = GetDestination(light_to_temperature, light);
                    Int64 humidity = GetDestination(temperature_to_humidity, temperature);
                    Int64 location = GetDestination(humidity_to_location, humidity);

                    //Console.WriteLine($"{seed} -> {soil} -> {fertilizer} -> {water} -> {light} -> {temperature} -> {humidity} -> {location}");

                    if (location < minDistance) { minDistance = location; Console.WriteLine("NEW MIN DISTANCE"); }
                    seedCnt++;
                    if (seedCnt == Int64.MaxValue) { Console.WriteLine($"Seed Count reached max value : {seedCnt}"); }
                }
                

            }

            sw.Stop();

            // output:
            Console.WriteLine($"Processed {seedCnt} seeds!");
            Console.WriteLine($"closest distance is: {minDistance}");
            Console.WriteLine($"Run time (s): {sw.Elapsed}");

        }


        private static Int64 GetDestination(Int64[,] sorce_dest_map, Int64 source)
        {
            Int64 destination = source;
            for (int i = 0; i <= sorce_dest_map.GetUpperBound(0); i++)
            {
                Int64 destRangeStart = sorce_dest_map[i, 0];
                Int64 sourceRangeStart = sorce_dest_map[i, 1];
                Int64 rangeLength = sorce_dest_map[i, 2];
                if (source >= sourceRangeStart && source <= sourceRangeStart + rangeLength)
                {
                    destination = destRangeStart + (source - sourceRangeStart);
                    break;
                }
            }

            return destination;
        }
    }
}
