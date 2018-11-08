using System;
using QuizApp.Models;
using QuizApp.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace QuizApp.ViewModels
{
    public class QuestionViewModel : ObservableBase
    {
        #region Constructors
        public QuestionViewModel()
        {
            QuizInformation QuizInstance = new QuizInformation();
            _questionNumber = 0;
            _score = 0;
            AEnabled = true;
            BEnabled = true;
            CEnabled = true;
            DEnabled = true;
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

        public int QuestionNumber
        {
            set
            {
                SetProperty<int>(ref _questionNumber, value, "QuestionNumber");
            }
            get
            {
                return _questionNumber;
            }
        }
        public int Score
        {
            set
            {
                SetProperty<int>(ref _score, value, "Score");
            }
            get
            {
                return _score;
            }
        }
        public string Q
        {
            set
            {
                SetProperty<string>(ref _q, value, "Q");
            }
            get
            {
                return _q;
            }
        }
        public string A
        {
            set
            {
                SetProperty<string>(ref _a, value, "A");
            }
            get
            {
                return _a;
            }
        }
        public string B
        {
            set
            {
                SetProperty<string>(ref _b, value, "B");
            }
            get
            {
                return _b;
            }
        }
        public string C
        {
            set
            {
                SetProperty<string>(ref _c, value, "C");
            }
            get
            {
                return _c;
            }
        }
        public string D
        {
            set
            {
                SetProperty<string>(ref _d, value, "D");
            }
            get
            {
                return _d;
            }
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

        #region Functions
        public async Task RetrieveQuestions()
        {
            string s = "";
            if (s == "dummy")
            {
                QuizInstance.QuestionList.Add(new Question("What is the capital of the UK?", "A: London", "B: Birmingham", "C: Edinburgh", "D: Cardiff", 1)); //A
                QuizInstance.QuestionList.Add(new Question("What is the capital of France?", "A: Marseille", "B: Paris", "C: Toulouse", "D: Bordeaux", 2)); //B
                QuizInstance.QuestionList.Add(new Question("What is the capital of Spain?", "A: Valencia", "B: Barcelona", "C: Madrid", "D: Sevilla", 3)); //C
                QuizInstance.QuestionList.Add(new Question("What is the capital of Germany?", "A: Hamburg", "B: Munich", "C: Köln", "D: Berlin", 4)); //D
            }
            else
            {

            }
            Q = QuizInstance.QuestionList[0].QuestionText;
            A = QuizInstance.QuestionList[0].Answers[0];
            B = QuizInstance.QuestionList[0].Answers[1];
            C = QuizInstance.QuestionList[0].Answers[2];
            D = QuizInstance.QuestionList[0].Answers[3];
        }
        public Question GetSpecificQuestion(int i)
        {
            try
            {
                return QuizInstance.QuestionList[i];
            }
            catch (IndexOutOfRangeException e)
            {
                throw;
            }
        }
        public void UpdateQuestions()
        {
            if (Q == null || A == null || B == null || C == null || D == null)
            {
                RetrieveQuestions();
            }
            else
            {
                Q = QuizInstance.QuestionList[_questionNumber].QuestionText;
                A = QuizInstance.QuestionList[_questionNumber].Answers[0];
                B = QuizInstance.QuestionList[_questionNumber].Answers[1];
                C = QuizInstance.QuestionList[_questionNumber].Answers[2];
                D = QuizInstance.QuestionList[_questionNumber].Answers[3];

                AEnabled = true;
                BEnabled = true;
                CEnabled = true;
                DEnabled = true;

            }
        }
        public void ResolveAnswerButton(object obj)
        {
            string bText = (string)obj;
            int numOfQuestions = QuizInstance.QuestionList.Count - 1; ;
            string corrAnsText = QuizInstance.QuestionList[QuestionNumber].Answers[QuizInstance.QuestionList[QuestionNumber].CorrectAnswer]; ;
               
            if(bText == corrAnsText)
            {
                QuizInstance.Score += 2;
                Score = QuizInstance.Score;
                if (QuestionNumber < numOfQuestions)
                {
                    QuestionNumber++;
                    UpdateQuestions();
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
        #endregion
    }
}
