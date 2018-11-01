using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizApp.Common.Commands
{


    //public class NavigateAsyncCommand : ICommand
    //{
    //    private bool _isBusy = false;

    //    public event EventHandler CanExecuteChanged;

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public void RaiseCanExecuteChanged()
    //    {
    //        var handler = CanExecuteChanged;
    //        if(handler != null)
    //        {
    //            handler(this, EventArgs.Empty);                
    //        }
    //    }

    //    public void Execute(object parameter)
    //    {
    //        NavigateAsync((string)parameter);
    //    }

    //    private async void NavigateAsync(string page)
    //    {
    //        this._isBusy = true;
    //        this.RaiseCanExecuteChanged();
    //        switch (page)
    //        {
    //            case "Custom Question":
    //                await App.MainNavigation.PushAsync(new Views.CustomQuestionPage(), true);
    //                return;

    //            case "Question":
    //                await App.MainNavigation.PushAsync(new Views.QuestionPage(), true);
    //                return;

    //            case "Result":
    //                await App.MainNavigation.PushAsync(new Views.DisplayResultsPage(), true);
    //                return;

    //            case "Score":
    //                await App.MainNavigation.PushAsync(new Views.DisplayScoresPage(), true);
    //                return;

    //            case "Settings":
    //                await App.MainNavigation.PushAsync(new Views.SettingsPage(), true);
    //                return;

    //            case "Setup":
    //                await App.MainNavigation.PushAsync(new Views.QuizSetupPage(), true);
    //                return;
    //        }
    //        this._isBusy = false;
    //        this.RaiseCanExecuteChanged();
    //    }
    //}
    //public class NavigateCommand : ICommand
    //{
    //    public event EventHandler CanExecuteChanged;

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public void RaiseCanExecuteChanged()
    //    {
    //        var handler = CanExecuteChanged;

    //        if (handler != null)
    //        {
    //            handler(this, EventArgs.Empty);
    //        }
    //    }

    //    public void Execute(object parameter)
    //    {
    //        NavigateAsync();
    //    }

    //    private async void NavigateAsync()
    //    {
    //        await App.MainNavigation.PushAsync(new Views.SettingsPage(), true);
    //    }
    //}
    public class AnswerButtonCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;

            if(handler != null)
            {
                handler(this, EventArgs.Empty);
                    
            }
        }

        public void Execute(object parameter)
        {
            ButtonClicked((string)parameter);
        }


        public void ButtonClicked(string parameter)
        {
            App.QuestionViewModel.ResolveAnswerButton(parameter);
        }
    }
    public class QuitCommandAsync : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;

            if(handler != null)
            {
                handler(this, EventArgs.Empty);
                    
            }
        }

        public void Execute(object parameter)
        {
            QuitClicked();
        }

        private async void QuitClicked()
        {
            await App.MainNavigation.PopModalAsync();
        }
    }
    public class SaveScoreButtonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;

            if(handler != null)
            {
                handler(this, EventArgs.Empty);
                    
            }
        }

        public void Execute(object parameter)
        {
            SaveScore();
        }

        public void SaveScore()
        {
            App.ResultsViewModel.SaveScore();
        }
    }
    public class BeginQuizButtonCommand : ICommand
    {
        private bool _isBusy = false;
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return !_isBusy;
        }
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;

            if(handler != null)
            {
                handler(this, EventArgs.Empty);
                    
            }
        }
        public void Execute(object parameter)
        {
            BeginQuiz((string)parameter);
        }
        public void BeginQuiz(string entry)
        {
            _isBusy = true;
            RaiseCanExecuteChanged();
            App.StartSetupViewModel.BeginButtonClicked(entry);
            _isBusy = false;
        }
    }
    public class SpeakCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            Helpers.GeneralHelper.Speak((string)parameter);
        }
    }


}
