
using System.Collections.ObjectModel;
using System.Linq;
using QuizApp.Models;
using QuizApp.Data;
using System.Threading.Tasks;

using QuizApp.Common;
using System;
using Xamarin.Forms;
using System.Windows.Input;
using System.Diagnostics;

namespace QuizApp.ViewModels
{
    /// <summary>
    /// The ViewModel for displaying scores. Handles all functionality of that page.
    /// </summary>
    public class DisplayScoresViewModel : ObservableBase
    {

        #region Constructors
        /// <summary>
        /// A default constructor.
        /// </summary>
        public DisplayScoresViewModel()
        {

        }
        #endregion

        #region Members
        /// <summary>
        /// A bool for determining if the VM is currently performing any tasks.
        /// </summary>
        private bool _isBusy;
        /// <summary>
        /// A bool for determining if the VM is currently performing any tasks.
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private int _synced;
        /// <summary>
        /// Determines if the item has been synced or not. Ususally I'd use a bool for this but I want to use a converter.
        /// </summary>
        public int Synced
        {
            set { SetProperty<int>(ref _synced, value, "Synced"); }
            get { return _synced; }
        }

        /// <summary>
        /// The path to the relevant icon
        /// </summary>
        private string  _syncImagePath;

        public string  SyncImagePath
        {
            set { SetProperty<string >(ref _syncImagePath, value, "SyncImagePath"); }
            get { return _syncImagePath; }
        }


        /// <summary>
        /// An ObservableCollection of <see cref="ScoresTable"/> objects. These objects contain the raw data of the scores obtained from the Azure Server.
        /// </summary>
        private ObservableCollection<ScoresTable> _dbScores = new ObservableCollection<ScoresTable>();
        /// <summary>
        /// An ObservableCollection of <see cref="ScoresTable"/> objects. These objects contain the raw data of the scores obtained from the Azure Server.
        /// </summary>
        public ObservableCollection<ScoresTable> DBScores
        {
            get { return _dbScores; }
            set { SetProperty(ref _dbScores, value); }
        }
        #endregion

        #region Commands
        /// <summary>
        /// A command for refreshing the scores. Is bound to the refreshing process of the page.
        /// </summary>
        public ICommand RefreshScoresAsyncCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;              //We're doing something

                    await GetAzureScores("");   //A call to the GetAzureScores function. An empty string is passed as a parameter since it expects any object.    

                    IsBusy = false;             //No longer doing something
                });
            }
        }
        #endregion
      
        #region Functions
        /// <summary>
        /// Performs a call to the <see cref="AzureDataService"/> to retrieve the scores in a <see cref="ITemplatedItemsList{TItem}"/> format then transfers this over to an <see cref="ObservableCollection{T}"/>
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task GetAzureScores(object parameter)
        {
            IsBusy = true;                                                                                              //We're doing something
            try
            {
                var downloadedList = await App.AzureService.GetScores();                                                //Get the scores from the service.                                                        
                downloadedList = downloadedList.OrderByDescending(x => x.Points).ThenBy(x => x.DisplayName).ToList();   //Order the scores.

                DBScores = new ObservableCollection<ScoresTable>();                                                     //Re-initialise the collection to prevent double population.    

                foreach (ScoresTable element in downloadedList)                                                         //Go through the List, add each element to the ObservableCollection. Needs doing since Lists dont support Data Binding while ObservableCollections do.
                {
                    if(element.Synced == 0)
                    {
                        element.SyncImagePath = "notsynced.png";
                    }
                    else
                    {
                        element.SyncImagePath = "synced.png";
                    }
                    DBScores.Add(element);                                                                              //Add the element to the list.
                    Debug.WriteLine("");
                }
                Debug.WriteLine("");
            }
            catch(Exception e)
            {

            }

            finally
            {
                IsBusy = false;                                                                                         //We're not doing anything anymore       
            }
        }
        public bool CanRefresh(object parameter)
        {
            return !IsBusy;
        }
        #endregion
    }
}
