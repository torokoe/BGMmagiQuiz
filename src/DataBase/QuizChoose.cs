using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizChoose
    {
        [PrimaryKey, AutoIncrement]
        public int Choose_id { get; set; } = -1;
        [Indexed]
        public int Quiz_id { get; set; } = -1;
        public int ChooseMaster_id { get; set; } = -1;
        public int ChooseIndex { get; set; } = 0;
        public int Priority { get; set; } = 0;
    }

}
