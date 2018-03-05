using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatestCircle
{
    public class CustomerManager
    {

        public void GetCustomersInRangeFromFile(string filename) 
        {
            var customrs = ReadCustomersFromFile(filename);
            List<Customer> customersInRange = new List<Customer>();

            foreach (Customer c in customrs)
            {
                if (CalculateDistanceFromDublinOffice(new GeoCoordinate(c.Latitude, c.Longitude)) <= 100)
                {
                    customersInRange.Add(c);
                }
            }

            List<Customer> sortedList = customersInRange.OrderBy(c => c.UserId).ToList();
            foreach (Customer c in sortedList)
            {
                Console.WriteLine(c.ToString());
            }
            Console.ReadKey();
        }


        public List<Customer> ReadCustomersFromFile(string filename)
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                using (StreamReader streamReader = new StreamReader(filename))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string json = streamReader.ReadLine();
                        customers.Add(Customer.FromJson(json));
                    }
                    streamReader.Close();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new FileNotFoundException("Could not find Gist File Containing JSON");
            }

            return customers;
        }

        public float CalculateDistanceBetweenCoordinates(GeoCoordinate coordinateA, GeoCoordinate coordinateB)
        {
            //Returns distance in meters, divide by 1000 to get KM.
            return (float)coordinateA.GetDistanceTo(coordinateB) / 1000;
        }

        private float CalculateDistanceFromDublinOffice(GeoCoordinate candidateCoordinate)
        {
            //Returns distance in meters, divide by 1000 to get KM.
            GeoCoordinate dublinCoordinates = new GeoCoordinate(53.339428, -6.257664);
            return (float)dublinCoordinates.GetDistanceTo(candidateCoordinate) / 1000;
        }
    }
}
