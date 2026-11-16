using System;
using System.IO;
using System.Windows.Forms;

namespace Lab15_SoftwareQuality
{
    public static class Logger
    {
        private static readonly string logDirectory = Path.Combine(Application.StartupPath, "Logs");
        private static readonly string logFile = Path.Combine(logDirectory, "app_log.txt");

        public static void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public static void LogError(string message, Exception ex = null)
        {
            string details = ex != null ? $" | Exception: {ex.Message} \nStackTrace: {ex.StackTrace}" : "";
            WriteLog("ERROR", message + details);
        }

        private static void WriteLog(string level, string message)
        {
            try
            {
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}{Environment.NewLine}{new string('-', 60)}{Environment.NewLine}";
                File.AppendAllText(logFile, logEntry);
            }
            catch
            {
                // Tránh lỗi ghi log làm sập ứng dụng chính
            }
        }
    }
}