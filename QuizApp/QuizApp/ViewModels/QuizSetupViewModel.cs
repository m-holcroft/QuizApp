using QuizApp.Models;
using QuizApp.Common;
using System;
using Xamarin.Forms;

namespace QuizApp.ViewModels
{
    public class QuizSetupViewModel : ObservableBase
    {
        #region Constructors
        public QuizSetupViewModel()
        {
            DisplayName = "";
            _quizInstance = new QuizInformation();
            //TODO: Change this to save to Azure Questions Table, create that table too
        }
        #endregion

        #region Members
        private QuizInformation _quizInstance;
        public QuizInformation QuizInstance
        {
            set { SetProperty<QuizInformation>(ref _quizInstance, value, "QuizInstance"); }
            get { return _quizInstance; }
        }
        private string _displayName;
        public string DisplayName
        {
            set
            {
                SetProperty<string>(ref _displayName, value, "DisplayName");
            }
            get { return _displayName; }
        }
        private bool _showButton = false;
        public bool ShowButton
        {
            set { SetProperty<bool>(ref _showButton, value, "ShowButton"); }
            get { return _showButton; }
        }
        #endregion

        #region Functions
        public async void BeginButtonClicked(string name)
        {
            App.QuestionViewModel = new QuestionViewModel();
            App.QuestionViewModel.QuizInstance = QuizInstance;
            App.QuestionViewModel.QuizInstance.User = name;
            App.QuestionViewModel.QuestionNumber = 0;
            await App.MainNavigation.PushModalAsync(new Views.QuestionPage());
        }

        #endregion
    }
}
