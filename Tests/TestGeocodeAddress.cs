using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TestGeocodeAddress
    {
        [TestMethod]
        public void GetGeoLocation()
        {

            //create object
            var geocodio = new Geocodio.Geocodio() { ApiKey = "546744456454a96ab412454693921ef325b15a3" };

            var resp = geocodio.GetGeolocation("875 Pone Lane, Franklin, PA 16323");

        }
    }
}
