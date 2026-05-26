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
    public partial class frmNCC : Form
    {
        NhaCungCapController _controller = new NhaCungCapController();
        int selectedID = -1;
        public frmNCC()
        {
            InitializeComponent();
        }

        private void frmNCC_Load(object sender, EventArgs e)
        {
            LoadData();

            cbTrangThai.Items.Add("active");
            cbTrangThai.Items.Add("inactive");
            cbTrangThai.SelectedIndex = 0;
            cbTrangThai.Text = "";

            if (Session.user != null && Session.user.quyen == "staff")
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                txtTen.Enabled = false;
                txtEmail.Enabled = false;
                txtDiaChi.Enabled = false;
                txtSDT.Enabled = false;
                cbTrangThai.Enabled = false;
            }
        }

        private void LoadData()
        {
            dgvNCC.DataSource = null;
            dgvNCC.DataSource = _controller.getAll();

            dgvNCC.Columns["id"].Visible = false;

            dgvNCC.Columns["tenNCC"].HeaderText = "Tên NCC";
            dgvNCC.Columns["email"].HeaderText = "Email";
            dgvNCC.Columns["diaChi"].HeaderText = "Địa chỉ";
            dgvNCC.Columns["sdt"].HeaderText = "SĐT";
            dgvNCC.Columns["trangthai"].HeaderText = "Trạng thái";
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvNCC.Rows[e.RowIndex];

            selectedID = Convert.ToInt32(row.Cells["id"].Value);

            txtTen.Text = row.Cells["tenNCC"].Value.ToString();
            txtEmail.Text = row.Cells["email"].Value.ToString();
            txtDiaChi.Text = row.Cells["diaChi"].Value.ToString();
            txtSDT.Text = row.Cells["sdt"].Value.ToString();
            cbTrangThai.Text = row.Cells["trangthai"].Value.ToString();
            btnThem.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            NhaCungCap ncc = new NhaCungCap
            {
                tenNCC = txtTen.Text,
                email = txtEmail.Text,
                diaChi = txtDiaChi.Text,
                sdt = txtSDT.Text,
                trangthai = cbTrangThai.Text
            };

            string msg;
            if (_controller.add(ncc, out msg))
            {
                MessageBox.Show(msg);
                LoadData();
                ResetForm();
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedID == -1)
            {
                MessageBox.Show("Chọn dòng cần sửa!");
                return;
            }

            NhaCungCap ncc = new NhaCungCap
            {
                id = selectedID,
                tenNCC = txtTen.Text,
                email = txtEmail.Text,
                diaChi = txtDiaChi.Text,
                sdt = txtSDT.Text,
                trangthai = cbTrangThai.Text
            };

            string msg;
            if (_controller.update(ncc, out msg))
            {
                MessageBox.Show(msg);
                LoadData();
                ResetForm();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedID == -1)
            {
                MessageBox.Show("Chọn dòng cần xóa!");
                return;
            }

            if (MessageBox.Show("Xác nhận xóa?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string msg;
                if (_controller.delete(selectedID, out msg))
                {
                    MessageBox.Show(msg);
                    LoadData();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ResetForm()
        {
            txtTen.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();

            cbTrangThai.SelectedIndex = 0;
            cbTrangThai.Text = "";

            selectedID = -1;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
            LoadData();
            btnThem.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvNCC.DataSource = _controller.search(txtSearch.Text);
        }
    }
}
