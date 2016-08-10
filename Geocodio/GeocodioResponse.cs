using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geocodio
{
    /// <summary>
    /// Respresents the response from Geocodio
    /// </summary>
    public class GeocodioResponse
    {
        #region Properties
        /// <summary>
        /// Gets a list of results from a query
        /// </summary>
        public List<GeocodioResult> Results { get; private set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of the GeocodioResponse class from part of the response JSON object
        /// </summary>
        /// <param name="response"></param>
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
                        PostDirectional = (string)addrComp?["postdirectional"],
                        City = (string)addrComp?["city"],
                        County = (string)addrComp?["county"],
                        State = (string)addrComp?["state"],
                        Zip = (string)addrComp?["zip"],
                        FormattedStreet = (string)result["formatted_street"],
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
            if (Results.Count==0 && response["error"]!=null) {
				Results.Add(new GeocodioResult() {
					Error = response["error"].ToString()
				});
			}
        }
        #endregion

    }
}
