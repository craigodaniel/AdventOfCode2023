
namespace AocLibrary
{
    public class Day07
    {
        // --- Day 7: Camel Cards ---
        // https://adventofcode.com/2023/day/7
        //
        // Part 2:
        // 254212690 too low...miscalculated Jokers
        // 255842780 too high...calculate Jokers in loop AFTER all other pairs counted
        // 254412181 Correct!


        public static string fileDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string fileName = "day_7_input.txt";
        //public static string fileName = "day_7_test_input.txt";
        public static string[] inputs = File.ReadAllLines(fileDir + "\\" + fileName);

        public static void Part1()
        {
            int total = 0;

            List<CamelHand> list = new List<CamelHand>();

            foreach (string line in inputs)
            {
                string hand = line.Split(" ")[0];
                int bid = int.Parse(line.Split(' ')[1]);

                Console.WriteLine($"hand: {hand} bid: {bid}");
                int score = ScoreHand(hand);
                Console.WriteLine($"score: {score}");
                List<int> highCards = HighCards(hand);

                list.Add(new CamelHand(hand, bid, score, highCards));

            }

            list = SortCamelHands(list);

            for (int i = 0; i < list.Count; i++)
            {
                total += list[i].Bid * (i + 1);
            }

            Console.WriteLine("Camel Hands:");
            foreach (CamelHand hand in list)
            {
                Console.WriteLine(hand.ToString());
            }

            Console.WriteLine($"Total: {total}");
            
        }

        public static void Part2()
        {
            int total = 0;

            List<CamelHand> list = new List<CamelHand>();

            foreach (string line in inputs)
            {
                string hand = line.Split(" ")[0];
                int bid = int.Parse(line.Split(' ')[1]);

                Console.WriteLine($"hand: {hand} bid: {bid}");
                int score = ScoreHand2(hand);
                Console.WriteLine($"score: {score}");
                List<int> highCards = HighCards2(hand);

                list.Add(new CamelHand(hand, bid, score, highCards));

            }

            list = SortCamelHands(list);

            for (int i = 0; i < list.Count; i++)
            {
                total += list[i].Bid * (i + 1);
            }

            Console.WriteLine("Camel Hands:");
            foreach (CamelHand hand in list)
            {
                Console.WriteLine(hand.ToString());
            }

            Console.WriteLine($"Total: {total}");
        }

        private static int ScoreHand(string hand)
        {
            int[] matches = new int[13];


            foreach (char c in hand)
            {
                switch (c)
                {
                    case 'A':
                        matches[0]++;
                        break;

                    case 'K':
                        matches[1]++;
                        break;

                    case 'Q':
                        matches[2]++;
                        break;

                    case 'J':
                        matches[3]++;
                        break;

                    case 'T':
                        matches[4]++;
                        break;

                    case '9':
                        matches[5]++;
                        break;

                    case '8':
                        matches[6]++;
                        break;

                    case '7':
                        matches[7]++;
                        break;

                    case '6':
                        matches[8]++;
                        break;

                    case '5':
                        matches[9]++;
                        break;

                    case '4':
                        matches[10]++;
                        break;

                    case '3':
                        matches[11]++;
                        break;

                    case '2':
                        matches[12]++;
                        break;
                }
            }

            if (matches.Contains(5))
            {
                return 6;
            }
            else if (matches.Contains(4))
            {
                return 5;
            }
            else if (matches.Contains(3) && matches.Contains(2))
            {
                return 4;
            }
            else if (matches.Contains(3))
            {
                return 3;
            }
            else
            {
                int pairCnt = 0;
                foreach (int match in matches)
                {
                    if ( match == 2)
                    {
                        pairCnt++;
                    }
                }

                return pairCnt;
            }

        }

        private static int ScoreHand2(string hand)
        {
            int[] matches = new int[13];


            foreach (char c in hand)
            {
                switch (c)
                {
                    case 'A':
                        matches[0]++;
                        break;

                    case 'K':
                        matches[1]++;
                        break;

                    case 'Q':
                        matches[2]++;
                        break;

                    case 'T':
                        matches[4]++;
                        break;

                    case '9':
                        matches[5]++;
                        break;

                    case '8':
                        matches[6]++;
                        break;

                    case '7':
                        matches[7]++;
                        break;

                    case '6':
                        matches[8]++;
                        break;

                    case '5':
                        matches[9]++;
                        break;

                    case '4':
                        matches[10]++;
                        break;

                    case '3':
                        matches[11]++;
                        break;

                    case '2':
                        matches[12]++;
                        break;
                }
            }

            foreach (char c in hand) //jokers act as wildcard
            {
                if (c == 'J')
                {
                    int bestIndex = -1;
                    for (int i = 0; i < matches.Length; i++)    
                    {
                        if (matches[i] == matches.Max())
                        {
                            bestIndex = i;
                        }

                    }
                    matches[bestIndex]++;
                }
            }

            if (matches.Contains(5))
            {
                return 6;
            }
            else if (matches.Contains(4))
            {
                return 5;
            }
            else if (matches.Contains(3) && matches.Contains(2))
            {
                return 4;
            }
            else if (matches.Contains(3))
            {
                return 3;
            }
            else
            {
                int pairCnt = 0;
                foreach (int match in matches)
                {
                    if (match == 2)
                    {
                        pairCnt++;
                    }
                }

                return pairCnt;
            }

        }

