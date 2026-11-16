using System;
using System.Threading;
using System.Windows.Forms;

namespace Lab15_SoftwareQuality
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Đăng ký bắt lỗi trên luồng giao diện UI
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Đăng ký bắt lỗi toàn cục AppDomain (Non-UI Thread)
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.Run(new Form1());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                Logger.Error("Lỗi UI Thread chưa được xử lý", e.Exception);
                MessageBox.Show("Đã xảy ra lỗi hệ thống không mong muốn. Ứng dụng sẽ đóng an toàn.",
                                "Lỗi Hệ Thống",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            catch { }
            finally
            {
                Application.Exit();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = e.ExceptionObject as Exception;
                Logger.Error("Lỗi nghiêm trọng toàn cục AppDomain", ex);
                MessageBox.Show("Ứng dụng gặp lỗi nghiêm trọng và cần phải kết thúc.",
                                "Lỗi Nghiêm Trọng",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            catch { }
            finally
            {
                Environment.Exit(1);
            }
        }
    }
}