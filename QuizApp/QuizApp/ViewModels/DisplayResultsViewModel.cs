using System;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using QuizApp.Models;
using QuizApp.Views;
using Xamarin.Forms;

namespace QuizApp.ViewModels
{
    /// <summary>
    /// The view model for the disaplay results page.
    /// </summary>
    public class DisplayResultsViewModel : Common.ObservableBase
    {
        #region Constructors
        /// <summary>
        /// Initialises the instance of the ViewModel.
        /// </summary>
        /// <param name="q">The quiz instance being played.</param>
        public DisplayResultsViewModel(QuizInformation q)
        {
            _quizInstance = q;
            _finalScore = q.Score;
            _displayName = q.User;
            SaveScoreAsyncCommand = new Command(async () => await SaveScoreAsync());
        }
        #endregion

        #region Members
        private QuizInformation _quizInstance;
        private int _finalScore;
        private string _displayName;

        /// <summary>
        /// The DisplayName of the user, bound to a UI Control
        /// </summary>
        public string DisplayName
        {
            set { SetProperty<string>(ref _displayName, value, "DisplayName"); }
            get { return _displayName; }
        }

        /// <summary>
        /// The score the user achieved, bound to a UI Control
        /// </summary>
        public int FinalScore
        {
            set { SetProperty<int>(ref _finalScore, value, "FinalScore"); }
            get { return _finalScore; }
        }

        /// <summary>
        /// The quiz the user played.
        /// </summary>
        public QuizInformation QuizInstance
        {
            set { SetProperty<QuizInformation>(ref _quizInstance, value, "QuizInstance"); }
            get { return _quizInstance; }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command for saving the score to the server. Initialised with <see cref="SaveScoreAsync()"/>.
        /// </summary>
        public ICommand SaveScoreAsyncCommand { get; private set; }
        #endregion

        #region Functions
        /// <summary>
        /// Save a score to the database.
        /// </summary>
        /// <returns></returns>
        public async Task SaveScoreAsync()
        {
            Data.ScoresTable score = new Data.ScoresTable();
            score.DisplayName = _quizInstance.User;
            score.Points = _quizInstance.Score;
            score.AchievedOn = DateTime.UtcNow;

            string[] loc = await App.GetLocation();
            score.Latitude = loc[0];
            score.Longitude = loc[1];

            await App.MainNavigation.PushModalAsync(new LoadingPage());
            try
            {
                await App.AzureService.AddScore(score);
                await App.AzureService.SyncScores();
            }
            catch(Exception e)
            {
                App.PopUpHelper.ShortAlert(Common.Constants.ConnectionFailedMessage);
            }
            await App.MainNavigation.PopToRootAsync(false);
            await App.MainNavigation.PushAsync(new DisplayScoresPage());
            await App.MainNavigation.PopModalAsync();
        }
        #endregion
    }
}
