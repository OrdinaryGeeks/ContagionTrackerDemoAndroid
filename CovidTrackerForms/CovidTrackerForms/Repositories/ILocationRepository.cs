using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CovidTrackerForms.Models;
namespace CovidTrackerForms.Repositories
{
    public interface ILocationRepository
    {

        Task Save(Location location);
        Task<List<Location>> GetAllLocations();
        Task<List<GeoFence>> GetAllGeoFences();




    }
}
