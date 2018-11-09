using System;
using QuizApp.Models;
using QuizApp.Common;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuizApp.ViewModels
{
    public class QuestionViewModel : ObservableBase
    {
        #region Constructors
        public QuestionViewModel()
        {
            QuizInformation QuizInstance = new QuizInformation();
            QuestionNumber = 0;
            Score = 0;
            AEnabled = true;
            BEnabled = true;
            CEnabled = true;
            DEnabled = true;

            AnswerQuestionCommand = new Command<object>(ResolveAnswerButton);
            QuitAsyncCommand = new Command(async () => await QuitAsync());
            SpeakCommand = new Command<object>(Speak);
        }
        #endregion

        #region Members
        private QuizInformation _quizInstance;
        private int _questionNumber; //Should not be tracked in here, should be tracked in QuizInformation
        private int _score; //Should not be tracked in here, should be tracked in QuizInformation
        private string _q;
        private string _a;
        private string _b;
        private string _c;
        private string _d;
        private bool _aEnabled;
        private bool _bEnabled;
        private bool _cEnabled;
        private bool _dEnabled;
        private string _questionNumberDisplayed;
        public string QuestionNumberDisplayed
        {
            set { SetProperty<string>(ref _questionNumberDisplayed, value, "QuestionNumberDisplayed"); }
            get { return _questionNumberDisplayed; }
        }
        public int QuestionNumber
        {
            set { SetProperty<int>(ref _questionNumber, value, "QuestionNumber"); }
            get { return _questionNumber; }
        }
        public int Score
        {
            set { SetProperty<int>(ref _score, value, "Score"); }
            get { return _score; }
        }
        public string Q
        {
            set { SetProperty<string>(ref _q, value, "Q"); }
            get { return _q; }
        }
        public string A
        {
            set { SetProperty<string>(ref _a, value, "A"); }
            get { return _a; }
        }
        public string B
        {
            set { SetProperty<string>(ref _b, value, "B"); }
            get { return _b; }
        }
        public string C
        {
            set { SetProperty<string>(ref _c, value, "C"); }
            get { return _c; }
        }
        public string D
        {
            set { SetProperty<string>(ref _d, value, "D"); }
            get { return _d; }
        }
        public bool AEnabled
        {
            set { SetProperty<bool>(ref _aEnabled, value, "AEnabled"); }
            get { return _aEnabled; }
        }
        public bool BEnabled
        {
            set { SetProperty<bool>(ref _bEnabled, value, "BEnabled"); }
            get { return _bEnabled; }
        }
        public bool CEnabled
        {
            set { SetProperty<bool>(ref _cEnabled, value, "CEnabled"); }
            get { return _cEnabled; }
        }
        public bool DEnabled
        {
            set { SetProperty<bool>(ref _dEnabled, value, "DEnabled"); }
            get { return _dEnabled; }
        }
        public QuizInformation QuizInstance
        {
            set { SetProperty<QuizInformation>(ref _quizInstance, value, "QuizInstance"); }
            get { return _quizInstance; }
        }
        #endregion

        #region Commands    
        
        public ICommand AnswerQuestionCommand { get; private set; }
        public ICommand QuitAsyncCommand { get; private set; }
        public ICommand SpeakCommand { get; private set; }

        #endregion

        #region Functions
        public void UpdateLabels()
        {
            Q = QuizInstance.QuestionList[_questionNumber].QuestionText;
            A = QuizInstance.QuestionList[_questionNumber].Answers[0];
            B = QuizInstance.QuestionList[_questionNumber].Answers[1];
            C = QuizInstance.QuestionList[_questionNumber].Answers[2];
            D = QuizInstance.QuestionList[_questionNumber].Answers[3];

            int actualQuestionNum = QuestionNumber + 1;
            QuestionNumberDisplayed = "Question No: " + actualQuestionNum + " of " + QuizInstance.QuestionList.Count;

            AEnabled = true;
            BEnabled = true;
            CEnabled = true;
            DEnabled = true;
        }
        public void ResolveAnswerButton(object parameter)
        {
            string bText = parameter as string;
            int numOfQuestions = QuizInstance.QuestionList.Count - 1; ;
            string corrAnsText = QuizInstance.QuestionList[QuestionNumber].Answers[QuizInstance.QuestionList[QuestionNumber].CorrectAnswer]; ;
               
            if(bText == corrAnsText)
            {
                QuizInstance.Score += 2;
                Score = QuizInstance.Score;
                if (QuestionNumber < numOfQuestions)
                {
                    QuestionNumber++;
                    UpdateLabels();
                }
                else
                {
                    ShowResults(QuizInstance);
                }
            }
            else
            {
                if (bText == A)
                {
                   AEnabled = false;
                }
                else if (bText == B)
                {
                    BEnabled = false;
                }

                else if (bText == C)
                {
                    CEnabled = false;
                }

                else if (bText == D)
                {
                    DEnabled = false;
                }
                if (QuizInstance.Score > 0)
                {
                    QuizInstance.Score -= 1;
                    Score = QuizInstance.Score;
                }
            }

        }

        public async void ShowResults(QuizInformation q)
        {
            App.ResultsViewModel = new DisplayResultsViewModel(QuizInstance);
            await App.MainNavigation.PushAsync(new Views.DisplayResultsPage(), false);
            await App.MainNavigation.PopModalAsync(false);
        }

        public async Task QuitAsync()
        {
            await App.MainNavigation.PopModalAsync();
        }

        public void Speak(object parameter)
        {
            Helpers.GeneralHelper.Speak((string)parameter);
        }
        #endregion
    }
}
