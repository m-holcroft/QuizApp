using QuizApp.Models;
using QuizApp.Common;
using System;
using Xamarin.Forms;
using System.Windows.Input;
using QuizApp.Common.Commands;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuizApp.ViewModels
{
    /// <summary>
    /// The ViewModel for the setup page. Inherits from ObservableBase to allow for data binding.
    /// </summary>
    public class QuizSetupViewModel : ObservableBase
    {
        #region Constructors
        /// <summary>
        /// A default constructor that initialises appropriate members and commands.
        /// </summary>
        public QuizSetupViewModel()
        {
            DisplayName = "";                                                                                       //Initialise to an empty string.
            IsBusy = false;                                                                                         //The VM starts off doing nothing.
            _quizInstance = new QuizInformation();                                                                  //Initalise the current instance of the quiz.
            BeginQuizAsyncCommand = new Command<string>(async (parameter) => await BeginQuizAsync (parameter));     //Initialise the command, passing it the CommandParameter from the XAML and casting that to a string
        }
        #endregion

        #region Members
        /// <summary>
        /// Indicates whether this ViewModel is performing a task.
        /// </summary>
        /// 
        private bool _isBusy;
        /// <summary>
        /// Indicates whether this ViewModel is performing a task.
        /// </summary>
        public bool IsBusy
        {
            set { SetProperty<bool>(ref _isBusy, value, "IsBusy"); }
            get { return _isBusy; }
        }
        /// <summary>
        /// The instance of the quiz that is being played.
        /// </summary>
        private QuizInformation _quizInstance;
        /// <summary>
        /// The instance of the quiz that is being played.
        /// </summary>
        public QuizInformation QuizInstance
        {
            set { SetProperty<QuizInformation>(ref _quizInstance, value, "QuizInstance"); }
            get { return _quizInstance; }
        }
        /// <summary>
        /// The user's DisplayName. Corresponds to <see cref="QuizInstance.User"/>.
        /// </summary>
        private string _displayName;
        /// <summary>
        /// The user's DisplayName. Corresponds to <see cref="QuizInstance.User"/>.
        /// </summary>
        public string DisplayName
        {
            set { SetProperty<string>(ref _displayName, value, "DisplayName"); }
            get { return _displayName; }
        }
        /// <summary>
        /// The list of question groups, retrieved from Azure. Pairs up the ID of the group with a string name.
        /// </summary>
        private ObservableCollection<QuestionGroup> _questionGroupList;
        /// <summary>
        /// The list of question groups, retrieved from Azure. Pairs up the ID of the group with a string name.
        /// </summary>
        public ObservableCollection<QuestionGroup> QuestionGroupList
        {
            set { SetProperty<ObservableCollection<QuestionGroup>>(ref _questionGroupList, value, "QuestionGroupList"); }
            get { return _questionGroupList; }
        }

        /// <summary>
        /// The collection of questions to be passed to the quiz instance
        /// </summary>
        private ObservableCollection<Question> _questionCollection;
        /// <summary>
        /// The collection of questions to be passed to the quiz instance
        /// </summary>
        public ObservableCollection<Question> QuestionCollection
        {
            set { SetProperty<ObservableCollection<Question>>(ref _questionCollection, value, "QuestionCollection"); }
            get { return _questionCollection; }
        }
        #endregion

        #region Commands
        /// <summary>
        /// The command for starting the quiz.
        /// </summary>
        public ICommand BeginQuizAsyncCommand { get; private set; }
        #endregion

        #region Functions

        /// <summary>
        /// Handles the beginning of the quiz, creates the next viewmodel and passes the quiz instance data over then creates the new page.
        /// </summary>
        /// <param name="parameter">The CommandParameter from the XAML.</param>
        /// <returns></returns>
        public async Task BeginQuizAsync(object parameter)
        {
            if (!IsBusy)
            {
                IsBusy = true;                                                              //We're doing something, set IsBusy to true.       
                string name = parameter as string;                                          //Get the text.
                await App.MainNavigation.PushModalAsync(new Views.LoadingPage(), false);

                App.QuestionViewModel = new QuestionViewModel();                            //Create the next ViewModel.
                App.QuestionViewModel.QuizInstance = QuizInstance;                          //Set the new VM's quiz instance to the one initalised previously.
                App.QuestionViewModel.QuizInstance.User = name;                             //Pass the quiz instance the name entered in the Entry box, obtained from the CommandParameter in XAML.
                App.QuestionViewModel.QuestionNumber = 0;                                   //Initalise the question number to 0 to avoid a nullpointer when trying to populate the button and label texts.

                try
                {
                    List<Data.QuestionsTable> downloadedList = await App.AzureService.GetQuestions(0);
                    foreach (Data.QuestionsTable element in downloadedList)
                    {
                        Question q = new Question();
                        q.QuestionText = element.QuestionText;
                        q.Answers[0] = element.Ans1;
                        q.Answers[1] = element.Ans2;
                        q.Answers[2] = element.Ans3;
                        q.Answers[3] = element.Ans4;
                        q.CorrectAnswer = element.CorAns;
                        App.QuestionViewModel.QuizInstance.QuestionList.Add(q);
                    }
                    App.QuestionViewModel.UpdateLabels();
                }

                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                finally
                {
                    IsBusy = false;
                    await App.MainNavigation.PopModalAsync(false);
                    await App.MainNavigation.PushModalAsync(new Views.QuestionPage(),false);          //Create the new page as a modal page, a page that cannot be navigated away from unless explicitly cancelled. It exists on a separate navigation stack.
                }                
            }

        }

        /// <summary>
        /// Simply returns the inverse of IsBusy to be used in logical tests for execution.
        /// </summary>
        /// <param name="parameter">Not used, but required for the command initialisation</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return !IsBusy;
        }
        #endregion
    }
}
