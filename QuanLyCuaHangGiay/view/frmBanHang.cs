using QuanLyCuaHangGiay.controller;
using QuanLyCuaHangGiay.model;
using QuanLyCuaHangGiay.util;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyCuaHangGiay.view
{
    public partial class frmBanHang : Form, util.IBaseForm
    {
        private int diemKhachHangHienTai = 0;
        private DataTable dtTatCaSanPham;
        private BindingList<ChiTietGioHang> gioHang = new BindingList<ChiTietGioHang>();
        private BanHangController banHangCtrl = new BanHangController();

        public frmBanHang()
        {
            InitializeComponent();
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            CauHinhBangGioHang();
            chkDungDiem.CheckedChanged += CapNhatTien_Event;
            LoadDanhSachSanPham();
            cboLoaiGiamGia.SelectedIndex = 0;
            txtGiamGia.TextChanged += CapNhatTien_Event;
            cboLoaiGiamGia.SelectedIndexChanged += CapNhatTien_Event;
            txtSdtKhach.TextChanged += txtSdtKhach_TextChanged;
            cboThanhToan.SelectedIndex = 0;
        }

        private void CauHinhBangGioHang()
        {
            dgvGioHang.AutoGenerateColumns = false;
            dgvGioHang.Columns.Clear();
            dgvGioHang.DataSource = gioHang;
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSanPham", HeaderText = "Mã SP", Width = 50, ReadOnly = true });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenSanPham", HeaderText = "Tên Giày", Width = 140, ReadOnly = true });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MauSac", HeaderText = "Màu", Width = 60, ReadOnly = true });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "KichCo", HeaderText = "Size", Width = 50, ReadOnly = true });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuongMua", HeaderText = "SL", Width = 40 });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn Giá", Width = 80, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GiamGia", HeaderText = "Giảm (-)", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThanhTien", HeaderText = "Thành Tiền", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            DataGridViewButtonColumn btnXoa = new DataGridViewButtonColumn();
            btnXoa.Name = "colXoa";
            btnXoa.HeaderText = "";
            btnXoa.Text = "Xóa";
            btnXoa.UseColumnTextForButtonValue = true;
            btnXoa.Width = 35;
            dgvGioHang.Columns.Add(btnXoa);
            dgvGioHang.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvGioHang.AllowUserToAddRows = false;
            dgvGioHang.BackgroundColor = Color.White;
            dgvGioHang.CellContentClick += DgvGioHang_CellContentClick;
            dgvGioHang.CellEndEdit += DgvGioHang_CellEndEdit;
        }

        private void DgvGioHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGioHang.Columns[e.ColumnIndex].Name == "colXoa" && e.RowIndex >= 0)
            {
                gioHang.RemoveAt(e.RowIndex);
                TinhToanTongTien();
            }
        }

        private void DgvGioHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvGioHang.Refresh();
            TinhToanTongTien();
        }

        public void ReloadData()
        {
            LoadDanhSachSanPham();
        }

        private void LoadDanhSachSanPham()
        {
            dtTatCaSanPham = banHangCtrl.LayDanhSachSanPhamBan();
            if (dtTatCaSanPham != null && dtTatCaSanPham.Rows.Count > 0)
            {
                var danhMucs = dtTatCaSanPham.AsEnumerable()
                                             .Select(r => r.Field<string>("DanhMuc"))
                                             .Where(d => !string.IsNullOrEmpty(d))
                                             .Distinct().ToList();
                danhMucs.Insert(0, "Tất cả");
                cboDanhMuc.DataSource = danhMucs;
                txtTimKiem.TextChanged -= TxtTimKiem_TextChanged;
                txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
                cboDanhMuc.SelectedIndexChanged -= CboDanhMuc_SelectedIndexChanged;
                cboDanhMuc.SelectedIndexChanged += CboDanhMuc_SelectedIndexChanged;
                txtMaSP.KeyDown -= TxtMaSP_KeyDown;
                txtMaSP.KeyDown += TxtMaSP_KeyDown;
            }
            ThucHienLocVaVeGiaoDien();
        }

        private void ThucHienLocVaVeGiaoDien()
        {
            flpSanPham.Controls.Clear();
            if (dtTatCaSanPham == null) return;
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();
            string danhMuc = cboDanhMuc.Text;
            var query = dtTatCaSanPham.AsEnumerable();
            if (!string.IsNullOrEmpty(tuKhoa))
                query = query.Where(r => r.Field<string>("TenSP").ToLower().Contains(tuKhoa));
            if (!string.IsNullOrEmpty(danhMuc) && danhMuc != "Tất cả")
                query = query.Where(r => r.Field<string>("DanhMuc") != null && r.Field<string>("DanhMuc") == danhMuc);
            if (!query.Any()) return;
            var nhomGiay = query.GroupBy(r => r.Field<string>("TenSP"));
            foreach (var nhom in nhomGiay)
            {
                string tenGiay = nhom.Key;
                string colAnh = nhom.First().Table.Columns.Contains("Anh") ? "Anh" : "anh";
                var dongCoAnh = nhom.FirstOrDefault(r => r[colAnh] != DBNull.Value && !string.IsNullOrWhiteSpace(r[colAnh].ToString()));
                string anhSP = dongCoAnh != null ? dongCoAnh[colAnh].ToString() : "";
                System.Diagnostics.Debug.WriteLine("Sản phẩm: " + tenGiay + ", Ảnh: " + anhSP);
                DataRow[] cacBienThe = nhom.ToArray();
                ucSanPham uc = new ucSanPham();
                uc.LoadData(tenGiay, anhSP, cacBienThe);
                uc.OnThemVaoGioHang += (maSanPhamChon) =>
                {
                    ThemVaoGioHangTrucTiep(maSanPhamChon);
                };
                flpSanPham.Controls.Add(uc);
            }
        }

        private void ThemVaoGioHangTrucTiep(int id)
        {
            DataRow[] kqTimKiem = dtTatCaSanPham.Select($"MaSP = {id}");
            if (kqTimKiem.Length > 0)
            {
                ThemSanPhamVaoGio(kqTimKiem[0]);
            }
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            ThucHienLocVaVeGiaoDien();
        }

        private void CboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThucHienLocVaVeGiaoDien();
        }

        private void TxtMaSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string maNhap = txtMaSP.Text.Trim();
                if (string.IsNullOrEmpty(maNhap)) return;
                DataRow[] kq = dtTatCaSanPham.Select($"MaSP = '{maNhap}'");
                if (kq.Length > 0)
                {
                    ThemSanPhamVaoGio(kq[0]);
                    txtMaSP.Clear();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã sản phẩm này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaSP.SelectAll();
                }
            }
        }

        private void TinhToanTongTien()
        {
            decimal tongTienHang = 0;
            foreach (ChiTietGioHang item in gioHang)
            {
                tongTienHang += item.ThanhTien;
            }
            decimal tienGiamGia = 0;
            decimal giaTriNhap = 0;
            decimal.TryParse(txtGiamGia.Text, out giaTriNhap);
            if (cboLoaiGiamGia.Text == "%")
            {
                if (giaTriNhap > 100) giaTriNhap = 100;
                tienGiamGia = tongTienHang * (giaTriNhap / 100);
            }
            else
            {
                if (giaTriNhap > tongTienHang) giaTriNhap = tongTienHang;
                tienGiamGia = giaTriNhap;
            }
            decimal tienTruTuDiem = 0;
            if (chkDungDiem.Checked == true)
            {
                tienTruTuDiem = diemKhachHangHienTai * 1000;
            }
            decimal tienThanhToanCuoiCung = tongTienHang - tienGiamGia - tienTruTuDiem;
            if (tienThanhToanCuoiCung < 0)
            {
                tienThanhToanCuoiCung = 0;
            }
            lblTongTien.Text = tienThanhToanCuoiCung.ToString("N0") + " VNĐ";
        }

        private void CapNhatTien_Event(object sender, EventArgs e)
        {
            TinhToanTongTien();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (gioHang.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống! Vui lòng chọn sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string ghiChuCuaKhach = txtGhiChu.Text.Trim();
            decimal tongGiaGoc = 0;
            foreach (var item in gioHang) tongGiaGoc += item.ThanhTien;
            decimal tongTienSauGiam = 0;
            string tienChu = lblTongTien.Text.Replace(",", "")
                                             .Replace(".", "")
                                             .Replace("VNĐ", "")
                                             .Trim();
            decimal.TryParse(tienChu, out tongTienSauGiam);
            decimal tienChietKhau = tongGiaGoc - tongTienSauGiam;
            string sdt = txtSdtKhach.Text.Trim();
            string pttt = cboThanhToan.Text;
            int diemDaDung = chkDungDiem.Checked ? diemKhachHangHienTai : 0;
            int taiKhoanID = Session.user.id;
            DialogResult hoiNhanh = MessageBox.Show($"Xác nhận thanh toán đơn hàng với số tiền: {lblTongTien.Text}?",
                                                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (hoiNhanh == DialogResult.Yes)
            {
                try
                {
                    int maDonHangVuaTao = banHangCtrl.ThanhToanDonHang(taiKhoanID, tongGiaGoc, tienChietKhau, tongTienSauGiam, sdt, pttt, ghiChuCuaKhach, diemDaDung, gioHang);
                    if (maDonHangVuaTao > 0)
                    {
                        MessageBox.Show("Thanh toán thành công! Đã lưu hóa đơn và trừ kho.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmHoaDon frmBill = new frmHoaDon(maDonHangVuaTao);
                        frmBill.ShowDialog();
                        gioHang.Clear();
                        txtSdtKhach.Clear();
                        txtGiamGia.Text = "0";
                        chkDungDiem.Checked = false;
                        LoadDanhSachSanPham();
                        if (this.MdiParent is frmMain mainForm)
                        {
                            mainForm.RefreshAllOpenForms();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ThemSanPhamVaoGio(DataRow row)
        {
            int maSP = Convert.ToInt32(row["MaSP"]);
            int tonKho = Convert.ToInt32(row["TonKho"]);
            if (tonKho <= 0)
            {
                MessageBox.Show("Sản phẩm này đã hết hàng trong kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool daCoTrongGio = false;
            foreach (ChiTietGioHang item in gioHang)
            {
                if (item.MaSanPham == maSP)
                {
                    if (item.SoLuongMua < tonKho)
                    {
                        item.SoLuongMua += 1;
                        daCoTrongGio = true;
                        dgvGioHang.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Trong kho chỉ còn " + tonKho + " đôi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        daCoTrongGio = true;
                    }
                    break;
                }
            }
            if (daCoTrongGio == false)
            {
                ChiTietGioHang monMoi = new ChiTietGioHang();
                monMoi.MaSanPham = maSP;
                monMoi.TenSanPham = row["TenSP"].ToString();
                monMoi.MauSac = row["MauSac"].ToString();
                monMoi.KichCo = row["KichCo"].ToString();
                monMoi.DonGia = Convert.ToDecimal(row["GiaBan"]);
                monMoi.SoLuongMua = 1;
                monMoi.GiamGia = 0;
                gioHang.Add(monMoi);
            }
            TinhToanTongTien();
        }

        private void txtSdtKhach_TextChanged(object sender, EventArgs e)
        {
            string sdt = txtSdtKhach.Text.Trim();
            chkDungDiem.Checked = false;
            diemKhachHangHienTai = 0;
            if (sdt.Length < 10)
            {
                lblTenKhachHang.Text = "Tên: (Khách lẻ)";
                lblTenKhachHang.ForeColor = Color.Gray;
                chkDungDiem.Visible = false;
                TinhToanTongTien();
                return;
            }
            DataTable dtKhach = banHangCtrl.TimKhachHangTheoSDT(sdt);
            if (dtKhach != null && dtKhach.Rows.Count > 0)
            {
                string tenKhach = dtKhach.Rows[0]["hoTen"].ToString();
                diemKhachHangHienTai = Convert.ToInt32(dtKhach.Rows[0]["diemTichLuy"]);
                lblTenKhachHang.Text = $"Tên: {tenKhach} - Điểm: {diemKhachHangHienTai}";
                lblTenKhachHang.ForeColor = Color.DarkGreen;
                chkDungDiem.Visible = true;
                chkDungDiem.Text = $"Dùng {diemKhachHangHienTai} điểm (-{diemKhachHangHienTai * 1000:N0} đ)";
            }
            else
            {
                lblTenKhachHang.Text = "Khách mới (Chưa có trong hệ thống)";
                lblTenKhachHang.ForeColor = Color.DarkRed;
                chkDungDiem.Visible = false;
                DialogResult hoi = MessageBox.Show("Khách hàng này chưa có trên hệ thống! Bạn có muốn tạo mới để tích điểm không?", "Khách Mới", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (hoi == DialogResult.Yes)
                {
                    frmThemKhachHang frmThem = new frmThemKhachHang(sdt);
                    if (frmThem.ShowDialog() == DialogResult.OK)
                    {
                        string tenMoi = frmThem.TenKhachHangMoi;
                        banHangCtrl.ThemKhachHangMoi(tenMoi, sdt);
                        MessageBox.Show($"Đã thêm thành công khách hàng: {tenMoi}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblTenKhachHang.Text = $"Tên: {tenMoi} - Điểm: 0";
                        lblTenKhachHang.ForeColor = Color.DarkGreen;
                        diemKhachHangHienTai = 0;
                    }
                }
            }
            TinhToanTongTien();
        }
    }
}