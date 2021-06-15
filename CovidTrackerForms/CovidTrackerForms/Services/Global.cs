using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTrackerForms.Services
{
   public  class Global
    {

        public static GeoFenceDataStore geoFenceDataStormForms;

        public Global() { geoFenceDataStormForms = new GeoFenceDataStore(); }
    }
}
