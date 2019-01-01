using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizPassrateData
    {
        [PrimaryKey, AutoIncrement]
        public int PassrateData_id { get; set; } = -1;
        [Indexed]
        public int Quiz_id { get; set; } = -1;
        public int PassrateDataMaster_id { get; set; } = -1;
    }
}
