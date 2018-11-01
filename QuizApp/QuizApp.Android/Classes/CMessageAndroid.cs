using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QuizApp.Interfaces;

/*Android implementation of the IMessage interface using Toast*/


[assembly: Xamarin.Forms.Dependency(typeof(QuizApp.Droid.CMessageAndroid))]
namespace QuizApp.Droid
{
    public class CMessageAndroid : IMessage
    {
        public void LongAlert(string m)
        {
            Toast.MakeText(Application.Context, m, ToastLength.Long).Show();
        }

        public void ShortAlert(string m)
        {
            Toast.MakeText(Application.Context, m, ToastLength.Short).Show();
        }
    }
}