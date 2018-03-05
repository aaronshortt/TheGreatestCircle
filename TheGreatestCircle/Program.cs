using System;
using System.Collections.Generic;
using System.IO;
using System.Device.Location;
using System.Linq;

namespace TheGreatestCircle
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            //Read in Customers
            var customrs = program.ReadCustomersFromFile();

            List<Customer> customersInRange = new List<Customer>();
            foreach(Customer c in customrs)
            {
                if (program.CalculateDistanceFromDublinOffice(new GeoCoordinate(c.Latitude, c.Longitude) ) <= 100)
                {
                    customersInRange.Add(c);
                }
            }

            List<Customer> sortedList = customersInRange.OrderBy(c => c.UserId).ToList();
            foreach(Customer c in sortedList)
            {
                Console.WriteLine(c.Name + "\t" + c.UserId);
            }
            Console.ReadKey();
        }


        private List<Customer> ReadCustomersFromFile()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                using (StreamReader streamReader = new StreamReader("gistFile1.txt"))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string json = streamReader.ReadLine();
                        customers.Add(Customer.FromJson(json));
                    }
                    streamReader.Close();
                }
            }catch (IOException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new FileNotFoundException("Could not find Gist File Containing JSON");
            }

            return customers;
        }

        private float CalculateDistanceBetweenCoordinates(GeoCoordinate coordinateA, GeoCoordinate coordinateB)
        {
            //Returns distance in meters, divide by 1000 to get KM.
            return (float)coordinateA.GetDistanceTo(coordinateB) /1000;
        }

        private float CalculateDistanceFromDublinOffice(GeoCoordinate candidateCoordinate)
        {
            GeoCoordinate dublinCoordinates = new GeoCoordinate(53.339428, -6.257664);
            return (float)dublinCoordinates.GetDistanceTo(candidateCoordinate) / 1000;
        }




    }

}
