using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

[assembly: Xamarin.Forms.Dependency(typeof(QuizApp.UWP.CMessageUWP))]
namespace QuizApp.UWP
{

    public class CMessageUWP : Interfaces.IMessage
    {
        public async void LongAlert(string s)
        {

            ContentDialog clearCalc = new ContentDialog
            {
                Title = "Alert",
                Content = s,
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await clearCalc.ShowAsync();
        }

        public async void ShortAlert(string s)
        {
            ContentDialog clearCalc = new ContentDialog
            {
                Title = "Alert",
                Content = s,
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await clearCalc.ShowAsync();
        }

    }
}
