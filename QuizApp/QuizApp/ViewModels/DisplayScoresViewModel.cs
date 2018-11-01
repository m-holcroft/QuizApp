
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using QuizApp.Models;
using QuizApp.Data;
using System.Threading.Tasks;

using QuizApp.Common;
using System;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;
using QuizApp.Common.Commands;
using Xamarin.Forms;
using System.Windows.Input;

namespace QuizApp.ViewModels
{
    public class DisplayScoresViewModel : ObservableBase
    {

        #region Constructors
        public DisplayScoresViewModel()
        {

        }
        #endregion

        #region Members
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private ObservableCollection<QuizInformation> _scores = new ObservableCollection<QuizInformation>();
        public ObservableCollection<QuizInformation> Scores
        {
            get { return _scores; }
            set { SetProperty(ref _scores, value); }
        }
        private ObservableCollection<Data.ScoresTable> _dbScores = new ObservableCollection<ScoresTable>();
        public ObservableCollection<ScoresTable> DBScores
        {
            get { return _dbScores; }
            set { SetProperty(ref _dbScores, value); }
        }
        #endregion

        #region Commands
        public ICommand RefreshScoresAsyncCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    await GetAzureScores("");

                    IsBusy = false;
                });
            }
        }
        #endregion
      
        #region Functions
        public async Task GetAzureScores(object parameter)
        {
            IsBusy = true;
            try
            {
                var downloadedList = await App.AzureService.GetScores();
                downloadedList = downloadedList.OrderByDescending(x => x.Points).ThenBy(x => x.DisplayName).ToList();

                DBScores = new ObservableCollection<ScoresTable>();

                foreach (ScoresTable element in downloadedList)
                {
                    DBScores.Add(element);
                }
            }
            catch(Exception e)
            {
            }

            finally
            {
                IsBusy = false;
            }
        }
        public bool CanRefresh(object parameter)
        {
            return !IsBusy;
        }
        #endregion
    }
}
