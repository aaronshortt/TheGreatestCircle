using TheGreatestCircle;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Device.Location;

namespace TheGreatestCircle.UnitTests
{
    [TestClass]
    public class CustomerTests
    {

        CustomerManager customerManager = new CustomerManager();

        [TestMethod]
        [DeploymentItem(@"gistFile1.txt")]
        [ExpectedException(typeof(FileNotFoundException), "Could not find Gist File Containing JSON")]
        public void When_Try_To_Read_File_return_exception()
        {
             var result = customerManager.ReadCustomersFromFile("gistFile.txt");

        }

        [TestMethod]
        public void Calculate_Distance_From_Two_Points_Returns_True()
        {
            var result = customerManager.CalculateDistanceBetweenCoordinates(new GeoCoordinate(34, -10.0), new GeoCoordinate(32.043, 10.03));

            Console.WriteLine(result);

            Assert.AreEqual(1878.68371582031, result, 0.100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Latitude or longitude is out of range.")]
        public void Throw_Exception_Calculating_Coordinates_Out_Of_Range()
        {
            var result = customerManager.CalculateDistanceBetweenCoordinates(new GeoCoordinate(200, -10), new GeoCoordinate(32.043, 10));
        }


        [TestMethod]
        public void Was_Able_To_Parse_JSON()
        {
            string json = "{\"latitude\":\"52.986375\",\"user_id\": 12,\"name\":\"Christina McArdle\",\"longitude\":\"-6.043701\"}";

            Customer customer = Customer.FromJson(json);

            int id = (int)customer.UserId;

            Assert.AreEqual(id, 12);
        }
    }
}
