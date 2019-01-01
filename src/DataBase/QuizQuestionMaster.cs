using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizQuestionMaster
    {
        [PrimaryKey, AutoIncrement]
        public int QuestionMaster_id { get; set; } = -1;
        public string Question { get; set; } = "";
        [Indexed]
        public string QuestionHash { get; set; } = "";
        public string ReferenceData { get; set; } = "";
    }
}
