using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using CovidTrackerForms.Droid.Services;
using CovidTrackerForms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Android;
using Plugin.Permissions;
using AndroidX.Core.App;
using Java.Interop;

namespace CovidTrackerForms.Droid
{
    [Activity(Label = "CovidTrackerForms", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        Globals globals;

        const int RequestLocationId = 0;

        readonly string[] AllPermissions =
        {
    Manifest.Permission.AccessCoarseLocation,
    Manifest.Permission.AccessFineLocation,
    Manifest.Permission.Internet,
    Manifest.Permission.ReceiveBootCompleted,
    Manifest.Permission.AccessNetworkState


};


        public static List<GeoFence> geoFences;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            CheckPermissionGranted(Manifest.Permission.Internet, 1001);
            CheckPermissionGranted(Manifest.Permission.AccessFineLocation, 1002);
            CheckPermissionGranted(Manifest.Permission.ReceiveBootCompleted, 1003);
        


            globals = new Globals();
            Task.Run(() => {
                geoFences = Globals.geoFenceDataStore.GetGeoFences().Result; });

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.FormsMaps.Init(this, savedInstanceState);



            Bootstrapper.Init();
           
            LoadApplication(new App());
        }

        private void RequestPermissions(string Permission, int RequestCode)
        {

            ActivityCompat.RequestPermissions(this, new string[] { Permission }, RequestCode);
            /*    if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.AccessFineLocation))
                {
                    // Provide an additional rationale to the user if the permission was not granted
                    // and the user would benefit from additional context for the use of the permission.
                    // For example if the user has previously denied the permission.
                    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessFineLocation }, 1000);


                }
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReceiveBootCompleted }, 1002);
            */

        }
        [Export]
        public void CheckPermissionGranted(string Permissions, int RequestCode)
        {
            // Check if the permission is already available.
            if (ActivityCompat.CheckSelfPermission(this, Permissions) != Permission.Granted)
            {
                Console.WriteLine(Permissions + " not granted");
                RequestPermissions(Permissions, RequestCode);
            }



        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == 1002)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
                {
                    Console.WriteLine("Location permissions granted.");
                }
                else
                {
                    Console.WriteLine("Location permissions denied.");
                }
            }
            else if (requestCode == 1001)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
                {
                    Console.WriteLine("Internet permissions granted.");
                }
                else
                    Console.WriteLine("Internet denied");
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        } 
        }

        /* public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
         {
             if (requestCode == RequestLocationId)
             {
               *//*  if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
             // Permissions granted - display a message.
         else
             // Permissions denied - display a message.*//*
     }
             else
             {
                 base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
             }
         }*/
    }
}