        private static List<int> HighCards(string hand)
        {
            List<int> list = new List<int>();


            for (int i = 0; i < 5; i++)
            {
                switch (hand[i])
                {
                    case 'A':
                        list.Add(13);
                        break;

                    case 'K':
                        list.Add(12);
                        break;

                    case 'Q':
                        list.Add(11);
                        break;

                    case 'J':
                        list.Add(10);
                        break;

                    case 'T':
                        list.Add(9);
                        break;

                    case '9':
                        list.Add(8);
                        break;

                    case '8':
                        list.Add(7);
                        break;

                    case '7':
                        list.Add(6);
                        break;

                    case '6':
                        list.Add(5);
                        break;

                    case '5':
                        list.Add(4);
                        break;

                    case '4':
                        list.Add(3);
                        break;

                    case '3':
                        list.Add(2);
                        break;

                    case '2':
                        list.Add(1);
                        break;
                }
            }

            return list;
        }

        private static List<int> HighCards2(string hand)
        {
            List<int> list = new List<int>();


            for (int i = 0; i < 5; i++)
            {
                switch (hand[i])
                {
                    case 'A':
                        list.Add(13);
                        break;

                    case 'K':
                        list.Add(12);
                        break;

                    case 'Q':
                        list.Add(11);
                        break;

                    case 'J':
                        list.Add(1); //Jokers now worth 1
                        break;

                    case 'T':
                        list.Add(10);
                        break;

                    case '9':
                        list.Add(9);
                        break;

                    case '8':
                        list.Add(8);
                        break;

                    case '7':
                        list.Add(7);
                        break;

                    case '6':
                        list.Add(6);
                        break;

                    case '5':
                        list.Add(5);
                        break;

                    case '4':
                        list.Add(4);
                        break;

                    case '3':
                        list.Add(3);
                        break;

                    case '2':
                        list.Add(2);
                        break;
                }
            }

            return list;
        }

        private static List<CamelHand> SortCamelHands(List<CamelHand> list)
        {

            bool isChanged = false;
            
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].Score > list[i+1].Score)
                {
                    CamelHand temp = list[i];
                    list.RemoveAt(i);
                    list.Insert(i + 1, temp);
                    isChanged = true;
                }
                else if (list[i].Score == list[i + 1].Score)
                {
                    if (list[i].HighCards[0] > list[i + 1].HighCards[0])
                    {
                        CamelHand temp = list[i];
                        list.RemoveAt(i);
                        list.Insert(i + 1, temp);
                        isChanged = true;
                    }
                    else if (list[i].HighCards[0] == list[i + 1].HighCards[0])
                    {
                        if (list[i].HighCards[1] > list[i + 1].HighCards[1])
                        {
                            CamelHand temp = list[i];
                            list.RemoveAt(i);
                            list.Insert(i + 1, temp);
                            isChanged = true;
                        }
                        else if (list[i].HighCards[1] == list[i + 1].HighCards[1])
                        {
                            if (list[i].HighCards[2] > list[i + 1].HighCards[2])
                            {
                                CamelHand temp = list[i];
                                list.RemoveAt(i);
                                list.Insert(i + 1, temp);
                                isChanged = true;
                            }
                            else if (list[i].HighCards[2] == list[i + 1].HighCards[2])
                            {
                                if (list[i].HighCards[3] > list[i + 1].HighCards[3])
                                {
                                    CamelHand temp = list[i];
                                    list.RemoveAt(i);
                                    list.Insert(i + 1, temp);
                                    isChanged = true;
                                }
                                else if (list[i].HighCards[3] == list[i + 1].HighCards[3])
                                {
                                    if (list[i].HighCards[4] > list[i + 1].HighCards[4])
                                    {
                                        CamelHand temp = list[i];
                                        list.RemoveAt(i);
                                        list.Insert(i + 1, temp);
                                        isChanged = true;
                                    }
                                    else if (list[i].HighCards[4] == list[i + 1].HighCards[4])
                                    {
                                        Console.WriteLine($"Error? Hands match? {list[i].Hand} {list[i+1].Hand}");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if ( isChanged ) { list = SortCamelHands( list ); }
            return list;
        }
    }

    public class CamelHand
    {
        //public CamelHand() { }

        public CamelHand(string hand, int bid, int score, List<int> highCards)
        {
            Hand = hand;
            Bid = bid;
            Score = score;
            HighCards = highCards;

        }

        // Properties.
        public string Hand { get; set; }
        public int Bid { get; set; }
        public int Score { get; set; }
        public List<int> HighCards { get; set; }

        public override string ToString() => $"{Hand} {Bid} {Score} {HighCards[0]} {HighCards[1]} {HighCards[2]} {HighCards[3]} {HighCards[4]}";
    }
}
