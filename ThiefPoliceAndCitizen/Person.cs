using System;
using System.Collections.Generic;
using System.Threading;

namespace ThiefPoliceAndCitizen
{
    class Person
    {
        public static List<Person> citizens = new List<Person>();
        public static List<Thief> prison = new List<Thief>();

        static Random random = new Random();

        public int XStartingPosition { get; set; }
        public int YStartingPosition { get; set; }
        public int XCurrentPosition { get; set; }
        public int YCurrentPosition { get; set; }
        public int XMovement { get; set; }
        public int YMovement { get; set; }
        public string Name { get; set; }

        // RANDOMISERAR EN RIKTNING SOM PERSONEN SKA RÖRA SIG I
        public (int directionX, int directionY) MovementDirection()
        {
            int movementX = 0, movementY = 0;
            bool movement = false;
            while (!movement)
            {
                movementX = random.Next(-1, 1 + 1);
                movementY = random.Next(-1, 1 + 1);
                if (movementX == 0 && movementY == 0)
                    continue;
                else
                    movement = true;
            }
            return (movementX, movementY);
        }

        // RANDOMISERAR X OCH Y STARTPOSITIONEN
        public (int xPosition, int yPosition) RandomizeStartingPosition(string[,] array)
        {
            int positionX = random.Next(0, array.GetLength(0));
            int positionY = random.Next(0, array.GetLength(1));
            return (positionX, positionY);
        }

        // LÄGG TILL MEDBORGARE OCH LÄGGER DOM I EN LISTA
        public static void AddCitizensToCity()
        {
            for (int i = 0; i <= 25; i++)
            {
                citizens.Add(new Police());
            }
            for (int i = 0; i <= 65; i++)
            {
                citizens.Add(new Citizen());
            }
            for (int i = 0; i <= 25; i++)
            {
                citizens.Add(new Thief());
            }
        }
       
        // LÄGG TILL MEDBORGARNAS POSITIONER TILL ARRAYEN OCH KOLLAR OM DOM ÄR PÅ RAMEN
        public static void AddCitizensToCityMap(string[,] array)
        {
            foreach (Person person in citizens)
            {
                if (person.XCurrentPosition == 0)
                    person.XCurrentPosition = array.GetLength(0) - 2;
                if (person.YCurrentPosition == 0)
                    person.YCurrentPosition = array.GetLength(1) - 2;
                if (person.XCurrentPosition == array.GetLength(0) - 1)
                    person.XCurrentPosition = 1;
                if (person.YCurrentPosition == array.GetLength(1) - 1)
                    person.YCurrentPosition = 1;

                Program.cityMap[person.XCurrentPosition, person.YCurrentPosition] = person.Name;
            }
        }

        // RENSAR PERSONENS POSITION OCH GER DEN EN NY
        public static void MoveCitizens(string[,] array)
        {
            foreach (Person person in citizens)
            {
                array[person.XCurrentPosition, person.YCurrentPosition] = " ";
                person.XCurrentPosition += person.XMovement;
                person.YCurrentPosition += person.YMovement;
            }
        }

        // LETAR EFTER KONTAKTER I STADEN OCH AGERAR EFTER VILKA SOM MÖTS
        public static void CheckContact()
        {
            for (int i = 0; i < citizens.Count; i++)
            {
                for (int j = i + 1; j < citizens.Count; j++)
                {
                    if (citizens[i].XCurrentPosition == citizens[j].XCurrentPosition && citizens[i].YCurrentPosition == citizens[j].YCurrentPosition)
                    {
                        if (citizens[i] is Police && citizens[j] is Thief && ((Thief)citizens[j]).InPrison != true || citizens[i] is Thief && citizens[j] is Police && ((Thief)citizens[i]).InPrison != true)
                        {
                            PoliceMeetThief(citizens[i], citizens[j]);
                            Thread.Sleep(2000);
                        }
                        if (citizens[i] is Thief && citizens[j] is Citizen && ((Thief)citizens[i]).InPrison != true || citizens[i] is Citizen && citizens[j] is Thief && ((Thief)citizens[j]).InPrison != true)
                        {
                            ThiefMeetCitizen(citizens[i], citizens[j]);
                            Thread.Sleep(2000);
                        }
                    }
                }
            }
        }

        // TJUV MÖTER EN MEDBORGARE OCH SORTERAR VEM SOM ÄR VEM
        public static void ThiefMeetCitizen(Person person1, Person person2)
        {
            if (person1 is Citizen)
            {
                ThiefMeetCitizenAction(person1, person2);
            }
            if (person1 is Thief)
            {
                ThiefMeetCitizenAction(person2, person1);
            }
        }

        // DET SOM SKER NÄR EN MEDBORGARE MÖTER EN TJUV
        static void ThiefMeetCitizenAction(Person citizen, Person thief)
        {
            if (((Citizen)citizen).ItemsInPockets.Count == 0)
                Console.WriteLine("A thief meets a citizen, but the citizens pockets are empty so the thief moves on.");
            else
            {
                int itemToSteal = random.Next(0, ((Citizen)citizen).ItemsInPockets.Count);
                Console.WriteLine($"A thief meets a citizen and takes {((Citizen)citizen).ItemsInPockets[itemToSteal].ItemName}.");
                ((Thief)thief).Loot.Add(((Citizen)citizen).ItemsInPockets[itemToSteal]);
                ((Citizen)citizen).ItemsInPockets.RemoveAt(itemToSteal);
                Program.MuggingsInTownNumber++;
            }
        }

        // TJUV MÖTER EN POLIS OCH SORTERAR VEM SOM ÄR VEM
        public static void PoliceMeetThief(Person person1, Person person2)
        {
            if (person1 is Police)
            {
                PoliceMeetThiefAction(person2, person1);
            }
            if (person1 is Thief)
            {
                PoliceMeetThiefAction(person1, person2);
            }
        }

        // DET SOM SKER NÄR EN POLIS MÖTER EN TJUV
        static void PoliceMeetThiefAction(Person thief, Person police)
        {
            foreach (Item item in ((Thief)thief).Loot)
            {
                ((Police)police).Confiscated.Add(item);
            }
            ((Thief)thief).Loot.Clear();
            Console.WriteLine("A thief meets a police and gets arrested and thrown into jail.");
            Program.ArrestedThievesNumber++;
            Thief.AddThiefToPrison(((Thief)thief));
        }
    }
}
