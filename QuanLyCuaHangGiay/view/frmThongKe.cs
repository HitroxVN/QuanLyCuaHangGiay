using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuanLyCuaHangGiay.controller;
using QuanLyCuaHangGiay.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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
            this.WindowState = FormWindowState.Maximized;
        }

        public void ReloadData()
        {
            LoadThongKe();
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDenNgay.Value = DateTime.Now;

            CaiDatDgvTopSanPham();
            CaiDatGiaoDienChuyenNghiep();
            CaiDatGiaoDienCoGian();
            GanSuKienClickThongKe();

            LoadThongKe(); 
            ToMauTonKhoThap();
        }

        private void CaiDatGiaoDienCoGian()
        {
            chartDoanhThu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            chartNhapHang.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            chartTopSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvTopSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        private void CaiDatDgvTopSanPham()
        {
            dgvTopSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvTopSanPham.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            dgvTopSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopSanPham.MultiSelect = false;
            dgvTopSanPham.ReadOnly = true;
            dgvTopSanPham.AllowUserToAddRows = false;
            dgvTopSanPham.AllowUserToDeleteRows = false;
            dgvTopSanPham.RowHeadersVisible = false;

            dgvTopSanPham.ScrollBars = ScrollBars.Both;
            dgvTopSanPham.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvTopSanPham.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgvTopSanPham.RowTemplate.Height = 32;
            dgvTopSanPham.ColumnHeadersHeight = 45;
        }

        private void ChinhCotDataGridView()
        {
            if (dgvTopSanPham.Columns.Count == 0)
                return;

            dgvTopSanPham.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvTopSanPham.ScrollBars = ScrollBars.Vertical;

            dgvTopSanPham.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgvTopSanPham.ColumnHeadersDefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            dgvTopSanPham.RowTemplate.Height = 32;
            dgvTopSanPham.ColumnHeadersHeight = 36;

            foreach (DataGridViewColumn col in dgvTopSanPham.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.MinimumWidth = 95;
            }

            SetFill("Mã đơn", 70);
            SetFill("Mã SP", 70);
            SetFill("Mã NCC", 80);
            SetFill("Mã phiếu nhập", 110);

            SetFill("Ngày tạo", 130);
            SetFill("Ngày bán", 130);
            SetFill("Ngày nhập", 130);
            SetFill("Thời gian", 130);

            SetFill("Tên sản phẩm", 180);
            SetFill("Sản phẩm", 180);
            SetFill("Tên khách hàng", 180);
            SetFill("Tên nhà cung cấp", 190);

            SetFill("Số lượng", 90);
            SetFill("Số lượng nhập", 110);
            SetFill("Số lượng tồn", 110);
            SetFill("Tồn kho", 90);

            SetFill("Giá", 110);
            SetFill("Giá bán", 110);
            SetFill("Giá nhập", 110);
            SetFill("Tổng tiền", 130);
            SetFill("Thành tiền", 130);
            SetFill("Tổng doanh thu", 150);

            SetFill("Số điện thoại", 140);
            SetFill("SĐT", 120);
            SetFill("Trạng thái", 120);
            SetFill("Màu", 90);
            SetFill("Kích cỡ", 90);
            SetFill("Địa chỉ", 180);

            DinhDangCotNgay("Ngày tạo");
            DinhDangCotNgay("Ngày bán");
            DinhDangCotNgay("Ngày nhập");
            DinhDangCotNgay("Thời gian");

            DinhDangCotTien("Giá");
            DinhDangCotTien("Giá bán");
            DinhDangCotTien("Giá nhập");
            DinhDangCotTien("Tổng tiền");
            DinhDangCotTien("Thành tiền");
            DinhDangCotTien("Tổng doanh thu");

            ToMauTonKhoThap();
        }

        private void SetCot(string tenCot, int width)
        {
            if (dgvTopSanPham.Columns.Contains(tenCot))
                dgvTopSanPham.Columns[tenCot].Width = width;
        }
        private void SetFill(string tenCot, float fillWeight)
        {
            if (dgvTopSanPham.Columns.Contains(tenCot))
                dgvTopSanPham.Columns[tenCot].FillWeight = fillWeight;
        }
        private void DinhDangCotNgay(string tenCot)
        {
            if (dgvTopSanPham.Columns.Contains(tenCot))
                dgvTopSanPham.Columns[tenCot].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        private void DinhDangCotTien(string tenCot)
        {
            if (dgvTopSanPham.Columns.Contains(tenCot))
                dgvTopSanPham.Columns[tenCot].DefaultCellStyle.Format = "N0";
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

                ThongKe tk = thongKeController.LayTongQuan(tuNgay, denNgay);

                lblTongSanPham.Text = tk.TongSanPham.ToString();
                lblTongNCC.Text = tk.TongNhaCungCap.ToString();
                lblTongDonHang.Text = tk.TongDonHang.ToString();
                lblTongPhieuNhap.Text = tk.TongPhieuNhap.ToString();
                lblTongTonKho.Text = tk.TongSoLuongTon.ToString();
                lblTongDoanhThu.Text = tk.TongDoanhThu.ToString("N0") + " VNĐ";
                lblCapNhatLuc.Text ="Cập nhật lúc: "+ DateTime.Now.ToString("HH:mm - dd/MM/yyyy");
                VeChartDoanhThu(tuNgay, denNgay);
                VeChartNhapHang(tuNgay, denNgay);
                VeChartTopSanPham(tuNgay, denNgay);
                LoadBangTopSanPham(tuNgay, denNgay);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê: " + ex.Message);
            }
        }

        private void GanSuKienClickThongKe()
        {
            panelTongSanPham.Cursor = Cursors.Hand;
            panelTongNCC.Cursor = Cursors.Hand;
            panelTongDonHang.Cursor = Cursors.Hand;
            panelTongPhieuNhap.Cursor = Cursors.Hand;
            panelTongTonKho.Cursor = Cursors.Hand;
            panelTongDoanhThu.Cursor = Cursors.Hand;

            lblTongSanPham.Cursor = Cursors.Hand;
            lblTongNCC.Cursor = Cursors.Hand;
            lblTongDonHang.Cursor = Cursors.Hand;
            lblTongPhieuNhap.Cursor = Cursors.Hand;
            lblTongTonKho.Cursor = Cursors.Hand;
            lblTongDoanhThu.Cursor = Cursors.Hand;

            EventHandler clickSanPham = (s, e) =>
            {
                dgvTopSanPham.DataSource = thongKeController.LayDanhSachSanPham();
                ChinhCotDataGridView();
            };

            panelTongSanPham.Click += clickSanPham;
            lblTongSanPham.Click += clickSanPham;
            lblTextTongSanPham.Click += clickSanPham;

            EventHandler clickNCC = (s, e) =>
            {
                dgvTopSanPham.DataSource = thongKeController.LayDanhSachNhaCungCap();
                ChinhCotDataGridView();
            };

            panelTongNCC.Click += clickNCC;
            lblTongNCC.Click += clickNCC;
            lblTextTongNCC.Click += clickNCC;

            EventHandler clickDonHang = (s, e) =>
            {
                dgvTopSanPham.DataSource =
                    thongKeController.LayDanhSachDonHang(dtpTuNgay.Value.Date, dtpDenNgay.Value.Date);

                ChinhCotDataGridView();
            };

            panelTongDonHang.Click += clickDonHang;
            lblTongDonHang.Click += clickDonHang;
            lblTextTongDonHang.Click += clickDonHang;

            EventHandler clickPhieuNhap = (s, e) =>
            {
                dgvTopSanPham.DataSource =
                    thongKeController.LayDanhSachPhieuNhap(dtpTuNgay.Value.Date, dtpDenNgay.Value.Date);

                ChinhCotDataGridView();
            };

            panelTongPhieuNhap.Click += clickPhieuNhap;
            lblTongPhieuNhap.Click += clickPhieuNhap;
            lblTextTongPhieuNhap.Click += clickPhieuNhap;

            EventHandler clickTonKho = (s, e) =>
            {
                dgvTopSanPham.DataSource = thongKeController.LayTonKhoLauNhat();
                ChinhCotDataGridView();
            };

            panelTongTonKho.Click += clickTonKho;
            lblTongTonKho.Click += clickTonKho;
            lblTextTongTonKho.Click += clickTonKho;

            EventHandler clickDoanhThu = (s, e) =>
            {
                dgvTopSanPham.DataSource =
                    thongKeController.LayDanhSachDoanhThu(dtpTuNgay.Value.Date, dtpDenNgay.Value.Date);

                ChinhCotDataGridView();
            };

            panelTongDoanhThu.Click += clickDoanhThu;
            lblTongDoanhThu.Click += clickDoanhThu;
            lblTextTongDoanhThu.Click += clickDoanhThu;
        }

        private void VeChartDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds =
                thongKeController.LayDoanhThuTheoThang(tuNgay, denNgay);

            chartDoanhThu.Series.Clear();
            chartDoanhThu.ChartAreas.Clear();
            chartDoanhThu.Titles.Clear();
            chartDoanhThu.Legends.Clear();

            chartDoanhThu.ChartAreas.Add(new ChartArea("ChartArea1"));
            chartDoanhThu.Titles.Add("Doanh thu theo tháng");

            Series series = new Series("DoanhThu");

            series.ChartType = SeriesChartType.Column;

            series.Color = Color.Firebrick;
            series.BackGradientStyle = GradientStyle.TopBottom;
            series.BackSecondaryColor = Color.IndianRed;
            series["PointWidth"] = "0.4";
            series.Label = "#VALY{N0} VNĐ";
            series.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            series.Color = Color.Firebrick;

            series.BackGradientStyle =
                GradientStyle.TopBottom;

            series.BackSecondaryColor =
                Color.IndianRed;

            series.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            series.LabelForeColor = Color.Black;

            series.BorderWidth = 1;

            foreach (var item in ds)
            {
                series.Points.AddXY(item.Nhan, item.GiaTri);
            }

            chartDoanhThu.Series.Add(series);
        }

        private void VeChartNhapHang(DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds =
                thongKeController.LayNhapHangTheoThang(tuNgay, denNgay);

            chartNhapHang.Series.Clear();
            chartNhapHang.ChartAreas.Clear();
            chartNhapHang.Titles.Clear();
            chartNhapHang.Legends.Clear();

            ChartArea area = new ChartArea("ChartArea1");
            area.BackColor = Color.White;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
            area.AxisX.LineColor = Color.LightGray;
            area.AxisY.LineColor = Color.LightGray;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 9);
            area.AxisX.Interval = 1;

            chartNhapHang.ChartAreas.Add(area);

            Title title = new Title();
            title.Text = "Nhập hàng theo tháng";
            title.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            chartNhapHang.Titles.Add(title);

            Series series = new Series("NhapHang");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.DarkRed;
            series.BackGradientStyle = GradientStyle.TopBottom;
            series.BackSecondaryColor = Color.IndianRed;
            series.IsValueShownAsLabel = true;
            series.Label = "#VALY";
            series.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            series["PointWidth"] = "0.4";

            foreach (var item in ds)
            {
                series.Points.AddXY(item.Nhan, item.GiaTri);
            }

            chartNhapHang.Series.Add(series);

            chartNhapHang.BackColor = Color.White;
            chartNhapHang.BorderlineColor = Color.Gainsboro;
            chartNhapHang.BorderlineWidth = 1;
            chartNhapHang.BorderlineDashStyle = ChartDashStyle.Solid;
        }

        private void VeChartTopSanPham(DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds =
                thongKeController.LayTop5SanPhamBanChay(tuNgay, denNgay);

            chartTopSanPham.Series.Clear();
            chartTopSanPham.ChartAreas.Clear();
            chartTopSanPham.Titles.Clear();
            chartTopSanPham.Legends.Clear();

            ChartArea area = new ChartArea("ChartArea1");
            area.BackColor = Color.White;
            area.Area3DStyle.Enable3D = false;
            area.Position.Width = 70;
            area.Position.Height = 80;

            chartTopSanPham.ChartAreas.Add(area);

            Title title = new Title();
            title.Text = "Top 5 sản phẩm bán chạy";
            title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            chartTopSanPham.Titles.Add(title);

            Series series = new Series("TopSanPham");
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            series.Label = "#PERCENT{P0}";

            series.Color = Color.Firebrick;
            series.BackGradientStyle = GradientStyle.TopBottom;
            series.BackSecondaryColor = Color.IndianRed;

            series.BorderWidth = 2;
            series.BorderColor = Color.White;
            series.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            series["PieLabelStyle"] = "Outside";
            series["PieDrawingStyle"] = "SoftEdge";

            foreach (var item in ds)
            {
                int pointIndex = series.Points.AddXY(item.Nhan, item.GiaTri);
                series.Points[pointIndex].LegendText = item.Nhan;
            }

            chartTopSanPham.Series.Add(series);

            Legend legend = new Legend("Legend1");
            legend.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            legend.Docking = Docking.Right;
            legend.Enabled = ds.Count > 1;

            chartTopSanPham.Legends.Add(legend);
            chartTopSanPham.BackColor = Color.White;
        }
        private void LoadBangTopSanPham(DateTime tuNgay, DateTime denNgay)
        {
            dgvTopSanPham.DataSource =
                thongKeController.LayBangTopSanPhamBanChay(tuNgay, denNgay);

            ChinhCotDataGridView();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDenNgay.Value = DateTime.Now;

            LoadThongKe();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadThongKe();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
            sfd.FileName = "BaoCaoThongKeDayDu_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                XuatExcelTatCa(sfd.FileName);
                MessageBox.Show("Xuất Excel đầy đủ thành công!");
            }
        }
        private void XuatExcelTatCa(string path)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;


