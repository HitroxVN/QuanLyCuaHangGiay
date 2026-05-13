using QuanLyCuaHangGiay.controller;
using QuanLyCuaHangGiay.util;
using QuanLyCuaHangGiay.view;
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
            //this.IsMdiContainer = true;
        }

        private void OpenForm(Form f)
        {
            //foreach (Form frm in this.MdiChildren)
            //{
            //    if (frm.GetType() == f.GetType())
            //    {
            //        frm.Activate();
            //        if (frm is util.IBaseForm refreshable)
            //        {
            //            refreshable.ReloadData();
            //        }
            //        return;
            //    }
            //}

            //f.MdiParent = this;
            //f.WindowState = FormWindowState.Maximized;
            //f.Show();

            //this.Hide();

            //f.StartPosition = FormStartPosition.CenterScreen;

            //f.FormClosed += (s, args) =>
            //{
            //    this.Show();
            //    f.Dispose(); 
            //};

            //f.Show();

            this.Hide();
            f.WindowState = FormWindowState.Maximized;
            f.StartPosition = FormStartPosition.CenterScreen;

            f.ShowDialog();

            this.Show(); 
            f.Dispose();
        }

        public void RefreshAllOpenForms()
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is util.IBaseForm refreshable)
                {
                    refreshable.ReloadData();
                }
            }
        }

        private void nhậpKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmPhieuNhap());
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
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                AuthController auth = new AuthController();
                auth.logout();
                this.Hide();
                flogin f = new flogin();
                f.ShowDialog();
                this.Close();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!Authorization.Logged())
            {
                MessageBox.Show("Bạn chưa đăng nhập!");
                this.Close();
            }
            foreach (Control ctl in this.Controls)
            {
                if (ctl is MdiClient)
                {
                    ctl.BackColor = Color.White;
                }
            }
            //labelName.Text = Session.user.hoTen.ToString();

            // giới hạn quyền staff (CHỈ UI)
            if (Authorization.IsStaff())
            {
                //hệThốngToolStripMenuItem.DropDownItems.Remove(quảnLýTàiKhoảnToolStripMenuItem);
                quảnLýTàiKhoảnToolStripMenuItem.Enabled = false;
            }
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmThongKe());
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new F_Product());
        }

        private void danhMụcSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new F_Category());
        }

        private void tạoĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmBanHang());
        }

        private void danhSáchĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmQuanLyDonHang());
        }

        private void danhSáchKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmQuanLyKhachHang());
        }

        private void xemTồnKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmKho());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenForm(new frmBanHang());
        }
    }
}
