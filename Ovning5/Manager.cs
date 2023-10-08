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
            Console.WriteLine("5. Pick up a vehicle");
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
                case "5":
                    PickUpVehicles();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Wrong input!");
                    break;
            }
        }

        private void PickUpVehicles()
        {
            Console.WriteLine("Licence number of vehicle to pick up?");
            string regNo = Console.ReadLine().ToUpper();

            garage.Remove(regNo);
                        
        }



        private void ParkVehicles()
        {
            
            Console.WriteLine("Which type of vehicle?");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Airplane");
            Console.WriteLine("0. Main Menu");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ParkCar();
                    break;
                case "2":
                    ParkAirplane();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Wrong input!");
                    break;

            }
        }

        private void ParkAirplane()
        {
            Console.WriteLine("Number of wheels?");
            int nrOfWh = int.Parse(Console.ReadLine());

            Console.WriteLine("Colour?");
            string col = Console.ReadLine();

            Console.WriteLine("Registration number?");
            string reg = Console.ReadLine().ToUpper();

            Console.WriteLine("Wingspan?");
            int wingsp = int.Parse(Console.ReadLine());

            var airplane = new Airplane(nrOfWheels: nrOfWh, color: col, regNo: reg, wingspan: wingsp);

            garage.Park(airplane);
        }

        private void ParkCar()
        {
            Console.WriteLine("Number of wheels?");
            int nrOfWh = int.Parse(Console.ReadLine());

            Console.WriteLine("Colour?");
            string col = Console.ReadLine();

            Console.WriteLine("Registration number?");
            string reg = Console.ReadLine().ToUpper();

            Console.WriteLine("Brand?");
            string bra = Console.ReadLine();

            var car = new Car(nrOfWheels: nrOfWh, color: col, regNo: reg, brand: bra);

            garage.Park(car);
        }

        private void SeedVehicles()
        {
            
            // Skapa tre instanser av Vehicle med olika egenskaper
            var car1 = new Car(nrOfWheels: 4, color: "Red", regNo: "ABC123", brand: "Nissan");
            var car2 = new Car(nrOfWheels: 4, color: "Blue", regNo: "XYZ456", brand: "Seat");
            var airplane1 = new Airplane(nrOfWheels: 4, color: "Green", regNo: "DEF789", wingspan: 15);

            // Anropa Park-metoden för att parkera bilarna i garaget
            garage.Park(car1);
            garage.Park(car2);
            garage.Park(airplane1);

           
           
        }

        private void GetVehicleByRegNo()
        {
            //Fråga efter regnummer att söka på
            Console.WriteLine("Registration number of car?");
            string regNo = Console.ReadLine().ToUpper();

            bool found = false;

            foreach (var vehicle in garage)
            {
                if(vehicle.RegNo == regNo)
                {
                    Console.WriteLine($"Vehicle with {vehicle.RegNo} was found");
                    // Hur kan jag skriva om vehicle är en bil eller ett flygplan?
                    Console.WriteLine($"It is a {vehicle.Color} vehicle with {vehicle.NrOfWheels} wheels");
                    //Hur skriver jag ut vilken plats fordonet är parkerat på?
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
            //int capacity = 10;
            //Skapa ett garage
            Console.WriteLine("\tGarage creation\n");
            int capacity;
            while (true)
            {
                Console.WriteLine("How many parking spaces does the garage need?");
            string input = Console.ReadLine();

                if (int.TryParse(input, out capacity) && capacity > 0)
                {
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            var li = new List<Vehicle>();

            garage = new Garage<Vehicle>(capacity);

            Console.WriteLine($"Garage with {capacity} parking spaces created.");
        }
    }
}