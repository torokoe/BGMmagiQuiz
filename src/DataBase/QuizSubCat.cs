using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizSubCat
    {
        [PrimaryKey, AutoIncrement]
        public int SubCat_id { get; set; } = -1;
        [Indexed]
        public int Quiz_id { get; set; } = -1;
        public int SubCatMaster_id { get; set; } = -1;
    }
}
