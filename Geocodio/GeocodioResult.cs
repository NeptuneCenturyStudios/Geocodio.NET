using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geocodio
{
    public class GeocodioResult
    {
        #region Properties

        public GeocodioAddressComponents AddressComponents { get; set; }
        public string FormattedAddress { get; set; }
        public GeocodioLocation Location { get; set; }
        public decimal Accuracy { get; set; }
        public string Error { get; set; }
        #endregion

    }
}
