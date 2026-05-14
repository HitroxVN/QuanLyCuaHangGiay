using QuanLyCuaHangGiay.controller;
using QuanLyCuaHangGiay.Database;
using QuanLyCuaHangGiay.util;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyCuaHangGiay.view
{
    public partial class frmPhieuNhap : Form, util.IBaseForm
    {
        PhieuNhapController _controller = new PhieuNhapController();
        private DataTable _dtThongTinNhap;

        // Giả sử có biến cục bộ lưu ID nhân viên đang đăng nhập hệ thống
        //private int _nhanVienDangNhapID = 1;

        public frmPhieuNhap()
        {
            InitializeComponent();
            TaoBangTam();
        }

        private void TaoBangTam()
        {
            _dtThongTinNhap = new DataTable();
            _dtThongTinNhap.Columns.Add("Mã SP", typeof(int));
            _dtThongTinNhap.Columns.Add("Tên SP", typeof(string));
            _dtThongTinNhap.Columns.Add("Số Lượng", typeof(int));
            _dtThongTinNhap.Columns.Add("Đơn Giá", typeof(decimal));
            _dtThongTinNhap.Columns.Add("Thành Tiền", typeof(decimal));

            dgvDanhSachNhap.DataSource = _dtThongTinNhap;
        }

        private void ResetForm()
        {
            txtSoLuong.Clear();
            txtGiaNhap.Clear();
            txtGhiChu.Clear();
            cbSanPham.SelectedIndex = -1;
            cbNCC.SelectedIndex = -1;
        }
        private string DocSoThanhChu(long number)
        {
            string[] dv = { "", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] chuc = { "", "mười", "hai mươi", "ba mươi", "bốn mươi", "năm mươi", "sáu mươi", "bảy mươi", "tám mươi", "chín mươi" };

            if (number == 0) return "không đồng";

            string result = "";
            int i = 0;

            string[] donvi = { "", "nghìn", "triệu", "tỷ" };

            while (number > 0)
            {
                int n = (int)(number % 1000);
                if (n != 0)
                {
                    int tram = n / 100;
                    int ch = (n % 100) / 10;
                    int dvn = n % 10;

                    string temp = "";

                    if (tram > 0)
                        temp += dv[tram] + " trăm ";

                    if (ch > 1)
                    {
                        temp += chuc[ch] + " ";
                        if (dvn == 1) temp += "mốt ";
                        else if (dvn == 5) temp += "lăm ";
                        else if (dvn > 0) temp += dv[dvn] + " ";
                    }
                    else if (ch == 1)
                    {
                        temp += "mười ";
                        if (dvn == 5) temp += "lăm ";
                        else if (dvn > 0) temp += dv[dvn] + " ";
                    }
                    else if (ch == 0 && dvn > 0)
                    {
                        if (tram > 0) temp += "lẻ ";
                        temp += dv[dvn] + " ";
                    }

                    result = temp + donvi[i] + " " + result;
                }

                number /= 1000;
                i++;
            }

            return result.Trim() + " đồng";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSoLuong.Text) || string.IsNullOrWhiteSpace(txtGiaNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Số lượng và Giá nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên và lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtGiaNhap.Text, out decimal giaNhap) || giaNhap < 0)
            {
                MessageBox.Show("Giá nhập không đúng định dạng số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int maSp = Convert.ToInt32(cbSanPham.SelectedValue);
            string tenSp = cbSanPham.Text;

            bool daTonTai = false;
            foreach (DataRow row in _dtThongTinNhap.Rows)
            {
                if ((int)row["Mã SP"] == maSp)
                {
                    row["Số Lượng"] = (int)row["Số Lượng"] + soLuong;
                    row["Thành Tiền"] = (int)row["Số Lượng"] * (decimal)row["Đơn Giá"];

                    daTonTai = true;
                    TinhTongTien();
                    break;
                }
            }

            if (!daTonTai)
            {
                _dtThongTinNhap.Rows.Add(maSp, tenSp, soLuong, giaNhap, soLuong * giaNhap);
                TinhTongTien();
            }
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (_dtThongTinNhap == null || _dtThongTinNhap.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào trong phiếu để lưu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbNCC.SelectedValue == null || cbNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbNCC.Focus();
                return;
            }


            DateTime thoiGianLuu = DateTime.Now;
            int nccID = Convert.ToInt32(cbNCC.SelectedValue);

            // 1. Chuyển DataTable tạm thành List<Model>
            List<PhieuNhap> danhSachCanNhap = new List<PhieuNhap>();
            foreach (DataRow row in _dtThongTinNhap.Rows)
            {
                PhieuNhap pn = new PhieuNhap
                {
                    nhaCungCapID = nccID,
                    sanPhamID = (int)row["Mã SP"],
                    soLuong = (int)row["Số Lượng"],
                    giaNhap = (decimal)row["Đơn Giá"],
                    ghiChu = txtGhiChu.Text
                };
                danhSachCanNhap.Add(pn);
            }


            // 2. GỌI CONTROLLER KÈM THỜI GIAN LƯU
            string ketQua = _controller.LuuPhieuNhap(danhSachCanNhap, thoiGianLuu);

            if (ketQua == "Success")
            {
                // 3. HỎI NGƯỜI DÙNG CÓ MUỐN IN KHÔNG
                DialogResult result = MessageBox.Show(
                    "Nhập hàng và cập nhật kho thành công!\nBạn có muốn in phiếu nhập này không?",
                    "Thông báo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    // Lấy dữ liệu đầy đủ từ DB lên (Bao gồm tên NCC, Địa chỉ...) dựa vào thoiGianLuu
                    DataTable dtReport = _controller.GetPhieuNhapReport(thoiGianLuu, nccID);

                    if (dtReport != null && dtReport.Rows.Count > 0)
                    {
                        string nguoiTao = Session.user?.hoTen ?? "Người tạo";
                        frmReport frm = new frmReport("QuanLyCuaHangGiay.ReportPhieuNhap.rdlc", dtReport, nguoiTao);
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Không thể tải dữ liệu để in, có thể dữ liệu chưa được cập nhật kịp thời!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                _dtThongTinNhap.Clear();
                ResetForm();
                txtTong.Clear();
                lblThanhTien.Text = "";

                if (this.MdiParent is frmMain mainForm)
                {
                    mainForm.RefreshAllOpenForms();
                }
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ReloadData()
        {
            LoadAllCombobox();
        }

        private void frmPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadAllCombobox();
        }
        private void LoadAllCombobox()
        {
            try
            {
                // 1. Load Nhà Cung Cấp
                cbNCC.DataSource = _controller.LayNCC();
                cbNCC.DisplayMember = "tenNCC"; 
                cbNCC.ValueMember = "id";       
                cbNCC.SelectedIndex = -1;      

                // 2. Load Sản Phẩm
                cbSanPham.DataSource = _controller.LaySanPham();
                cbSanPham.DisplayMember = "tenSP";
                cbSanPham.ValueMember = "id";
                cbSanPham.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách: " + ex.Message);
            }
        }
        private void TinhTongTien()
        {
            decimal tong = 0;
            foreach (DataRow row in _dtThongTinNhap.Rows)
            {
                tong += Convert.ToDecimal(row["Thành Tiền"]);
            }
            txtTong.Text = string.Format("{0:N0} VNĐ", tong);

            string tienChu = DocSoThanhChu((long)tong);
            lblThanhTien.Text = char.ToUpper(tienChu[0]) + tienChu.Substring(1);
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachNhap.CurrentRow != null)
            {
                int rowIndex = dgvDanhSachNhap.CurrentRow.Index;
                _dtThongTinNhap.Rows.RemoveAt(rowIndex);

                TinhTongTien();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
