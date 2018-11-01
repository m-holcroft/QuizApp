using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuizSetupPage : ContentPage
	{
		public QuizSetupPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            App.StartSetupViewModel = new ViewModels.QuizSetupViewModel();
            this.BindingContext = App.StartSetupViewModel;
            base.OnAppearing();
        }
    }
}