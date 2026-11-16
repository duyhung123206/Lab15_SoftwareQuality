using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Lab15_SoftwareQuality.BUS;
using Lab15_SoftwareQuality.Validation;

namespace Lab15_SoftwareQuality
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoa != null)
                {
                    cboKhoa.Items.Clear();
                    cboKhoa.Items.Add("Công nghệ thông tin");
                    cboKhoa.Items.Add("Quản trị kinh doanh");
                    cboKhoa.Items.Add("Kế toán");

                    if (cboKhoa.Items.Count > 0)
                    {
                        cboKhoa.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khởi tạo Form1_Load", ex);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SinhVienDTO sv = new SinhVienDTO
                {
                    MaSV = txtMaSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim()
                };

                if (float.TryParse(txtDiemTB.Text.Trim(), out float diem))
                {
                    sv.DiemTB = diem;
                }
                else
                {
                    sv.DiemTB = -1;
                }

                SinhVienBUS bus = new SinhVienBUS();
                string errors;

                if (!bus.ThemMoi(sv, out errors))
                {
                    MessageBox.Show("Dữ liệu không hợp lệ:\n" + errors,
                                    "Lỗi Kiểm Tra Dữ Liệu",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Thêm sinh viên thành công!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi thực hiện thêm sinh viên", ex);
                MessageBox.Show("Đã xảy ra lỗi hệ thống! Chi tiết đã được ghi vào tệp log.",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private DataTable GetStudentData()
        {
            DataTable dt = new DataTable("SinhVien");
            dt.Columns.Add("MaSV", typeof(string));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("DiemTB", typeof(float));

            string maSV = string.IsNullOrEmpty(txtMaSV.Text) ? "24001296" : txtMaSV.Text.Trim();
            string hoTen = string.IsNullOrEmpty(txtHoTen.Text) ? "Nguyễn Duy Hưng" : txtHoTen.Text.Trim();
            float diemTB = float.TryParse(txtDiemTB.Text, out float d) ? d : 9.0f;

            dt.Rows.Add(maSV, hoTen, diemTB);
            return dt;
        }

        // Nút Xem báo cáo theo Khoa
        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rptStudent.rpt");

                if (!File.Exists(reportPath))
                {
                    MessageBox.Show("Không tìm thấy tệp báo cáo tại: " + reportPath, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ReportDocument report = new ReportDocument();
                report.Load(reportPath);

                // 1. Gán nguồn dữ liệu DataTable để tránh hỏi Database Login
                DataTable dt = GetStudentData();
                report.SetDataSource(dt);

                // 2. Truyền giá trị cho các tham số (Parameters) trong file .rpt để ẩn hộp thoại nhập tham số
                string tenKhoa = (cboKhoa != null && cboKhoa.SelectedItem != null) ? cboKhoa.SelectedItem.ToString() : "Công nghệ thông tin";
                string khoangDiem = string.IsNullOrEmpty(txtDiemTB.Text) ? "9" : txtDiemTB.Text.Trim();

                report.SetParameterValue("pTenKhoa", tenKhoa);
                report.SetParameterValue("pKhoangDiem", khoangDiem);

                // 3. Hiển thị lên Viewer
                crystalReportViewer1.ReportSource = report;
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi xem báo cáo theo Khoa", ex);
                MessageBox.Show("Không thể tải báo cáo. Chi tiết đã được ghi log.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Xem báo cáo theo Điểm
        private void btnXemTheoDiem_Click(object sender, EventArgs e)
        {
            try
            {
                string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rptStudent.rpt");

                if (!File.Exists(reportPath))
                {
                    MessageBox.Show("Không tìm thấy tệp báo cáo tại: " + reportPath, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ReportDocument report = new ReportDocument();
                report.Load(reportPath);

                DataTable dt = GetStudentData();
                report.SetDataSource(dt);

                // Truyền tham số tương tự cho nút xem theo điểm
                string tenKhoa = (cboKhoa != null && cboKhoa.SelectedItem != null) ? cboKhoa.SelectedItem.ToString() : "Công nghệ thông tin";
                string khoangDiem = string.IsNullOrEmpty(txtDiemTB.Text) ? "9" : txtDiemTB.Text.Trim();

                report.SetParameterValue("pTenKhoa", tenKhoa);
                report.SetParameterValue("pKhoangDiem", khoangDiem);

                crystalReportViewer1.ReportSource = report;
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi xem báo cáo theo Điểm", ex);
                MessageBox.Show("Không thể tải báo cáo. Chi tiết đã được ghi log.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Đang tiến hành xuất file Excel...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi xuất file Excel", ex);
                MessageBox.Show("Xuất file Excel thất bại. Chi tiết đã được ghi log.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}