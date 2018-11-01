using QuizApp.Models;
using QuizApp.Common;
using System;
using Xamarin.Forms;

namespace QuizApp.ViewModels
{
    public class QuizSetupViewModel : ObservableBase
    {
        #region Constructors
        public QuizSetupViewModel()
        {
            DisplayName = "";

            _quizInstance = new QuizInformation();

            Question[] tempQArr = new Question[4];
            tempQArr[0] = new Question("What is 1+1", "2", "53", "65", "846", 1);
            tempQArr[1] = new Question("What is 2+1", "22", "3", "565", "61221", 2);
            tempQArr[2] = new Question("What is 3+1", "88", "43", "4", "626", 3);
            tempQArr[3] = new Question("What is 4+1", "874", "2323", "4841", "5", 4);

            int count = 0;
            foreach (Question element in tempQArr)
            {
                Data.QuestionsTable question = new Data.QuestionsTable();

                question.QuestionText = element.QuestionText;
                question.Ans1 = element.Answers[0];
                question.Ans2 = element.Answers[1];
                question.Ans3 = element.Answers[2];
                question.Ans4 = element.Answers[3];
                question.CorAns = element.CorrectAnswer;
                //TODO: Change this to save to Azure Questions Table, create that table too
                count++;
            }
        }
        #endregion

        #region Members
        private QuizInformation _quizInstance;
        public QuizInformation QuizInstance
        {
            set { SetProperty<QuizInformation>(ref _quizInstance, value, "QuizInstance"); }
            get { return _quizInstance; }
        }
        private string _displayName;
        public string DisplayName
        {
            set
            {
                SetProperty<string>(ref _displayName, value, "DisplayName");
            }
            get { return _displayName; }
        }
        private bool _showButton = false;
        public bool ShowButton
        {
            set { SetProperty<bool>(ref _showButton, value, "ShowButton"); }
            get { return _showButton; }
        }
        #endregion

        #region Functions
        public async void BeginButtonClicked(string name)
        {
            App.QuestionViewModel = new QuestionViewModel();
            App.QuestionViewModel.QuizInstance = QuizInstance;
            App.QuestionViewModel.QuizInstance.User = name;
            App.QuestionViewModel.QuestionNumber = 0;
            await App.MainNavigation.PushModalAsync(new Views.QuestionPage());
        }
        //public void EntryTextChanged()
        //{
        //    ShowButton = false;
        //    if(DisplayName.Length > 0)
        //    {
        //        ShowButton = true;
        //    }
        //}
        #endregion
    }
}
