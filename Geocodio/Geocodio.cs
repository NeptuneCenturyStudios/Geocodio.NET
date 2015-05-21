using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        public Geocodio()
        {

        }
        #endregion

        #region Methods

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
                
                //create response from json
                return GeocodioResponse.CreateFromJSON(json);
            }

            
        }

        /// <summary>
        /// Asyncronously looks up a single address and returns the geocoded result from Geocodio
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<GeocodioResponse> GetGeolocationAsync(string address)
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
            var webresp = await webreq.GetResponseAsync();

            //read the response into a json string
            using (var sr = new StreamReader(webresp.GetResponseStream()))
            {
                //read the stream
                var json = await sr.ReadToEndAsync();

                //create response from json
                return GeocodioResponse.CreateFromJSON(json);
            }
        }
        #endregion
    }
}
