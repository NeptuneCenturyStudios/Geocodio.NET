using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Geocodio
{
    public class GeocodioBatchResponse
    {
        #region Properties
        public List<GeocodioBatchResult> Results { get; private set; }
        #endregion

        #region Constructor
        public GeocodioBatchResponse(JToken response)
        {
            //create list
            Results = new List<GeocodioBatchResult>();
            
            try
            {
                var contents = response["results"].ToObject<IDictionary<string, JObject>>();
                foreach (KeyValuePair<string, JObject> result in ((Dictionary<string, JObject>)contents))
                {
                    Results.Add(new GeocodioBatchResult()
                    {
                        ID = result.Key,
                        Query = result.Value["query"],
                        Response = new GeocodioResponse(result.Value["response"])
                    });
                }
            }
            catch (Exception)
            {
                int count = 0;
                foreach (var result in response["results"])
                {
                    count++;
                    Results.Add(new GeocodioBatchResult()
                    {
                        ID = count.ToString(),
                        Query = (string)result["query"],
                        Response = new GeocodioResponse(result["response"])
                    });
                }
            }
        }
        #endregion
    }
}
