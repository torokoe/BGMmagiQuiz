using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizSouceIDMaster
    {
        [PrimaryKey, AutoIncrement]
        public int SouceIDMaster_id { get; set; } = -1;
        public int SouceID { get; set; } = -1;
    }
}
