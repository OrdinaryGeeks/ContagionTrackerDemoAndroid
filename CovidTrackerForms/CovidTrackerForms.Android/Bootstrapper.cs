
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
        private void FinishInitialization()
        {
            var container = ContainerBuilder.Build();
            Resolver.Initialize(container);
        }
    }
}