using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geocodio;
using System.Configuration;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {

            // Beginning tests for Geocodio
            Console.WriteLine("Beginning tests for Geocodio");

            // Return a set of addresses based on latitude and longitude
            var geo = new Geocodio.Geocodio();
            // Get the api key from app settings
            geo.ApiKey = ConfigurationManager.AppSettings["ApiKey"];

            // TODO: Create some tests

            Console.ReadKey();

        }
    }
}
