using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using SQLite;

namespace QuizApp.Data
{
    public class ScoresTable
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        public string DisplayName { get; set;}   
        public int Points { get; set; }
        public DateTime AchievedOn { get; set; }
    }


    /*NOT CONVERTED TO EASYTABLE YET*/
    public class QuestionsTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string QuestionText { get; set; }
        public string Ans1 { get; set; }
        public string Ans2 { get; set; }
        public string Ans3 { get; set; }
        public string Ans4 { get; set; }
        public int CorAns { get; set; }
    }

    //public class User
    //{
    //    [PrimaryKey, AutoIncrement]
    //    public int UserID { get; set; }
    //    public string DisplayName { get; set; }
    //}
}
