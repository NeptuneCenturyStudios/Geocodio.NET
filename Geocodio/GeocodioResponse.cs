using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geocodio
{
    public class GeocodioResponse
    {
        #region Properties
        /// <summary>
        /// Gets a list of results from a query
        /// </summary>
        public List<GeocodioResult> Results { get; private set; }
        #endregion

        #region Constructors
        public GeocodioResponse(JToken response)
        {
            //private constructor
            Results = new List<GeocodioResult>();

            //for each result, create a result object
            foreach (var result in response["results"])
            {
                //get the address components field
                var addrComp = result["address_components"];
                //get the location object
                var loc = result["location"];

                //create result object
                var geoResult = new GeocodioResult()
                {
                    AddressComponents = new GeocodioAddressComponents()
                    {
                        Number = (string)addrComp?["number"],
                        Street = (string)addrComp?["street"],
                        Suffix = (string)addrComp?["suffix"],
                        City = (string)addrComp?["city"],
                        County = (string)addrComp?["county"],
                        State = (string)addrComp?["state"],
                        Zip = (string)addrComp?["zip"],
                    },
                    FormattedAddress = (string)result["formatted_address"],
                    Location = new GeocodioLocation()
                    {
                        Lat = (double)loc?["lat"],
                        Lng = (double)loc?["lng"]
                    },
                    Accuracy = (decimal)result["accuracy"]
                };

                //add to the collection of results
                Results.Add(geoResult);
            }
        }
        #endregion

    }
}
