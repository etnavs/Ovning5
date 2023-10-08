using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovning5
{
    internal class Car : Vehicle
    {
        public string Brand { get; set; }

        public Car(int nrOfWheels, string color, string regNo, string brand)
            : base(nrOfWheels, color, regNo)
        {
            Brand = brand;
        }
    }
}
