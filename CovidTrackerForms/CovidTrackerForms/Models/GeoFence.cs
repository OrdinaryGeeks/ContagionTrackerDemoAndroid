using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTrackerForms.Models
{
    public class GeoFence 
    {

        public int ID { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public double Radius { get; set; }
        public string Title { get; set; }
        public GeoFence(double Latitude, double Longitude, double Radius)
        {
            Lat = Latitude;
            Long = Longitude;
            this.Radius = Radius;

        }
        public GeoFence() { }
    }
}
