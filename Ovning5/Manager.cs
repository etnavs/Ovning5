namespace Ovning5
{
    internal class Manager
    {
        private Garage<Vehicle> garage;

        public Manager()
        {
        }

        internal void Run()
        {
            InitGarage();
             while (true)
            {
                ShowMainMenu();
            }
            
        }

        

        private void ShowMainMenu()
        {
            

            //Skriv ut menyn
            //Ta input
            //Agera på det

            Console.WriteLine("1. Search on regno");
            Console.WriteLine("2. Seed Vehicles");
            Console.WriteLine("3. Print vehicles");
            Console.WriteLine("4. Park a vehicle");
            Console.WriteLine("0. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    GetVehicleByRegNo();
                    break; 
                case "2":
                    SeedVehicles();
                    break;
                case "3":
                    PrintVehicles();
                    break;
                case "4":
                    ParkVehicles();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Wrong input!");
                    break;
            }
        }

        private void ParkVehicles()
        {
            if (garage == null)
            {
                Console.WriteLine("Garage has not been initialized.");
                return;
            }
            Console.WriteLine("Which type of vehicle?");
            Console.WriteLine("1. Car");
            Console.WriteLine("0. Main Menu");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ParkCar();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Wrong input!");
                    break;

            }
        }

        private void ParkCar()
        {
            if (garage == null)
            {
                Console.WriteLine("Garage has not been initialized.");
                return;
            }
            Console.WriteLine("Number of wheels?");
            int nrOfWh = int.Parse(Console.ReadLine());

            Console.WriteLine("Colour?");
            string col = Console.ReadLine();

            Console.WriteLine("Registration number?");
            string reg = Console.ReadLine().ToUpper();

            var car = new Vehicle(nrOfWheels: nrOfWh, color: col, regNo: reg);

            garage.Park(car);
        }

        private void SeedVehicles()
        {
            if (garage == null)
            {
                Console.WriteLine("Garage has not been initialized.");
                return;
            }

            // Skapa tre instanser av Vehicle med olika egenskaper
            var car1 = new Vehicle(nrOfWheels: 4, color: "Red", regNo: "ABC123");
            var car2 = new Vehicle(nrOfWheels: 4, color: "Blue", regNo: "XYZ456");
            var car3 = new Vehicle(nrOfWheels: 4, color: "Green", regNo: "DEF789");

            // Anropa Park-metoden för att parkera bilarna i garaget
            garage.Park(car1);
            garage.Park(car2);
            garage.Park(car3);

           
           
        }

        private void GetVehicleByRegNo()
        {
            //Fråga efter regnummer att söka på
            Console.WriteLine("Registration number of car?");
            string regNo = Console.ReadLine().ToUpper();

            bool found = false;

            //var regNo = "ABC123";
            foreach (var vehicle in garage)
            {
                if(vehicle.RegNo == regNo)
                {
                    Console.WriteLine($"Vehicle with {vehicle.RegNo} was found");
                    Console.WriteLine($"It is a {vehicle.Color} car with {vehicle.NrOfWheels} wheels");
                    //Console.WriteLine($"Parked in space number {??}");
                    found = true;
                    break;
                }

                
            }
            
           if (!found)
            {
                Console.WriteLine($"Vehicle with {regNo} was NOT found");
            }
              
           
            

            //var res = garage.FirstOrDefault(v => v.RegNo == regNo);
        }

        private void PrintVehicles()
        {
            if (garage == null)
            {
                Console.WriteLine("Garage has not been initialized.");
                return;
            }

            Console.WriteLine("Vehicles in the garage:");

            foreach (var vehicle in garage)
            {
                Console.WriteLine($"Registration Number: {vehicle.RegNo}");
                Console.WriteLine($"Color: {vehicle.Color}");
                Console.WriteLine($"Number of Wheels: {vehicle.NrOfWheels}");
               
            }
        }

        private void InitGarage()
        {
            //Fråga efter garagets storlek
            //Ta input från användaren
            //Parsa inputsträngen till en int
            int capacity = 10;
            //Skapa ett garage

            var li = new List<Vehicle>();

            garage = new Garage<Vehicle>(capacity);
        }
    }
}