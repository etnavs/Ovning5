using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovning5
{
    internal class Airplane : Vehicle
    {
        public int Wingspan { get; set; }

        public Airplane(int nrOfWheels, string color, string regNo, int wingspan)
            : base(nrOfWheels, color, regNo)
        {
            Wingspan = wingspan;
        }
    }
}
