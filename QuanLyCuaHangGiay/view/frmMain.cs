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
    }
}
