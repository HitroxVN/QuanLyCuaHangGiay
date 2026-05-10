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
        private DataTable _dtLichSu;
        public frmKho()
        {
            InitializeComponent();
            _controller = new KhoController();
        }

        private void frmKho_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            LoadComboboxNhaCungCap();
            LoadDataLichSu();
        }

        private void LoadComboboxNhaCungCap()
        {
            DataTable dtNCC = _controller.LayDanhSachNhaCungCap();
            DataRow dr = dtNCC.NewRow();
            dr["id"] = 0;
            dr["tenNCC"] = "--- Tất cả Nhà Cung Cấp ---";
            dtNCC.Rows.InsertAt(dr, 0);

            cbLocNCC.DataSource = dtNCC;
            cbLocNCC.DisplayMember = "tenNCC";
            cbLocNCC.ValueMember = "id";
            cbLocNCC.SelectedIndex = 0;
        }

        public void ReloadData()
        {
            LoadDataLichSu();
        }

        private void LoadDataLichSu()
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value;
                DateTime denNgay = dtpDenNgay.Value;
                string idNccLoc = cbLocNCC.SelectedValue?.ToString() ?? "0";
                string tuKhoa = txtSearch.Text.Trim();

                _dtLichSu = _controller.LayLichSuNhapHang(tuNgay, denNgay, idNccLoc, tuKhoa);
                dgvKho.DataSource = _dtLichSu;

                // Tùy chỉnh cột Thành Tiền và Đơn giá (định dạng tiền tệ)
                if (dgvKho.Columns["Đơn Giá"] != null)
                    dgvKho.Columns["Đơn Giá"].DefaultCellStyle.Format = "N0";
                if (dgvKho.Columns["Thành Tiền"] != null)
                    dgvKho.Columns["Thành Tiền"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadDataLichSu();
        }

        private void cbLocNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocNCC.SelectedIndex > -1 && cbLocNCC.ValueMember != "")
            {
                LoadDataLichSu();
            }
        }
    }
}
