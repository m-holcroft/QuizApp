using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizApp.Common.Commands
{
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
    //public class BeginQuizButtonCommand : ICommand
    //{
    //    private bool _isBusy = false;
    //    public event EventHandler CanExecuteChanged;
    //    public bool CanExecute(object parameter)
    //    {
    //        return !_isBusy;
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
    //        BeginQuiz((string)parameter);
    //    }
    //    public void BeginQuiz(string entry)
    //    {
    //        _isBusy = true;
    //        RaiseCanExecuteChanged();
    //        App.StartSetupViewModel.BeginButtonClicked(entry);
    //        _isBusy = false;
    //    }
    //}
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
