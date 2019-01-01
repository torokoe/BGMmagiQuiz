using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizPassrateDataAnalysisMaster
    {
        [PrimaryKey, AutoIncrement]
        public int PassrateDataAnalysisMaster_id { get; set; } = -1;
        public double PassratePercentage { get; set; } = -1;
        public int PassrateTotal { get; set; } = -1;
        public int PassrateCorrect { get; set; } = -1;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
