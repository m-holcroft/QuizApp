using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Data
{
    public class AzureDataService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<ScoresTable> _scoresTable;
        IMobileServiceSyncTable<QuestionsTable> _questionsTable;

        public async Task Initialise()
        {
            //Create our client
            MobileService = new MobileServiceClient(Common.Constants.ApplicationURL);

            const string path = "localstore.db";
            MobileServiceSQLiteStore store = new MobileServiceSQLiteStore(path);


            store.DefineTable<ScoresTable>();
            store.DefineTable<QuestionsTable>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            _scoresTable = MobileService.GetSyncTable<ScoresTable>();
            _questionsTable = MobileService.GetSyncTable<QuestionsTable>();
        }

        #region ScoresFunctions
        public async Task<List<ScoresTable>> GetScores()
        {
            await SyncScores();
            return await _scoresTable.OrderByDescending(x => x.Points).ToListAsync();
        }

        public async Task AddScore(ScoresTable newScore)
        {
            ScoresTable score = new ScoresTable {
                DisplayName = newScore.DisplayName,
                Points = newScore.Points,
                AchievedOn = newScore.AchievedOn };
            await _scoresTable.InsertAsync(score);
        }

        public async Task SyncScores()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await _scoresTable.PullAsync("allScores", _scoresTable.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch(MobileServicePushFailedException mspfe)
            {
                if(mspfe.PushResult != null)
                {
                    syncErrors = mspfe.PushResult.Errors;
                }
            }

            if(syncErrors != null)
            {
                foreach(MobileServiceTableOperationError error in syncErrors)
                {
                    if(error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        await error.CancelAndDiscardItemAsync();
                    }
                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
        #endregion

        #region QuestionsFunctions
        public async Task<List<QuestionsTable>> GetQuestions(int questionGroup)
        {
            await SyncQuestions();
            return await _questionsTable.Where(x => x.QuestionGroup == questionGroup).OrderBy(x => x.QuestionNumber).ToListAsync();
        }

        public async Task AddQuestion(QuestionsTable newQuestion)
        {
            await _questionsTable.InsertAsync(newQuestion);
        }

        public async Task SyncQuestions()
        {
            try
            {
                await _questionsTable.PullAsync("allQuestionsInGroup", _questionsTable.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch (Exception e)
            {

            }
        }
        #endregion
    }
}
