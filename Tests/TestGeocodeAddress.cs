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
            var geocodio = new Geocodio.Geocodio() { ApiKey = "" };

            var resp = geocodio.GetGeolocation("60 E Broadway, Bloomington, MN 55425");

        }
    }
}
