using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using QuizApp.Views;
using System.Threading.Tasks;

namespace QuizApp.Helpers
{
    /// <summary>
    /// A helper used to simplify standard navigation.
    /// </summary>
    public class NavigationHelper
    {
        /// <summary>
        /// Navigate to a given page with a fixed 200ms delay.
        /// </summary>
        /// <param name="page">The page type to be navigated to.</param>
        /// <param name="animation">Whether or not to perform an animation.</param>
        /// <returns></returns>
        public async Task Navigate(Page page, bool animation)
        {
            await App.MainNavigation.PushModalAsync(new LoadingPage(), animation);
            await App.MainNavigation.PushAsync(page, animation);
            await Task.Delay(200);
            await App.MainNavigation.PopModalAsync(animation);
        }

        /// <summary>
        /// Navigate to a given page.
        /// </summary>
        /// <param name="page">The page type to be navigated to.</param>
        /// <param name="animation">Whether or not to perform an animation.</param>
        /// <param name="delay">Any delay to be perfomed in miliseconds, 1000 is a good number.</param>
        /// <returns></returns>
        public async Task Navigate(Page page, bool animation, int delay)
        {
            await App.MainNavigation.PushModalAsync(new LoadingPage(), animation);
            await App.MainNavigation.PushAsync(page, animation);
            await Task.Delay(delay);
            await App.MainNavigation.PopModalAsync(animation);
        }

    }
}
