using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Geocodio
{

    /// <summary>
    /// Provides methods to access Geocodio's REST API
    /// </summary>
    public class Geocodio
    {
        #region Properties
        /// <summary>
        /// Gets or sets the api key used to authenticate requests to Geocodio's REST API
        /// </summary>
        public string ApiKey { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of the Geocodio class
        /// </summary>
        public Geocodio() { }
        #endregion

        #region Methods

        #region Geocoding

        /// <summary>
        /// Looks up a single address and returns the geocoded result from Geocodio
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        /// <exception cref="WebException">Throws a WebException if an error occurs</exception>
        public GeocodioResponse GetGeolocation(string address)
        {
            //some sanity checks
            if (string.IsNullOrWhiteSpace(address))
            {
                //must have an address
                throw new ArgumentNullException(nameof(address));
            }

            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                //must have an api key
                throw new InvalidOperationException("An API key is required for this request");
            }

            //create a web request
            var webreq = WebRequest.Create($"https://api.geocod.io/v1/geocode?q={address}&api_key={ApiKey}");

            //ensure our response comes in json
            webreq.ContentType = "application/json";

            //execute and get the response
            var webresp = webreq.GetResponse();

            //read the response into a json string
            using (var sr = new StreamReader(webresp.GetResponseStream()))
            {
                //read the stream
                var json = sr.ReadToEnd();

                //deserialze
                var jobj = (JToken)JsonConvert.DeserializeObject(json);

                //create response from json
                return new GeocodioResponse(jobj);
            }


        }

        /// <summary>
        /// Asyncronously looks up a single address and returns the geocoded result from Geocodio
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<GeocodioResponse> GetGeolocationAsync(string address)
        {
            return await Task.Factory.StartNew(() => { return GetGeolocation(address); });
        }

        /// <summary>
        /// Executes a batch request to look up multiple addresses
        /// </summary>
        /// <param name="addresses"></param>
        /// <remarks>You can batch up to 10000 addresses in a single request</remarks>
        /// <returns></returns>
        public GeocodioBatchResponse GetGeolocations(Object addresses)
        {

            //some sanity checks
            if (addresses == null)
            {
                //must have an address
                throw new ArgumentNullException(nameof(addresses));
            }

            if (addresses.GetType()==typeof(String[]) && (((String[])addresses).Length > 10000))
            {
                //too many addresses in the batch
                throw new InvalidOperationException("Geocodio only allows a maximum of 10000 addresses per batch (array of string addresses)");
            } else if (addresses.GetType() == typeof(List<GeocodioAddressRequest>) && (((List<GeocodioAddressRequest>)addresses).Count > 10000)) {
				//too many addresses in the batch
				throw new InvalidOperationException("Geocodio only allows a maximum of 10000 addresses per batch (Dictionary of identified addresses)");
			}

            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                //must have an api key
                throw new InvalidOperationException("An API key is required for this request");
            }

            //create a web request
            var webreq = WebRequest.Create($"https://api.geocod.io/v1/geocode?api_key={ApiKey}");

            //this request is sent as a post
            webreq.Method = "POST";
            //ensure our response comes in json
            webreq.ContentType = "application/json";

            //build the body of the request
            using (var sw = new StreamWriter(webreq.GetRequestStream()))
            {
                //serialize our addresses as a json string
                var json = JsonConvert.SerializeObject(addresses);
                //write the string to the stream
                sw.Write(json);
            }

            //execute and get the response
            var webresp = webreq.GetResponse();

            //read the response into a json string
            using (var sr = new StreamReader(webresp.GetResponseStream()))
            {
                //read the stream
                var json = sr.ReadToEnd();

                //deserialze
                var jobj = (JToken)JsonConvert.DeserializeObject(json);

                //create response from json
                return new GeocodioBatchResponse(jobj);
            }
        }

        /// <summary>
        /// Asyncronously executes a batch request to look up multiple addresses
        /// </summary>
        /// <param name="addresses"></param>
        /// <remarks>You can batch up to 10000 addresses in a single request</remarks>
        /// <returns></returns>
        public async Task<GeocodioBatchResponse> GetGeolocationsAsync(string[] addresses)
        {
            return await Task.Factory.StartNew(() => { return GetGeolocations(addresses); });
        }

        #endregion

        #region Reverse Geocoding

        /// <summary>
        /// Looks up an address based on latitude and longitue coordinates
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public GeocodioResponse GetAddress(double lat, double lng)
        {
            
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                //must have an api key
                throw new InvalidOperationException("An API key is required for this request");
            }

            //create a web request
            var webreq = WebRequest.Create($"https://api.geocod.io/v1/reverse?q={lat},{lng}&api_key={ApiKey}");

            //ensure our response comes in json
            webreq.ContentType = "application/json";

            //execute and get the response
            var webresp = webreq.GetResponse();

            //read the response into a json string
            using (var sr = new StreamReader(webresp.GetResponseStream()))
            {
                //read the stream
                var json = sr.ReadToEnd();

                //deserialze
                var jobj = (JToken)JsonConvert.DeserializeObject(json);

                //create response from json
                return new GeocodioResponse(jobj);
            }


        }

        #endregion

        #endregion
    }
}
