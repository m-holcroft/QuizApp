using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.Models
{
    public class User : Common.ObservableBase
    {
        private int _id;
        private string _displayName;
        private string _password;

        public User()
        {
            _id = -1;
            _displayName = "";
            _password = "";
        }

        public User(string d, string p)
        {
            _displayName = d;
            _password = p;
        }

        private int myVar;

        public int MyProperty
        {
            set { SetProperty<int>(ref myVar, value, "MyProperty"); }
            get { return myVar; }
        }

        public int UID { get { return _id; } set { _id = value; } }
        public string DisplayName { get { return _displayName; } set { _displayName = value; } }
        public string Password { get { return _password; } set { _password = value; } }
    }
}
