using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizAnswer
    {
        [PrimaryKey, AutoIncrement]
        public int Answer_id { get; set; } = -1;
        [Indexed]
        public int Quiz_id { get; set; } = -1;
        public int AnswerMaster_id { get; set; } = -1;
        public string ReferenceData { get; set; } = "";
        public int Priority { get; set; } = 0;
    }

}
