using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlingIntegration
{
    public static class ExportData
    {
        public static void ExportCsv<T>(List<T> genericList, string fileName)
        {
            var sb = new StringBuilder();
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var finalPath = Path.Combine(basePath, fileName);
            var header = "";
            var info = typeof(T).GetProperties();

            if (!File.Exists(finalPath))
            {
                var file = File.Create(finalPath);
                file.Close();
                foreach (var prop in typeof(T).GetProperties())
                {
                    header += prop.Name + ";";
                }
                header = header.Substring(0, header.Length - 1);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }

            foreach (var obj in genericList)
            {
                sb = new StringBuilder();
                var line = "";
                foreach (var prop in info)
                {
                    line += FormattedValue(prop.GetValue(obj, null)) + ";";
                }
                line = line.Substring(0, line.Length - 1);
                sb.AppendLine(line);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }

            FTPUtils.SendFileFTP(finalPath);
        }

        private static string FormattedValue(object value)
        {
            if (value == null)
                return string.Empty;
            if (value.ToString().Contains("\n"))
                value = value.ToString().Replace("\n", "");
            if (value.ToString().Contains(";"))
                value = value.ToString().Replace(";", "|");
            DateTime fakeDate;
            if (DateTime.TryParse(value.ToString(), out fakeDate))
                return fakeDate.ToString("yyyy-MM-dd");

            return value.ToString();
        }
    }
}
