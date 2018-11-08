using System;
using System.ComponentModel;
using System.Threading.Tasks;
using QuizApp.Models;
using QuizApp.Views;

namespace QuizApp.ViewModels
{
    public class DisplayResultsViewModel : Common.ObservableBase, INotifyPropertyChanged
    {
        /*Constructors*/
        public DisplayResultsViewModel()
        {
            _quizInstance = new QuizInformation();
            _finalScore = -1;
        }
        public DisplayResultsViewModel(QuizInformation q)
        {
            _quizInstance = q;
            _finalScore = q.Score;
            _user = q.User;
        }

        /*Members*/
        private QuizInformation _quizInstance;
        private int _finalScore;
        private string _user;

        /*Getters / Setters with Data Binding*/
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

        /*Methods*/
        public async Task SaveScore()
        {
            Data.ScoresTable score = new Data.ScoresTable();
            score.DisplayName = _quizInstance.User;
            score.Points = _quizInstance.Score;
            score.AchievedOn = DateTime.UtcNow;
            await App.MainNavigation.PushModalAsync(new LoadingPage());
            await App.AzureService.AddScore(score);
            await App.AzureService.SyncScores();
            await App.MainNavigation.PopToRootAsync(false);
            await App.MainNavigation.PushAsync(new DisplayScoresPage());
            await App.MainNavigation.PopModalAsync();
        }


    }
}
