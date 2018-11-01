//using System.Collections.Generic;
//using System.Threading.Tasks;
//using SQLite;

//namespace QuizApp.Data
//{
//    public class QuizAppDatabase
//    {
//        readonly SQLiteAsyncConnection db;

//        /* Constructor */
//        public QuizAppDatabase(string dbPath)
//        {
//            db = new SQLiteAsyncConnection(dbPath);

//            db.DropTableAsync<QuestionsTable>().Wait();

//            db.CreateTableAsync<ScoresTable>().Wait();
//            db.CreateTableAsync<QuestionsTable>().Wait();
//            //database.CreateTableAsync<Data.User>().Wait();
//        }

//        /* Score Specific Methods */
//        public Task<int> SaveScoreAsync(ScoresTable score)
//        {

//            if (!string.IsNullOrEmpty(score.Id))
//            {
//                return db.UpdateAsync(score);
//            }
//            else
//            {
//                return db.InsertAsync(score);
//            }
//        }
//        public Task<int> DeleteScoreAsync(ScoresTable score)
//        {
//            return db.DeleteAsync(score);
//        }
//        public async Task<List<ScoresTable>> GetAllScoresAsync()
//        {
//            List<ScoresTable> result = await db.Table<ScoresTable>().ToListAsync();

//            if(result == null)
//            {
//                result = new List<ScoresTable>();
//                result.Add(new ScoresTable());
//                result[0].Id = "-10";
//                //result[0].Id = -10;
//                result[0].Points = -99;
//                result[0].DisplayName = "Error";
//            }
//            return result;
//        }

//        /* Question Specific Methods */
//        public Task<int> SaveQuestionAsync(QuestionsTable question)
//        {
//            if(question.ID != 0)
//            {
//                return db.UpdateAsync(question);
//            }
//            else
//            {
//                return db.InsertAsync(question);
//            }
//        }
//        public Task<int> DeleteQuestionAsync(QuestionsTable question)
//        {
//            return db.DeleteAsync(question);
//        }
//        public async Task<List<QuestionsTable>> GetAllQuestionsAsync()
//        {
//            List<QuestionsTable> result = await db.Table<QuestionsTable>().ToListAsync();

//            if (result == null)
//            {
//                result = new List<QuestionsTable>();
//                result.Add(new QuestionsTable());
//                result[0].ID = -10;
//                result[0].QuestionText = "You dun goof'd";
//                result[0].Ans1 = "Error";
//                result[0].Ans2 = "Error";
//                result[0].Ans3 = "Error";
//                result[0].Ans4 = "Error";
//                result[0].CorAns = 1;
//            }
//            return result;
//        }
//        public Task<int> DropQuestionTableAsync()
//        {
//            return db.DropTableAsync<QuestionsTable>();
//        }
//    }
//}
