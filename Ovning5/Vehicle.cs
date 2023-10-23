namespace Ovning5
{
    internal class Vehicle
    {
        // public string Type => GetType().Name;
        //public VehicleType VehicleType => VehicleType.Vehicle;
        public int NrOfWheels { get; set; }
        public string Color { get; set; }
        public string RegNo { get; set; }
        public List<int> MyList { get; set; }

        public Vehicle(int nrOfWheels, string color, string regNo)
        {
            NrOfWheels = nrOfWheels;
            Color = color;
            RegNo = regNo;
            MyList = new List<int>();
        }
    }

    public enum VehicleType
    {
        Vehicle,
        Car,
        Buss
    }
}