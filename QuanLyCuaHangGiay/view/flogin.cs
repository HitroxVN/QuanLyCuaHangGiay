using QuanLyCuaHangGiay.controller;
using QuanLyCuaHangGiay.model;
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
    public partial class flogin : Form
    {
        AuthController auth;
        public flogin()
        {
            InitializeComponent();
            auth = new AuthController();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Login: " + txtEmail.Text + "\nPassword: " + txtPassword.Text);
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            Users u = auth.login(email, password);

            if(u == null)
            {
                MessageBox.Show("Sai email hoặc mật khẩu!");
            } else
            {
                MessageBox.Show("Đăng nhập thành công!");

                // phân quyền form
                // test
                if(Session.user.quyen == "admin" || Session.user.quyen == "staff")
                {
                    frmMain f = new frmMain();
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                } else
                {
                    MessageBox.Show("Bạn chưa có quyền truy cập vào phần mềm!");
                }
            }
        }

        private void showPass_CheckedChanged(object sender, EventArgs e)
        {
            if (showPass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            } else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void dangKy_Click(object sender, EventArgs e)
        {
            this.Hide();
            fregister f = new fregister();
            f.ShowDialog();
            this.Show();
        }
    }
}
