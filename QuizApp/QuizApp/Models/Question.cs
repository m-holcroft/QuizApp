namespace QuizApp.Models
{
    public class Question : Common.ObservableBase
    {

        /* Constructors */
        public Question()
        {
            QuestionText = "Question Text";
            _correctAns = 0;
            _answers = new string[4];
            _answers[0] = "A";
            _answers[1] = "B";
            _answers[2] = "C";
            _answers[3] = "D";
        }
        public Question(string question, string a, string b, string c, string d, int ans)
        {
            QuestionText = question;
            _correctAns = ans;
            _answers = new string[4];
            _answers[0] = a;
            _answers[1] = b;
            _answers[2] = c;
            _answers[3] = d;
        }

        /* Private Members */
        private string _question;
        private int _correctAns;
        private string[] _answers;

        /* Getters / Setters */
        public string QuestionText
        {
            set { SetProperty<string>(ref _question, value, "QuestionText"); }
            get { return _question; }
        }
        public int CorrectAnswer
        {
            set { SetProperty<int>(ref _correctAns, value, "CorrectAnswer"); }
            get { return _correctAns; }
        }
        public string[] Answers
        {
            set { SetProperty<string[]>(ref _answers, value, "Answers"); }
            get { return _answers; }
        }

    }
}
