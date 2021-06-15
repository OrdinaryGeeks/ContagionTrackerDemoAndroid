using Android.App;
using Android.App.Job;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CovidTrackerForms.Droid.Services;
using CovidTrackerForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationTrackingService))]
namespace CovidTrackerForms.Droid.Services
{
    public class LocationTrackingService : ILocationTrackingService
    {

        public void StartTracking()
        {
            var javaClass = Java.Lang.Class.FromType(typeof(LocationJobService));
            var componentName = new ComponentName(Android.App.Application.Context,            javaClass);
            var jobBuilder = new JobInfo.Builder(1, componentName);
            jobBuilder.SetOverrideDeadline(1000);
            jobBuilder.SetPersisted(true);
            jobBuilder.SetRequiresDeviceIdle(false);
            jobBuilder.SetRequiresBatteryNotLow(true);
            var jobInfo = jobBuilder.Build();

            var jobScheduler = (JobScheduler)Android.App.Application.Context.GetSystemService(Context.JobSchedulerService);
            jobScheduler.Schedule(jobInfo);

        }
    }
}