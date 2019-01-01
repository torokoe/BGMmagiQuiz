using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz
{
    class DataHandler
    {

        public static void LoadMagiQuizData()
        {
            try
            {
                if (File.Exists(@"BGMmagiQuiz.json"))
                {
                    string readText = File.ReadAllText(@"BGMmagiQuiz.json");
                    List<MagiQuiz_Data> tmp = JsonConvert.DeserializeObject<List<MagiQuiz_Data>>(readText);
                    if (tmp !=null && tmp.Count > 0)
                    {
                        MagiQuizController.Data = tmp.FirstOrDefault();
                    }
                }
                else
                {
                    SaveMagiQuizData();
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        public static void SaveMagiQuizData()
        {
            try
            {
                lock (MagiQuizController.mqlock)
                {
                    List<MagiQuiz_Data> tmp = new List<MagiQuiz_Data>();
                    tmp.Add(MagiQuizController.Data);
                    var json = JsonConvert.SerializeObject(tmp);
                    File.WriteAllText(@"BGMmagiQuiz.json", json);
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

    }
}
