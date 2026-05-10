using System;
using System.Data;
using System.Windows.Forms;
using shoe_store.controller;

namespace shoe_store.view
{
    public partial class frmQuanLyKhachHang : Form
    {
        private KhachHangController khController = new KhachHangController();
        private int selectedKhachHangID = -1;

        public frmQuanLyKhachHang()
        {
            InitializeComponent();
            this.Load += FrmQuanLyKhachHang_Load;
            this.Enter += (s, e) => {
                BtnLamMoi_Click(null, null);
            };
            this.txtTimKiem.TextChanged += TieuChiLoc_Changed;
            this.dgvKhachHang.CellClick += DgvKhachHang_CellClick;
            this.btnSua.Click += BtnSua_Click;
            this.btnXoa.Click += BtnXoa_Click;
            this.btnLamMoi.Click += BtnLamMoi_Click;
        }

        private void FrmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void LoadDanhSach()
        {
            try
            {
                DataTable dt = khController.GetDanhSachKhachHang(txtTimKiem.Text);
                dgvKhachHang.DataSource = dt;
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu khách hàng: " + ex.Message, "Lỗi hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TieuChiLoc_Changed(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void DgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                selectedKhachHangID = Convert.ToInt32(row.Cells["Mã KH"].Value);
                txtHoTen.Text = row.Cells["Họ tên"].Value.ToString();
                txtSdt.Text = row.Cells["Số điện thoại"].Value.ToString();
            }
        }

        private void ClearInputs()
        {
            selectedKhachHangID = -1;
            txtHoTen.Clear();
            txtSdt.Clear();
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            txtTimKiem.Clear();
            txtHoTen.Focus();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (selectedKhachHangID == -1)
            {
                MessageBox.Show("Vui lòng click chọn một khách hàng từ danh sách bên phải để sửa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSdt.Text.Trim();

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Họ tên và Số điện thoại không được để trống!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (khController.UpdateKhachHang(selectedKhachHangID, hoTen, sdt))
                {
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thành công");
                    LoadDanhSach();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi xử lý");
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (selectedKhachHangID == -1)
            {
                MessageBox.Show("Vui lòng click chọn một khách hàng để xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hoTen = txtHoTen.Text;

            DialogResult dr = MessageBox.Show($"Xác nhận xóa khách hàng [{hoTen}] khỏi danh sách?\n\n(Lưu ý: Hệ thống áp dụng cơ chế xóa an toàn. Lịch sử các hóa đơn cũ của khách này vẫn được giữ nguyên vẹn để tính doanh thu).",
                "Xác nhận xóa an toàn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    if (khController.DeleteKhachHang(selectedKhachHangID))
                    {
                        MessageBox.Show("Đã xóa khách hàng thành công!", "Thành công");
                        LoadDanhSach();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa khách hàng: " + ex.Message, "Lỗi hệ thống");
                }
            }
        }
    }
}