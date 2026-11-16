using Lab15_SoftwareQuality.BUS;
using Lab15_SoftwareQuality.Validation;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab15_SoftwareQuality
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Sự kiện khi Form vừa mở: Nạp dữ liệu mẫu cho ComboBox
        private void Form1_Load(object sender, EventArgs e)
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

        // Nút Thêm / Kiểm tra dữ liệu (Validation)
        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        // Các nút xem báo cáo và xuất Excel
        private void btnXem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đang tải dữ liệu báo cáo theo Khoa...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXemTheoDiem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đang tải dữ liệu báo cáo theo Điểm...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đang tiến hành xuất file Excel...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}