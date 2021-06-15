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
using Autofac;
using CovidTrackerForms.Droid.Services;
using CovidTrackerForms.Services;


namespace CovidTrackerForms.Droid
{
    public class Bootstrapper : CovidTrackerForms.Bootstrapper
    {
        public static void Init()
        {
            var instance = new Bootstrapper();
        }
        protected override void Initialize()
        {
            base.Initialize();
            ContainerBuilder.RegisterType <LocationTrackingService>().As<ILocationTrackingService>().SingleInstance();
        }
    }
}