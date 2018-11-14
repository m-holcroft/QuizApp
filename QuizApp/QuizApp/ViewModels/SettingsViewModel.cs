using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using QuizApp.Common;
using QuizApp.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizApp.ViewModels
{
    public class SettingsViewModel : ObservableBase
    {
        #region Constructors
        public SettingsViewModel()
        {
            ResetButtonText = "Reset Database";
            UpdateLocationText = "Get Location";
            CanExecute = true;
            ResetButtonCommand = new Command(async () => await ResetDatabaseAsync(), () => CanExecute);
            UpdateLocationCommand = new Command(async () => await UpdateLocation());
            IsBusy = false;
        }
        #endregion

        #region Members
        private string _resetButtonText;

        public string ResetButtonText
        {
            set { SetProperty<string>(ref _resetButtonText, value, "ResetButtonText"); }
            get { return _resetButtonText; }
        }

        private bool _canExecute;

        public bool CanExecute
        {
            set { SetProperty<bool>(ref _canExecute, value, "CanExecute"); }
            get { return _canExecute; }
        }

        private string _location;

        public string Location
        {
            set { SetProperty<string>(ref _location, value, "Location"); }
            get { return _location; }
        }

        private string _updateLocationText;

        public string UpdateLocationText
        {
            set { SetProperty<string>(ref _updateLocationText, value, "UpdateLocationText"); }
            get { return _updateLocationText; }
        }

        private string _townName;

        public string HumanReadable
        {
            set { SetProperty<string>(ref _townName, value, "TownName"); }
            get { return _townName; }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            set { SetProperty<bool>(ref _isBusy, value, "IsBusy"); }
            get { return _isBusy; }
        }

        #endregion

        #region Commands
        public ICommand ResetButtonCommand { get; private set; }
        public ICommand UpdateLocationCommand { get; private set; }
        #endregion

        #region Functions
        public async Task ResetDatabaseAsync()
        {
            CanExecute = false;
            try
            {
                var scores = await App.AzureService.GetScores();
                foreach(ScoresTable element in scores)
                {
                    await App.AzureService.DeleteScore(element);
                }
            }
            catch(Exception e)
            {

            }
            finally
            {
                CanExecute = true;
            }
        }

        public async Task UpdateLocation()
        {
            try
            {
                IsBusy = true;
                var locator = Plugin.Geolocator.CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                string pos = position.Latitude+","+position.Longitude;
                var json = new WebClient().DownloadString("https://maps.googleapis.com/maps/api/geocode/json?latlng=" + pos + "&key=AIzaSyBNzZy8w9dcYvPx2vZnu0XI27gA66qq3Tg");
                RevGeoResponse response = JsonConvert.DeserializeObject<RevGeoResponse>(json);
                HumanReadable = response.results[4].formatted_address;               
                Location = "Time: " + position.Timestamp + "\nLatitude: " + position.Latitude + "\nLongitude: " + position.Longitude +"\nHuman Readable: " + HumanReadable;
            }

            catch(Exception e)
            {                 
                App.PopUpHelper.ShortAlert(e.Message);
                await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
