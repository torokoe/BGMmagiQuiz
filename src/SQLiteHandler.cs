using BGMmagiQuiz.DataBase;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz
{
    public static class SQLiteHandler
    {
        static SQLiteConnection db = new SQLiteConnection("Quiz.db");
        public static void create()
        {

            db.BeginTransaction();
            db.CreateTable<Quiz>();
            db.CreateTables<QuizAnswer, QuizAnswerMaster>();
            db.CreateTables<QuizChoose, QuizChooseMaster>();
            db.CreateTables<QuizCreator, QuizCreatorMaster>();
            db.CreateTables<QuizPassrateData, QuizPassrateDataMaster>();
            db.CreateTables<QuizPassrateDataAnalysis, QuizPassrateDataAnalysisMaster>();
            db.CreateTables<QuizQuestion, QuizQuestionMaster>();
            db.CreateTables<QuizSouceID, QuizSouceIDMaster>();
            db.CreateTables<QuizSubCat, QuizSubCatMaster>();
            db.Commit();

            int CurrentProcessing = 0;
            MagiQuizController.Data.MQL.ForEach(e =>
            {
                db.BeginTransaction();
                Quiz _Quiz = new Quiz();
                QuizAnswer _QuizAnswer = new QuizAnswer();
                QuizAnswerMaster _QuizAnswerMaster = new QuizAnswerMaster();
                QuizChoose _QuizChoose = new QuizChoose();
                QuizChooseMaster _QuizChooseMaster = new QuizChooseMaster();
                QuizCreator _QuizCreator = new QuizCreator();
                QuizCreatorMaster _QuizCreatorMaster = new QuizCreatorMaster();
                QuizPassrateData _QuizPassrateData = new QuizPassrateData();
                QuizPassrateDataMaster _QuizPassrateDataMaster = new QuizPassrateDataMaster();
                QuizPassrateDataAnalysis _QuizPassrateDataAnalysis = new QuizPassrateDataAnalysis();
                QuizPassrateDataAnalysisMaster _QuizPassrateDataAnalysisMaster = new QuizPassrateDataAnalysisMaster();
                QuizQuestion _QuizQuestion = new QuizQuestion();
                QuizQuestionMaster _QuizQuestionMaster = new QuizQuestionMaster();
                QuizSouceID _QuizSouceID = new QuizSouceID();
                QuizSouceIDMaster _QuizSouceIDMaster = new QuizSouceIDMaster();
                QuizSubCat _QuizSubCat = new QuizSubCat();
                QuizSubCatMaster _QuizSubCatMaster = new QuizSubCatMaster();

                _Quiz.SouceID = e.Qid;
                _Quiz.Question = e.Quiz;
                _Quiz.Updated = DateTime.Now;
                _Quiz.CreateDateTime = DateTime.Parse("2017/10/18 12:00:00");
                _Quiz.Source = e.source;
                _Quiz.Answer = e.Answer;
                _Quiz.AnswerNumber = e.AnswerNumber;
                _Quiz.Choose1 = e.Choose1;
                _Quiz.Choose2 = e.Choose2;
                _Quiz.Choose3 = e.Choose3;
                _Quiz.Choose4 = e.Choose4;
                _Quiz.TotalChoose = e.TotalChoose;
                _Quiz.Cat = e.Cat;
                _Quiz.Type = e.type;

                //
                int _Quiz_id;
                int _SouceIDMaster_id;
                int _SouceID_id;
                int _QuizAnswerMaster_id;
                int _QuizAnswer_id;
                int _QuizChooseMaster_id;
                //Master
                db.Insert(_Quiz);
                _Quiz_id = _Quiz.Quiz_id;

                //QID
                if (e.Qid_sub != null & e.Qid_sub.Count > 0)
                {
                    e.Qid_sub.ForEach(f =>
                    {
                        _QuizSouceIDMaster.SouceID = f;
                        db.Insert(_QuizSouceIDMaster);
                        _SouceIDMaster_id = _QuizSouceIDMaster.SouceIDMaster_id;
                        _QuizSouceID.SouceIDMaster_id = _SouceIDMaster_id;
                        _QuizSouceID.Quiz_id = _Quiz_id;
                        db.Insert(_QuizSouceID);
                        _SouceID_id = _QuizSouceID.SouceID_id;
                    });
                }

                _QuizSouceIDMaster.SouceID = e.Qid;
                db.Insert(_QuizSouceIDMaster);
                _SouceIDMaster_id = _QuizSouceIDMaster.SouceIDMaster_id;
                _QuizSouceID.SouceIDMaster_id = _SouceIDMaster_id;
                _QuizSouceID.Quiz_id = _Quiz_id;
                db.Insert(_QuizSouceID);
                _SouceID_id = _QuizSouceID.SouceID_id;


                //QuizAnswer
                QuizAnswerMaster tmp = db.Query<QuizAnswerMaster>($"SELECT QuizAnswerMaster WHERE Answer = {e.Answer}").ToList().DefaultIfEmpty().FirstOrDefault();

                if (tmp == null)
                {
                    _QuizAnswerMaster.Answer = e.Answer;
                    _QuizAnswerMaster.AnswerHash = e.Answer.GetHashCode().ToString("{2:X8}");
                    _QuizAnswerMaster_id = db.Insert(_QuizSouceID);
                }
                else
                {
                    _QuizAnswerMaster_id = tmp.AnswerMaster_id;
                }

                _QuizAnswer.AnswerMaster_id = _QuizAnswerMaster_id;
                _QuizAnswer.Priority = 0;
                _QuizAnswer.Quiz_id = _Quiz_id;
                _QuizAnswer_id = db.Insert(_QuizAnswer);

                //QuizChoose
                for (int count = 0; count < e.TotalChoose; count++)
                {
                    _QuizChooseMaster.Choose = (string)GetPropValue(e, $"Choose{count+1}");
                    _QuizChooseMaster.Choose_Hash = _QuizChooseMaster.Choose.GetHashCode().ToString("{2:X8}");
                    _QuizChooseMaster_id = db.Insert()
                }


                ConsoleHandler.WriteLine(CurrentProcessing.ToString() + " / " + MagiQuizController.Data.MQL.Count.ToString());
                ConsoleHandler.SetCursorPositionLine(1);

                CurrentProcessing++;
                db.Commit();
            });


        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

    }
}

