using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizAnswerMaster
    {
        [PrimaryKey, AutoIncrement]
        public int AnswerMaster_id { get; set; } = -1;
        public string Answer { get; set; } = "";
        [Indexed]
        public string AnswerHash { get; set; } = "";
        public string ReferenceData { get; set; } = "";
    }
}
