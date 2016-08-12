using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geocodio
{
    public class GeocodioAddressRequestBatch : Dictionary<int, GeocodioAddressRequest>
    {
        public GeocodioAddressRequestBatch()
            : base()
        {
        }

        /// <summary>
        /// Builds a GeocodioAddressRequestBatch from an array of GeocodioAddressRequest objects
        /// </summary>
        /// <param name="addresses"></param>
        public GeocodioAddressRequestBatch(GeocodioAddressRequest[] addresses)
        {
            // Add the addresses to the internal dictionary, using the index as the key
            foreach (var address in addresses)
            {
                Add(Array.IndexOf(addresses, address) + 1, address);
            }

        }
    }
}
