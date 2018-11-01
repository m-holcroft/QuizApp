using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Data
{
    public class AzureDataService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<ScoresTable> _scoresTable;

        public async Task Initialise()
        {
            //Create our client
            MobileService = new MobileServiceClient(Common.Constants.ApplicationURL);

            const string path = "localstore.db";
            MobileServiceSQLiteStore store = new MobileServiceSQLiteStore(path);
            store.DefineTable<ScoresTable>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            _scoresTable = MobileService.GetSyncTable<ScoresTable>();
        }

        public async Task<List<ScoresTable>> GetScores()
        {
            await SyncScores();
            return await _scoresTable.OrderByDescending(x => x.Points).ToListAsync();
        }

        public async Task AddScore(ScoresTable newScore)
        {
            ScoresTable score = new ScoresTable { DisplayName = newScore.DisplayName, Points = newScore.Points, AchievedOn = newScore.AchievedOn };
            await _scoresTable.InsertAsync(score);
        }

        public async Task SyncScores()
        {
            try
            {
                await _scoresTable.PullAsync("allScores", _scoresTable.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch(Exception e)
            {

            }
        }
    }
}
