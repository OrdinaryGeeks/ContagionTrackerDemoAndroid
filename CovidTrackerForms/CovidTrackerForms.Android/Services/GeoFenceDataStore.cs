using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CovidTrackerForms.Models;
using CovidTrackerForms.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;
using Xamarin.Essentials;

namespace CovidTrackerForms.Droid.Services
{
    public class GeoFenceDataStore : IGeoFenceDataStore
    {
        System.Net.Http.HttpClient client;


        public GeoFenceDataStore()
        {
            client = new HttpClient(new AndroidClientHandler());
            client.BaseAddress = new Uri("https://www.ordinarygeeks.com/CMEDemo/");


        }

        bool IsConnected => Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet;



        public async Task<List<GeoFence>> GetGeoFences()
        {

            if (IsConnected)
            {
                var json = await client.GetStringAsync($"api/Places/");
                return await Task.Run(() => JsonConvert.DeserializeObject<List<GeoFence>>(json));
            }

            return null;

        }
    }

}