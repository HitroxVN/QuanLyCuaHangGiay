using QuanLyCuaHangGiay.controller;
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
    public partial class frmKho : Form, util.IBaseForm
    {
        private KhoController _controller;
        private DataTable _dtTonKho;
        public frmKho()
        {
            InitializeComponent();
            _controller = new KhoController();
        }

        private void frmKho_Load(object sender, EventArgs e)
        {
            LoadComboboxKho();
            LoadDataTonKho("Tất cả");
        }

        private void LoadComboboxKho()
        {
            DataTable dtKho = _controller.LayDanhSachKho();

            // Thêm mục "Tất cả" lên đầu danh sách để người dùng dễ lọc
            DataRow dr = dtKho.NewRow();
            dr["tenKho"] = "Tất cả";
            dtKho.Rows.InsertAt(dr, 0);

            cbLocKho.DataSource = dtKho;
            cbLocKho.DisplayMember = "tenKho";
            cbLocKho.ValueMember = "tenKho";
            cbLocKho.SelectedIndex = 0; 
        }

        public void ReloadData()
        {
            LoadDataTonKho("Tất cả");
        }

        // 2. Tải dữ liệu lên lưới (Có tham số tên kho)
        private void LoadDataTonKho(string tenKhoLoc)
        {
            try
            {
                _dtTonKho = _controller.LayChiTietTonKho(tenKhoLoc);
                dgvKho.DataSource = _dtTonKho;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu tồn kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbLocKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tránh lỗi null khi form vừa khởi tạo chưa kịp gán DataSource
            if (cbLocKho.SelectedValue != null && cbLocKho.SelectedValue is string)
            {
                string khoDuocChon = cbLocKho.SelectedValue.ToString();
                LoadDataTonKho(khoDuocChon);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_dtTonKho != null)
            {
                // Dùng RowFilter để lọc trực tiếp trên DataTable thông qua DataView
                DataView dv = _dtTonKho.DefaultView;
                string tuKhoa = txtSearch.Text.Trim().Replace("'", "''"); 

                dv.RowFilter = $"[Tên Sản Phẩm] LIKE '%{tuKhoa}%'";
            }
        }

        private void dgvKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKho.Rows[e.RowIndex];
                txtIdKho.Text = row.Cells["Mã Dòng"].Value.ToString();

                
                txtDiaChi.Text = row.Cells["Hà Nội"].Value != DBNull.Value ? row.Cells["Hà Nội"].Value.ToString() : "";
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdKho.Text))
            {
                MessageBox.Show("Vui lòng chọn một dòng sản phẩm để cập nhật địa chỉ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idKho = Convert.ToInt32(txtIdKho.Text);
            string viTriMoi = txtDiaChi.Text.Trim();

            string ketQua = _controller.CapNhatDiaChi(idKho, viTriMoi);

            if (ketQua == "Success")
            {
                MessageBox.Show("Cập nhật địa chỉ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Load lại dữ liệu lưới với bộ lọc hiện tại để thấy sự thay đổi
                LoadDataTonKho(cbLocKho.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
