using System;
using System.IO;
using System.Reflection;

namespace BlingIntegration
{
    public static class LogFile
    {
        private static string GetLogFile()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string file = Path.Combine(path, "Log");

            if (!File.Exists(file))
            {
                FileStream fileStream = File.Create(file);
                fileStream.Close();
            }

            return file;
        }

        public static void Log(string logMessage)
        {
            string file = GetLogFile();
            using (StreamWriter w = File.AppendText(file))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine("  :");
                w.WriteLine($"  :{logMessage}");
                w.WriteLine("-------------------------------");
            }
        }
    }
}
