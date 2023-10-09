using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovning5
{
    internal class Motorcycle : Vehicle
    {
        public int Horsepowers { get; set; }

        public Motorcycle(int nrOfWheels, string color, string regNo, int horsepowers)
            : base(nrOfWheels, color, regNo)
        {
            Horsepowers = horsepowers;
        }
    }
}
