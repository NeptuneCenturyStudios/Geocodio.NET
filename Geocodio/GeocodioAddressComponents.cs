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
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
