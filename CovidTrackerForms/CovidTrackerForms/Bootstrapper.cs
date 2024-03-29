﻿using Autofac;
using CovidTrackerForms.Repositories;
using CovidTrackerForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace CovidTrackerForms
{
    public class Bootstrapper
    {
        protected ContainerBuilder ContainerBuilder
        {
            get; private
set;
        }
        public Bootstrapper()
        {
            Initialize();
            FinishInitialization();
        }
        protected virtual void Initialize()
        {
            ContainerBuilder = new ContainerBuilder();
            var currentAssembly = Assembly.GetExecutingAssembly();
            foreach (var type in currentAssembly.DefinedTypes.
            Where(e => e.IsSubclassOf(typeof(Page))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }
            foreach (var type in currentAssembly.DefinedTypes.
     Where(e => e.IsSubclassOf(typeof(ViewModel))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }
            ContainerBuilder.RegisterType<LocationRepository>
            ().As<ILocationRepository>();

            foreach (var type in currentAssembly.DefinedTypes.
   Where(e => e.IsSubclassOf(typeof(ViewModel))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }
            ContainerBuilder.RegisterType<GeoFenceRepository>
            ().As<IGeoFenceRepository>();
        
    }


        private void FinishInitialization()
        {
            var container = ContainerBuilder.Build();
            Resolver.Initialize(container);
        }
    }
}
