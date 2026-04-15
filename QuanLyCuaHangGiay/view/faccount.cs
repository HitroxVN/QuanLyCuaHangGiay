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
    public partial class faccount : Form
    {
        UserController uctr;
        public faccount()
        {
            InitializeComponent();
            uctr = new UserController();
        }

        int currentId = -1;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string thongbao;

            Users u = new Users
            {
                hoTen = txtHoTen.Text.Trim(),
                email = txtEmail.Text.Trim(),
                matKhau = txtPassword.Text.Trim(),
                sdt = txtSdt.Text.Trim(),
                diaChi = txtDiaChi.Text.Trim(),
                quyen = cbQuyen.Text,
                ngayTao = DateTime.Now,
                trangThai = cbTrangThai.Text
            };

            if (uctr.addUser(u, out thongbao))
            {
                MessageBox.Show(thongbao);
                loadData();
                clearForm();
            }
            else
            {
                MessageBox.Show(thongbao);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string thongbao;
            Users u = new Users
            {
                id = currentId,
                hoTen = txtHoTen.Text.Trim(),
                sdt = txtSdt.Text.Trim(),
                diaChi = txtDiaChi.Text.Trim(),
                quyen = cbQuyen.Text,
                trangThai = cbTrangThai.Text
            };

            if (uctr.updateUser(u, out thongbao))
            {
                MessageBox.Show("Cập nhật thành công!");
                loadData();
                clearForm();
            }
            else
            {
                MessageBox.Show(thongbao);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentId == -1)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!");
                return;
            }
            DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo);

            if (rs == DialogResult.Yes)
            {
                if (uctr.deleteUser(currentId))
                {
                    MessageBox.Show("Xóa thành công!");
                    loadData();
                    clearForm();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
            clearForm();
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

            if (row.Cells["id"].Value != null)
            {
                currentId = Convert.ToInt32(row.Cells["id"].Value);
            }

            txtHoTen.Text = row.Cells["hoTen"].Value.ToString();
            txtEmail.Text = row.Cells["email"].Value.ToString();
            txtSdt.Text = row.Cells["sdt"].Value.ToString();
            txtDiaChi.Text = row.Cells["diaChi"].Value.ToString();
            cbQuyen.Text = row.Cells["quyen"].Value.ToString();
            cbTrangThai.Text = row.Cells["trangThai"].Value.ToString();

            txtEmail.Enabled = false;
            txtPassword.Enabled = false;
        }

        private void loadData()
        {
            dgvUsers.DataSource = uctr.getAllUsers();
            dgvUsers.Columns["matKhau"].Visible = false;    // Ẩn cột mật khẩu
            txtEmail.Enabled = true;
            txtPassword.Enabled = true;
        }

        private void clearForm()
        {
            txtHoTen.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtSdt.Clear();
            txtDiaChi.Clear();
            cbQuyen.SelectedIndex = 0;
            cbTrangThai.SelectedIndex = 0;

            currentId = -1;
        }

        private void faccount_Load(object sender, EventArgs e)
        {
            cbQuyen.DataSource = new List<string> { "user", "staff", "admin" };
            cbTrangThai.DataSource = new List<string> { "active", "banned" };
            cbLocTheoQuyen.DataSource = new List<string> { "all", "user", "staff", "admin" };

            loadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string k = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(k))
            {
                loadData();
            }
            else
            {
                dgvUsers.DataSource = uctr.searchUsers(k);
            }
        }

        private void cbLocTheoQuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            string quyen = cbLocTheoQuyen.SelectedItem.ToString();
            if(quyen == "all")
            {
                loadData();
            } else
            {
                dgvUsers.DataSource = uctr.filterUsersByRole(quyen);
            }
        }
    }
}
