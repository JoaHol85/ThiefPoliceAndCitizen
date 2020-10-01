using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThiefPoliceAndCitizen
{
    class Thief : Person
    {
        public List<Item> Loot { get; set; }
        public int ThiefNumberInPrison { get; set; } // ??
        public int PrisonTime { get; set; }
        public bool InPrison { get; set; } = false;

        public Thief()
        {
            (XMovement, YMovement) = MovementDirection();
            (int XStartingPosition, int YStartingPosition) = RandomizeStartingPosition(Program.cityMap);
            XCurrentPosition = XStartingPosition;
            YCurrentPosition = YStartingPosition;
            Name = "T";
            Loot = new List<Item>();
        }

        //LÄGGER TILL EN PERSON(THIEF) I LISTAN 'PRISON'
        public static void AddThiefToPrison(Thief thief)
        {
            prison.Add(thief);
            thief.Name = " ";
            thief.InPrison = true;
            thief.ThiefNumberInPrison = Program.ArrestedThievesNumber;
            thief.PrisonTime = 0;
        }

        // SKRIVER UT LISTAN 'PRISON'
        public static void PrintPrison()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Prison:");
            foreach (Thief person in prison)
            {
                Console.WriteLine($"Thief nr: {person.ThiefNumberInPrison} - Prison time: {person.PrisonTime}");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        // ADDERAR TILL TID TILL FÅNGAR OCH SER OM DOM ÄR REDO ATT SLÄPPAS
        public static void CheckPrisonerStatus()
        {
            foreach (Thief thief in prison)
            {
                thief.PrisonTime++;
                if (thief.PrisonTime == 20)
                {
                    thief.InPrison = false;
                    thief.Name = "T";
                }
            }
        }

        // SLÄPPER FÅNGAR
        public static void ReleasePrisoner()
        {
            foreach (Person person in citizens)
            {
                if (person is Thief && ((Thief)person).PrisonTime == 20 && prison.Count > 0)
                {
                    ((Thief)person).PrisonTime = 0;
                    prison.RemoveAt(0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A thief is released from prison, WATCH OUT CITIZENS!!!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(4000);
                }
            }
        }





    }
}
