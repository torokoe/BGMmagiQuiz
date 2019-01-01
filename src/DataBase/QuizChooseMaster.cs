using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizChooseMaster
    {
        [PrimaryKey, AutoIncrement]
        public int ChooseMaster_id { get; set; } = -1;
        public string Choose { get; set; } = "";
        [Indexed]
        public string Choose_Hash { get; set; } = "";
    }
}
