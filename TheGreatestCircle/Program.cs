using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatestCircle
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            //Read in Customers
            var customrs = program.ReadCustomersFromFile();


            
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


    }

}
