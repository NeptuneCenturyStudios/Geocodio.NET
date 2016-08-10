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

            geo.ApiKey = ConfigurationManager.AppSettings["ApiKey"];

        }
    }
}
