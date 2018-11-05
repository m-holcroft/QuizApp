namespace QuizApp.Models
{
    /// <summary>
    /// A class used to pair question group identifiers with a readable name.
    /// </summary>
    public class QuestionGroup
    {
        /// <summary>
        /// A default constructor that initialises the values.
        /// </summary>
        public QuestionGroup()
        {
            Index = 0;
            Name = "";
        }

        /// <summary>
        /// A constructor that provides meaningful initialisation
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="index">The index of the group in the picker</param>
        public QuestionGroup(string name, int index)
        {
            Index = index;
            Name = name;
        }

        private int _index;

        /// <summary>
        /// The index of the group in the picker
        /// </summary>
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        private string _name;

        /// <summary>
        /// The name of the group
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


    }
}
