using CovidTrackerForms.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidTrackerForms.Services
{
    public interface IGeoFenceDataStore
    {

         Task<List<GeoFence>> GetGeoFences();

    }
}
