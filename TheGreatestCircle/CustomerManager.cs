using Newtonsoft.Json;
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
                        try
                        {
                            string json = streamReader.ReadLine();
                            customers.Add(Customer.FromJson(json));
                        }
                        catch (JsonSerializationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (JsonReaderException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        
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
            try
            {
                return (float)coordinateA.GetDistanceTo(coordinateB) / 1000;
            }catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("Latitude or longitude is out of range.");
            }
            
        }

        private float CalculateDistanceFromDublinOffice(GeoCoordinate candidateCoordinate)
        {
            //Returns distance in meters, divide by 1000 to get KM.
            GeoCoordinate dublinCoordinates = new GeoCoordinate(53.339428, -6.257664);
            return CalculateDistanceBetweenCoordinates(dublinCoordinates, candidateCoordinate);
        }
    }
}
