using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;
using QuizApp.Data;
using QuizApp.ViewModels;
using QuizApp.Helpers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace QuizApp
{
    public partial class App : Application
    {
        #region Public Members
        /// <summary>
        /// An implementation of the INavigation Interface.
        /// </summary>
        public static INavigation MainNavigation { get; set; }
        /// <summary>
        /// A ViewModel for the scores page.
        /// </summary>
        public static DisplayScoresViewModel ScoreViewModel { get; set; }
        /// <summary>
        /// A ViewModel for the quiz setup page.
        /// </summary>
        public static QuizSetupViewModel StartSetupViewModel { get; set; }
        /// <summary>
        /// A ViewModel for the main page.
        /// </summary>
        public static MainViewModel MainViewModel { get; set; }
        /// <summary>
        /// A ViewModel for the question displaying page.
        /// </summary>
        public static QuestionViewModel QuestionViewModel { get; set; }
        /// <summary>
        /// A ViewModel for the add question page.
        /// </summary>
        public static AddCustomQuestionViewModel CustomQuestionViewModel { get; set; }
        /// <summary>
        /// A ViewModel for the results displaying page.
        /// </summary>
        public static DisplayResultsViewModel ResultsViewModel { get; set; }
        /// <summary>
        /// A helper class to simplify the calling of <see cref="DependencyService.Get{T}(DependencyFetchTarget)"/>
        /// </summary>
        public static PopUpHelper PopUpHelper { get; set; }
        /// <summary>
        /// A <see cref="MobileServiceClient"/> that is used in setting up the <see cref="AzureDataService"/>.
        /// </summary>
        public static MobileServiceClient MobileService = new MobileServiceClient(Common.Constants.ApplicationURL);
        /// <summary>
        /// A more specific implementation of the <see cref="MobileServiceClient"/>.
        /// </summary>
        public static AzureDataService AzureService = new AzureDataService();
        #endregion


        public App()
        {
            InitializeComponent();                              //Still no idea what this does.
            PopUpHelper = new PopUpHelper();                    //Initialise the popup helper.
            MainPage = new NavigationPage(new MainPage());      //Initialise the main page as a Navigation Page to be the base of the Navigation Stack.
        }

        protected override async void OnStart()
        {
            await AzureService.Initialise();
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
