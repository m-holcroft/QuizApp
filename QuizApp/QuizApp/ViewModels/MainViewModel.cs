

using QuizApp.Common.Commands;
using System.Threading.Tasks;

namespace QuizApp.ViewModels
{
    public class MainViewModel : Common.ObservableBase
    {
        #region Constructors
        public MainViewModel()
        {
            StartText = "Start";
            CustomText = "Custom Questions";
            ScoreText = "Scores";
            IsBusy = false;

            NavigateCommandAsync = new RelayCommandAsync<object>(NavigateAsync, CanClickButton);
        }
        #endregion

        #region Members
        private string _startText;
        public string StartText
        {
            set { SetProperty<string>(ref _startText, value, "StartText"); }
            get { return _startText; }
        }

        private string _customText;
        public string CustomText
        {
            set { SetProperty<string>(ref _customText, value, "CustomText"); }
            get { return _customText; }
        }

        private string _scoreText;
        public string ScoreText
        {
            set { SetProperty<string>(ref _scoreText, value, "ScoreText"); }
            get { return _scoreText; }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            set { SetProperty<bool>(ref _isBusy, value, "IsBusy"); }
            get { return _isBusy; }
        }
        #endregion

        #region Commands
        public RelayCommandAsync<object> NavigateCommandAsync { get; private set; }
        #endregion

        #region Functions
        public async Task NavigateAsync(object parameter)
        {
            IsBusy = true;
            NavigateCommandAsync.OnCanExecuteChanged();
            string page = parameter as string;

            switch (page)
            {
                case "Custom":
                    await App.MainNavigation.PushAsync(new Views.AddCustomQuestionPage(), true);
                    IsBusy = false;
                    NavigateCommandAsync.OnCanExecuteChanged();
                    return;

                case "Question":
                    await App.MainNavigation.PushAsync(new Views.QuestionPage(), true);
                    IsBusy = false;
                    NavigateCommandAsync.OnCanExecuteChanged();
                    return;

                case "Result":
                    await App.MainNavigation.PushAsync(new Views.DisplayResultsPage(), true);
                    IsBusy = false;
                    NavigateCommandAsync.OnCanExecuteChanged();
                    return;

                case "Score":
                    await App.MainNavigation.PushAsync(new Views.DisplayScoresPage(), true);
                    IsBusy = false;
                    NavigateCommandAsync.OnCanExecuteChanged();
                    return;

                case "Settings":
                    await App.MainNavigation.PushAsync(new Views.SettingsPage(), true);
                    IsBusy = false;
                    NavigateCommandAsync.OnCanExecuteChanged();
                    return;

                case "Setup":
                    await App.MainNavigation.PushAsync(new Views.QuizSetupPage(), true);
                    IsBusy = false;
                    NavigateCommandAsync.OnCanExecuteChanged();
                    return;
                default:
                    return;
            }
        }
        public bool CanClickButton(object parameter)
        {
            return !IsBusy;
        }
        #endregion

    }
}
