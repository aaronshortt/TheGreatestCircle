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
        public void Calculate_Distance_From_Two_Points()
        {
            var result = customerManager.CalculateDistanceBetweenCoordinates(new GeoCoordinate(34, -10.0), new GeoCoordinate(32.043, 10.03));

            Console.WriteLine(result);

            Assert.AreEqual(1878.68371582031, result, 0.100);
        }
    }
}
