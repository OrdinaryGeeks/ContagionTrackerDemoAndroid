using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using CovidTrackerForms.Droid.Services;
using CovidTrackerForms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidTrackerForms.Droid
{
    [Activity(Label = "CovidTrackerForms", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        Globals globals;
       
        public static List<GeoFence> geoFences;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            globals = new Globals();
            Task.Run(() => { geoFences =  Globals.geoFenceDataStore.GetGeoFences().Result; });
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.FormsMaps.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            Bootstrapper.Init();
            LoadApplication(new App());
        }
 

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode,permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode,permissions, grantResults);
        }
    }
}