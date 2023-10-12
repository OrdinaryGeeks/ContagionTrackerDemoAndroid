using CovidTrackerForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CovidTrackerForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
     //   Services.Global global;
        Models.GeoFence geoFence;
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Services.Global.geoFenceDataStormForms = new Services.GeoFenceDataStore();
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                   await viewModel.LoadData();
                    var location = await Geolocation.GetLocationAsync();
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(location.Latitude, location.Longitude),
                    Distance.FromKilometers(2)));
                    //location changing should make it change and redraw the map
                    geoFence = viewModel.checkClosestAndInRange(location);
                    
                });
                return true;
            });
        }
        

        private async void checkIn_Clicked_1(object sender, EventArgs e)
        {
            if (geoFence != null && geoFence.Title != "")
            {
                Models.CheckIn checkedIn = new Models.CheckIn { userCMEID = 1, placeID = geoFence.ID, BeginTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc) , EndTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc) };

                //Task.Run(async () => { await CovidTrackerForms.Services.Global.geoFenceDataStormForms.PostCheckInAsync(checkedIn); });

                if (await Services.Global.geoFenceDataStormForms.PostCheckInAsync(checkedIn))
                    currentLocation.Text = geoFence.Title;

                

            }
        }
    }
}