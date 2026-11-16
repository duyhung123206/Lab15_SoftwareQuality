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
                // Tạo thư mục Logs nằm cùng thư mục chạy ứng dụng (bin/Debug hoặc bin/Release)
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                Directory.CreateDirectory(dir);

                // Tên file log theo ngày tháng (ví dụ: 20261116.log)
                string file = Path.Combine(dir, DateTime.Now.ToString("yyyyMMdd") + ".log");

                // Định dạng nội dung dòng log
                string line = string.Format("{0:yyyy-MM-dd HH:mm:ss} | ERROR | {1}\r\n{2}\r\n",
                    DateTime.Now, message, ex);

                // Ghi log an toàn bằng khóa luồng (thread-safe)
                lock (Sync)
                {
                    File.AppendAllText(file, line, Encoding.UTF8);
                }
            }
            catch
            {
                /* Bỏ qua ngoại lệ để không làm gián đoạn/dừng ứng dụng khi ghi log thất bại */
            }
        }
    }
}