using System;
using System.ComponentModel;
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
    public class DisplayResultsViewModel : Common.ObservableBase, INotifyPropertyChanged
    {
        #region Constructors
        public DisplayResultsViewModel()
        {
            _quizInstance = new QuizInformation();
            _finalScore = -1;
            SaveScoreAsyncCommand = new Command(async () => await SaveScoreAsync());
        }
        public DisplayResultsViewModel(QuizInformation q)
        {
            _quizInstance = q;
            _finalScore = q.Score;
            _user = q.User;
        }
        #endregion

        #region Members
        private QuizInformation _quizInstance;
        private int _finalScore;
        private string _user;

        public string User
        {
            set { SetProperty<string>(ref _user, value, "User"); }
            get { return _user; }
        }
        public int FinalScore
        {
            set { SetProperty<int>(ref _finalScore, value, "FinalScore"); }
            get { return _finalScore; }
        }
        public QuizInformation QuizInstance
        {
            set { SetProperty<QuizInformation>(ref _quizInstance, value, "QuizInstance"); }
            get { return _quizInstance; }
        }
        #endregion

        #region Commands
        public ICommand SaveScoreAsyncCommand { get; private set; }
        #endregion

        #region Functions
        public async Task SaveScoreAsync()
        {
            Data.ScoresTable score = new Data.ScoresTable();
            score.DisplayName = _quizInstance.User;
            score.Points = _quizInstance.Score;
            score.AchievedOn = DateTime.UtcNow;
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
