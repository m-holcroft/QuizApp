using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.Common
{
    public class Constants
    {
        /// <summary>
        /// The URL of the Azure Mobile App Service.
        /// </summary>
        public static string ApplicationURL = @"https://holcroftquizapp.azurewebsites.net";
        /// <summary>
        /// An error message for the connection failing.
        /// </summary>
        public static string ConnectionFailedMessage = "There was a problem connecting to the web server, check your connection";
        public static string AuthTokenUWP = "";
    }
}
