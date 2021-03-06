﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();      
		}

        protected override void OnAppearing()
        {
            App.SettingsViewModel = new ViewModels.SettingsViewModel();
            BindingContext = App.SettingsViewModel;
            base.OnAppearing();
        }
    }
}