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
            var customerManager = new CustomerManager();
            customerManager.GetCustomersInRangeFromFile("gistFile1.txt");


        }
    }

}
