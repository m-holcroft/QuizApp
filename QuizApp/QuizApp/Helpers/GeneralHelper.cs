using QuizApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.Helpers
{
    public static class GeneralHelper
    {
        public static void Speak(string text)
        {
            Xamarin.Forms.DependencyService.Get<ITextSpeecher>().Speak(text);
        }

        public static DeviceOrientations GetOrientation()
        {
            var orientation = Xamarin.Forms.DependencyService.Get<IDeviceOrientation>().GetOrientation();

            return orientation;
        }
    }
}
