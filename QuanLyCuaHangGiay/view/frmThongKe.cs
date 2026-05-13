using OfficeOpenXml;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Style;
using QuanLyCuaHangGiay.controller;
using QuanLyCuaHangGiay.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace QuanLyCuaHangGiay.view
{
    public partial class frmThongKe : Form, util.IBaseForm
    {
        private ThongKeController thongKeController = new ThongKeController();

        public frmThongKe()
        {
            InitializeComponent();
        }

        public void ReloadData()
        {
            LoadThongKe();
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

            sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
            sfd.FileName = "BaoCaoThongKe_"
            + DateTime.Now.ToString("dd-MM-yyyy")
            + ".xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                XuatExcel(dgvTopSanPham, sfd.FileName);

                MessageBox.Show("Xuất Excel thành công!");
            }
        }

        private void XuatExcel(DataGridView dgv, string path)
        {

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("ThongKe");

                // Tiêu đề
                ws.Cells["A1:F1"].Merge = true;
                ws.Cells["A1"].Value = "BÁO CÁO THỐNG KÊ NGÀY "
                + DateTime.Now.ToString("dd/MM/yyyy");
                MessageBox.Show(ws.Cells["A1"].Value.ToString());

                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A1"].Style.Font.Size = 18;
                ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.DarkRed);
                ws.Cells["A1"].Style.Font.Color.SetColor(Color.White);

                // Thời gian
                ws.Cells["A3"].Value = "Từ ngày:";
                ws.Cells["B3"].Value = dtpTuNgay.Value.ToString("dd/MM/yyyy");

                ws.Cells["A4"].Value = "Đến ngày:";
                ws.Cells["B4"].Value = dtpDenNgay.Value.ToString("dd/MM/yyyy");

                // Thống kê
                ws.Cells["A6"].Value = "Tổng sản phẩm";
                ws.Cells["B6"].Value = lblTongSanPham.Text;

                ws.Cells["A7"].Value = "Tổng nhà cung cấp";
                ws.Cells["B7"].Value = lblTongNCC.Text;

                ws.Cells["A8"].Value = "Tổng đơn hàng";
                ws.Cells["B8"].Value = lblTongDonHang.Text;

                ws.Cells["A9"].Value = "Tổng phiếu nhập";
                ws.Cells["B9"].Value = lblTongPhieuNhap.Text;

                ws.Cells["A10"].Value = "Tổng tồn kho";
                ws.Cells["B10"].Value = lblTongTonKho.Text;

                ws.Cells["A11"].Value = "Tổng doanh thu";
                ws.Cells["B11"].Value = lblTongDoanhThu.Text;

                ws.Cells["A6:A11"].Style.Font.Bold = true;

                // Tiêu đề bảng
                ws.Cells["A13"].Value = "TOP SẢN PHẨM BÁN CHẠY";
                ws.Cells["A13"].Style.Font.Bold = true;
                ws.Cells["A13"].Style.Font.Size = 14;

                // Header
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    ws.Cells[15, i + 1].Value = dgv.Columns[i].HeaderText;

                    ws.Cells[15, i + 1].Style.Font.Bold = true;
                    ws.Cells[15, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[15, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                    ws.Cells[15, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws.Cells[15, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws.Cells[15, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws.Cells[15, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

                // Data
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        ws.Cells[i + 16, j + 1].Value =
                            dgv.Rows[i].Cells[j].Value?.ToString();

                        ws.Cells[i + 16, j + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws.Cells[i + 16, j + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws.Cells[i + 16, j + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[i + 16, j + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                }

                ws.Cells.AutoFitColumns();

                FileInfo fi = new FileInfo(path);
                pck.SaveAs(fi);
            }
        }
    }
}