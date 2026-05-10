using System;
using System.Windows.Forms;

namespace QuanLyCuaHangGiay.view
{
    public partial class frmThemKhachHang : Form
    {
        public string TenKhachHangMoi { get; set; }

        public frmThemKhachHang(string sdt)
        {
            InitializeComponent();
            txtSdt.Text = sdt; 
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKhach.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None; 
                return;
            }
            TenKhachHangMoi = txtTenKhach.Text.Trim();
        }
    }
}