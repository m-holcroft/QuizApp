using System;
using System.Collections.Generic;
using System.Text;

/*An interface for a messaging system, in Android I will implement this using the Toast functionality*/

namespace QuizApp.Helpers
{
    public class PopUpHelper
    {
        public void LongAlert(string m)
        {
            Xamarin.Forms.DependencyService.Get<Interfaces.IMessage>().LongAlert(m);
        }
        public void ShortAlert(string m)
        {
            Xamarin.Forms.DependencyService.Get<Interfaces.IMessage>().ShortAlert(m);
        }
    }
}
