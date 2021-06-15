using CovidTrackerForms.Services;
using CovidTrackerForms.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidTrackerForms
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = Resolver.Resolve<MainView>();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

            MainPage = Resolver.Resolve<MainView>();

        }
    }
}
