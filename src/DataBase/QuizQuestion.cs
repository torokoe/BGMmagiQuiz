using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizQuestion
    {
        [PrimaryKey, AutoIncrement]
        public int Question_id { get; set; } = -1;
        [Indexed]
        public int Quiz_id { get; set; } = -1;
        public int QuestionMaster_id { get; set; } = -1;
        public int Priority { get; set; } = 0;
    }
}
