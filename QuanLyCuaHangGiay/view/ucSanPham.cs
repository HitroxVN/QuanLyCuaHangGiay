using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyCuaHangGiay.view
{
    public partial class ucSanPham : UserControl
    {
        public event Action<int> OnThemVaoGioHang;
        private DataRow[] dsBienThe;
        private string cMau, cSize, cTon, cGia, cID;

        public ucSanPham()
        {
            InitializeComponent();
            cboMau.SelectedIndexChanged -= cboMau_SelectedIndexChanged;
            cboSize.SelectedIndexChanged -= cboSize_SelectedIndexChanged;
            btnReset.Click -= btnReset_Click;
            btnThem.Click -= btnThem_Click;
            cboMau.SelectedIndexChanged += cboMau_SelectedIndexChanged;
            cboSize.SelectedIndexChanged += cboSize_SelectedIndexChanged;
            btnReset.Click += btnReset_Click;
            btnThem.Click += btnThem_Click;
        }

        public void LoadData(string tenSanPham, string anh, DataRow[] cacBienThe)
        {
            lblTenSP.Text = tenSanPham;
            LoadAnhVaoKhung(anh);
            dsBienThe = cacBienThe;
            if (dsBienThe == null || dsBienThe.Length == 0) return;
            var cols = dsBienThe[0].Table.Columns;
            cMau = cols.Contains("MauSac") ? "MauSac" : "mau";
            cSize = cols.Contains("KichCo") ? "KichCo" : "kichco";
            cTon = cols.Contains("TonKho") ? "TonKho" : "soLuong";
            cGia = cols.Contains("GiaBan") ? "GiaBan" : "gia";
            cID = cols.Contains("id") ? "id" : (cols.Contains("sanphamID") ? "sanphamID" : "MaSP");
            ResetVeTrangThaiBanDau();
        }

        private void ResetVeTrangThaiBanDau()
        {
            if (dsBienThe == null || dsBienThe.Length == 0) return;
            cboMau.SelectedIndexChanged -= cboMau_SelectedIndexChanged;
            cboSize.SelectedIndexChanged -= cboSize_SelectedIndexChanged;
            var mauList = dsBienThe.Where(r => Convert.ToInt32(r[cTon]) > 0)
                                   .Select(r => r[cMau].ToString()).Distinct().ToArray();
            cboMau.Items.Clear();
            cboMau.Items.AddRange(mauList);
            if (cboMau.Items.Count > 0)
            {
                cboMau.SelectedIndex = 0;
            }
            cboMau.SelectedIndexChanged += cboMau_SelectedIndexChanged;
            cboSize.SelectedIndexChanged += cboSize_SelectedIndexChanged;
            if (cboMau.Items.Count > 0)
            {
                LocThongTinBienThe("MAU");
            }
            else
            {
                lblTonKho.Text = "Hết hàng";
                btnThem.Enabled = false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetVeTrangThaiBanDau();
        }

        private void LocThongTinBienThe(string loaiVuaChon)
        {
            if (dsBienThe == null) return;
            if (loaiVuaChon == "MAU" && cboMau.SelectedItem != null)
            {
                string mauChon = cboMau.Text;
                var sizeList = dsBienThe.Where(r => r[cMau].ToString() == mauChon && Convert.ToInt32(r[cTon]) > 0)
                                        .Select(r => r[cSize].ToString()).Distinct().ToArray();
                cboSize.SelectedIndexChanged -= cboSize_SelectedIndexChanged;
                cboSize.Items.Clear();
                cboSize.Items.AddRange(sizeList);
                if (cboSize.Items.Count > 0) cboSize.SelectedIndex = 0;
                cboSize.SelectedIndexChanged += cboSize_SelectedIndexChanged;
            }
            else if (loaiVuaChon == "SIZE" && cboSize.SelectedItem != null)
            {
                string sizeChon = cboSize.Text;
                var mauList = dsBienThe.Where(r => r[cSize].ToString() == sizeChon && Convert.ToInt32(r[cTon]) > 0)
                                       .Select(r => r[cMau].ToString()).Distinct().ToArray();
                cboMau.SelectedIndexChanged -= cboMau_SelectedIndexChanged;
                cboMau.Items.Clear();
                cboMau.Items.AddRange(mauList);
                if (cboMau.Items.Count > 0) cboMau.SelectedIndex = 0;
                cboMau.SelectedIndexChanged += cboMau_SelectedIndexChanged;
            }
            CapNhatThongTinPhanLoai();
        }

        private void CapNhatThongTinPhanLoai()
        {
            if (cboMau.SelectedItem == null || cboSize.SelectedItem == null || dsBienThe == null) return;
            string mauChon = cboMau.Text;
            string sizeChon = cboSize.Text;
            var r = dsBienThe.FirstOrDefault(x => x[cMau].ToString() == mauChon && x[cSize].ToString() == sizeChon);
            if (r != null)
            {
                int soLuongTon = Convert.ToInt32(r[cTon]);
                lblGia.Text = string.Format("{0:N0} đ", r[cGia]);
                lblTonKho.Text = "Tồn: " + soLuongTon;
                lblTonKho.ForeColor = soLuongTon > 0 ? Color.DarkGreen : Color.Red;
                string cAnh = r.Table.Columns.Contains("Anh") ? "Anh" : "anh";
                string linkAnh = r[cAnh] != DBNull.Value ? r[cAnh].ToString() : "";
                LoadAnhVaoKhung(linkAnh);
                if (soLuongTon > 0)
                {
                    btnThem.Enabled = true; btnThem.BackColor = Color.Brown;
                    btnThem.Tag = Convert.ToInt32(r[cID]);
                }
                else
                {
                    btnThem.Enabled = false; btnThem.BackColor = Color.Gray;
                }
            }
        }

        private void cboMau_SelectedIndexChanged(object sender, EventArgs e) => LocThongTinBienThe("MAU");
        private void cboSize_SelectedIndexChanged(object sender, EventArgs e) => LocThongTinBienThe("SIZE");

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (btnThem.Tag != null)
            {
                int idSanPham = Convert.ToInt32(btnThem.Tag);
                OnThemVaoGioHang?.Invoke(idSanPham);
            }
        }

        private void LoadAnhVaoKhung(string tenFile)
        {
            if (string.IsNullOrEmpty(tenFile)) return;
            try
            {
                string thuMucGoc = @"C:\BaitapTrenLop\.Net\QL_Giay\QuanLyCuaHangGiay\Images";
                string duongDanDayDu = System.IO.Path.Combine(thuMucGoc, tenFile);
                if (System.IO.File.Exists(duongDanDayDu))
                {
                    if (picAnh.Image != null) picAnh.Image.Dispose();
                    picAnh.Image = Image.FromFile(duongDanDayDu);
                }
            }
            catch { }
        }
    }
}