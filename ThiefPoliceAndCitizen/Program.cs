using System;
using System.Globalization;
using System.Threading;

namespace ThiefPoliceAndCitizen
{
    class Program
    {
        public static int MuggingsInTownNumber = 0;
        public static int ArrestedThievesNumber = 0;

        public static string[,] cityMap = new string[35, 100];

        static void Main(string[] args)
        {
            CreateCity();
            Person.AddCitizensToCity();

            while (true)
            {
                Person.AddCitizensToCityMap(cityMap);

                PrintCityMap();

                PrintNumberOfMuggingAndArrests();

                Person.MoveCitizens(cityMap);

                Thief.CheckPrisonerStatus();

                Thief.PrintPrison();

                Console.WriteLine("Happenings:");
                Thief.ReleasePrisoner();

                Person.CheckContact();

                Thread.Sleep(400);

                Console.Clear();
                //Console.SetCursorPosition(0, 0); // Provade med denna men då blev det massa knas med meddelandena som kommer när en tjuv möter en polis osv, så fick bli Console.Clear(); tyvärr!
            }
        }

        // Håller koll på antal stölder och antal fångade tjuvar
        private static void PrintNumberOfMuggingAndArrests()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nNumber of citizens mugged: {MuggingsInTownNumber}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Number of arrested thieves: {ArrestedThievesNumber}\n");
            Console.ResetColor();
        }

        // Skriver ut 'Staden' i consolen
        private static void PrintCityMap()
        {
            for (int i = 0; i < cityMap.GetLength(0); i++)
            {
                for (int j = 0; j < cityMap.GetLength(1); j++)
                {
                    Console.Write(cityMap[i, j]);
                }
                Console.WriteLine();
            }
        }

        // Bygger upp staden innan alla personer läggs in
        static void CreateCity()
        {
            for (int i = 0; i < cityMap.GetLength(0); i++)
            {
                for (int j = 0; j < cityMap.GetLength(1); j++)
                {
                    if (i == 0 || i == cityMap.GetLength(0) - 1 || j == 0 || j == cityMap.GetLength(1) - 1)
                        cityMap[i, j] = "+";
                    else
                        cityMap[i, j] = " ";
                }
            }
        }
    }
}