using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddCustomQuestionPage : ContentPage
	{
		public AddCustomQuestionPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            App.CustomQuestionViewModel = new ViewModels.AddCustomQuestionViewModel();
            BindingContext = App.CustomQuestionViewModel;
            base.OnAppearing();
        }
    }
}