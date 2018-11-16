using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Auth;
using Microsoft.WindowsAzure.MobileServices;
using QuizApp.Data;
using QuizApp.ViewModels;
using QuizApp.Helpers;
using System;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using Plugin.Connectivity;

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
        /// A ViewModel for the settings page
        /// </summary>
        public static SettingsViewModel SettingsViewModel { get; set; }
        /// <summary>
        /// A helper class to simplify the calling of <see cref="DependencyService.Get{T}(DependencyFetchTarget)"/>
        /// </summary>
        public static PopUpHelper PopUpHelper { get; set; }
        /// <summary>
        /// A <see cref="MobileServiceClient"/> that is used in setting up the <see cref="AzureDataService"/>.
        /// </summary>

        /// <summary>
        /// A helper used to reduce boilerplate navigation code.
        /// </summary>
        public static NavigationHelper NavHelper { get; set; }    

        public static MobileServiceClient MobileService = new MobileServiceClient(Common.Constants.ApplicationURL);
        /// <summary>
        /// A more specific implementation of the <see cref="MobileServiceClient"/>.
        /// </summary>
        public static AzureDataService AzureService = new AzureDataService();
        #endregion


        public App()
        {
            PopUpHelper = new PopUpHelper();                    //Initialise the popup helper.
            NavHelper = new NavigationHelper();
            MainPage = new NavigationPage(new MainPage());      //Initialise the main page as a Navigation Page to be the base of the Navigation Stack.
            InitializeComponent();                              //Still no idea what this does.
        }

        /// <summary>
        /// Latitude = 0, longitude = 1
        /// </summary>
        /// <returns></returns>
        public static async Task<string[]> GetLocation()
        {
            string[] loc = new string[2];
            try
            {
                await MainNavigation.PushModalAsync(new Views.LoadingPage(), false);
                var locator = Plugin.Geolocator.CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

                loc[0] = position.Latitude.ToString();
                loc[1] = position.Longitude.ToString();
                await MainNavigation.PopModalAsync(false);
                return loc;
            }

            catch (Exception e)
            {
                App.PopUpHelper.ShortAlert(e.Message);
                await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
            }
            loc[0] = "No permission given.";
            loc[1] = "No permission given.";
            return loc;
        }

        protected override async void OnStart()
        {
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    App.AzureService.SyncQuestions();
                    App.AzureService.SyncScores();
                    App.PopUpHelper.ShortAlert("Connected");
                }
                else
                {
                    App.PopUpHelper.ShortAlert("Disconnected");
                }
            };
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
