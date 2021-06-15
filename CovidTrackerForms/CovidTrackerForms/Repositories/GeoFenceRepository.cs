using CovidTrackerForms.Models;
using CovidTrackerForms.Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CovidTrackerForms.Services;
using System.Collections.Concurrent;

namespace CovidTrackerForms.Repositories
{
   public   class GeoFenceRepository : IGeoFenceRepository
    {
        public static ConcurrentDictionary<int, GeoFence> enteredGeoFences;
        private SQLiteAsyncConnection connection;

        public GeoFenceDataStore geoFenceDataStore;

        public GeoFenceRepository()
        {
            enteredGeoFences = new ConcurrentDictionary<int, GeoFence>();
            geoFenceDataStore = new GeoFenceDataStore();
        }
        private async Task CreateConnection()
        {
            if (connection != null)
            {
                return;
            }
              
            
            
            
            
            var databasePath =
            Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.MyDocuments), "GeoFences.db");
            connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<GeoFence>();
            string check = "";
        }
        public async Task Save(GeoFence geoFence)
        {

            await CreateConnection();
            await connection.InsertAsync(geoFence);

        }


        public async Task<List<GeoFence>> GetAllGeoFences()
        {


            // await CreateConnection();
            // var geoFences = await connection.Table<GeoFence>
            // ().ToListAsync();
            // return geoFences;

            return await geoFenceDataStore.GetGeoFences();
        }
    }
}
