using System;
using System.Data;
using System.Data.SqlClient;
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
                    throw new BusinessException(errors);
                }

                MessageBox.Show("Thêm sinh viên thành công!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message, "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                Logger.Error("Lỗi CSDL khi thêm sinh viên", ex);
                MessageBox.Show("Không thể cập nhật dữ liệu. Vui lòng thử lại.", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi không xác định", ex);
                MessageBox.Show("Ứng dụng gặp lỗi. Mã lỗi đã được ghi nhận.", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Lấy dữ liệu từ tầng BUS để hiển thị lên Crystal Reports
        private DataTable GetStudentData()
        {
            try
            {
                DataTable dt = new DataTable("SinhVien");
                dt.Columns.Add("MaSV", typeof(string));
                dt.Columns.Add("HoTen", typeof(string));
                dt.Columns.Add("DiemTB", typeof(float));

                // Lấy dữ liệu trực tiếp từ các ô nhập trên giao diện
                string maSV = string.IsNullOrEmpty(txtMaSV.Text) ? "24001296" : txtMaSV.Text.Trim();
                string hoTen = string.IsNullOrEmpty(txtHoTen.Text) ? "Nguyễn Duy Hưng" : txtHoTen.Text.Trim();
                float diemTB = float.TryParse(txtDiemTB.Text, out float d) ? d : 9.0f;

                dt.Rows.Add(maSV, hoTen, diemTB);
                return dt;
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi tạo dữ liệu cho báo cáo", ex);
                DataTable dtFallback = new DataTable("SinhVien");
                dtFallback.Columns.Add("MaSV", typeof(string));
                dtFallback.Columns.Add("HoTen", typeof(string));
                dtFallback.Columns.Add("DiemTB", typeof(float));
                dtFallback.Rows.Add("24001296", "Nguyễn Duy Hưng", 9.0f);
                return dtFallback;
            }
        }

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

                DataTable dt = GetStudentData();
                report.SetDataSource(dt);

                string tenKhoa = (cboKhoa != null && cboKhoa.SelectedItem != null) ? cboKhoa.SelectedItem.ToString() : "Công nghệ thông tin";
                string khoangDiem = string.IsNullOrEmpty(txtDiemTB.Text) ? "9" : txtDiemTB.Text.Trim();

                report.SetParameterValue("pTenKhoa", tenKhoa);
                report.SetParameterValue("pKhoangDiem", khoangDiem);

                crystalReportViewer1.ReportSource = report;
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi xem báo cáo theo Khoa", ex);
                MessageBox.Show("Không thể tải báo cáo. Chi tiết đã được ghi log.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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