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
    public partial class fChangePasswords : Form
    {
        private UserController uctr;
        public fChangePasswords()
        {
            InitializeComponent();
            uctr = new UserController();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            string oldpass = txtOldPass.Text.Trim();
            string newpass = txtNewPass.Text.Trim();

            string thongbao;

            bool result = uctr.changePassword(Session.user.id, newpass, oldpass, out thongbao);

            if (result)
            {
                MessageBox.Show(thongbao);
                this.Close();
            }
            else
            {
                MessageBox.Show(thongbao);
            }
        }

        private void fChangePasswords_Load(object sender, EventArgs e)
        {
            labelName.Text = Session.user.hoTen.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtNewPass.UseSystemPasswordChar = false;
                txtOldPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtNewPass.UseSystemPasswordChar = true;
                txtOldPass.UseSystemPasswordChar = true;
            }
        }
    }
}
