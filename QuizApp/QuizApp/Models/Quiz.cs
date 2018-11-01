using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;

namespace QuizApp.Models
{
    public class QuizInformation : Common.ObservableBase
    {
        /* Constructors */
        public QuizInformation()
        {
            _user = "";
            _score = 0;
            _questionList = new ObservableCollection<Question>();
        }
        public QuizInformation(string s)
        {
            _user = "";
            _score = 0;
            _questionList = new ObservableCollection<Question>();
        }

        /* Private Members */
        //private ObservableCollection<Question> _questionList;
        //private string _user;
        //private int _score;
        private string _user;
        private int _score;
        private ObservableCollection<Question> _questionList;

        /* Getters / Setters */
        public string User
        {
            set { SetProperty<string>(ref _user, value, "User"); }
            get { return _user; }
        }
        public int Score
        {
            set { SetProperty<int>(ref _score, value, "Score"); }
            get { return _score; }
        }
        public ObservableCollection<Question> QuestionList
        {
            set { SetProperty<ObservableCollection<Question>>(ref _questionList, value, "QuestionList"); }
            get { return _questionList; }
        }


    }
}
