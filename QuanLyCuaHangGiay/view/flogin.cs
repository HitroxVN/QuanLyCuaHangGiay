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
                Session.user = u; // lưu session

                MessageBox.Show("Đăng nhập thành công!");

                // phân quyền form
                // test
                if(u.quyen == "admin")
                {
                    // gọi form admin
                } else if(Session.user.quyen == "staff")
                {
                    // form staff
                } else
                {
                    // form user
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
