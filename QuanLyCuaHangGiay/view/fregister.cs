using QuanLyCuaHangGiay.controller;
using QuanLyCuaHangGiay.model;
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
    public partial class fregister : Form
    {
        AuthController auth;
        public fregister()
        {
            InitializeComponent();
            auth = new AuthController();
        }

        private void showPass_CheckedChanged(object sender, EventArgs e)
        {
            if (showPass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtRePassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                txtRePassword.UseSystemPasswordChar = true;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Users u = new Users
            {
                hoTen = txtHoTen.Text.Trim(),
                email = txtEmail.Text.Trim(),
                matKhau = txtPassword.Text.Trim(),
                sdt = txtSdt.Text.Trim(),
                diaChi = txtDiaChi.Text.Trim(),
            };

            if(txtPassword.Text != txtRePassword.Text)
            {
                MessageBox.Show("Mật khẩu không khớp!");
                return;
            }

            if (auth.register(u))
            {
                MessageBox.Show("Đăng ký thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại!");
            }
        }
    }
}
