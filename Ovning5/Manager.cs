using System.Diagnostics.Metrics;
using System.Drawing;
using System.Threading.Channels;

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

            Console.WriteLine("\n1. Search on regno");
            Console.WriteLine("2. Seed Vehicles");
            Console.WriteLine("3. Print vehicles");
            Console.WriteLine("4. Park a vehicle");
            Console.WriteLine("5. Pick up a vehicle");
            Console.WriteLine("6. Search vehicle with properties");
            Console.WriteLine("7. List number of different vehicles");
            Console.WriteLine("0. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                // Ta emot input
                case "1":
                    SearchOnREgNo();
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
                case "6":
                    SearchProperties();
                    break;
                case "7":
                    NumberOfDifferentVehicles();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }
        }

        private void SearchOnREgNo()
        {
            Console.WriteLine("Registration number of vehicle?");
            string regNo = Console.ReadLine()!.ToUpper();


            var vehicle = GetVehicleByRegNo(regNo);


            if (vehicle != null)
            {
                Console.WriteLine($"Vehicle with {vehicle.RegNo} was found");
                Console.WriteLine($"It is a {vehicle.Color} {vehicle.GetType().Name} with {vehicle.NrOfWheels} wheels");

            }
            else
            {
                Console.WriteLine($"Vehicle with {regNo} was NOT found");
            }
        }

        private void NumberOfDifferentVehicles() // Hur många av varje fordon finns det?
        {
            // Också med hjälp av ChatGPT
            var vehicleCounts = garage.GroupBy(vehicle => vehicle.GetType().Name)
        .Select(group => new
        {
            VehicleType = group.Key, // Fordonstypen
            Count = group.Count() // Antalet fordon i typen
        });

            var res = garage.GroupBy(vehicle => vehicle.GetType().Name)
                            .Select(group => $"{group.Key}: {group.Count()}");
                                      // .ToList().ForEach(r => Console.WriteLine(r));
            foreach ( var str in res ) 
            {
                Console.WriteLine(str);
            }

            Console.WriteLine("Vehicle counts:");

            foreach (var count in vehicleCounts)
            {
                Console.WriteLine($"{count.VehicleType}: {count.Count}");
            }
        }

        private void SearchProperties() // Sök fordon efter egenskaper
        {
            Console.WriteLine("What properties are you searching for?");
            Console.WriteLine("Press enter on not valid properties.");

            Console.WriteLine("Number of wheels?");
            int? nrOfWh = TryParseInt(Console.ReadLine()!);

            Console.WriteLine("Colour?");
            string col = Console.ReadLine();

            Console.WriteLine("Brand?");
            string bra = Console.ReadLine();

            Console.WriteLine("Wingspan?");
            int? wingsp = TryParseInt(Console.ReadLine());

            Console.WriteLine("Length?");
            int? len = TryParseInt(Console.ReadLine());

            Console.WriteLine("Horsepowers?");
            int? horsep = TryParseInt(Console.ReadLine());

            // Det här tack vare ChatGPT
            var matchingVehicles = garage.Where(vehicle =>
                (!nrOfWh.HasValue || vehicle.NrOfWheels == nrOfWh.Value) &&
                (string.IsNullOrWhiteSpace(col) || vehicle.Color.Equals(col, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(bra) || (vehicle is Car car && car.Brand.Equals(bra, StringComparison.OrdinalIgnoreCase))) &&
                (!wingsp.HasValue || (vehicle is Airplane airplane && airplane.Wingspan == wingsp.Value)) &&
                (!len.HasValue || (vehicle is Boat boat && boat.Boatlength == len.Value)) &&
                (!horsep.HasValue || (vehicle is Motorcycle motorcycle && motorcycle.Horsepowers == horsep.Value)));

            if (matchingVehicles.Any())
            {
                Console.WriteLine("Matching vehicles found:");
                foreach (var vehicle in matchingVehicles)
                {
                    Console.WriteLine($"Registration Number: {vehicle.RegNo} " +
                    $"\tColor: {vehicle.Color} " +
                    $"\tNumber of Wheels: {vehicle.NrOfWheels}");
                }
            }
            else
            {
                Console.WriteLine("No matching vehicles found.");
            }
        }

        private int? TryParseInt(string input)
        {
            if (int.TryParse(input, out int value))
            {
                return value;
            }
            return null;
        }


        private void PickUpVehicles()
        {
            // Plocka ut ett fordon ur garaget med registreringsnummer
            Console.WriteLine("Registration number of vehicle to pick up?");
            string regNo = Console.ReadLine().ToUpper();

            //Ta bort från garage
            garage.Remove(regNo);

        }



        private void ParkVehicles() // Parkera ett fordon i garaget
        {
            if(garage.IsFull)
                Console.WriteLine("Garage is full no spots left");

            Console.WriteLine("Which type of vehicle?");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Airplane");
            Console.WriteLine("3. Boat");
            Console.WriteLine("4. Motorcycle");
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
                case "3":
                    ParkBoat();
                    break;
                case "4":
                    ParkMotorcycle();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Wrong input");
                    break;

            }
        }

        private void ParkMotorcycle()
        {
            Console.WriteLine("Number of wheels?");
            int nrOfWh = int.Parse(Console.ReadLine());

            Console.WriteLine("Colour?");
            string col = Console.ReadLine();

            Console.WriteLine("Registration number?");
            string reg = Console.ReadLine().ToUpper();

            Console.WriteLine("Horsepowers?");
            int horsep = int.Parse(Console.ReadLine());

            var motorcycle = new Motorcycle(nrOfWheels: nrOfWh, color: col, regNo: reg, horsepowers: horsep);

            garage.Park(motorcycle);
        }

        private void ParkBoat()
        {
            Console.WriteLine("Number of wheels?");
            int nrOfWh = int.Parse(Console.ReadLine());

            Console.WriteLine("Colour?");
            string col = Console.ReadLine();

            Console.WriteLine("Registration number?");
            string reg = Console.ReadLine().ToUpper();

            Console.WriteLine("Length?");
            int len = int.Parse(Console.ReadLine());

            var boat = new Boat(nrOfWheels: nrOfWh, color: col, regNo: reg, boatlength: len);

            garage.Park(boat);
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

            bool success = false;
            string reg;
            do
            {
                Console.WriteLine("Registration number?");
                reg = Console.ReadLine()!.ToUpper();
                var parked = GetVehicleByRegNo(reg);

                if (parked != null)
                {
                    Console.WriteLine("RegNo is already parked");
                }
                else
                {
                    success = true;
                }

            } while (!success);


            Console.WriteLine("Brand?");
            string bra = Console.ReadLine();

            var car = new Car(nrOfWheels: nrOfWh, color: col, regNo: reg, brand: bra);

            garage.Park(car);
        }

        private void SeedVehicles()
        {

            // Skapa tre instanser av Vehicle med olika egenskaper
            Car car1 = new Car(nrOfWheels: 4, color: "Red", regNo: "ABC123", brand: "Nissan");
            Airplane airplane1 = new Airplane(nrOfWheels: 3, color: "Green", regNo: "DEF789", wingspan: 15);
            Boat boat1 = new Boat(nrOfWheels: 0, color: "Blue", regNo: "XYZ456", boatlength: 8);
            Motorcycle motorcycle1 = new Motorcycle(nrOfWheels: 2, color: "White", regNo: "RRR321", horsepowers: 54);

            // Anropa Park-metoden för att parkera bilarna i garaget
            garage.Park(car1);
            garage.Park(airplane1);
            garage.Park(boat1);
            garage.Park(motorcycle1);

        }



        private Vehicle? GetVehicleByRegNo(string regNo)
        {
            //Fråga efter regnummer att söka på
            //Console.WriteLine("Registration number of vehicle?");
            //string regNo = Console.ReadLine()!.ToUpper();


            return garage.FirstOrDefault(v => v.RegNo == regNo);

            //if(vehicle != null)
            //{
            //    Console.WriteLine($"Vehicle with {vehicle.RegNo} was found");
            //    Console.WriteLine($"It is a {vehicle.Color} {vehicle.GetType().Name} with {vehicle.NrOfWheels} wheels");

            //}
            //else
            //{
            //    Console.WriteLine($"Vehicle with {regNo} was NOT found");
            //}

            //bool found = false;

            //foreach (var vehicle in garage)
            //{
            //    if (vehicle.RegNo == regNo)
            //    {

            //        //// Avgör vilken sort fordonet är.
            //        //string vehic = "vehicle";
            //        //if (vehicle is Car)
            //        //{
            //        //    vehic = "car";
            //        //}
            //        //if (vehicle is Airplane)
            //        //{
            //        //    vehic = "airplane";
            //        //}
            //        //if (vehicle is Boat)
            //        //{
            //        //    vehic = "boat";
            //        //}
            //        //if (vehicle is Motorcycle)
            //        //{
            //        //    vehic = "motorcycle";
            //        //}

            //        // Skriv om fordonet hittas och dess egenskaper.

            //        Console.WriteLine($"Vehicle with {vehicle.RegNo} was found");
            //        Console.WriteLine($"It is a {vehicle.Color} {vehicle.GetType().Name} with {vehicle.NrOfWheels} wheels");

            //        // Hur skriver jag ut vilken plats fordonet är parkerat på?
            //        // Console.WriteLine($"Parked in space number {??}");
            //        found = true;
            //        break;
            //    }


            //}

            ////Om fordonet inte hittas.
            //if (!found)
            //{
            //    Console.WriteLine($"Vehicle with {regNo} was NOT found");
            //}


        }

        private void PrintVehicles() // Skriv ut vilka fordon som finns i garaget
        {
            Console.WriteLine("\nVehicles in the garage:");

            foreach (Vehicle vehicle in garage)
            {
                Console.WriteLine($"Registration Number: {vehicle.RegNo} " +
                    $"\tColor: {vehicle.Color} " +
                    $"\tNumber of Wheels: {vehicle.NrOfWheels}");

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

            // var li = new List<Vehicle>();

            garage = new Garage<Vehicle>(capacity);
            //IEnumerable<Vehicle> vehicles = garage;
            //IEnumerable<string> strings = new string[23];

            //foreach (var item in strings)
            //{
            //    Console.WriteLine(item);
            //}

            Console.WriteLine($"Garage with {capacity} parking spaces created.");
        }
    }
}