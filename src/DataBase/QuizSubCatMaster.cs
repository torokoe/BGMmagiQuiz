using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizSubCatMaster
    {
        [PrimaryKey, AutoIncrement]
        public int SubCatMaster_id { get; set; } = -1;
        public Category SubCat { get; set; } = Category.na;
    }
}
