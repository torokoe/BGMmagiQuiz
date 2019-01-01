using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizPassrateDataMaster
    {
        [PrimaryKey, AutoIncrement]
        public int PassrateDataMaster_id { get; set; } = -1;
        public int PassrateChooseNumber { get; set; } = -1;
        public double PassrateChoosePercentage { get; set; } = -1;
        public int PassrateChooseIndex { get; set; } = 0;
    }
}
