using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using CovidTrackerForms.Controls;
using CovidTrackerForms.Droid.Renderers;
using CovidTrackerForms.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using System.Collections.Generic;

[assembly: ExportRenderer(typeof(CustomMap),
typeof(CustomMapRenderer))]

namespace CovidTrackerForms.Droid.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {
        
        private GoogleMap map;
        List<Point> pointList;
       
        public CustomMapRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            this.map = map;
            base.OnMapReady(map);
        }

        private void UpdatePoints()
        {


        }
        protected override void OnElementPropertyChanged(object sender,
 PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName == CustomMap.GeoFenceProperty.PropertyName)
            {
                //THIS DOESNT WORK BUT LEAVING IT IN AS A LEARNING TOOL IT WONT FIRE BECAUSE ITS NOT BOUND ON THE MAP LIKE BINDING POINTS
                var element = (CustomMap)Element;

                foreach (var fence in element.GeoFences)
                {
                    var options = new CircleOptions();
                    options.InvokeStrokeWidth(10);
                    options.InvokeStrokeColor(Color.Black.ToAndroid());
                    // options.InvokeFillColor(point.Heat.ToAndroid());
                    options.InvokeRadius(fence.Radius);
                    options.InvokeCenter(new
                    LatLng(fence.Lat,
                    fence.Long));
                    map.AddCircle(options);
                }
            }
            if (e.PropertyName == CustomMap.PointsProperty.PropertyName)
            {

                if (map != null)
                    map.Clear();
                var element = (CustomMap)Element;
                foreach (var point in element.Points)
                {
                    var options = new CircleOptions();
                    options.InvokeStrokeWidth(0);
                    options.InvokeFillColor(point.Heat.ToAndroid());
                    options.InvokeRadius(200);
                    options.InvokeCenter(new
                    LatLng(point.Location.Latitude,
                    point.Location.Longitude));
                    map.AddCircle(options);

                }

                foreach(var fence in MainActivity.geoFences)
                {
                    var options = new CircleOptions();
                    options.InvokeStrokeWidth(10);
                    options.InvokeStrokeColor(Color.Black.ToAndroid());
                    // options.InvokeFillColor(point.Heat.ToAndroid());
                    options.InvokeRadius(fence.Radius);
                    options.InvokeCenter(new
                    LatLng(fence.Lat,
                    fence.Long));
                    map.AddCircle(options);



                }

                foreach (var fence in GeoFenceRepository.enteredGeoFences.Values)
                {
                    var options = new CircleOptions();
                    options.InvokeStrokeWidth(10);
                    options.InvokeStrokeColor(Color.Red.ToAndroid());
                    // options.InvokeFillColor(point.Heat.ToAndroid());
                    options.InvokeRadius(fence.Radius * 1.1);
                    options.InvokeCenter(new
                    LatLng(fence.Lat,
                    fence.Long));
                    map.AddCircle(options);


                    
                }
                Control.GetMapAsync(this);
            }

        }
    }
}