using QuizApp.Common;
using QuizApp.Data;
using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.ViewModels
{
    /// <summary>
    /// The ViewModel for the adding of custom questions.
    /// </summary>
    public class AddCustomQuestionViewModel : ObservableBase
    {
        #region Constructors
        public AddCustomQuestionViewModel()
        {
            QuestionText = "";
            Answer1 = "";
            Answer2 = "";
            Answer3 = "";
            Answer4 = "";
            CorrectAnswer = -1;
        }
        #endregion

        #region Members
        private string _questionText;

        public string QuestionText
        {
            set { SetProperty<string>(ref _questionText, value, "QuestionText"); }
            get { return _questionText; }
        }
        private string _answer1;

        public string Answer1
        {
            set { SetProperty<string>(ref _answer1, value, "Answer1"); }
            get { return _answer1; }
        }
        private string _answer2;

        public string Answer2
        {
            set { SetProperty<string>(ref _answer2, value, "Answer2"); }
            get { return _answer2; }
        }
        private string _answer3;

        public string Answer3
        {
            set { SetProperty<string>(ref _answer3, value, "Answer3"); }
            get { return _answer3; }
        }
        private string _answer4;

        public string Answer4
        {
            set { SetProperty<string>(ref _answer4, value, "Answer4"); }
            get { return _answer4; }
        }

        private int _correctAnswer;

        public int CorrectAnswer
        {
            set { SetProperty<int>(ref _correctAnswer, value, "CorrectAnswer"); }
            get { return _correctAnswer; }
        }




        #endregion

        #region Commands
        #endregion

        #region Functions
        public async Task SaveNewQuestionAsync()
        {
            QuestionsTable q = new QuestionsTable();

            q.QuestionText = QuestionText;
            q.Ans1 = Answer1;
            q.Ans2 = Answer2;
            q.Ans3 = Answer3;
            q.Ans4 = Answer4;

            if( string.IsNullOrEmpty(QuestionText) && 
                string.IsNullOrEmpty(Answer1) && 
                string.IsNullOrEmpty(Answer2) && 
                string.IsNullOrEmpty(Answer3) && 
                string.IsNullOrEmpty(Answer4) && 
                CorrectAnswer != -1)
            {
                try
                {
                    await App.AzureService.AddQuestion(q);
                }
                catch(Exception e)
                {
                    
                }
                finally
                {

                }

            }
        }
        #endregion
    }
}
