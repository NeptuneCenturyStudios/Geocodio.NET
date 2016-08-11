using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geocodio
{
    public class GeocodioAddressComponents
    {
        public string Number { get; set; }
        public string Street { get; set; }
        public string Suffix { get; set; }
        public string PostDirectional { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string FormattedStreet { get; set; }
    }

    public class GeocodioAddressRequest {
        // This is not a supported parameter in geocodio
        //[JsonProperty(propertyName: "number")]
        //public string Number { get; set; }
        [JsonProperty(propertyName: "street")]
        public string Street { get; set; }
        [JsonProperty(propertyName: "city")]
        public string City { get; set; }
        [JsonProperty(propertyName: "county")]
        public string County { get; set; }
        [JsonProperty(propertyName: "state")]
        public string State { get; set; }
        [JsonProperty(propertyName: "postal_code")]
        public string PostalCode { get; set; }
        [JsonProperty(propertyName: "address")]
        public string Address { get; set; }
    }
}
