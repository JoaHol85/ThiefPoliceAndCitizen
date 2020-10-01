using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ThiefPoliceAndCitizen
{
    class Citizen : Person
    {

        public List<Item> ItemsInPockets { get; set; }


        public Citizen()
        {
            (XMovement, YMovement) = MovementDirection();
            (int XStartingPosition, int YStartingPosition) = RandomizeStartingPosition(Program.cityMap);
            XCurrentPosition = XStartingPosition;
            YCurrentPosition = YStartingPosition;
            Name = "M";
            ItemsInPockets = FillCitizenPockets();
        }

        // Lägger in saker i medborgarnas inventory
        public List<Item> FillCitizenPockets()
        {
            List<Item> items = new List<Item>();
            items.Add(new Item("a Watch"));
            items.Add(new Item("An amount of money"));
            items.Add(new Item("a Phone"));
            items.Add(new Item("keys"));
            return items;
        }
    }
}
