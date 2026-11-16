using System;
using System.IO;
using System.Text;

namespace Lab15_SoftwareQuality
{
    public static class Logger
    {
        private static readonly object Sync = new object();

        public static void Error(string message, Exception ex)
        {
            try
            {
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                Directory.CreateDirectory(dir);
                string file = Path.Combine(dir, DateTime.Now.ToString("yyyyMMdd") + ".log");
                string line = string.Format("{0:yyyy-MM-dd HH:mm:ss} | ERROR | {1}\r\n{2}\r\n",
                    DateTime.Now, message, ex);
                lock (Sync)
                {
                    File.AppendAllText(file, line, Encoding.UTF8);
                }
            }
            catch
            {
                /* Không để lỗi log làm dừng ứng dụng */
            }
        }
    }
}