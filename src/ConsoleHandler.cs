using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BGMmagiQuiz
{
    public static class ConsoleHandler
    {
        public static object Consolelock = new object();
        private static int lastline = 0;
        static ConsoleEventDelegate handler;
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

        public static void SetCloseEvent(Action<int> callback)
        {
            handler = new ConsoleEventDelegate((eventType)=> {
                callback(eventType);
                return true;
            });
            SetConsoleCtrlHandler(handler, true);
        }


        public static void WriteLine(string text, ConsoleColor cc = ConsoleColor.White)
        {
            lock (Consolelock)
            {
                ClearCurrentConsoleLine();
                ConsoleColor last = Console.ForegroundColor;
                Console.ForegroundColor = cc;
                Console.WriteLine(text);
                Console.ForegroundColor = last;
                lastline = Console.CursorTop > lastline ? Console.CursorTop : lastline;
            }
        }

        public static void WriteLinePreLocation(List<string> text, ConsoleColor cc = ConsoleColor.Black)
        {
            lock (Consolelock)
            {
                int c = Console.CursorTop;
                ConsoleColor last = Console.ForegroundColor;
                Console.ForegroundColor = cc;
                text.ForEach(f =>
                {
                    ClearCurrentConsoleLine();
                    Console.WriteLine(f);
                });
                Console.ForegroundColor = last;
                lastline = Console.CursorTop> lastline ? Console.CursorTop : lastline;
            //    ClearNextLine();
                Console.SetCursorPosition(0, c );
            }
        }

        public static void WriteMulitLine(List<string> text, ConsoleColor cc = ConsoleColor.Black)
        {
            lock (Consolelock)
            {
                ConsoleColor last = Console.ForegroundColor;
                Console.ForegroundColor = cc;
                text.ForEach(f =>
                {
                    ClearCurrentConsoleLine();
                    Console.WriteLine(f);
                });
                Console.ForegroundColor = last;
                lastline = Console.CursorTop > lastline ? Console.CursorTop : lastline;
            //    ClearNextLine();
            }
        }
        public static void SetCursorPositionLine(int lineno)
        {
            lock (Consolelock)
            {
                Console.SetCursorPosition(0, Console.CursorTop - lineno);
            }
        }


        public static void ClearNext()
        {
            ClearNextLine();
        }
        private static void ClearCurrentConsoleLine()
        {
            lock (Consolelock)
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }
        }

        private static void ClearNextLine()
        {
            lock (Consolelock)
            {
                int currentLineCursor = Console.CursorTop;
                int currentLine = Console.CursorTop;
                if (lastline > currentLine)
                {
                    for (int line = currentLine; line < lastline; line++)
                    {
                        Console.SetCursorPosition(0, line);
                        Console.Write(new string(' ', Console.WindowWidth));
                    }
                    Console.SetCursorPosition(0, currentLineCursor);
                }
            }
        }

    }
}
