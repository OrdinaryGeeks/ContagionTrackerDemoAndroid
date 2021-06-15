using System;
using System.Collections.Generic;
using System.Text;
using CovidTrackerForms.Models;
using System.Threading.Tasks;
using SQLite;
using System.IO;

namespace CovidTrackerForms.Repositories
{
    public  class LocationRepository : ILocationRepository
    {

        private SQLiteAsyncConnection connection;
        private async Task CreateConnection()
        {
            if (connection != null)
            {
                return;
            }
            var databasePath =
            Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.MyDocuments), "Locations.db");
            connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<Location>();
        }


        public async Task Save(Location location)
        {

            await CreateConnection();
            await connection.InsertAsync(location);
        }
        public async Task<List<Location>> GetAllLocations()
        {
            await CreateConnection();
            var locations = await connection.Table<Location>
            ().ToListAsync();
            return locations;
        }

        public async Task<List<GeoFence>> GetAllGeoFences()
        {



            return  new List<GeoFence>();


        }
    }
}
