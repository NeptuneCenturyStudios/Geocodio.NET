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
            // Perfect address :)
            GeolocateSingleAddress(geo, "655 W 34th St, New York, NY 10001");
            // Slightly wrong address ;)
            GeolocateSingleAddress(geo, "6355 W 35th St, New York, NY 10002");
            // Really wrong address :O
            GeolocateSingleAddress(geo, "gdfgfdgfdg");

            // Look up multiple addresses using request object
            GeolocateAddresses(
                geo,
                new GeocodioAddressRequestBatch(
                    new[] 
                    {
                        new GeocodioAddressRequest()
                        {
                            Street = "1 Google Drive",
                            PostalCode = "78105"
                        },
                        new GeocodioAddressRequest()
                        {
                            Street = "1600 Amphitheatre Parkway",
                            City = "Mountain View",
                            State = "California",
                            PostalCode = "94043"
                        }
                    }
                )

            );


            // Look up multiple addresses using request object with explicit ids
            GeolocateAddresses(
                geo,
                new GeocodioAddressRequestBatch()
                {
                    { 5621, new GeocodioAddressRequest()
                        {
                            Street = "1 Google Drive",
                            PostalCode = "78105"
                        }
                    },
                    { 5622, new GeocodioAddressRequest()
                        {
                            Street = "1600 Amphitheatre Parkway",
                            City = "Mountain View",
                            State = "California",
                            PostalCode = "94043"
                        }
                    }
                }

            );




            // Wait to exit
            Console.WriteLine();
            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadKey();

        }

        /// <summary>
        /// Finds a single address for testing
        /// </summary>
        private static void GeolocateSingleAddress(Geocodio.Geocodio geo, string address)
        {
            Console.WriteLine();
            Console.WriteLine("Looking up address: {0}", address);

            try
            {
                // Request geolocation information
                var response = geo.GetGeolocation(address);

                // How many results?
                Console.WriteLine("Found {0} result(s)", response.Results.Count);

                // Iterate over our responses
                foreach (var result in response.Results)
                {
                    Console.WriteLine();
                    Console.WriteLine(result.FormattedAddress);
                    Console.WriteLine("With an accuracy of {0}", result.Accuracy);
                    Console.WriteLine("Lat/Lng: {0}, {1}", result.Location.Lat, result.Location.Lng);

                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }

        }

        /// <summary>
        /// Looks up addresses from a dictionary of GeocodioAddressRequest objects
        /// </summary>
        /// <param name="geo"></param>
        /// <param name="addresses"></param>
        private static void GeolocateAddresses(Geocodio.Geocodio geo, GeocodioAddressRequestBatch addresses)
        {
            Console.WriteLine();
            Console.WriteLine("Looking up multiple addresses");

            try
            {
                // Request geolocation information
                var batch = geo.GetGeolocations(addresses);

                // How many batches?
                Console.WriteLine("Processing {0} batch(es)", batch.Results.Count);

                // Iterate over our batch results
                foreach (var batchResult in batch.Results)
                {
                    Console.WriteLine();
                    Console.WriteLine("Processing batch with id {0}", batchResult.ID);
                    // How many results?
                    Console.WriteLine("Found {0} result(s)", batchResult.Response.Results.Count);

                    // Iterate over our results
                    foreach (var result in batchResult.Response.Results)
                    {
                        Console.WriteLine();
                        Console.WriteLine(result.FormattedAddress);
                        Console.WriteLine("With an accuracy of {0}", result.Accuracy);
                        Console.WriteLine("Lat/Lng: {0}, {1}", result.Location.Lat, result.Location.Lng);
                    }

                    // Was there an error?
                    if (!string.IsNullOrWhiteSpace(batchResult.Response.Error))
                    {
                        throw new Exception(batchResult.Response.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        /// <summary>
        /// Logs an exception that occurs
        /// </summary>
        /// <param name="ex"></param>
        private static void LogError(Exception ex)
        {
            // Store current color
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            // Write error
            Console.WriteLine();
            Console.WriteLine(ex.Message);

            // Reset color
            Console.ForegroundColor = oldColor;
        }

    }

}
