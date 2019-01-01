using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BGMmagiQuiz
{
    static class QuizTask
    {
        static string cookies = "{REMOVED}";

        public static void SkipQuiz(int Qid, Action callback, string cat = "")
        {
            var client = new RestClient("http://bgm.tv");
            var request = new RestRequest("magi/answer", Method.POST);

            foreach (var s in cookies.Split(';').Select(x => x.Trim()))
            {
                var nameValue = s.Split('=');
                request.AddCookie(nameValue[0], nameValue[1]);
            }

            request.AddParameter("formhash", "{REMOVED}");
            request.AddParameter("quiz_id", Qid);
            request.AddParameter("cat", cat);
            request.AddParameter("submit", "%E8%B7%B3%E8%BF%87");

            client.AddDefaultHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            client.AddDefaultHeader("Accept-Encoding", "gzip, deflate");
            client.AddDefaultHeader("DNT", "1");
            client.AddDefaultHeader("Accept-Language", "zh-TW,zh;q=0.8,en-US;q=0.6,en;q=0.4");
            client.AddDefaultHeader("Content-Type", "application/x-www-form-urlencoded");
            client.AddDefaultHeader("Upgrade-Insecure-Requests", "1");
            client.AddDefaultHeader("Origin", "1");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";

            try
            {
                client.ExecuteAsync(request, (response) =>
                {
                    callback();
                });
            }
            catch (Exception ex) { ConsoleHandler.WriteLine(ex.Message); }
        }

        public static void GetMagiQuizID_cat(Category cat, Action<MagiQuiz> callback)
        {
            string result = "";

            var client = new RestClient("http://bgm.tv");
            var request = new RestRequest(string.Format("magi?cat={0}", Enum.GetName(typeof(Category), cat)), Method.GET);

            foreach (var s in cookies.Split(';').Select(x => x.Trim()))
            {
                var nameValue = s.Split('=');
                request.AddCookie(nameValue[0], nameValue[1]);
            }

            client.AddDefaultHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            client.AddDefaultHeader("Accept-Encoding", "gzip, deflate");
            client.AddDefaultHeader("Accept-Language", "zh-TW,zh;q=0.8,en-US;q=0.6,en;q=0.4");
            client.AddDefaultHeader("Content-Type", "application/x-www-form-urlencoded");
            client.AddDefaultHeader("DNT", "1");
            client.AddDefaultHeader("Upgrade-Insecure-Requests", "1");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";

            try
            {
                client.ExecuteAsync(request, (response) =>
                {
                    if (response.ResponseStatus == ResponseStatus.Completed)
                    {
                        MagiQuiz mq = new MagiQuiz();

                        mq.Cat = cat;
                        mq.updated = DateTime.Now;

                        result = Regex.Replace(response.Content, @"\r\n?|\n", "");
                        result = result.Replace("&amp;", "&");
                        result = result.Replace("&quot;", "\"");
                        result = result.Replace("&lt;", "<");
                        result = result.Replace("&gt;", ">");
                        result = result.Replace("&nbsp;", " ");
                        result = result.Replace("&copy;", "©");
                        string pattern = "";
                        Regex rgx = new Regex(pattern);
                        Match match = rgx.Match(result);

                        pattern = "<div class=\"magiQuiz\">.*#(.*)<\\/small> (.*)<\\/h2>";
                        rgx = new Regex(pattern);
                        match = rgx.Match(result);
                        if (match.Success)
                        {
                            mq = RegexGroupToMagiQuiz(mq, match);
                            callback(mq);
                        }
                        else
                        {
                            if (result.Contains("暂时没有需要挑战的问题了"))
                            {
                                mq.type = -3;
                                ConsoleHandler.WriteLine("Cat=" + Enum.GetName(typeof(Category), cat) + ": 暂时没有需要挑战的问题了，请稍后再来。", ConsoleColor.Red);
                                callback(mq);
                            }
                            else
                            {
                                mq.type = -1;
                                ConsoleHandler.WriteLine( "Result not match");
                            }
                        }
                    }
                });
            }
            catch { }
        }

        public static void GetMagiQuizData(int Qid, Action<MagiQuiz> callback, Category cat = Category.other)
        {
            string result = "";
            var client = new RestClient("http://bgm.tv");
            var request = new RestRequest(string.Format("magi/q/{0}", Qid), Method.GET);

            foreach (var s in cookies.Split(';').Select(x => x.Trim()))
            {
                var nameValue = s.Split('=');
                request.AddCookie(nameValue[0], nameValue[1]);
            }

            client.AddDefaultHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            client.AddDefaultHeader("Accept-Encoding", "gzip, deflate");
            client.AddDefaultHeader("Accept-Language", "zh-TW,zh;q=0.8,en-US;q=0.6,en;q=0.4");
            client.AddDefaultHeader("Content-Type", "application/x-www-form-urlencoded");
            client.AddDefaultHeader("DNT", "1");
            client.AddDefaultHeader("Upgrade-Insecure-Requests", "1");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
            request.AddHeader("Referer", "http://bgm.tv");

            try
            {
                client.ExecuteAsync(request, (response) =>
                {
                MagiQuiz mq = new MagiQuiz();
                mq.Qid = Qid;
                mq.Cat = cat;
                mq.updated = DateTime.Now;

                    if (response.ResponseStatus == ResponseStatus.Completed)
                    {
                        //  result = response.Content.Replace(Environment.NewLine, "");
                        result = Regex.Replace(response.Content, @"\r\n?|\n", "");
                        result = result.Replace("&amp;", "&");
                        result = result.Replace("&quot;", "\"");
                        result = result.Replace("&lt;", "<");
                        result = result.Replace("&gt;", ">");
                        result = result.Replace("&nbsp;", " ");
                        result = result.Replace("&copy;", "©");
                        string pattern = "";
                        Regex rgx = new Regex(pattern);
                        Match match = rgx.Match(result);

                        string patternleft = "<div class=\"magiQuiz\">.*#(.*)<\\/small> (.*)<\\/.*<ul class=\"opts\">";
                        string patternright = ".*<div class=\"quizInfo clearit\">.*<img src=\"(.*)\" .*<a href=\"(.*)\" class=\"l\">(.*)<\\/a>.*>通过率: <small class=\"na\">(.*)%<\\/small> \\((.*)\\/(.*)\\)<\\/small>.*<small class=\"rr\">(.*)<\\/small>A.(.*)%<\\/li>.*<small class=\"rr\">(.*)<\\/small>B.(.*)%<\\/li>.*<small class=\"rr\">(.*)<\\/small>C.(.*)%<\\/li>.*<small class=\"rr\">(.*)<\\/small>D.(.*)%<\\/li>.*<small class=\"rr\">(.*)<\\/small>-.(.*)%<\\/li>";
                        string patterncore = "<li class=\".*\">(.*) <\\/li>";

                        for (int i = 4; i > 0; i--)
                        {
                            string tmp = "";
                            for (int a = 1; a <= i; a++)
                            {
                                tmp += patterncore;
                            }
                            pattern = patternleft + tmp + patternright;
                            rgx = new Regex(pattern);
                            match = rgx.Match(result);
                            if (match.Success)
                            {
                                mq.TotalChoose = i;
                                break;
                            }
                        }

                        if (mq.TotalChoose > 0)
                        {
                            mq.type = 1;
                            mq = RegexGroupToMagiQuiz(mq, match);

                            if (mq.AnswerNumber == -1)
                            {
                                ConsoleHandler.WriteLine(mq.Qid + ": Answer not found", ConsoleColor.DarkRed);
                                mq.type = -2;
                            }
                            if (mq.TotalChoose < 4)
                            {
                                ConsoleHandler.WriteLine(mq.Qid + "=TotalChoose=" + mq.TotalChoose, ConsoleColor.Cyan);
                            }

                            callback(mq);
                        }
                        else
                        {
                            if (result.Contains("数据库中没有查询到题目信息"))
                            {
                                mq.type = -1;

                                ConsoleHandler.WriteLine("QID=" + Qid + ": 数据库中没有查询到题目信息", ConsoleColor.Red);
                                callback(mq);
                            }
                            else
                            {
                                ConsoleHandler.WriteLine(Qid + ": result not match");
                            }
                        }
                    }
                    else if (response.ResponseStatus == ResponseStatus.Error)
                    {
                        ConsoleHandler.WriteLine(Qid + ": Error", ConsoleColor.Red);
                    }
                    else
                    {
                        ConsoleHandler.WriteLine(Qid + ": " + response.ResponseStatus, ConsoleColor.Red);
                    }
                });
            }
            catch (Exception ex) { ConsoleHandler.WriteLine(ex.Message);
            }
        }

        private static MagiQuiz RegexGroupToMagiQuiz(MagiQuiz mq, Match match)
        {

            try
            {
                int _Qid = mq.Qid;
                Int32.TryParse(match.Groups[1].Value, out _Qid);
                mq.Qid = _Qid;

                mq.Quiz = match.Groups[2].Value;

                if (match.Groups[3].Value.Contains("正确答案</span>") | match.Groups[2].Value.Contains("回答正确</span>"))
                {
                    mq.Answer = match.Groups[3].Value.Replace("<span class=\"rr\">", "").Replace("正确答案</span>", "").Replace("回答正确</span>", "");
                    mq.AnswerNumber = 1;
                }
                mq.Choose1 = match.Groups[3].Value.Replace("<span class=\"rr\">", "").Replace("正确答案</span>", "").Replace("回答正确</span>", "");

                if (mq.TotalChoose > 1)
                {
                    if (match.Groups[4].Value.Contains("正确答案</span>") | match.Groups[3].Value.Contains("回答正确</span>"))
                    {
                        mq.Answer = match.Groups[4].Value.Replace("<span class=\"rr\">", "").Replace("正确答案</span>", "").Replace("回答正确</span>", "");
                        mq.AnswerNumber = 2;
                    }
                    mq.Choose2 = match.Groups[4].Value.Replace("<span class=\"rr\">", "").Replace("正确答案</span>", "").Replace("回答正确</span>", "");
                }
                if (mq.TotalChoose > 2)
                {
                    if (match.Groups[5].Value.Contains("正确答案</span>") | match.Groups[4].Value.Contains("回答正确</span>"))
                    {
                        mq.Answer = match.Groups[5].Value.Replace("<span class=\"rr\">", "").Replace("正确答案</span>", "").Replace("回答正确</span>", "");
                        mq.AnswerNumber = 3;
                    }
                    mq.Choose3 = match.Groups[5].Value.Replace("<span class=\"rr\">", "").Replace("正确答案</span>", "").Replace("回答正确</span>", "");
                }
                if (mq.TotalChoose > 3)
                {
                    if (match.Groups[6].Value.Contains("正确答案</span>") | match.Groups[5].Value.Contains("回答正确</span>"))
                    {
                        mq.Answer = match.Groups[6].Value.Replace("<span class=\"rr\">", "").Replace("正确答案</span>", "").Replace("回答正确</span>", "");
                        mq.AnswerNumber = 4;
                    }
                    mq.Choose4 = match.Groups[6].Value.Replace("<span class=\"rr\">", "").Replace("正确答案</span>", "").Replace("回答正确</span>", "");
                }

                mq.Creator.CreatorIconUrl = match.Groups[mq.TotalChoose + 3].Value;
                mq.Creator.CreatorHomePage = match.Groups[mq.TotalChoose + 4].Value;
                mq.Creator.CreatorUserName = match.Groups[mq.TotalChoose + 5].Value;

                double _Passrate_Percentage = 0;
                double.TryParse(match.Groups[mq.TotalChoose + 6].Value, out _Passrate_Percentage);
                mq.PassrateData.Passrate_Percentage = _Passrate_Percentage;

                int _Passrate_Number = 0;
                Int32.TryParse(match.Groups[mq.TotalChoose + 7].Value, out _Passrate_Number);
                mq.PassrateData.Passrate_Number = _Passrate_Number;

                int _Passrate_TotalTester = 0;
                Int32.TryParse(match.Groups[mq.TotalChoose + 8].Value, out _Passrate_TotalTester);
                mq.PassrateData.Passrate_TotalTester = _Passrate_TotalTester;

                int _Passrate_Choose1_Number = 0;
                Int32.TryParse(match.Groups[mq.TotalChoose + 9].Value, out _Passrate_Choose1_Number);
                mq.PassrateData.Passrate_Choose1_Number = _Passrate_Choose1_Number;

                double _Passrate_Choose1_Percentage = 0;
                double.TryParse(match.Groups[mq.TotalChoose + 10].Value, out _Passrate_Choose1_Percentage);
                mq.PassrateData.Passrate_Choose1_Percentage = _Passrate_Choose1_Percentage;

                int _Passrate_Choose2_Number = 0;
                Int32.TryParse(match.Groups[mq.TotalChoose + 11].Value, out _Passrate_Choose2_Number);
                mq.PassrateData.Passrate_Choose2_Number = _Passrate_Choose2_Number;

                double _Passrate_Choose2_Percentage = 0;
                double.TryParse(match.Groups[mq.TotalChoose + 12].Value, out _Passrate_Choose2_Percentage);
                mq.PassrateData.Passrate_Choose2_Percentage = _Passrate_Choose2_Percentage;

                int _Passrate_Choose3_Number = 0;
                Int32.TryParse(match.Groups[mq.TotalChoose + 13].Value, out _Passrate_Choose3_Number);
                mq.PassrateData.Passrate_Choose3_Number = _Passrate_Choose3_Number;

                double _Passrate_Choose3_Percentage = 0;
                double.TryParse(match.Groups[mq.TotalChoose + 14].Value, out _Passrate_Choose3_Percentage);
                mq.PassrateData.Passrate_Choose3_Percentage = _Passrate_Choose3_Percentage;

                int _Passrate_Choose4_Number = 0;
                Int32.TryParse(match.Groups[mq.TotalChoose + 15].Value, out _Passrate_Choose4_Number);
                mq.PassrateData.Passrate_Choose4_Number = _Passrate_Choose4_Number;

                double _Passrate_Choose4_Percentage = 0;
                double.TryParse(match.Groups[mq.TotalChoose + 16].Value, out _Passrate_Choose4_Percentage);
                mq.PassrateData.Passrate_Choose4_Percentage = _Passrate_Choose4_Percentage;

                int _Passrate_ChooseSkip_Number = 0;
                Int32.TryParse(match.Groups[mq.TotalChoose + 17].Value, out _Passrate_ChooseSkip_Number);
                mq.PassrateData.Passrate_ChooseSkip_Number = _Passrate_ChooseSkip_Number;

                double _Passrate_ChooseSkip_Percentage = 0;
                double.TryParse(match.Groups[mq.TotalChoose + 18].Value, out _Passrate_ChooseSkip_Percentage);
                mq.PassrateData.Passrate_ChooseSkip_Percentage = _Passrate_ChooseSkip_Percentage;
            }
            catch(Exception ex) {

            }

            return mq;
        }
    }
}
    

