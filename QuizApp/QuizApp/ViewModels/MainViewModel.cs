
using QuizApp.Common;
using QuizApp.Common.Commands;
using System.Threading.Tasks;

namespace QuizApp.ViewModels
{
    /// <summary>
    /// The ViewModel for the main page, inherits from ObservableBase to allow for Data Binding.
    /// </summary>
    public class MainViewModel : ObservableBase
    {
        #region Constructors
        /// <summary>
        /// A default constructor that initialises the button text and sets IsBusy to false.
        /// </summary>
        public MainViewModel()
        {
            StartText = "Start";                                                                    //Initialise a string.
            CustomText = "Custom Questions";                                                        //Initialise a string.
            ScoreText = "Scores";                                                                   //Initialise a string.
            IsBusy = false;                                                                         //Set IsBusy to false.

            NavigateCommandAsync = new RelayCommandAsync<object>(NavigateAsync, CanClickButton);    //Initialise the navigation command with the appropriate function and CanExecute checks.
        }
        #endregion

        #region Members
        /// <summary>
        /// The text to be displayed on the StartButton. Bound.
        /// </summary>
        private string _startText;
        /// <summary>
        /// The text to be displayed on the StartButton. Bound.
        /// </summary>
        public string StartText
        {
            set { SetProperty<string>(ref _startText, value, "StartText"); }
            get { return _startText; }
        }
        /// <summary>
        /// The text to be displayed on the AddCustomQuestionButton. Bound.
        /// </summary>
        private string _customText;
        /// <summary>
        /// The text to be displayed on the AddCustomQuestionButton. Bound.
        /// </summary>
        public string CustomText
        {
            set { SetProperty<string>(ref _customText, value, "CustomText"); }
            get { return _customText; }
        }
        /// <summary>
        /// The text to be displayed on the Scores page button. Bound.
        /// </summary>
        private string _scoreText;
        /// <summary>
        /// The text to be displayed on the scores page button. Bound.
        /// </summary>
        public string ScoreText
        {
            set { SetProperty<string>(ref _scoreText, value, "ScoreText"); }
            get { return _scoreText; }
        }
        /// <summary>
        /// The IsBusy property.
        /// </summary>
        private bool _isBusy;
        /// <summary>
        /// The IsBusy property.
        /// </summary>
        public bool IsBusy
        {
            set { SetProperty<bool>(ref _isBusy, value, "IsBusy"); }
            get { return _isBusy; }
        }
        #endregion

        #region Commands
        /// <summary>
        /// The only functional RelayCommandAsync instance. Not a clue why. 
        /// </summary>
        public RelayCommandAsync<object> NavigateCommandAsync { get; private set; }
        #endregion

        #region Functions
        /// <summary>
        /// The function that pairs with the <see cref="NavigateCommandAsync"/> command.
        /// </summary>
        /// <param name="parameter">The CommandParameter, passed from XAML.</param>
        /// <returns></returns>
        public async Task NavigateAsync(object parameter)
        {
            IsBusy = true;                                                                          //We're doing something.
            NavigateCommandAsync.OnCanExecuteChanged();                                             //Raises the OnCanExecuteChanged event. Should be automatically handled since we inherit from ObservableBase. Not sure if actually needed.    
            string page = parameter as string;                                                      //Cast the CommandParameter to a string.

            /*
             Each of these cases in the switch statement follow a standard pattern.
             case "{CommandParameter}"
                 await {Create the new page}
                 IsBusy = false
                 {raise the OnCanExecuteChanged event}
                 return
            */

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
