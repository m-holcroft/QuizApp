using System;
using System.Collections.Generic;
using System.Text;

/*An interface for a messaging system, in Android I will implement this using the Toast functionality*/ 

namespace QuizApp.Interfaces
{
    public interface IMessage
    {
        void LongAlert(string m);
        void ShortAlert(string m);
    }
}
