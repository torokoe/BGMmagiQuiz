using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizSouceID
    {
        [PrimaryKey, AutoIncrement]
        public int SouceID_id { get; set; } = -1;
        [Indexed]
        public int Quiz_id { get; set; } = -1;
        public int SouceIDMaster_id { get; set; } = -1;
    }
}
