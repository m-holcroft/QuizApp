using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.Helpers
{
    public static class StorageHelper
    {
        public static string GetLocalFilePath()
        {
            return Xamarin.Forms.DependencyService.Get<IFileHelper>().GetLocalFilePath("quizapp.db3");
        }


        public static List<string> GetSpecialFolders()
        {
            return Xamarin.Forms.DependencyService.Get<IFileHelper>().GetSpecialFolders();
        }
    }
}
