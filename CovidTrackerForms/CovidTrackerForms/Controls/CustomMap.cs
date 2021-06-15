using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using CovidTrackerForms.Models;

namespace CovidTrackerForms.Controls
{
    public class CustomMap : Map
    {
        public static BindableProperty GeoFenceProperty =
            BindableProperty.Create(nameof(GeoFences), typeof(List<GeoFence>), typeof(CustomMap), new List<GeoFence>());

        public List<Models.GeoFence> GeoFences
        {
            get => GetValue(GeoFenceProperty) as List<GeoFence>;
            set => SetValue(GeoFenceProperty, value);
        }
        public static BindableProperty PointsProperty =
 BindableProperty.Create(nameof(Points), typeof(List<Models.Point>), typeof(CustomMap), new List<Models.Point>());
        public List<Models.Point> Points
        {
            get => GetValue(PointsProperty) as List<Models.Point>;
            set => SetValue(PointsProperty, value);
        }

    }
}
