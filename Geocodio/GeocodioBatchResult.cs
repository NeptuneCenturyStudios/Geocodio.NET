using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geocodio
{
    public class GeocodioBatchResult
    {
        public string ID { get; set; }
        public string Query { get; set; }
        public GeocodioResponse Response { get; set; }
    }
}
