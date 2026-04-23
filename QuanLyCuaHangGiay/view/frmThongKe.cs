using QuanLyCuaHangGiay.controller;
using QuanLyCuaHangGiay.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyCuaHangGiay.view
{
    public partial class frmThongKe : Form
    {
        private ThongKeController thongKeController = new ThongKeController();

        public frmThongKe()
        {
            InitializeComponent();
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            LoadNam();

            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDenNgay.Value = DateTime.Now;

            CaiDatDgvTopSanPham();
            LoadThongKe();
        }

        private void LoadNam()
        {
            cboNam.Items.Clear();
            int namHienTai = DateTime.Now.Year;

            for (int i = namHienTai; i >= namHienTai - 5; i--)
            {
                cboNam.Items.Add(i);
            }

            cboNam.SelectedItem = namHienTai;
        }

        private void CaiDatDgvTopSanPham()
        {
            dgvTopSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopSanPham.MultiSelect = false;
            dgvTopSanPham.ReadOnly = true;
            dgvTopSanPham.AllowUserToAddRows = false;
            dgvTopSanPham.AllowUserToDeleteRows = false;
            dgvTopSanPham.RowHeadersVisible = false;
        }

        private void LoadThongKe()
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;

                if (tuNgay > denNgay)
                {
                    MessageBox.Show("Từ ngày không được lớn hơn đến ngày.");
                    return;
                }

                int nam = DateTime.Now.Year;
                if (cboNam.SelectedItem != null)
                {
                    nam = Convert.ToInt32(cboNam.SelectedItem);
                }

                ThongKe tk = thongKeController.LayTongQuan(tuNgay, denNgay);

                lblTongSanPham.Text = tk.TongSanPham.ToString();
                lblTongNCC.Text = tk.TongNhaCungCap.ToString();
                lblTongDonHang.Text = tk.TongDonHang.ToString();
                lblTongPhieuNhap.Text = tk.TongPhieuNhap.ToString();
                lblTongTonKho.Text = tk.TongSoLuongTon.ToString();
                lblTongDoanhThu.Text = tk.TongDoanhThu.ToString("N0") + " VNĐ";

                VeChartDoanhThu(nam, tuNgay, denNgay);
                VeChartNhapHang(nam, tuNgay, denNgay);
                VeChartTopSanPham(tuNgay, denNgay);
                LoadBangTopSanPham(tuNgay, denNgay);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê: " + ex.Message);
            }
        }

        private void VeChartDoanhThu(int nam, DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds = thongKeController.LayDoanhThuTheoThang(nam, tuNgay, denNgay);

            chartDoanhThu.Series.Clear();
            chartDoanhThu.ChartAreas.Clear();
            chartDoanhThu.Titles.Clear();
            chartDoanhThu.Legends.Clear();

            chartDoanhThu.ChartAreas.Add(new ChartArea("ChartArea1"));
            chartDoanhThu.Titles.Add("Doanh thu theo tháng");

            Series series = new Series("DoanhThu");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;

            foreach (var item in ds)
            {
                series.Points.AddXY(item.Nhan, item.GiaTri);
            }

            chartDoanhThu.Series.Add(series);
        }

        private void VeChartNhapHang(int nam, DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds = thongKeController.LayNhapHangTheoThang(nam, tuNgay, denNgay);

            chartNhapHang.Series.Clear();
            chartNhapHang.ChartAreas.Clear();
            chartNhapHang.Titles.Clear();
            chartNhapHang.Legends.Clear();

            chartNhapHang.ChartAreas.Add(new ChartArea("ChartArea1"));
            chartNhapHang.Titles.Add("Nhập hàng theo tháng");

            Series series = new Series("NhapHang");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.IsValueShownAsLabel = true;

            foreach (var item in ds)
            {
                series.Points.AddXY(item.Nhan, item.GiaTri);
            }

            chartNhapHang.Series.Add(series);
        }

        private void VeChartTopSanPham(DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds = thongKeController.LayTop5SanPhamBanChay(tuNgay, denNgay);

            chartTopSanPham.Series.Clear();
            chartTopSanPham.ChartAreas.Clear();
            chartTopSanPham.Titles.Clear();
            chartTopSanPham.Legends.Clear();

            chartTopSanPham.ChartAreas.Add(new ChartArea("ChartArea1"));
            chartTopSanPham.Titles.Add("Top 5 sản phẩm bán chạy");

            Series series = new Series("TopSanPham");
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            series.Label = "#PERCENT{P0}";

            foreach (var item in ds)
            {
                int pointIndex = series.Points.AddXY(item.Nhan, item.GiaTri);
                series.Points[pointIndex].LegendText = item.Nhan;
            }

            chartTopSanPham.Series.Add(series);
            chartTopSanPham.Legends.Add(new Legend("Legend1"));
        }

        private void LoadBangTopSanPham(DateTime tuNgay, DateTime denNgay)
        {
            dgvTopSanPham.DataSource = thongKeController.LayBangTopSanPhamBanChay(tuNgay, denNgay);
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDenNgay.Value = DateTime.Now;
            cboNam.SelectedItem = DateTime.Now.Year;
            LoadThongKe();
        }

        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNam.SelectedItem != null)
            {
                LoadThongKe();
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadThongKe();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvTopSanPham.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel file (*.csv)|*.csv";
            sfd.FileName = "TopSanPhamBanChay.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                XuatCSV(dgvTopSanPham, sfd.FileName);
                MessageBox.Show("Xuất file thành công.");
            }
        }

        private void XuatCSV(DataGridView dgv, string path)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                sb.Append(dgv.Columns[i].HeaderText);
                if (i < dgv.Columns.Count - 1)
                    sb.Append(",");
            }
            sb.AppendLine();

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    string value = dgv.Rows[i].Cells[j].Value == null
                        ? ""
                        : dgv.Rows[i].Cells[j].Value.ToString().Replace(",", " ");

                    sb.Append(value);

                    if (j < dgv.Columns.Count - 1)
                        sb.Append(",");
                }
                sb.AppendLine();
            }

            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }
    }
}