using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws =
                    pck.Workbook.Worksheets.Add("BaoCaoThongKe");

                // ===== TIÊU ĐỀ =====

                ws.Cells["A1:H1"].Merge = true;

                ws.Cells["A1"].Value =
                    "BÁO CÁO THỐNG KÊ";

                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A1"].Style.Font.Size = 20;

                ws.Cells["A1"].Style.HorizontalAlignment =
                    ExcelHorizontalAlignment.Center;

                ws.Cells["A1"].Style.Fill.PatternType =
                    ExcelFillStyle.Solid;

                ws.Cells["A1"].Style.Fill.BackgroundColor
                    .SetColor(Color.DarkRed);

                ws.Cells["A1"].Style.Font.Color
                    .SetColor(Color.White);

                // ===== THỜI GIAN =====

                ws.Cells["A3"].Value = "Từ ngày:";
                ws.Cells["B3"].Value =
                    tuNgay.ToString("dd/MM/yyyy");

                ws.Cells["D3"].Value = "Đến ngày:";
                ws.Cells["E3"].Value =
                    denNgay.ToString("dd/MM/yyyy");

                ws.Cells["A3"].Style.Font.Bold = true;
                ws.Cells["D3"].Style.Font.Bold = true;

                int dong = 6;

                dong = ThemBangVaoSheet(
                    ws,
                    "DANH SÁCH SẢN PHẨM",
                    thongKeController.LayDanhSachSanPham(),
                    dong
                );

                dong = ThemBangVaoSheet(
                    ws,
                    "DANH SÁCH NHÀ CUNG CẤP",
                    thongKeController.LayDanhSachNhaCungCap(),
                    dong + 2
                );

                dong = ThemBangVaoSheet(
                    ws,
                    "DANH SÁCH ĐƠN HÀNG",
                    thongKeController.LayDanhSachDonHang(
                        tuNgay,
                        denNgay
                    ),
                    dong + 2
                );

                dong = ThemBangVaoSheet(
                    ws,
                    "DANH SÁCH PHIẾU NHẬP",
                    thongKeController.LayDanhSachPhieuNhap(
                        tuNgay,
                        denNgay
                    ),
                    dong + 2
                );

                dong = ThemBangVaoSheet(
                    ws,
                    "TỒN KHO",
                    thongKeController.LayTonKhoLauNhat(),
                    dong + 2
                );

                dong = ThemBangVaoSheet(
                    ws,
                    "DOANH THU",
                    thongKeController.LayDanhSachDoanhThu(
                        tuNgay,
                        denNgay
                    ),
                    dong + 2
                );

                ws.Cells.AutoFitColumns();

                FileInfo fi = new FileInfo(path);

                pck.SaveAs(fi);
            }

}


        private int ThemBangVaoSheet(
        ExcelWorksheet ws,
        string tieuDe,
        DataTable dt,
        int dongBatDau
        )
        {
            int dong = dongBatDau;

ws.Cells[dong, 1].Value = tieuDe;

            ws.Cells[dong, 1].Style.Font.Bold = true;
            ws.Cells[dong, 1].Style.Font.Size = 14;
            ws.Cells[dong, 1].Style.Font.Color.SetColor(Color.DarkRed);

            dong += 2;

            if (dt == null || dt.Rows.Count == 0)
            {
                ws.Cells[dong, 1].Value = "Không có dữ liệu";

                return dong + 2;
            }

            // HEADER
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.Cells[dong, i + 1].Value =
                    dt.Columns[i].ColumnName;

                ws.Cells[dong, i + 1].Style.Font.Bold = true;

                ws.Cells[dong, i + 1].Style.Fill.PatternType =
                    ExcelFillStyle.Solid;

                ws.Cells[dong, i + 1].Style.Fill.BackgroundColor
                    .SetColor(Color.LightBlue);

                ws.Cells[dong, i + 1].Style.Border.Top.Style =
                    ExcelBorderStyle.Thin;

                ws.Cells[dong, i + 1].Style.Border.Left.Style =
                    ExcelBorderStyle.Thin;

                ws.Cells[dong, i + 1].Style.Border.Right.Style =
                    ExcelBorderStyle.Thin;

                ws.Cells[dong, i + 1].Style.Border.Bottom.Style =
                    ExcelBorderStyle.Thin;
            }

            dong++;

            // DATA
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ws.Cells[dong, j + 1].Value =
                        dt.Rows[i][j];

                    ws.Cells[dong, j + 1].Style.Border.Top.Style =
                        ExcelBorderStyle.Thin;

                    ws.Cells[dong, j + 1].Style.Border.Left.Style =
                        ExcelBorderStyle.Thin;

                    ws.Cells[dong, j + 1].Style.Border.Right.Style =
                        ExcelBorderStyle.Thin;

                    ws.Cells[dong, j + 1].Style.Border.Bottom.Style =
                        ExcelBorderStyle.Thin;
                }

                dong++;
            }

            return dong + 1;
}

        private void ThemSheetDataTable(ExcelPackage pck, string tenSheet, DataTable dt)
        {
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(tenSheet);

            ws.Cells[1, 1].Value = tenSheet;
            ws.Cells[1, 1].Style.Font.Bold = true;
            ws.Cells[1, 1].Style.Font.Size = 16;

            if (dt == null || dt.Rows.Count == 0)
            {
                ws.Cells[3, 1].Value = "Không có dữ liệu";
                return;
            }

            // Header
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.Cells[3, i + 1].Value = dt.Columns[i].ColumnName;
                ws.Cells[3, i + 1].Style.Font.Bold = true;
                ws.Cells[3, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                ws.Cells[3, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            // Data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ws.Cells[i + 4, j + 1].Value = dt.Rows[i][j];
                    ws.Cells[i + 4, j + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
            }

            ws.Cells.AutoFitColumns();
        }
        private void CaiDatGiaoDienChuyenNghiep()
        {
            // ================= DATAGRIDVIEW =================

            dgvTopSanPham.EnableHeadersVisualStyles = false;

            // Header đỏ
            dgvTopSanPham.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkRed;
            dgvTopSanPham.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTopSanPham.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DarkRed;
            dgvTopSanPham.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgvTopSanPham.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            // Dòng bình thường
            dgvTopSanPham.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9, FontStyle.Bold);

            dgvTopSanPham.DefaultCellStyle.BackColor = Color.White;
            dgvTopSanPham.DefaultCellStyle.ForeColor = Color.Black;

            // Dòng được chọn đỏ nhạt
            dgvTopSanPham.DefaultCellStyle.SelectionBackColor =Color.FromArgb(205, 92, 92);
            dgvTopSanPham.DefaultCellStyle.SelectionForeColor = Color.White;

            // Dòng xen kẽ
            dgvTopSanPham.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(255, 245, 245);

            dgvTopSanPham.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // Kẻ bảng nhẹ, chuyên nghiệp hơn
            dgvTopSanPham.GridColor = Color.Silver;
            // Kẻ bảng
            dgvTopSanPham.GridColor = Color.LightGray;

            dgvTopSanPham.CellBorderStyle =
                DataGridViewCellBorderStyle.Single;

            dgvTopSanPham.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dgvTopSanPham.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgvTopSanPham.BackgroundColor = Color.White;
            dgvTopSanPham.BorderStyle = BorderStyle.FixedSingle;

            // ================= CARD THỐNG KÊ =================

            CaiDatCardThongKe(panelTongSanPham);
            CaiDatCardThongKe(panelTongNCC);
            CaiDatCardThongKe(panelTongDonHang);
            CaiDatCardThongKe(panelTongPhieuNhap);
            CaiDatCardThongKe(panelTongTonKho);
            CaiDatCardThongKe(panelTongDoanhThu);

            // ================= CHART =================

            CaiDatChartDep(chartDoanhThu);
            CaiDatChartDep(chartNhapHang);
            CaiDatChartDep(chartTopSanPham);
        }
        private void CaiDatCardThongKe(Panel panel)
        {
            panel.BackColor = Color.FromArgb(252, 252, 252);
            panel.BorderStyle = BorderStyle.FixedSingle;

            panel.MouseEnter += (s, e) =>
            {
                panel.BackColor = Color.FromArgb(245, 245, 245);
            };

            panel.MouseLeave += (s, e) =>
            {
                panel.BackColor = Color.FromArgb(252, 252, 252);
            };
            panel.Padding = new Padding(1);
            panel.BackColor =Color.FromArgb(245, 245, 245);
        }

        private void GanHoverPanel(Panel panel)
        {
            panel.BackColor =
            Color.FromArgb(252, 252, 252);

            panel.MouseEnter += (s, e) =>
            {
                panel.BackColor =
                Color.FromArgb(235, 235, 235);
            };

            panel.MouseLeave += (s, e) =>
            {
                panel.BackColor = Color.White;
            };
        }

        private void CaiDatChartDep(Chart chart)
        {
            chart.BackColor = Color.White;
            chart.BorderlineColor = Color.Gainsboro;
            chart.BorderlineWidth = 1;
            chart.BorderlineDashStyle = ChartDashStyle.Solid;

            foreach (ChartArea area in chart.ChartAreas)
            {
                area.BackColor = Color.White;
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);

                area.AxisX.LineColor = Color.LightGray;
                area.AxisY.LineColor = Color.LightGray;

                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 9);

                area.AxisX.Interval = 1;
            }
        }
        private void ToMauTonKhoThap()
        {
            if (!dgvTopSanPham.Columns.Contains("Tồn kho") &&
                !dgvTopSanPham.Columns.Contains("Số lượng tồn"))
                return;

            string tenCot = dgvTopSanPham.Columns.Contains("Tồn kho")
                ? "Tồn kho"
                : "Số lượng tồn";

            foreach (DataGridViewRow row in dgvTopSanPham.Rows)
            {
                if (row.Cells[tenCot].Value == null)
                    continue;

                int soLuong;

                if (int.TryParse(row.Cells[tenCot].Value.ToString(), out soLuong))
                {
                    if (soLuong <= 5)
                    {
                        row.DefaultCellStyle.BackColor = Color.MistyRose;
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                }
            }
        }
    }
}