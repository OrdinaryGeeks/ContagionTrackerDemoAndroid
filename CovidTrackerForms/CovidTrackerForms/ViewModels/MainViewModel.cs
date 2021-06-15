using CovidTrackerForms.Repositories;
using CovidTrackerForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
namespace CovidTrackerForms.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly ILocationRepository  locationRepository;
        private readonly ILocationTrackingService locationTrackingService;

        private readonly IGeoFenceRepository geoFenceRepository;
        public MainViewModel(ILocationTrackingService  locationTrackingService,ILocationRepository locationRepository, IGeoFenceRepository geoFenceRepository)
        {
            this.geoFenceRepository = geoFenceRepository;
            this.locationTrackingService = locationTrackingService;
            this.locationRepository = locationRepository;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                locationTrackingService.StartTracking();
                await LoadData();
            });

        }
        public Models.GeoFence checkClosestAndInRange(Location location)
        {
            double metersCheck = 0.0;
            double smallestMeters = double.MaxValue;
            Models.GeoFence smallestGeoFence = new Models.GeoFence();
            if(geoFences != null && geoFences.Count > 0)
            foreach (Models.GeoFence fence in geoFences)
            {
                metersCheck = Distance.BetweenPositions(new Position(location.Latitude, location.Longitude), new Position(fence.Lat, fence.Long)).Meters;
                if (metersCheck < fence.Radius && smallestMeters >= metersCheck)
                {

                    smallestMeters = metersCheck;
                    smallestGeoFence= fence;

                }
            }

            return smallestGeoFence;

        }
        public async Task LoadData()
        {
            //var geoFenceAll = geoFenceRepository.GetAllGeoFences();
            geoFences = await geoFenceRepository.GetAllGeoFences();
            var locations = await locationRepository.GetAllLocations();
            var pointList = new List<Models.Point>();
            foreach (var location in locations)
            {
                //If no points exist, create a new one an continue to the next  location in the list
                if (!pointList.Any())
                {
                    pointList.Add(new Models.Point() { Location = location });
                    continue;
                }
                var pointFound = false;
                //try to find a point for the current location
                foreach (var point in pointList)
                {
                    var distance =
                    Xamarin.Essentials.Location.CalculateDistance(
                    new Xamarin.Essentials.Location(
                    point.Location.Latitude, point.Location.Longitude),
                    new Xamarin.Essentials.Location(location.Latitude,
                    location.Longitude), DistanceUnits.Kilometers);
                    if (distance < 0.2)
                    {
                        pointFound = true;
                        point.Count++;
                        break;
                    }
                }
                //if no point is found, add a new Point to the list of points
                if (!pointFound)
                {
                    pointList.Add(new Models.Point() { Location = location });
                }

            }


            if (pointList == null || !pointList.Any())
            {
                return;
            }
            var pointMax = pointList.Select(x => x.Count).Max();
            var pointMin = pointList.Select(x => x.Count).Min();
            var diff = (float)(pointMax - pointMin);

            foreach (var point in pointList)
            {
                var heat = (2f / 3f) - ((float)point.Count / diff);
                point.Heat = Color.FromHsla(heat, 1, 0.5);
            }
            Points = pointList;
        }

        private List<Models.GeoFence> geoFences;
        public List<Models.GeoFence> GeoFences
        {

            get => geoFences;

            set {
                geoFences = value;
                RaisePropertyChanged(nameof(GeoFences));
            }
        }
                

        private List<Models.Point> points;
        public List<Models.Point> Points
        {
            get => points;
            set
            {
                points = value;
                RaisePropertyChanged(nameof(Points));
            }
        }
    }

}
