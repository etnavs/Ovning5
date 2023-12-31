﻿using System.Collections;

namespace Ovning5
{
    internal class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        private T[] spots;

        public Garage(int capacity)
        {
            spots = new T[capacity];
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var v in spots)
            {
                if (v is not null)
                    yield return v;
            }
        }

        internal void Park(T vehicle)
        {
            //Hitta första tomma platsen
            //Parkera fordonet på den platsen!
            //Returnera true om det går bra annars false

            for (int i = 0; i < spots.Length; i++)
            {
                if (spots[i] is null) // Om platsen är tom (null)
                {
                    spots[i] = vehicle; // Parkera fordonet på den tomma platsen
                    Console.WriteLine($"Parked vehicle with RegNo: {vehicle.RegNo} at spot {i + 1}");
                    return; // Avsluta loop efter att fordonet har parkerats
                }
            }

            // Om loopen avslutas utan att hitta en tom plats, är garaget fullt
            Console.WriteLine("Garage is full. Cannot park the vehicle.");

        }

        internal void Remove(string regNo)
        {
            bool found = false;
            for (int i = 0; i < spots.Length; i++)
            {
                if (spots[i] != null && spots[i].RegNo == regNo)
                {

                    Console.WriteLine($"Removed vehicle with RegNo: {regNo} from spot {i + 1}");
                    spots[i] = null; // Sätt platsen till null för att ta bort fordonet
                    found = true;
                    break;

                }
               
            }
            if (!found) // Fordonet hittas inte i garaget
            {
                Console.WriteLine($"Vehicle with RegNo: {regNo} was NOT found in the garage.");
               
            }
                
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}