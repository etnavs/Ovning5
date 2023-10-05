namespace Ovning5
{
    internal class Vehicle
    {
        public int NrOfWheels { get; set; }
        public string Color { get; set; }
        public string RegNo { get; set; }

        public Vehicle(int nrOfWheels, string color, string regNo)
        {
            NrOfWheels = nrOfWheels;
            Color = color;
            RegNo = regNo;
        }
    }
}