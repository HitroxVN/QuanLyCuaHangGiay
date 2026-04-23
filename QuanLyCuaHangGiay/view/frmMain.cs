using System;
using System.Windows.Forms;

namespace QuanLyCuaHangGiay.view
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Nếu có tên người dùng đăng nhập thì gán vào đây
            // labelName.Text = "Tên người dùng";
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongKe f = new frmThongKe();
            f.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rs == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đổi mật khẩu chưa cài đặt.");
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng quản lý tài khoản chưa cài đặt.");
        }

        private void danhMụcSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng danh mục sản phẩm chưa cài đặt.");
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng sản phẩm chưa cài đặt.");
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng nhà cung cấp chưa cài đặt.");
        }

        private void nhậpKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng nhập kho chưa cài đặt.");
        }

        private void xemTồnKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xem tồn kho chưa cài đặt.");
        }

        private void tạoĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng tạo đơn hàng chưa cài đặt.");
        }

        private void danhSáchĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng danh sách đơn hàng chưa cài đặt.");
        }

        private void hướngDẫnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng hướng dẫn chưa cài đặt.");
        }

        private void giớiThiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Phần mềm quản lý cửa hàng giày.");
        }
    }
}