using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Threading;
using Newtonsoft.Json;
using YamlDotNet;
using YamlDotNet.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using Steganography;
using ImgMetadata;
using System.Diagnostics;

namespace BGMmagiQuiz
{

    class Program
    {
        static int start = 0;
        static int end = 9000;
        static void Main(string[] args)
        {

            initlist();
            SQLiteHandler.create();
        }
        private static void removepanding()
        {
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Count.ToString());
            MagiQuizController.Data.MQL.Where(w => w.type == 888).ToList().ForEach(f =>
               ConsoleHandler.WriteLine(f.Quiz)
            );
            MagiQuizController.Data.MQL.RemoveAll(w => w.type == 888);
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Count.ToString());
        }
        private static void answeredit()
        {
            bool a = false;
            while (!a)
            {
                string b = Console.ReadLine();
                if (b == "-1")
                {
                    a = true;
                }
                else if (b != "")
                {
                    MagiQuizController.Data.MQL.Where(w => w.Answer.Contains(b)).ToList().ForEach(g =>
                      {
                          ConsoleHandler.WriteLine("=================");
                          ConsoleHandler.WriteLine(g.Quiz);
                          ConsoleHandler.WriteLine(g.Qid.ToString());
                          ConsoleHandler.WriteLine("1" + g.Choose1);
                          ConsoleHandler.WriteLine("2" + g.Choose2);
                          ConsoleHandler.WriteLine("3" + g.Choose3);
                          ConsoleHandler.WriteLine("4" + g.Choose4);
                          ConsoleHandler.WriteLine("AN" + g.AnswerNumber);
                          ConsoleHandler.WriteLine(g.Answer);
                          ConsoleHandler.WriteLine("=================");
                          b = Console.ReadLine();
                          if (b != "")
                          {
                              if (b == "5")
                              {
                                  g.type = 888;
                              }
                              if (b == "1")
                              {
                                  g.Answer = "";
                              }
                          }
                      });
                }

            }
        }
        private static void qizuclear()
        {
            string pattern;
            Regex rgx;
            MagiQuizController.Data.MQL.ForEach(e =>
            {
                if (
                e.Quiz.Contains("http://min.us/") | 
                e.Quiz.Contains("http://bangumi.tv/") |
                e.Quiz.Contains("http://p.tl/i/17947771")
                )
                {
                    e.type = 888;
                }
            });

            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Count.ToString());

            MagiQuizController.Data.MQL.Where(w => w.type == 888).ToList().ForEach(f =>
               ConsoleHandler.WriteLine(f.Quiz)
            );

            MagiQuizController.Data.MQL.RemoveAll(w => w.type == 888);

     //       MagiQuizController.Data.MQL.RemoveAll(w => w.Quiz == "");
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Count.ToString());
            Console.ReadLine();

            MagiQuizController.Data.MQL.ForEach(e =>
            {
                if (e.RawQuiz == "")
                {
                    e.RawQuiz = e.Quiz;
                }

                e.Quiz = e.Quiz.Replace("（", "(");
                e.Quiz = e.Quiz.Replace("）", ")");
                e.Quiz = e.Quiz.Replace("？", "?");
                e.Quiz = e.Quiz.Replace("：", ":");
                e.Quiz = e.Quiz.Replace("！", "!");
                e.Quiz = e.Quiz.Replace("【", "(");
                e.Quiz = e.Quiz.Replace("】", ")");
                e.Quiz = e.Quiz.Replace("[", "(");
                e.Quiz = e.Quiz.Replace("]", ")");
                e.Quiz = e.Quiz.Replace("［", "(");
                e.Quiz = e.Quiz.Replace("］", ")");
                e.Quiz = e.Quiz.Replace("「", "《");
                e.Quiz = e.Quiz.Replace("」", "》");
                e.Quiz = e.Quiz.Replace("『", "《");
                e.Quiz = e.Quiz.Replace("』", "》");
                e.Quiz = e.Quiz.Replace("<", "《");
                e.Quiz = e.Quiz.Replace(">", "》");
                e.Quiz = e.Quiz.Replace("＜＜", "《");
                e.Quiz = e.Quiz.Replace("＞＞", "》");
                e.Quiz = e.Quiz.Replace("「「", "《");
                e.Quiz = e.Quiz.Replace("」」", "》");
                e.Quiz = e.Quiz.Replace("‘", @"“");
                e.Quiz = e.Quiz.Replace("’", @"”");
                e.Quiz = e.Quiz.Replace("()", "");
                e.Quiz = e.Quiz.Replace("&quot;", @"""");
                e.Quiz = e.Quiz.Replace("&lt;", "「");
                e.Quiz = e.Quiz.Replace("&gt;", "」");
                e.Quiz = e.Quiz.Replace(Environment.NewLine, "");



                pattern = "(([(（\\[［「『<【]easy[)）\\]］」』>】])|([(（\\[［「『<【].*((送分)|(进阶)|(易)|(简单)|(簡單)|(难)|(難)|(良心)|(坑爹)|(作死)|(專業)|(上级)|(看图)|(填空)|(空耳)|(音游)|(剧透)|(擦边)|(推广)|(扫盲)|(neta)|(NETA)|(常识)|(厨向)|(科普)|(細節)|(小学生)|(哲♂学)|(测试人品)(声优)|(提示)|(强行ACG)).*[)）\\]］」』>】])|(提示:)|(向填空題:)|(填空:)|([(（\\[［「『<【].*送分$))";
                rgx = new Regex(pattern);
                e.Quiz = rgx.Replace(e.Quiz, "");

                pattern = "\\(((thd)|(THD)|(东方)|(東方)|(东方project))\\)";
                rgx = new Regex(pattern);
                e.Quiz = rgx.Replace(e.Quiz, "《东方Project》");

                pattern = "^([0-9]{1,3}《)";
                rgx = new Regex(pattern);
                e.Quiz = rgx.Replace(e.Quiz, "《");

                pattern = "\\([ \\.\\?]{1,5}\\)";
                rgx = new Regex(pattern);
                e.Quiz = rgx.Replace(e.Quiz, "（...）");

                pattern = "^((送分)|( {1,999})|([0-9][ .:?])|([0-9][0-9][ .:?])|([0-9][0-9][0-9][ .:?])|(#.*#)|( {1,999}))";
                rgx = new Regex(pattern);
                e.Quiz = rgx.Replace(e.Quiz, "");

                pattern = "((ww)|([ ]{1,999}))$";
                rgx = new Regex(pattern);
                e.Quiz = rgx.Replace(e.Quiz, "");



                ConsoleHandler.WriteLine(e.Quiz);

            });

            Console.ReadLine();
            DataHandler.SaveMagiQuizData();
        }
        private static void listdata()
        {
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Count.ToString());
            ConsoleHandler.WriteLine("Bangumi = " + MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bangumi).ToList().Count.ToString());
            ConsoleHandler.WriteLine("Bilibili = " + MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bilibili).ToList().Count.ToString());
            ConsoleHandler.WriteLine("Bilibili WC= " + MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bilibili && w.Choose1 != "").ToList().Count.ToString());
            ConsoleHandler.WriteLine("Bilibili AS= " + MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bilibili && w.Answer != "").ToList().Count.ToString());
            MagiQuizController.Data.MQL.RemoveAll(r => r.source == QuizSource.bilibili && r.Choose1 == "");

            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Count.ToString());
            ConsoleHandler.WriteLine("Bangumi = " + MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bangumi).ToList().Count.ToString());
            ConsoleHandler.WriteLine("Bilibili = " + MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bilibili).ToList().Count.ToString());
            ConsoleHandler.WriteLine("Bilibili WC= " + MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bilibili && w.Choose1 != "").ToList().Count.ToString());
            ConsoleHandler.WriteLine("Bilibili AS= " + MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bilibili && w.Answer != "").ToList().Count.ToString());

            MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bilibili && w.Choose1 == "").ToList().ForEach(f =>
            {
                ConsoleHandler.WriteLine(f.Quiz);
                ConsoleHandler.WriteLine(f.Choose1);
                ConsoleHandler.WriteLine(f.Choose2);
                ConsoleHandler.WriteLine(f.Answer);
                ConsoleHandler.WriteLine(f.AnswerNumber.ToString());
            });

            ConsoleHandler.WriteLine(MagiQuizController.Data.MQLT.Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQLC.Count.ToString());
            Console.ReadLine();

        }
        private static void maxdup()
        {
            ConsoleHandler.WriteLine("=====Same Qid=============");
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bangumi).ToList().GroupBy(s => s.Qid).SelectMany(grp => grp.Skip(1)).ToList().Count.ToString());
            MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bangumi).ToList().GroupBy(s => s.Qid).SelectMany(grp => grp.Skip(1)).ToList().ForEach(f =>
            {
                f.type = 888;
            });
            MagiQuizController.Data.MQL.RemoveAll(q => q.type == 888);
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bangumi).ToList().GroupBy(s => s.Qid).SelectMany(grp => grp.Skip(1)).ToList().Count.ToString());

            ConsoleHandler.WriteLine("==================");
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Count.ToString());
            ConsoleHandler.WriteLine("==================");
            Console.ReadLine();
            List<MagiQuiz> mqls = new List<MagiQuiz>();
            MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bangumi).ToList().GroupBy(s => s.Quiz).SelectMany(grp => grp.Skip(1)).ToList().ForEach(f =>
            {

                MagiQuiz mq = new MagiQuiz();
                List<MagiQuiz> mql = new List<MagiQuiz>();
                mql = MagiQuizController.Data.MQL.Where(v => v.Quiz == f.Quiz && v.Answer == f.Answer).OrderBy(o => o.Qid).ToList();

                if (mql.Count > 1)
                {
                    mq = mql[0];

                    for (int i = (mql.Count - 1); i > 0; i--)
                    {
                        if (i == 0)
                        {
                        }
                        else
                        {
                            mql[i].PassrateData.Qid = mql[i].Qid;
                            mql[i].Creator.Qid = mql[i].Qid;
                            mq.Qid_sub.Add(mql[i].Qid);
                            mq.PassrateData_sub.Add(mql[i].PassrateData);
                            mq.Creator_sub.Add(mql[i].Creator);
                        }

                    }
                    f.type = 10;
                    if (mqls.Where(w => w.Qid == mq.Qid).ToList().Count == 0)
                    {
                        mqls.Add(mq);
                    }
                }
                else
                {
                    mql = MagiQuizController.Data.MQL.Where(v => v.Quiz == f.Quiz).OrderBy(o => o.Qid).ToList();
                    mql.ForEach(d =>
                    {
                        Console.WriteLine(d.Answer);
                        Console.WriteLine(d.AnswerNumber);
                    });
                }
            });
            MagiQuizController.Data.MQL.ForEach(f =>
            {
                if (mqls.Where(w => w.Qid == f.Qid).ToList().Count == 1)
                {
                    f = mqls.Where(w => w.Qid == f.Qid).ToList().Last();
                }
                else if (mqls.Where(w => w.Qid == f.Qid).ToList().Count > 1)
                {
                    Console.WriteLine("???");
                }
            });


            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.type == 10).ToList().Count.ToString());
            MagiQuizController.Data.MQL.RemoveAll(r => r.type == 10);
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Count.ToString());
            ConsoleHandler.WriteLine("==================");

        }
        private static void cnull()
        {
            ConsoleHandler.WriteLine("Clear Null");
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Count.ToString());
            MagiQuizController.Data.MQL.RemoveAll(r => r.Quiz == "" && r.Answer == "");
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Count.ToString());
        }
        private static void csum()
        {
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bilibili).ToList().Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bangumi).ToList().Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQLT.Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQLT.Where(w => w.source == QuizSource.bilibili).ToList().Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.GroupBy(s => s.Quiz).SelectMany(grp => grp.Skip(1)).ToList().Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bangumi).ToList().GroupBy(s => s.Quiz).SelectMany(grp => grp.Skip(1)).ToList().Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bilibili).ToList().GroupBy(s => s.Quiz).SelectMany(grp => grp.Skip(1)).ToList().Count.ToString());
            Console.ReadLine();
            ConsoleHandler.WriteLine("==================");
            ConsoleHandler.WriteLine("=========Quiz=========");
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.Quiz == "" & w.source == QuizSource.bilibili).ToList().Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.Quiz == "" & w.source == QuizSource.bangumi).ToList().Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.Quiz == "" & w.source == QuizSource.bangumi & w.Cat != Category.na).ToList().Count.ToString());
            ConsoleHandler.WriteLine("==================");
            Console.ReadLine();
        }
        private static void Getsame()
        {
            MagiQuizController.Data.MQL.Where(w => w.source == QuizSource.bangumi).GroupBy(s => s.Quiz).SelectMany(grp => grp.Skip(1)).ToList().ForEach(f =>
            {
                ConsoleHandler.WriteLine(f.Quiz);
                MagiQuizController.Data.MQL.Where(w => w.Quiz == f.Quiz && w.source == QuizSource.bangumi).ToList().ForEach(g =>
                {
                    ConsoleHandler.WriteLine("=================");
                    ConsoleHandler.WriteLine(g.Quiz);
                    ConsoleHandler.WriteLine(g.Qid.ToString());
                    ConsoleHandler.WriteLine("1" + g.Choose1);
                    ConsoleHandler.WriteLine("2" + g.Choose2);
                    ConsoleHandler.WriteLine("3" + g.Choose3);
                    ConsoleHandler.WriteLine("4" + g.Choose4);
                    ConsoleHandler.WriteLine("AN" + g.AnswerNumber);
                    ConsoleHandler.WriteLine(g.Answer);

                });
                string a = Console.ReadLine();
                if (a == "")
                {
                    f.type = -999;
                }
            });
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.type == -999).ToList().Count.ToString());

        }
        private static void m1()
        {
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQLT.Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQLT.Where(w => w.Answer != "").ToList().Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQLT.Where(w => w.AnswerNumber != -1).ToList().Count.ToString());
            string asa = Console.ReadLine();

            MagiQuizController.Data.MQLT.Where(w => w.source != QuizSource.bilibili).ToList().ForEach(f =>
            {
                f.source = QuizSource.bilibili;
            });

            MagiQuizController.Data.MQLT.Where(w => w.Answer == "" & w.AnswerNumber == 0).ToList().ForEach(
                f =>
                {
                    f.AnswerNumber = -1;
                });
            DataHandler.SaveMagiQuizData();

            MagiQuizController.Data.MQLT.Where(w => w.Answer == "" && w.AnswerNumber != -1).ToList().ForEach(
                f =>
                {
                    if (f.Choose1.Replace(" ", "") == f.Answer.Replace(" ", ""))
                    {
                        f.AnswerNumber = 1;
                    }
                    else if (f.Choose2.Replace(" ", "") == f.Answer.Replace(" ", ""))
                    {
                        f.AnswerNumber = 2;
                    }
                    else if (f.Choose3.Replace(" ", "") == f.Answer.Replace(" ", ""))
                    {
                        f.AnswerNumber = 3;
                    }
                    else if (f.Choose4.Replace(" ", "") == f.Answer.Replace(" ", ""))
                    {
                        f.AnswerNumber = 4;
                    }
                    else if (f.Choose1 == "" & f.Choose2 == "" & f.Choose3 == "" & f.Choose4 == "")
                    {
                        f.AnswerNumber = 1;
                        f.Choose1 = f.Answer;
                    }
                    else
                    {
                        ConsoleHandler.WriteLine("=================");
                        ConsoleHandler.WriteLine(f.Quiz);
                        ConsoleHandler.WriteLine("1" + f.Choose1);
                        ConsoleHandler.WriteLine("2" + f.Choose2);
                        ConsoleHandler.WriteLine("3" + f.Choose3);
                        ConsoleHandler.WriteLine("4" + f.Choose4);
                        ConsoleHandler.WriteLine("AN" + f.AnswerNumber);
                        ConsoleHandler.WriteLine(f.Answer);
                        string a = Console.ReadLine();
                        int ai = -1;
                        Int32.TryParse(a, out ai);
                        f.AnswerNumber = ai;

                        ConsoleHandler.WriteLine("C => answer");

                        a = Console.ReadLine();
                        if (a == "1" | a == "2" | a == "3" | a == "4")
                        {
                            if (a == "1")
                            {
                                f.Choose1 = f.Answer;
                            }
                            if (a == "2")
                            {
                                f.Choose2 = f.Answer;
                            }
                            if (a == "3")
                            {
                                f.Choose3 = f.Answer;
                            }
                            if (a == "4")
                            {
                                f.Choose4 = f.Answer;
                            }
                        }
                        else if (a == "")
                        {
                            ConsoleHandler.WriteLine("Skip");
                        }

                        ConsoleHandler.WriteLine("answer f ch");
                        a = Console.ReadLine();
                        if (a == "1")
                        {
                            if (f.AnswerNumber == 1)
                            {
                                f.Answer = f.Choose1;
                            }
                            if (f.AnswerNumber == 2)
                            {
                                f.Answer = f.Choose2;
                            }
                            if (f.AnswerNumber == 3)
                            {
                                f.Answer = f.Choose3;
                            }
                            if (f.AnswerNumber == 4)
                            {
                                f.Answer = f.Choose4;
                            }
                            ConsoleHandler.WriteLine("=================");
                            ConsoleHandler.WriteLine(f.Quiz);
                            ConsoleHandler.WriteLine("1" + f.Choose1);
                            ConsoleHandler.WriteLine("2" + f.Choose2);
                            ConsoleHandler.WriteLine("3" + f.Choose3);
                            ConsoleHandler.WriteLine("4" + f.Choose4);
                            ConsoleHandler.WriteLine(f.Answer);
                            ConsoleHandler.WriteLine("K t s=================");
                            Console.ReadKey();
                        }

                    }

                });
            Console.ReadKey();
            DataHandler.SaveMagiQuizData();
        }
        private static void st3()
        {
            const Int32 BufferSize = 128;
            MagiQuiz mq = new MagiQuiz();

            using (var fileStream = File.OpenRead("15.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.StartsWith("#"))
                    {
                        if (mq.Quiz != "")
                        {

                            List<MagiQuiz> mql = new List<MagiQuiz>();
                            mql = MagiQuizController.Data.MQLT.Where(w => w.Quiz == mq.Quiz).ToList();
                            if (mql != null & mql.Count > 0)
                            {
                                mql.ForEach(f =>
                                {
                                    string a = "";
                                    if (mq.Choose1 != "")
                                    {
                                        if (f.Choose1 != "")
                                        {
                                            ConsoleHandler.WriteLine(string.Format("A---------[{0}]=>[{1}]", f.Choose1, mq.Choose1));
                                            a = Console.ReadLine();
                                            if (a == "1")
                                            {
                                                f.Choose1 = mq.Choose1;
                                            }
                                        }
                                        else
                                        {
                                            f.Choose1 = mq.Choose1;
                                        }

                                    }

                                    if (mq.Choose2 != "")
                                    {
                                        if (f.Choose2 != "")
                                        {
                                            ConsoleHandler.WriteLine(string.Format("B---------[{0}]=>[{1}]", f.Choose2, mq.Choose2));
                                            a = Console.ReadLine();
                                            if (a == "1")
                                            {
                                                f.Choose2 = mq.Choose2;
                                            }
                                        }
                                        else
                                        {
                                            f.Choose2 = mq.Choose2;
                                        }
                                    }

                                    if (mq.Choose3 != "")
                                    {
                                        if (f.Choose3 != "")
                                        {
                                            ConsoleHandler.WriteLine(string.Format("C---------[{0}]=>[{1}]", f.Choose3, mq.Choose3));
                                            a = Console.ReadLine();
                                            if (a == "1")
                                            {
                                                f.Choose3 = mq.Choose3;
                                            }
                                        }
                                        else
                                        {
                                            f.Choose3 = mq.Choose3;
                                        }

                                    }

                                    if (mq.Choose4 != "")
                                    {
                                        if (f.Choose4 != "")
                                        {
                                            ConsoleHandler.WriteLine(string.Format("D---------[{0}]=>[{1}]", f.Choose4, mq.Choose4));
                                            a = Console.ReadLine();
                                            if (a == "1")
                                            {
                                                f.Choose4 = mq.Choose4;
                                            }
                                        }
                                        else
                                        {
                                            f.Choose4 = mq.Choose4;
                                        }

                                    }
                                    if (mq.Answer != "")
                                    {
                                        if (f.Answer != "")
                                        {
                                            ConsoleHandler.WriteLine(string.Format("Answer----[{0}]=>[{1}]", f.Answer, mq.Answer));
                                            if (mq.Answer != f.Answer)
                                            {
                                                a = Console.ReadLine();
                                                if (a == "1")
                                                {
                                                    f.Answer = mq.Answer;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            f.Answer = mq.Answer;
                                        }
                                    }
                                    if (mq.AnswerNumber != -1)
                                    {
                                        f.AnswerNumber = mq.AnswerNumber;
                                    }
                                });
                            }
                            else
                            {
                                MagiQuizController.Data.MQLT.Add(mq);
                            }
                        }
                        mq = new MagiQuiz();
                        mq.Qid = 0;
                        mq.Quiz = line.Replace("#", "");
                    }
                    else
                    {
                        if (line.Length == 0)
                        {

                        }
                        else
                        {
                            mq.Answer = line;
                        }
                        mq.type = 1;
                    }
                }
            }

            DataHandler.SaveMagiQuizData();

        }

        private static void st2()
        {
            const Int32 BufferSize = 128;
            MagiQuiz mq = new MagiQuiz();

            using (var fileStream = File.OpenRead("5.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.StartsWith("#"))
                    {
                        if (mq.Quiz != "")
                        {

                            List<MagiQuiz> mql = new List<MagiQuiz>();
                            mql = MagiQuizController.Data.MQLT.Where(w => w.Quiz == mq.Quiz).ToList();
                            if (mql != null & mql.Count > 0)
                            {
                                mql.ForEach(f =>
                                {
                                    ConsoleHandler.WriteLine(string.Format("A---------{0}=>{1}", f.Choose1, mq.Choose1));
                                    ConsoleHandler.WriteLine(string.Format("B---------{0}=>{1}", f.Choose2, mq.Choose2));
                                    ConsoleHandler.WriteLine(string.Format("C---------{0}=>{1}", f.Choose3, mq.Choose3));
                                    ConsoleHandler.WriteLine(string.Format("D---------{0}=>{1}", f.Choose4, mq.Choose4));
                                    ConsoleHandler.WriteLine(string.Format("Answer----{0}=>{1}", f.Answer, mq.Answer));
                                    string a = Console.ReadLine();
                                    if (a == "1")
                                    {
                                        if (mq.Choose1 != "")
                                        {
                                            f.Choose1 = mq.Choose1;
                                        }
                                        if (mq.Choose2 != "")
                                        {
                                            f.Choose2 = mq.Choose2;
                                        }
                                        if (mq.Choose3 != "")
                                        {
                                            f.Choose3 = mq.Choose3;
                                        }
                                        if (mq.Choose4 != "")
                                        {
                                            f.Choose4 = mq.Choose4;
                                        }
                                        if (mq.Answer != "")
                                        {
                                            f.Answer = mq.Answer;
                                        }
                                        if (mq.AnswerNumber != -1)
                                        {
                                            f.AnswerNumber = mq.AnswerNumber;
                                        }
                                    }
                                });
                            }
                            else
                            {
                                MagiQuizController.Data.MQLT.Add(mq);
                            }
                        }
                        mq = new MagiQuiz();
                        mq.Qid = 0;
                        mq.Quiz = line.Replace("#", "");
                    }
                    else
                    {
                        if (line.Length == 0)
                        {

                        }
                        else
                        {

                            List<string> tmp = new List<string>();
                            if (line.Length != 1)
                            {
                                tmp = line.Split(new string[] { "  " }, StringSplitOptions.None).ToList();
                                if (tmp.Count == 4)
                                {
                                    mq.TotalChoose = 4;
                                    mq.Choose1 = tmp[0].Replace("[#]", "");
                                    mq.Choose2 = tmp[1].Replace("[#]", "");
                                    mq.Choose3 = tmp[2].Replace("[#]", "");
                                    mq.Choose4 = tmp[3].Replace("[#]", "");
                                    //   mq.Answer = tmp.Where(w => w.Contains("[#]")).DefaultIfEmpty("").Last().Replace("[#]", "");

                                    //   mq.AnswerNumber = tmp.FindIndex(a => a.Contains("[#]"))+1;
                                }
                                else
                                {
                                    ConsoleHandler.WriteLine("N 4");
                                    string a = Console.ReadLine();
                                }
                            }
                        }
                        int an = -1;
                        Int32.TryParse(line, out an);
                        mq.AnswerNumber = an;

                        if (mq.AnswerNumber == 1)
                        {
                            mq.Answer = mq.Choose1;
                        }
                        if (mq.AnswerNumber == 2)
                        {
                            mq.Answer = mq.Choose2;
                        }
                        if (mq.AnswerNumber == 3)
                        {
                            mq.Answer = mq.Choose3;
                        }
                        if (mq.AnswerNumber == 4)
                        {
                            mq.Answer = mq.Choose4;
                        }


                        mq.type = 1;
                    }
                }

            }

            DataHandler.SaveMagiQuizData();

        }

        private static void st1()
        {
            const Int32 BufferSize = 128;
            MagiQuiz mq = new MagiQuiz();

            using (var fileStream = File.OpenRead("3.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.StartsWith("#"))
                    {
                        mq.Qid = 0;
                        mq.Quiz = line.Replace("#", "");
                    }
                    else
                    {
                        List<string> tmp = new List<string>();
                        tmp = line.Split(new string[] { "  " }, StringSplitOptions.None).ToList();
                        mq.TotalChoose = 4;
                        mq.Choose1 = tmp[0].Replace("[#]", "");
                        mq.Choose2 = tmp[1].Replace("[#]", "");
                        mq.Choose3 = tmp[2].Replace("[#]", "");
                        mq.Choose4 = tmp[3].Replace("[#]", "");
                        mq.Answer = tmp.Where(w => w.Contains("[#]")).DefaultIfEmpty("").Last().Replace("[#]", "");
                        mq.AnswerNumber = tmp.FindIndex(a => a.Contains("[#]")) + 1;
                        mq.type = 1;
                        List<MagiQuiz> mql = new List<MagiQuiz>();
                        mql = MagiQuizController.Data.MQLT.Where(w => w.Quiz == mq.Quiz).ToList();
                        if (mql != null & mql.Count > 0)
                        {
                            mql.ForEach(f =>
                            {
                                ConsoleHandler.WriteLine(string.Format("A---------{0}=>{1}", f.Choose1, mq.Choose1));
                                ConsoleHandler.WriteLine(string.Format("B---------{0}=>{1}", f.Choose2, mq.Choose2));
                                ConsoleHandler.WriteLine(string.Format("C---------{0}=>{1}", f.Choose3, mq.Choose3));
                                ConsoleHandler.WriteLine(string.Format("D---------{0}=>{1}", f.Choose4, mq.Choose4));
                                ConsoleHandler.WriteLine(string.Format("Answer----{0}=>{1}", f.Answer, mq.Answer));
                                string a = Console.ReadLine();
                                if (a == "1")
                                {
                                    if (mq.Choose1 != "")
                                    {
                                        f.Choose1 = mq.Choose1;
                                    }
                                    if (mq.Choose2 != "")
                                    {
                                        f.Choose2 = mq.Choose2;
                                    }
                                    if (mq.Choose3 != "")
                                    {
                                        f.Choose3 = mq.Choose3;
                                    }
                                    if (mq.Choose4 != "")
                                    {
                                        f.Choose4 = mq.Choose4;
                                    }
                                    if (mq.Answer != "")
                                    {
                                        f.Answer = mq.Answer;
                                    }
                                    if (mq.AnswerNumber != -1)
                                    {
                                        f.AnswerNumber = mq.AnswerNumber;
                                    }
                                }
                            });
                        }
                        else
                        {
                            MagiQuizController.Data.MQLT.Add(mq);
                        }
                        mq = new MagiQuiz();
                    }
                }

            }

            DataHandler.SaveMagiQuizData();

        }
        private static void rYamltolist()
        {
            StreamReader r = new StreamReader("quiz.yml");

            var deserializer = new Deserializer();
            var yamlObject = deserializer.Deserialize(r);
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            dictionary = (Dictionary<object, object>)yamlObject;
            Console.WriteLine(dictionary.First().Key);


            foreach (var d in dictionary)
            {
                MagiQuiz mq = new MagiQuiz();
                int qid = 0;
                Int32.TryParse(d.Key.ToString(), out qid);
                mq.Qid = qid;
                mq.source = QuizSource.bilibili;

                Dictionary<object, object> quizData = new Dictionary<object, object>();
                quizData = (Dictionary<object, object>)d.Value;

                string quizquestion = quizData["question"].ToString();

                mq.Quiz = quizquestion;

                List<object> quizanswer = new List<object>();
                quizanswer = (List<object>)quizData["answers"];

                mq.TotalChoose = 4;
                Dictionary<object, object> ans = new Dictionary<object, object>();
                ans = (Dictionary<object, object>)quizanswer[0];
                mq.Choose1 = ans["answer"].ToString();
                mq.Choose1_Hash = ans["hash"].ToString();

                ans = (Dictionary<object, object>)quizanswer[1];
                mq.Choose2 = ans["answer"].ToString();
                mq.Choose2_Hash = ans["hash"].ToString();

                ans = (Dictionary<object, object>)quizanswer[2];
                mq.Choose3 = ans["answer"].ToString();
                mq.Choose3_Hash = ans["hash"].ToString();

                ans = (Dictionary<object, object>)quizanswer[3];
                mq.Choose4 = ans["answer"].ToString();
                mq.Choose4_Hash = ans["hash"].ToString();


                if (quizData["type"] is string)
                {
                    int t = 100;
                    Int32.TryParse(quizData["type"].ToString(), out t);
                    mq.Cat = CCat(t);
                }
                else
                {
                    List<object> type = new List<object>();
                    type = (List<object>)quizData["type"];
                    int t = 100;
                    Int32.TryParse(type.First().ToString(), out t);
                    mq.Cat = CCat(t);

                    List<Category> c = new List<Category>();

                    type.Skip(1).ToList().ForEach(f =>
                    {
                        int t1 = 100;
                        Int32.TryParse(f.ToString(), out t1);
                        mq.Cat = CCat(t1);
                    });

                }

                mq.type = 1;
                //   mq.updated = DateTime.Now;

                //        mq.Quiz = quizData[0];
                List<MagiQuiz> mql = new List<MagiQuiz>();
                mql = MagiQuizController.Data.MQLT.Where(w => w.Quiz == mq.Quiz).ToList();
                string a;

                if (mql != null && mql.Count > 0)
                {
                    mql.ForEach(f =>
                    {
                        if (mq.Choose1 != "")
                        {
                            if (f.Choose1 != "")
                            {
                                if (f.Choose1 != mq.Choose1)
                                {
                                    ConsoleHandler.WriteLine(string.Format("A---------[{0}]=>[{1}]", f.Choose1, mq.Choose1));
                                    ConsoleHandler.WriteLine(string.Format("N---------[{0}]=>[{1}]", f.AnswerNumber, mq.AnswerNumber));
                                    a = Console.ReadLine();
                                    if (a == "1")
                                    {
                                        f.Choose1 = mq.Choose1;
                                    }
                                }

                            }
                            else
                            {
                                f.Choose1 = mq.Choose1;
                            }

                        }

                        if (mq.Choose2 != "")
                        {
                            if (f.Choose2 != "")
                            {
                                if (f.Choose2 != mq.Choose2)
                                {
                                    ConsoleHandler.WriteLine(string.Format("B---------[{0}]=>[{1}]", f.Choose2, mq.Choose2));
                                    ConsoleHandler.WriteLine(string.Format("N---------[{0}]=>[{1}]", f.AnswerNumber, mq.AnswerNumber));
                                    a = Console.ReadLine();
                                    if (a == "1")
                                    {
                                        f.Choose2 = mq.Choose2;
                                    }
                                }
                            }
                            else
                            {
                                f.Choose2 = mq.Choose2;
                            }
                        }

                        if (mq.Choose3 != "")
                        {
                            if (f.Choose3 != "")
                            {
                                if (f.Choose3 != mq.Choose3)
                                {
                                    ConsoleHandler.WriteLine(string.Format("C---------[{0}]=>[{1}]", f.Choose3, mq.Choose3));
                                    ConsoleHandler.WriteLine(string.Format("N---------[{0}]=>[{1}]", f.AnswerNumber, mq.AnswerNumber));
                                    a = Console.ReadLine();
                                    if (a == "1")
                                    {
                                        f.Choose3 = mq.Choose3;
                                    }
                                }
                            }
                            else
                            {
                                f.Choose3 = mq.Choose3;
                            }

                        }

                        if (mq.Choose4 != "")
                        {
                            if (f.Choose4 != mq.Choose4)
                            {
                                if (f.Choose4 != "")
                                {
                                    ConsoleHandler.WriteLine(string.Format("D---------[{0}]=>[{1}]", f.Choose4, mq.Choose4));
                                    ConsoleHandler.WriteLine(string.Format("N---------[{0}]=>[{1}]", f.AnswerNumber, mq.AnswerNumber));
                                    a = Console.ReadLine();
                                    if (a == "1")
                                    {
                                        f.Choose4 = mq.Choose4;
                                    }
                                }
                            }
                            else
                            {
                                f.Choose4 = mq.Choose4;
                            }

                        }
                        if (mq.Answer != "")
                        {
                            if (f.Answer != "")
                            {
                                ConsoleHandler.WriteLine(string.Format("Answer----[{0}]=>[{1}]", f.Answer, mq.Answer));
                                if (mq.Answer != f.Answer)
                                {
                                    a = Console.ReadLine();
                                    if (a == "1")
                                    {
                                        f.Answer = mq.Answer;
                                    }
                                }
                            }
                            else
                            {
                                f.Answer = mq.Answer;
                            }
                        }
                        if (mq.AnswerNumber != -1)
                        {
                            if (f.AnswerNumber != -1)
                            {
                                ConsoleHandler.WriteLine(string.Format("AnswerNum----[{0}]=>[{1}]", f.AnswerNumber, mq.AnswerNumber));
                                if (mq.AnswerNumber != f.AnswerNumber)
                                {
                                    a = Console.ReadLine();
                                    if (a == "1")
                                    {
                                        f.AnswerNumber = mq.AnswerNumber;
                                    }
                                }
                            }
                            else
                            {
                                f.AnswerNumber = mq.AnswerNumber;
                            }
                        }

                    });
                }
                else
                {
                    MagiQuizController.Data.MQLT.Add(mq);
                }
            }
            DataHandler.SaveMagiQuizData();

        }
        private static Category CCat(int type)
        {
            switch (type)
            {
                case 11:
                    return Category.anime_Voice;
                case 12:
                    return Category.anime_Project;
                case 13:
                    return Category.anime_Content;
                case 14:
                    return Category.music_Vocaloid;
                case 15:
                    return Category.music_Acg;
                case 16:
                    return Category.music_Real;
                case 17:
                    return Category.game_Acs;
                case 19:
                    return Category.game_Atf;
                case 21:
                    return Category.game_Sm;
                case 23:
                    return Category.game_Ms;
                case 25:
                    return Category.game_Mmo;
                case 27:
                    return Category.real_Military;
                case 28:
                    return Category.real_Technology;
                case 30:
                    return Category.real_Ss;
                case 31:
                    return Category.real_As;
                case 5:
                    return Category.mono_Kichiku;
                case 9:
                    return Category.drama;
                default:
                    return Category.na;
            }
        }

        private static void init()
        {
            ConsoleHandler.SetCloseEvent((eventType) =>
            {
                if (eventType == 2)
                {
                    DataHandler.SaveMagiQuizData();
                }
            });
        }
        private static void initlist()
        {
            DataHandler.LoadMagiQuizData();
            Console.Title = "magiQuiz";
            if (MagiQuizController.Data.MQL == null)
            {
                MagiQuizController.Data.MQL = new List<MagiQuiz>();
            }
            else
            {
                if (MagiQuizController.Data.MQL.Count > 0)
                {
                    start = MagiQuizController.Data.MQL.Max(m => m.Qid);
                }
            }
        }
        private static void loopGetAnswer()
        {
            while (true)
            {
                ConsoleHandler.WriteLine(@"Please input question or   "":q"" to end request ");
                //    string input = ClipboardHelper.GetText();
                //    if (input == null) { continue; }
                //    if (input == "A" | input == "B" | input == "C" | input == "D") { continue; }

                string input = Console.ReadLine();
                if (input != "")
                {
                    if (input == ":q")
                    {
                        break;
                    }
                    List<MagiQuiz> mql = new List<MagiQuiz>();
                    mql = MagiQuizController.Data.MQL.Where(w => w.Quiz.Contains(input)).ToList();
                    Console.Clear();
                    mql.ForEach(Data =>
                    {
                        List<string> result = new List<string>();

                        result.Add("Q:" + Data.Quiz);

                        result.Add("1:" + Data.Choose1);

                        result.Add("2:" + Data.Choose2);

                        result.Add("3:" + Data.Choose3);

                        result.Add("4:" + Data.Choose4);

                        result.Add("Cat : " + Data.Cat.GetDescription());
                        result.Add(String.Format("正确答案:[{0}]{1}", Data.AnswerNumber, Data.Answer));
                        result.Add("---------------");
                        result.Add(String.Format(DataConventer.Number2String(Data.AnswerNumber, true)));
                        result.Add("---------------");

                        ConsoleHandler.WriteMulitLine(result, ConsoleColor.Green);
                    });
                }
            }
        }
        private static void GetMagiData()
        {

            ConsoleHandler.WriteLine("Task : Get MagiQuiz data");

            for (int qid = start; qid < end; qid++)
            {
                Run(qid, end);
                Thread.Sleep(500);
            }
        }

        private static void Retry()
        {
            ConsoleHandler.WriteLine("Task : Retry");

            List<int> iqid = new List<int>();
            List<int> nqid = new List<int>();
            iqid = Enumerable.Range(1, end).ToList();
            nqid = iqid.Except(MagiQuizController.Data.MQL.Where(w => w.AnswerNumber != -1 && w.Answer != "" && w.TotalChoose != -1).Select(s => s.Qid)).ToList();

            for (int cqid = 1; cqid < nqid.Count; cqid++)
            {
                int qid = nqid[cqid];
                Run(qid, end);
                Thread.Sleep(500);
            }
        }

        private static void Run_cat()
        {

            foreach (Category cat in Enum.GetValues(typeof(Category)))
            {
                int c = (int)cat;
                if (c % 10 >= 1)
                {
                    continue;
                }

                bool finish = false;
                while (!finish)
                {
                    bool response = false;
                    QuizTask.GetMagiQuizID_cat(cat, (mq) =>
                    {
                        if (mq.Qid > 0)
                        {
                            RunSkip(mq);
                            lock (MagiQuizController.mqlock)
                            {
                                MagiQuizController.Data.MQLC.Add(new MagiQuizCat { Qid = mq.Qid, Cat = mq.Cat, Quiz = mq.Quiz });
                            }
                        }

                        if (mq.type == -3)
                        {
                            finish = true;
                        }
                        response = true;
                        DataHandler.SaveMagiQuizData();
                    });

                    DateTime timeout = DateTime.Now;

                    while (!response)
                    {
                        TimeSpan duration = DateTime.Now - timeout;
                        if (duration.Seconds > 10)
                        {
                            break;
                        }
                        Thread.Sleep(100);
                    }

                    Thread.Sleep(300);
                }

                ConsoleHandler.WriteLine(@"finish cat:" + cat.ToString());
            }

            ConsoleHandler.WriteLine(@"Fin");
            Console.ReadLine();
        }
        private static void maxtwolist()
        {
            lock (MagiQuizController.mqlock)
            {
                MagiQuizController.Data.MQL.ForEach(f =>
                {
                    MagiQuizCat c = new MagiQuizCat();
                    c = MagiQuizController.Data.MQLC.Where(w => w.Qid == f.Qid).DefaultIfEmpty(new MagiQuizCat { Qid = -1 }).ToList().Last();
                    if (c.Qid == -1)
                    {
                        f.Cat = Category.na;

                    }
                    else
                    {
                        if (f.Quiz != c.Quiz)
                        {
                            ConsoleHandler.WriteLine("MQL:" + f.Quiz);
                            ConsoleHandler.WriteLine("MQLC" + c.Quiz);
                            ConsoleHandler.ClearNext();
                        }
                        f.Cat = c.Cat;
                    }
                    if (f.Cat == Category.other_old)
                    {
                        f.Cat = Category.other;
                    }
                });
            }

            DataHandler.SaveMagiQuizData();
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.Cat == Category.other_old).ToList().Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.Cat == Category.other).ToList().Count.ToString());
            ConsoleHandler.WriteLine(MagiQuizController.Data.MQL.Where(w => w.Cat == Category.na).ToList().Count.ToString());
            ConsoleHandler.WriteLine(@"FinCom");
            Console.ReadLine();

        }

        private static void RunSkip(MagiQuiz mq)
        {
            QuizTask.SkipQuiz(mq.Qid, () =>
            {
                int line = Console.CursorTop;

                List<string> result = new List<string>();
                result.Add("QID:" + mq.Qid);
                result.Add("Quiz:" + mq.Quiz);
                lock (MagiQuizController.mqlock)
                {
                    result.Add("Total:" + MagiQuizController.Data.MQLC.Count.ToString());
                }
                ConsoleHandler.WriteLinePreLocation(result, ConsoleColor.Green);
            }, mq.Cat.ToString());
        }

        private static void Run(int qid, int end, string cat = "")
        {
            //   QuizTask.SkipQuiz(qid, () =>
            //   {
            QuizTask.GetMagiQuizData(qid, (Data) =>
                {
                    int line = Console.CursorTop;

                    List<string> result = new List<string>();

                    result.Add(String.Format("{0}/{1}", qid, end));

                    result.Add("Q:" + Data.Quiz);

                    result.Add("1:" + Data.Choose1);

                    result.Add("2:" + Data.Choose2);

                    result.Add("3:" + Data.Choose3);

                    result.Add("4:" + Data.Choose4);

                    result.Add(String.Format("正确答案:[{0}]{1}", Data.AnswerNumber, Data.Answer));

                    result.Add(String.Format("通过率:{0}% {1}/{2}", Data.PassrateData.Passrate_Percentage, Data.PassrateData.Passrate_Number, Data.PassrateData.Passrate_TotalTester));

                    result.Add(String.Format("[1]{0} %  {1}", Data.PassrateData.Passrate_Choose1_Percentage.ToString(), Data.PassrateData.Passrate_Choose1_Number));

                    result.Add(String.Format("[2]{0} %  {1}", Data.PassrateData.Passrate_Choose2_Percentage.ToString(), Data.PassrateData.Passrate_Choose2_Number));

                    result.Add(String.Format("[3]{0} %  {1}", Data.PassrateData.Passrate_Choose3_Percentage.ToString(), Data.PassrateData.Passrate_Choose3_Number));

                    result.Add(String.Format("[4]{0} %  {1}", Data.PassrateData.Passrate_Choose4_Percentage.ToString(), Data.PassrateData.Passrate_Choose4_Number));

                    result.Add(String.Format("[-]{0} %  {1}", Data.PassrateData.Passrate_ChooseSkip_Percentage.ToString(), Data.PassrateData.Passrate_ChooseSkip_Number));

                    result.Add("Creator:" + Data.Creator.CreatorUserName);

                        //  Console.SetCursorPosition(0, line);
                        ConsoleHandler.WriteLinePreLocation(result, ConsoleColor.Green);

                    lock (MagiQuizController.mqlock)
                    {
                        MagiQuizCat mqc = new MagiQuizCat();
                        mqc = MagiQuizController.Data.MQLC.Where(w => w.Qid == Data.Qid).ToList().DefaultIfEmpty(new MagiQuizCat { Cat = Category.na }).Last();
                        Data.Cat = mqc.Cat;
                        MagiQuizController.Data.MQL.Add(Data);
                    }
                });
            //   }, cat);
        }

    }
}
