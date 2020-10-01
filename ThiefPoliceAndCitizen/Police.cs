using System;
using System.Collections.Generic;
using System.Text;

namespace ThiefPoliceAndCitizen
{
    class Police : Person
    {
        public List<Item> Confiscated { get; set; }


        public Police()
        {
            (XMovement, YMovement) = MovementDirection();
            (int XStartingPosition, int YStartingPosition) = RandomizeStartingPosition(Program.cityMap);
            XCurrentPosition = XStartingPosition;
            YCurrentPosition = YStartingPosition;
            Name = "P";
            Confiscated = new List<Item>();
        }


    }
}
