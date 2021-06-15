using CovidTrackerForms.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidTrackerForms.Repositories
{
    public interface IGeoFenceRepository
    {


        Task Save(GeoFence geofence);


        Task<List<GeoFence>> GetAllGeoFences();
    }
}
