using QuanLyCuaHangGiay.view;
using shoe_store.controller; // Sếp kiểm tra lại namespace controller của mình nhé
using System;
using System.Data;
using System.Windows.Forms;

namespace shoe_store.view
{
    public partial class frmQuanLyDonHang : Form
    {
        // Khai báo lớp điều khiển nghiệp vụ
        private DonHangController dhController = new DonHangController();

        public frmQuanLyDonHang()
        {
            InitializeComponent();

            // 1. Gán sự kiện Load Form
            this.Load += frmQuanLyDonHang_Load;

            // 2. Gán sự kiện Tìm kiếm Real-time (Thay đổi là lọc ngay)
            this.txtTuKhoa.TextChanged += TieuChiLoc_Changed;
            this.dtpTuNgay.ValueChanged += TieuChiLoc_Changed;
            this.dtpDenNgay.ValueChanged += TieuChiLoc_Changed;

            // 3. Gán sự kiện cho các nút chức năng
            this.btnXemHoaDon.Click += btnXemHoaDon_Click;
            this.btnHuyDon.Click += btnHuyDon_Click;
            this.btnXoaDon.Click += btnXoaDon_Click;

            // 4. Double click vào bảng để xem bill nhanh
            this.dgvDonHang.CellDoubleClick += dgvDonHang_CellDoubleClick;
            this.chkLocTheoNgay.CheckedChanged += new EventHandler(chkLocTheoNgay_CheckedChanged);
        }

        private void frmQuanLyDonHang_Load(object sender, EventArgs e)
        {
            // Mặc định hiển thị đơn hàng trong tháng hiện tại khi vừa mở form
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            LoadDanhSach();
        }

        // ====================================================================
        // HÀM LOAD DỮ LIỆU & BẮT LỖI NGÀY THÁNG
        // ====================================================================
        private void LoadDanhSach()
        {
            DateTime? tuNgay = null;
            DateTime? denNgay = null;

            // NẾU TÍCH CHECKBOX -> Mới gán ngày để lọc
            if (chkLocTheoNgay.Checked)
            {
                if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Cảnh báo");
                    dtpTuNgay.Value = dtpDenNgay.Value;
                    return;
                }
                tuNgay = dtpTuNgay.Value;
                denNgay = dtpDenNgay.Value;
            }

            try
            {
                dgvDonHang.DataSource = dhController.GetDanhSachDonHang(tuNgay, denNgay, txtTuKhoa.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }
        private void TieuChiLoc_Changed(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        // ====================================================================
        // XỬ LÝ XEM HÓA ĐƠN (CHI TIẾT)
        // ====================================================================
        private void MoChiTietHoaDon()
        {
            if (dgvDonHang.CurrentRow != null)
            {
                // Lấy ID đơn hàng từ cột đầu tiên trên Grid (Mã HĐ)
                int maHD = Convert.ToInt32(dgvDonHang.CurrentRow.Cells[0].Value);

                // Khởi tạo form Hóa Đơn với mã đơn hàng tương ứng
                frmHoaDon bill = new frmHoaDon(maHD);
                bill.ShowDialog();
            }
        }

        private void btnXemHoaDon_Click(object sender, EventArgs e)
        {
            MoChiTietHoaDon();
        }

        private void dgvDonHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) MoChiTietHoaDon();
        }

        // ====================================================================
        // XỬ LÝ HỦY ĐƠN (XÓA MỀM)
        // ====================================================================
        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow == null) return;

            int maHD = Convert.ToInt32(dgvDonHang.CurrentRow.Cells[0].Value);
            string trangThai = dgvDonHang.CurrentRow.Cells["Trạng thái"].Value.ToString();

            if (trangThai == "Đã hủy")
            {
                MessageBox.Show("Đơn hàng này đã ở trạng thái hủy!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show($"Xác nhận HỦY đơn hàng #{maHD}?\nGiày sẽ được cộng lại vào kho.",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    if (dhController.HuyDonHang(maHD))
                    {
                        MessageBox.Show("Đã hủy đơn hàng thành công!", "Thành công");
                        LoadDanhSach(); // Refresh lại danh sách
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi");
                }
            }
        }

        // ====================================================================
        // XỬ LÝ XÓA VĨNH VIỄN (XÓA CỨNG)
        // ====================================================================
        private void btnXoaDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow == null) return;

            int maHD = Convert.ToInt32(dgvDonHang.CurrentRow.Cells[0].Value);

            DialogResult dr = MessageBox.Show($"CẢNH BÁO: Bạn có chắc chắn muốn XÓA VĨNH VIỄN đơn #{maHD}?\nDữ liệu sẽ mất hoàn toàn khỏi hệ thống!",
                "Xác nhận xóa sạch", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    if (dhController.XoaVinhVienDonHang(maHD))
                    {
                        MessageBox.Show("Đã xóa vĩnh viễn đơn hàng!", "Thành công");
                        LoadDanhSach();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi");
                }
            }
        }
        private void chkLocTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpTuNgay.Enabled = chkLocTheoNgay.Checked;
            dtpDenNgay.Enabled = chkLocTheoNgay.Checked;
            LoadDanhSach();
        }
        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            LoadDanhSach(); // Thay đổi 'Từ ngày' là tự động lọc lại
        }
        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            LoadDanhSach(); // Thay đổi 'Đến ngày' là tự động lọc lại
        }
    }
}