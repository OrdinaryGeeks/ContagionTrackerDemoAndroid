using Android.App;
using Android.App.Job;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CovidTrackerForms.Models;
using CovidTrackerForms.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Maps;
namespace CovidTrackerForms.Droid.Services
{
    [Service(Name = "CovidTrackerForms.Droid.Services.LocationJobService",
    Permission = "android.permission.BIND_JOB_SERVICE")]

    public class LocationJobService :JobService, ILocationListener
    {
     
        public static LocationManager locationManager;
        private ILocationRepository locationRepository;
        public LocationJobService()
        {
            locationRepository = Resolver.Resolve<ILocationRepository>();
        }

        public override bool OnStopJob(JobParameters @params)
        {
            return true;
        }
        public override bool OnStartJob(JobParameters @params)
        {

          locationManager = (LocationManager)ApplicationContext.GetSystemService (Context.LocationService);

            locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 10L, 0.01f, this);
            return true;
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            var newLocation = new Models.Location(location.Latitude,  location.Longitude);


            List<int> dirtyFences = new List<int>();

            
            if(GeoFenceRepository.enteredGeoFences != null)
                if (GeoFenceRepository.enteredGeoFences.Count > 0)
                    foreach (int key in GeoFenceRepository.enteredGeoFences.Keys)
            {

                if (Distance.BetweenPositions(new Position(location.Latitude, location.Longitude), new Position(GeoFenceRepository.enteredGeoFences[key].Lat, GeoFenceRepository.enteredGeoFences[key].Long)).Meters > GeoFenceRepository.enteredGeoFences[key].Radius)
                    GeoFenceRepository.enteredGeoFences.TryRemove(key, out GeoFence returned);


            }

         



         //   GeoFenceRepository.enteredGeoFences.Clear();
            if (MainActivity.geoFences != null)
            {
                foreach(GeoFence fence in MainActivity.geoFences)
                {
                   
                    if (!GeoFenceRepository.enteredGeoFences.Values.Contains(fence))
                    {
                     
                        if (Distance.BetweenPositions(new Position(location.Latitude, location.Longitude), new Position(fence.Lat, fence.Long)).Meters < fence.Radius)
                        {
                            GeoFenceRepository.enteredGeoFences.TryAdd(fence.ID, fence);

                            //Just to trigger the load data
                           
                        }
                    }
               
                }

            }
            locationRepository.Save(newLocation);
        }

        public void OnProviderDisabled(string provider)
        {
           // throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
           // throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
           // throw new NotImplementedException();
        }
    }
}