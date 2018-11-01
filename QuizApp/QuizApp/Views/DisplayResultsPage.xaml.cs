using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayResultsPage : ContentPage
    {
        public DisplayResultsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            this.BindingContext = App.ResultsViewModel;
            base.OnAppearing();
        }
    }
}