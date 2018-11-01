using System;
using Xamarin.Forms;

namespace QuizApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            App.MainViewModel = new ViewModels.MainViewModel();
            App.MainNavigation = Navigation;
            this.BindingContext = App.MainViewModel;
            base.OnAppearing();
        }
    }
}
