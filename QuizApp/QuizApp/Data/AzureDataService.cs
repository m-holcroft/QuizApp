using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QuizApp.Data
{
    public class AzureDataService
    {
        /// <summary>
        /// The object that will handle the vast majority of the interactions with the database.
        /// </summary>
        public MobileServiceClient MobileService { get; set; }
        /// <summary>
        /// The object that will contain the table of <see cref="ScoresTable"/> objects.
        /// </summary>
        IMobileServiceSyncTable<ScoresTable> _scoresTable;
        /// <summary>
        /// The object that will contain the table of <see cref="QuestionsTable"/> objects.
        /// </summary>
        IMobileServiceSyncTable<QuestionsTable> _questionsTable;

        /// <summary>
        /// Initialise the mobile service and the local database.
        /// </summary>
        /// <returns></returns>
        public async Task Initialise()
        {
            MobileService = new MobileServiceClient(Common.Constants.ApplicationURL);

            const string path = "devicestore.db";
            MobileServiceSQLiteStore store = new MobileServiceSQLiteStore(path);

            store.DefineTable<ScoresTable>();
            store.DefineTable<QuestionsTable>();

            await MobileService.SyncContext.InitializeAsync(store);

            _scoresTable = MobileService.GetSyncTable<ScoresTable>();
            _questionsTable = MobileService.GetSyncTable<QuestionsTable>();

            try
            {
                await SyncQuestions();
                await SyncScores();
            }
            catch(Exception e)
            {
                App.PopUpHelper.ShortAlert(e.Message);
            }
        }



        #region ScoresFunctions
        public async Task<List<ScoresTable>> GetScores()
        {
            return await _scoresTable.OrderByDescending(x => x.Points).ToListAsync();
        }

        public async Task AddScore(ScoresTable newScore)
        {
            ScoresTable score = new ScoresTable {
                DisplayName = newScore.DisplayName,
                Points = newScore.Points,
                AchievedOn = newScore.AchievedOn,
                Latitude = newScore.Latitude,
                Longitude = newScore.Longitude,
                Synced = 0
                };
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

        public async Task DeleteScore(ScoresTable table)
        {
            await _scoresTable.DeleteAsync(table);
        }
        #endregion

        #region QuestionsFunctions
        public async Task<List<QuestionsTable>> GetQuestions(int questionGroup)
        {
            return await _questionsTable.OrderBy(x => x.QuestionNumber).ToListAsync(); 
        }

        public async Task AddQuestion(QuestionsTable newQuestion)
        {
            await _questionsTable.InsertAsync(newQuestion);
        }

        public async Task SyncQuestions()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await _questionsTable.PullAsync("allQuestionsInGroup", _questionsTable.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch (MobileServicePushFailedException mspfe)
            {
                if (mspfe.PushResult != null)
                {
                    syncErrors = mspfe.PushResult.Errors;
                }
            }

            if (syncErrors != null)
            {
                foreach (MobileServiceTableOperationError error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
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
    }
}
