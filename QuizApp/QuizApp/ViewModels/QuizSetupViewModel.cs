using QuizApp.Models;
using QuizApp.Common;
using System;
using Xamarin.Forms;
using System.Windows.Input;
using QuizApp.Common.Commands;
using System.Threading.Tasks;

namespace QuizApp.ViewModels
{
    public class QuizSetupViewModel : ObservableBase
    {
        #region Constructors
        public QuizSetupViewModel()
        {
            DisplayName = "";
            IsBusy = false;
            _quizInstance = new QuizInformation();
            BeginQuizAsyncCommand = new RelayCommandAsync<object>(BeginQuizAsync, CanExecute);

            //TODO: Change this to save to Azure Questions Table, create that table too
        }
        #endregion

        #region Members
        private bool _isBusy;

        public bool IsBusy
        {
            set { SetProperty<bool>(ref _isBusy, value, "IsBusy"); }
            get { return _isBusy; }
        }


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

        #region Commands
        public static RelayCommandAsync<object> BeginQuizAsyncCommand { get; private set; }
        #endregion

        #region Functions
        public async Task BeginQuizAsync(object parameter)
        {
            IsBusy = true;
            string name = parameter as string;
            App.QuestionViewModel = new QuestionViewModel();
            App.QuestionViewModel.QuizInstance = QuizInstance;
            App.QuestionViewModel.QuizInstance.User = name;
            App.QuestionViewModel.QuestionNumber = 0;
            await App.MainNavigation.PushModalAsync(new Views.QuestionPage());
            IsBusy = false;
        }

        public bool CanExecute(object parameter)
        {
            return !IsBusy;
        }
        #endregion
    }
}
