using QuizApp.Common;
using QuizApp.Data;
using System;
using System.Collections.Generic;
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
            CanExecute = true;
            ResetButtonCommand = new Command(async () => await ResetDatabaseAsync(), () => CanExecute);
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

        #endregion

        #region Commands
        public ICommand ResetButtonCommand { get; private set; }
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
        #endregion
    }
}
