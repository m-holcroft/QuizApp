using System.IO;
using Windows.Storage;
using System.Collections.Generic;
using Xamarin.Forms;
using System;

[assembly: Dependency(typeof(QuizApp.UWP.FileHelper))]
namespace QuizApp.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }

        public List<string> GetSpecialFolders()
        {
            return new List<string>();
        }
    }
}