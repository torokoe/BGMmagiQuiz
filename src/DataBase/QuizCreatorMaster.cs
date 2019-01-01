using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz.DataBase
{
    public class QuizCreatorMaster
    {
        [PrimaryKey, AutoIncrement]
        public int CreatorMaster_id { get; set; } = -1;
        public string CreatorUserName { get; set; } = "";
        public string CreatorHomePage { get; set; } = "";
        public string CreatorIconUrl { get; set; } = "";
        public string CreatorJoinDate { get; set; } = "";

    }
}
