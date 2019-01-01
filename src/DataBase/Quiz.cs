using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class Quiz
    {
        [PrimaryKey, AutoIncrement]
        public int Quiz_id { get; set; } = 0;
        public int CreatorMaster_id { get; set; } = -1;
        public int PassrateDataMaster_id { get; set; } = -1;
        public int TotalChoose { get; set; } = -1;
        public int AnswerNumber { get; set; } = -1;
        public int Type { get; set; } = 0;
        public Category Cat { get; set; } = Category.na;
        public QuizSource Source = QuizSource.bangumi;
        public int SouceID { get; set; } = -1;
        public DateTime Updated { get; set; } = DateTime.Now;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public string Question { get; set; } = "";
        public string Choose1 { get; set; } = "";
        public string Choose2 { get; set; } = "";
        public string Choose3 { get; set; } = "";
        public string Choose4 { get; set; } = "";
        public string Answer { get; set; } = "";
    }
}
