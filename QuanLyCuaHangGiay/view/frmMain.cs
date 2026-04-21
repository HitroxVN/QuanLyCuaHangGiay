using QuanLyCuaHangGiay.controller;
using QuanLyCuaHangGiay.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangGiay.view
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void OpenForm(Form f)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.GetType() == f.GetType())
                {
                    frm.Activate();
                    return;
                }
            }

            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }

        private void nhậpKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmPhieuNhap());
        }

        private void xemTồnKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmKho());
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmNCC());
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new faccount());
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new fChangePasswords());
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthController auth = new AuthController();
            auth.logout();
            this.Hide();
            flogin f = new flogin();
            f.ShowDialog();
            this.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Session.user == null)
            {
                MessageBox.Show("Bạn chưa đăng nhập!");
                this.Close();
            }

            labelName.Text = Session.user.hoTen.ToString();

            // giới hạn quyền staff
            if (Session.user.quyen == "staff") { 
                //hệThốngToolStripMenuItem.DropDownItems.Remove(quảnLýTàiKhoảnToolStripMenuItem);
                hệThốngToolStripMenuItem.Enabled = false;
            }
        }

    }
}
