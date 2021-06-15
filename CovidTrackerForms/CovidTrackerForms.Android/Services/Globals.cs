using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CovidTrackerForms.Droid.Services
{
   public class Globals
    {
        public static GeoFenceDataStore geoFenceDataStore;

        
        public Globals()
        {
            geoFenceDataStore = new GeoFenceDataStore();
            
        }
    }
}