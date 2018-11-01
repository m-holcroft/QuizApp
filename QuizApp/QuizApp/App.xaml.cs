using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;
using QuizApp.Data;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace QuizApp
{
    public partial class App : Application
    {
        /*Ease of access properties*/
        public static ViewModels.DisplayScoresViewModel ScoreViewModel { get; set; }
        public static ViewModels.QuizSetupViewModel StartSetupViewModel { get; set; }
        public static ViewModels.MainViewModel MainViewModel { get; set; }
        public static ViewModels.QuestionViewModel QuestionViewModel { get; set; }
        public static ViewModels.AddCustomQuestionViewModel CustomQuestionViewModel { get; set; }
        public static ViewModels.DisplayResultsViewModel ResultsViewModel { get; set; }
        public static INavigation MainNavigation { get; set; }
        public static Helpers.PopUpHelper PopUpHelper { get; set; }
        public static MobileServiceClient MobileService = new MobileServiceClient("https://holcroftquizapp.azurewebsites.net");
        public static AzureDataService AzureService = new AzureDataService();

        /*DB Stuff*/
        //static Data.QuizAppDatabase database;
        //public static Data.QuizAppDatabase Database
        //{
        //    get
        //    {
        //        if (database == null)
        //        {
        //            string localFilePath = DependencyService.Get<IFileHelper>().GetLocalFilePath("scores.db3");
        //            database = new Data.QuizAppDatabase(localFilePath);
        //        }
        //        return database;
        //    }
        //}


        public App()
        {

            InitializeComponent();
            PopUpHelper = new Helpers.PopUpHelper();
            MainPage = new NavigationPage(new MainPage());
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
