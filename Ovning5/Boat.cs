using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovning5
{
    internal class Boat : Vehicle
    {
        public int Boatlength { get; set; }

        public Boat(int nrOfWheels, string color, string regNo, int boatlength)
            : base(nrOfWheels, color, regNo)
        {
            Boatlength = boatlength;
        }
    }
}
