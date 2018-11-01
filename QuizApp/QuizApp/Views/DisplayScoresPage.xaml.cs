using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace QuizApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayScoresPage : ContentPage
    {
        public DisplayScoresPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            App.ScoreViewModel = new ViewModels.DisplayScoresViewModel();
            BindingContext = App.ScoreViewModel;
            await App.ScoreViewModel.GetAzureScores("");
            base.OnAppearing();
        }

    }
}