using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;

namespace BlingIntegration
{
    public static class FTPUtils
    {
        private static FtpWebRequest CreateFtpWebRequest(string fileName, string method)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri("ftp://173.230.138.236/extract_files" + @"/" + fileName));
            string user = ConfigurationManager.AppSettings["FTPUser"];
            string password = ConfigurationManager.AppSettings["FTPPassword"];
            request.Credentials = new NetworkCredential(user, password);
            request.Method = method;
            return request;
        }

        private static void MoveFileToDoneDirectory(string filePath)
        {
            string[] filePathArray = filePath.Split('\\');
            string fileName = filePathArray[filePathArray.Length - 1];
            string destinationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Done\\" + fileName;
            File.Move(filePath, destinationPath);
        }

        public static void DeleteFileFTP(string filePath)
        {
            int fileNameLength = filePath.Split('\\').Length;
            string fileName = filePath.Split('\\')[fileNameLength - 1];
            FtpWebRequest request = CreateFtpWebRequest(fileName, WebRequestMethods.Ftp.DeleteFile);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }

        public static void SendFileFTP(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                int fileNameLength = filePath.Split('\\').Length;
                string fileName = filePath.Split('\\')[fileNameLength - 1];
                FtpWebRequest request = CreateFtpWebRequest(fileName, WebRequestMethods.Ftp.UploadFile);
                request.UseBinary = true;
                request.ContentLength = fileInfo.Length;

                using (FileStream fs = fileInfo.OpenRead())
                {
                    byte[] buffer = new byte[2048];
                    int bytesSent = 0;
                    int bytes = 0;
                    using (Stream stream = request.GetRequestStream())
                    {
                        while (bytesSent < fileInfo.Length)
                        {
                            bytes = fs.Read(buffer, 0, buffer.Length);
                            stream.Write(buffer, 0, bytes);
                            bytesSent += bytes;
                        }
                    }
                }

                MoveFileToDoneDirectory(filePath);
            }
            catch (Exception ex)
            {
                LogFile.Log("FTP ERROR: " + ex.Message);
            }
        }

        public static void CreateDirectory(string directoryName)
        {
            FtpWebRequest request = CreateFtpWebRequest(directoryName, WebRequestMethods.Ftp.MakeDirectory);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
        }
    }
}
