using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace Lab15_SoftwareQuality
{
    public partial class Form1 : Form
    {
        private ReportDocument _report;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachKhoa();
                Logger.LogInfo("Khởi động ứng dụng thành công.");
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi tải Form1", ex);
            }
        }

        private void LoadDanhSachKhoa()
        {
            DataTable dtKhoa = new DataTable("KhoaTable");
            dtKhoa.Columns.Add("MaKhoa", typeof(string));
            dtKhoa.Columns.Add("TenKhoa", typeof(string));

            dtKhoa.Rows.Add("KH01", "Công nghệ thông tin");
            dtKhoa.Rows.Add("KH02", "Quản trị kinh doanh");
            dtKhoa.Rows.Add("KH03", "Kế toán kiểm toán");

            cboKhoa.DataSource = dtKhoa;
            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "MaKhoa";
        }

        // 1. Xem báo cáo theo Khoa
        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoa.SelectedValue == null)
                {
                    throw new BusinessException("Vui lòng chọn khoa trước khi xem báo cáo!");
                }

                string maKhoa = cboKhoa.SelectedValue.ToString();
                string tenKhoa = cboKhoa.Text;

                DataTable data = new DataTable("StudentTable");
                data.Columns.Add("MaSV", typeof(string));
                data.Columns.Add("HoTen", typeof(string));
                data.Columns.Add("MaKhoa", typeof(string));
                data.Columns.Add("TenKhoa", typeof(string));
                data.Columns.Add("DiemTB", typeof(double));

                data.Rows.Add("SV01", "Nguyễn Duy Hưng", "KH01", "Công nghệ thông tin", 8.5);
                data.Rows.Add("SV02", "Trần Văn An", "KH01", "Công nghệ thông tin", 9.2);
                data.Rows.Add("SV03", "Lê Thị Hồng", "KH02", "Quản trị kinh doanh", 7.4);
                data.Rows.Add("SV04", "Phạm Hoàng Long", "KH03", "Kế toán kiểm toán", 6.8);

                DataTable dataFiltered = data.Clone();
                var rows = data.AsEnumerable().Where(r => r.Field<string>("MaKhoa") == maKhoa);
                if (rows.Any())
                {
                    dataFiltered = rows.CopyToDataTable();
                }

                _report = new rptStudent();
                _report.SetDataSource(dataFiltered);

                _report.SetParameterValue("pTenKhoa", tenKhoa);
                _report.SetParameterValue("pKhoangDiem", "Tất cả khoảng điểm");
                _report.SetParameterValue("pNgayLap", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                crystalReportViewer1.ReportSource = _report;
                Logger.LogInfo($"Xem báo cáo thành công cho khoa: {tenKhoa}");
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Nghiệp Vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.LogInfo($"Cảnh báo nghiệp vụ (Khoa): {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogError("Lỗi hệ thống khi xem báo cáo theo khoa", ex);
            }
        }

        // 2. Xem báo cáo theo khoảng điểm Min / Max
        private void btnXemTheoDiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtMin.Text, out decimal min) || !decimal.TryParse(txtMax.Text, out decimal max))
                {
                    throw new BusinessException("Vui lòng nhập định dạng số hợp lệ cho điểm Min và Max!");
                }

                if (min < 0 || max > 10 || min > max)
                {
                    throw new BusinessException("Điểm trung bình phải thuộc [0,10].");
                }

                DataTable data = new DataTable("StudentTable");
                data.Columns.Add("MaSV", typeof(string));
                data.Columns.Add("HoTen", typeof(string));
                data.Columns.Add("MaKhoa", typeof(string));
                data.Columns.Add("TenKhoa", typeof(string));
                data.Columns.Add("DiemTB", typeof(double));

                data.Rows.Add("SV01", "Nguyễn Duy Hưng", "KH01", "Công nghệ thông tin", 8.5);
                data.Rows.Add("SV02", "Trần Văn An", "KH01", "Công nghệ thông tin", 9.2);
                data.Rows.Add("SV03", "Lê Thị Hồng", "KH02", "Quản trị kinh doanh", 7.4);
                data.Rows.Add("SV04", "Phạm Hoàng Long", "KH03", "Kế toán kiểm toán", 6.8);

                DataTable dataFiltered = data.Clone();
                var rows = data.AsEnumerable().Where(r => r.Field<double>("DiemTB") >= (double)min && r.Field<double>("DiemTB") <= (double)max);
                if (rows.Any())
                {
                    dataFiltered = rows.CopyToDataTable();
                }

                _report = new rptStudent();
                _report.SetDataSource(dataFiltered);

                _report.SetParameterValue("pKhoangDiem", $"Khoảng điểm từ {min} đến {max}");
                _report.SetParameterValue("pTenKhoa", "BÁO CÁO THEO KHOẢNG ĐIỂM");
                _report.SetParameterValue("pNgayLap", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                crystalReportViewer1.ReportSource = _report;
                Logger.LogInfo($"Xem báo cáo theo khoảng điểm [{min} - {max}] thành công.");
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Nghiệp Vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.LogInfo($"Cảnh báo nghiệp vụ (Điểm): {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogError("Lỗi hệ thống khi xem báo cáo theo điểm", ex);
            }
        }

        // 3. Xuất file Excel Workbook
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_report == null)
                {
                    throw new BusinessException("Vui lòng xem báo cáo trước khi xuất file Excel!");
                }

                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "Excel Workbook|*.xls";
                    dlg.FileName = "BaoCaoSinhVien.xls";

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        _report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook, dlg.FileName);
                        MessageBox.Show("Đã xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Logger.LogInfo($"Đã xuất file Excel thành công tại: {dlg.FileName}");
                    }
                }
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Nghiệp Vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.LogInfo($"Cảnh báo nghiệp vụ (Xuất Excel): {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file Excel: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogError("Lỗi hệ thống khi xuất file Excel", ex);
            }
        }
    }
}