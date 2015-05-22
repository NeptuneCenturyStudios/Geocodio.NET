using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geocodio
{
    public class GeocodioBatchResponse
    {
        #region Properties
        public List<GeocodioBatchResult> Results { get; set; }
        #endregion

        #region Constructor
        public GeocodioBatchResponse(JToken response) {

            //create list
            Results = new List<GeocodioBatchResult>();

            //iterate through each result
            foreach(var result in response["results"])
            {
                //create new result
                var geoRes = new GeocodioBatchResult()
                {
                    Query = (string)result["query"],
                    Response = new GeocodioResponse(result["response"])
                };

                Results.Add(geoRes);
            }

        }
        #endregion
    }
}
