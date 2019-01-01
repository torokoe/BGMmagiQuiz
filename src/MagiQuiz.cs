using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz
{
    public class MagiQuizController
    {
        public static MagiQuiz_Data Data = new MagiQuiz_Data();
        public static object mqlock = new object();
    }

    public class MagiQuiz_Data
    {
        public List<MagiQuiz> MQL { get; set; } = new List<MagiQuiz>();
        public List<MagiQuiz> MQLT { get; set; } = new List<MagiQuiz>();
        public List<MagiQuizCat> MQLC { get; set; } = new List<MagiQuizCat>();
    }

    public class MagiQuizCat
    {
        public int Qid { get; set; } = -1;
        public string Quiz { get; set; } = "";
        public Category Cat { get; set; } = Category.other;
    }

    public class MagiQuiz
    {
        public QuizSource source = QuizSource.bangumi;
        public int Qid { get; set; } = -1;
        public List<int> Qid_sub { get; set; } = new List<int>();
        public string Quiz { get; set; } = "";
        public string RawQuiz { get; set; } = "";
        public string Choose1 { get; set; } = "";
        public string Choose1_Hash { get; set; } = "";
        public string Choose2 { get; set; } = "";
        public string Choose2_Hash { get; set; } = "";
        public string Choose3 { get; set; } = "";
        public string Choose3_Hash { get; set; } = "";
        public string Choose4 { get; set; } = "";
        public string Choose4_Hash { get; set; } = "";
        public string Answer { get; set; } = "";
        public int TotalChoose { get; set; } = -1;
        public int AnswerNumber { get; set; } = -1;
        public MagiQuizCreator Creator { get; set; } = new MagiQuizCreator();
        public List<MagiQuizCreator> Creator_sub { get; set; } = new List<MagiQuizCreator>();
        public MagiQuizPassrateData PassrateData { get; set; } = new MagiQuizPassrateData();
        public List<MagiQuizPassrateData> PassrateData_sub { get; set; } = new List<MagiQuizPassrateData>();
        public int type { get; set; } = 0;
        public Category Cat { get; set; } =  Category.na;
        public List<Category> Cat_sub { get; set; } = new List<Category>();
        public DateTime updated { get; set; } = DateTime.Now;
    }

    public enum QuizSource : int
    {
        bangumi = 0,
        bilibili = 1,
        User = 10,
    }

    public enum Category : int
    {
        [Description("书籍")]
        book = 0,
        [Description("轻小说")]
        book_LightNovel = 1,
        /// <summary>
        /// 动画
        /// </summary>
        [Description("动画")]
        anime = 10,
        [Description("动画声优")]
        anime_Voice = 11,
        [Description("动漫作品")]
        anime_Project = 12,
        [Description("动漫内容")]
        anime_Content = 13,
        /// <summary>
        /// 音乐
        /// </summary>
        [Description("音乐")]
        music = 20,
        [Description("Vocaloid")]
        music_Vocaloid = 21,
        [Description("ACG音乐")]
        music_Acg = 22,
        [Description("三次元音乐")]
        music_Real = 23,
        /// <summary>
        /// 游戏
        /// </summary>
        [Description("游戏")]
        game = 30,
        [Description("动作射击")]
        game_Acs = 31,
        [Description("冒险格斗")]
        game_Atf = 32,
        [Description("策略模拟")]
        game_Sm = 33,
        [Description("音乐体育")]
        game_Ms = 34,
        [Description("角色扮演")]
        game_Mmo = 35,
        /// <summary>
        /// 三次元
        /// </summary>
        [Description("三次元")]
        real = 40,
        [Description("军事")]
        real_Military = 41,
        [Description("科技数码")]
        real_Technology = 42,
        [Description("理综")]
        real_Ss = 43,
        [Description("文综")]
        real_As = 44,
        /// <summary>
        /// 人物
        /// </summary>
        [Description("人物")]
        mono = 50,
        [Description("鬼畜")]
        mono_Kichiku = 51,
        /// <summary>
        /// 剧集
        /// </summary>
        [Description("剧集")]
        drama = 60,
        [Description("电影")]
        film = 61,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        other = 90,
        [Description("其他_old")]
        other_old = 99,
        [Description("N/A")]
        na = 100
    }

    public class MagiQuizCreator
    {
        public int Qid { get; set; } = -1;
        public string CreatorUserName { get; set; } = "";

        public string CreatorHomePage { get; set; } = "";

        public string CreatorIconUrl { get; set; } = "";

        public string CreatorJoinDate { get; set; } = "";

    }

    public class MagiQuizPassrateData
    {
        public int Qid { get; set; } = -1;
        public double Passrate_Percentage { get; set; } = -1;
        public int Passrate_TotalTester { get; set; } = -1;
        public int Passrate_Number { get; set; } = -1;
        public int Passrate_Choose1_Number { get; set; } = -1;
        public double Passrate_Choose1_Percentage { get; set; } = -1;
        public int Passrate_Choose2_Number { get; set; } = -1;
        public double Passrate_Choose2_Percentage { get; set; } = -1;
        public int Passrate_Choose3_Number { get; set; } = -1;
        public double Passrate_Choose3_Percentage { get; set; } = -1;
        public int Passrate_Choose4_Number { get; set; } = -1;
        public double Passrate_Choose4_Percentage { get; set; } = -1;
        public int Passrate_ChooseSkip_Number { get; set; } = -1;
        public double Passrate_ChooseSkip_Percentage { get; set; } = -1;
    }

}
