using QuanLyCuaHangGiay.controller;
using QuanLyCuaHangGiay.Database;
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
    public partial class frmPhieuNhap : Form
    {
        PhieuNhapController _controller = new PhieuNhapController();

        int selectedID = -1;
        bool isLoading = true;
        public frmPhieuNhap()
        {
            InitializeComponent();
        }

        private void frmKho_Load(object sender, EventArgs e)
        {
            isLoading = true;
            
            LoadDanhMuc();
            LoadComboBox();
            LoadData();
            cbDanhMuc.SelectedIndex = -1;
            cbNCC.SelectedIndex = -1;
            cbSanPham.DataSource = null;

            txtMau.Clear();
            txtSize.Clear();
            isLoading = false;
        }

        private void LoadData()
        {
            dgvDanhSach.DataSource = _controller.GetAll();
            dgvDanhSach.Columns["sanphamID"].Visible = false;
            dgvDanhSach.Columns["nhacungcapID"].Visible = false;
            dgvDanhSach.Columns["danhmucID"].Visible = false;
            TinhTien1Dong();
        }

        private void LoadDanhMuc()
        {
            cbDanhMuc.DataSource = _controller.GetDanhMuc();
            cbDanhMuc.DisplayMember = "tenDanhMuc";
            cbDanhMuc.ValueMember = "id";
        }

        private void LoadSanPhamByDanhMuc(int danhMucID)
        {
            cbSanPham.DataSource = _controller.GetSanPhamByDanhMuc(danhMucID);
            cbSanPham.DisplayMember = "tenSP";
            cbSanPham.ValueMember = "id";
            cbSanPham.SelectedIndex = -1;
        }

        private void LoadComboBox()
        {

            cbNCC.DataSource = _controller.GetNhaCungCap();
            cbNCC.DisplayMember = "tenNCC";
            cbNCC.ValueMember = "id";
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            dgvDanhSach.DataSource = _controller.Filter(dtFrom.Value, dtTo.Value);
        }

        private void ResetForm()
        {
            isLoading = true;

            txtSoLuong.Clear();
            txtGiaNhap.Clear();
            txtGhiChu.Clear();

            cbDanhMuc.SelectedIndex = -1;

            cbSanPham.DataSource = null;
            cbSanPham.Text = "";

            cbNCC.SelectedIndex = -1;

            txtMau.Clear();
            txtSize.Clear();

            selectedID = -1;

            isLoading = false;
        }
        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            btnHoanThanh.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            if (selectedID == -1)
            {
                MessageBox.Show("Vui lòng chọn dòng để sửa!");
                return;
            }

            try
            {
                int spID = Convert.ToInt32(cbSanPham.SelectedValue);
                int nccID = Convert.ToInt32(cbNCC.SelectedValue);
                int soLuong = int.Parse(txtSoLuong.Text);
                decimal gia = decimal.Parse(txtGiaNhap.Text);
                string ghiChu = txtGhiChu.Text;
                bool result = _controller.Update(selectedID, spID, nccID, soLuong, gia, ghiChu);

                if (result)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                    TinhTien1Dong();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedID == -1)
            {
                MessageBox.Show("Vui lòng chọn dòng!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool result = _controller.Delete(selectedID);

                if (result)
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                    TinhTien1Dong();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            try
            {
                int spID = Convert.ToInt32(cbSanPham.SelectedValue);
                int nccID = Convert.ToInt32(cbNCC.SelectedValue);
                int soLuong = int.Parse(txtSoLuong.Text);
                decimal gia = decimal.Parse(txtGiaNhap.Text);
                string ghiChu = txtGhiChu.Text;

                int userID = 1; // sau này dùng Session

                bool result = _controller.Insert(spID, nccID, soLuong, gia, userID, ghiChu);

                if (result)
                {
                    MessageBox.Show("Nhập kho thành công!");
                    LoadData();
                    TinhTien1Dong();

                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;

            btnHoanThanh.Enabled = false;
            DataGridViewRow row = dgvDanhSach.Rows[e.RowIndex];

            if (row.Cells["id"].Value == null || row.Cells["id"].Value == DBNull.Value)
                return;

            isLoading = true;

            selectedID = Convert.ToInt32(row.Cells["id"].Value);

            txtSoLuong.Text = row.Cells["soLuong"].Value.ToString();
            txtGiaNhap.Text = row.Cells["giaDonNhap"].Value.ToString();
            txtGhiChu.Text = row.Cells["ghiChu"].Value?.ToString();

            int danhMucID = Convert.ToInt32(row.Cells["danhmucID"].Value);
            cbDanhMuc.SelectedValue = danhMucID;

            LoadSanPhamByDanhMuc(danhMucID);

            int spID = Convert.ToInt32(row.Cells["sanphamID"].Value);
            cbSanPham.SelectedValue = spID;
            cbNCC.Text = row.Cells["tenNCC"].Value.ToString();
            txtSize.Text = row.Cells["kichco"].Value.ToString();
            txtMau.Text = row.Cells["mau"].Value.ToString();

            isLoading = false;

            TinhTien1Dong();
        }

        private void cbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (cbDanhMuc.SelectedValue != null)
            {
                int danhMucID = Convert.ToInt32(cbDanhMuc.SelectedValue);

                cbSanPham.DataSource = _controller.GetSanPhamByDanhMuc(danhMucID);
                cbSanPham.DisplayMember = "tenSP";
                cbSanPham.ValueMember = "id";
            }
        }

        private void cbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (cbSanPham.SelectedItem is DataRowView row)
            {
                txtMau.Text = row["mau"].ToString();
                txtSize.Text = row["kichco"].ToString();
            }
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

        private void TinhTien1Dong()
        {
            if (selectedID == -1) return;

            int sl = int.Parse(txtSoLuong.Text);
            decimal gia = decimal.Parse(txtGiaNhap.Text);

            decimal tongTien = sl * gia;

            txtTong.Text = tongTien.ToString("#,##0");

            string tienChu = DocSoThanhChu((long)tongTien);
            lblThanhTien.Text = char.ToUpper(tienChu[0]) + tienChu.Substring(1);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDanhSach.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn phiếu!");
                    return;
                }

                DateTime time = Convert.ToDateTime(dgvDanhSach.CurrentRow.Cells["thoiGian"].Value);
                int nccID = Convert.ToInt32(dgvDanhSach.CurrentRow.Cells["nhacungcapID"].Value);

                DataTable dt = _controller.GetPhieuNhapReport(time, nccID);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để in!");
                    return;
                }

                frmReport frm = new frmReport(
                    "QuanLyCuaHangGiay.ReportPhieuNhap.rdlc",
                    dt
                );

                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in phiếu: " + ex.Message);
            }
        }
    }
}
