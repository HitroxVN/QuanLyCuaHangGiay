namespace QuanLyCuaHangGiay.view
{
    partial class frmThongKe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.panelTongSanPham = new System.Windows.Forms.Panel();
            this.lblTongQuan = new System.Windows.Forms.Label();
            this.panelTongQuanBox =new System.Windows.Forms.Panel();
            this.panelTheoKyBox = new System.Windows.Forms.Panel();
            this.lblTheoKy = new System.Windows.Forms.Label();
            this.lblGhiChuLoc = new System.Windows.Forms.Label();
            this.lblTongSanPham = new System.Windows.Forms.Label();
            this.lblTextTongSanPham = new System.Windows.Forms.Label();
            this.panelTongNCC = new System.Windows.Forms.Panel();
            this.lblTongNCC = new System.Windows.Forms.Label();
            this.lblTextTongNCC = new System.Windows.Forms.Label();
            this.panelTongDonHang = new System.Windows.Forms.Panel();
            this.lblTongDonHang = new System.Windows.Forms.Label();
            this.lblTextTongDonHang = new System.Windows.Forms.Label();
            this.panelTongPhieuNhap = new System.Windows.Forms.Panel();
            this.lblTongPhieuNhap = new System.Windows.Forms.Label();
            this.lblTextTongPhieuNhap = new System.Windows.Forms.Label();
            this.panelTongTonKho = new System.Windows.Forms.Panel();
            this.lblTongTonKho = new System.Windows.Forms.Label();
            this.lblTextTongTonKho = new System.Windows.Forms.Label();
            this.panelTongDoanhThu = new System.Windows.Forms.Panel();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.lblTextTongDoanhThu = new System.Windows.Forms.Label();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartNhapHang = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTopSanPham = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvTopSanPham = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.lblRoleAdmin =
             new System.Windows.Forms.Label();
            this.lblCapNhatLuc = new System.Windows.Forms.Label();

            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelTongSanPham.SuspendLayout();
            this.panelTongNCC.SuspendLayout();
            this.panelTongDonHang.SuspendLayout();
            this.panelTongPhieuNhap.SuspendLayout();
            this.panelTongTonKho.SuspendLayout();
            this.panelTongDoanhThu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNhapHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSanPham)).BeginInit();
            this.SuspendLayout();

            // panel1
            this.panel1.BackColor = System.Drawing.Color.DarkRed;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblRoleAdmin);
            this.panel1.Controls.Add(this.lblCapNhatLuc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1837, 80);
            this.panel1.TabIndex = 0;

            // label7
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Lavender;
            this.label7.Location = new System.Drawing.Point(13, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 45);
            this.label7.TabIndex = 3;
            this.label7.Text ="Thống kê";
            // lblCapNhatLuc
            this.lblCapNhatLuc.AutoSize = false;
            this.lblCapNhatLuc.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.lblCapNhatLuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCapNhatLuc.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCapNhatLuc.Location = new System.Drawing.Point(1300, 18);
            this.lblCapNhatLuc.Name = "lblCapNhatLuc";
            this.lblCapNhatLuc.Size = new System.Drawing.Size(360, 25);
            this.lblCapNhatLuc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCapNhatLuc.Text = "Cập nhật lúc: --";

            // lblRoleAdmin
            this.lblRoleAdmin.AutoSize = false;
            this.lblRoleAdmin.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this.lblRoleAdmin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRoleAdmin.ForeColor = System.Drawing.Color.White;
            this.lblRoleAdmin.Location = new System.Drawing.Point(1300, 45);
            this.lblRoleAdmin.Name = "lblRoleAdmin";
            this.lblRoleAdmin.Size = new System.Drawing.Size(360, 25);
            this.lblRoleAdmin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRoleAdmin.Text = "👤 Quản trị viên";
            // panel2
            this.panel2.Controls.Add(this.btnXuatExcel);
            this.panel2.Controls.Add(this.lblTongQuan);
            this.panel2.Controls.Add(this.lblTheoKy);
            this.panel2.Controls.Add(this.lblGhiChuLoc);
            this.panel2.Controls.Add(this.btnLoc);
            this.panel2.Controls.Add(this.btnTaiLai);
            this.panel2.Controls.Add(this.dtpDenNgay);
            this.panel2.Controls.Add(this.lblDenNgay);
            this.panel2.Controls.Add(this.dtpTuNgay);
            this.panel2.Controls.Add(this.lblTuNgay);
            this.panel2.Controls.Add(this.panelTongSanPham);
            this.panel2.Controls.Add(this.panelTongNCC);
            this.panel2.Controls.Add(this.panelTongDonHang);
            this.panel2.Controls.Add(this.panelTongPhieuNhap);
            this.panel2.Controls.Add(this.panelTongTonKho);
            this.panel2.Controls.Add(this.panelTongDoanhThu);
            this.panel2.Controls.Add(this.chartDoanhThu);
            this.panel2.Controls.Add(this.chartNhapHang);
            this.panel2.Controls.Add(this.chartTopSanPham);
            this.panel2.Controls.Add(this.dgvTopSanPham);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1837, 945);
            this.panel2.TabIndex = 1;
            // ======================= BỘ LỌC =======================

            // lblTuNgay
            this.lblTuNgay.AutoSize = true;

            this.lblTuNgay.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10.2F
                );

            this.lblTuNgay.Location =
                new System.Drawing.Point(980, 35);

            this.lblTuNgay.Text =
                "Từ ngày:";


            // dtpTuNgay
            this.dtpTuNgay.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10.2F
                );

            this.dtpTuNgay.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.dtpTuNgay.Location =
                new System.Drawing.Point(1060, 30);

            this.dtpTuNgay.Name =
                "dtpTuNgay";

            this.dtpTuNgay.Size =
                new System.Drawing.Size(170, 30);


            // lblDenNgay
            this.lblDenNgay.AutoSize = true;

            this.lblDenNgay.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10.2F
                );

            this.lblDenNgay.Location =
                new System.Drawing.Point(1260, 35);

            this.lblDenNgay.Text =
                "Đến ngày:";


            // dtpDenNgay
            this.dtpDenNgay.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10.2F
                );

            this.dtpDenNgay.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.dtpDenNgay.Location =
                new System.Drawing.Point(1355, 30);

            this.dtpDenNgay.Name =
                "dtpDenNgay";

            this.dtpDenNgay.Size =
                new System.Drawing.Size(170, 30);


            // btnTaiLai
            this.btnTaiLai.BackColor =
                System.Drawing.Color.DarkRed;

            this.btnTaiLai.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10.2F
                );

            this.btnTaiLai.ForeColor =
                System.Drawing.Color.White;

            this.btnTaiLai.Location =
                new System.Drawing.Point(1550, 25);

            this.btnTaiLai.Name =
                "btnTaiLai";

            this.btnTaiLai.Size =
                new System.Drawing.Size(100, 40);

            this.btnTaiLai.Text =
                "Tải lại";

            this.btnTaiLai.UseVisualStyleBackColor =
                false;

            this.btnTaiLai.Click +=
                new System.EventHandler(
                    this.btnTaiLai_Click
                );


            // btnLoc
            this.btnLoc.BackColor =
                System.Drawing.Color.DarkRed;

            this.btnLoc.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10.2F
                );

            this.btnLoc.ForeColor =
                System.Drawing.Color.White;

            this.btnLoc.Location =
                new System.Drawing.Point(1670, 25);

            this.btnLoc.Name =
                "btnLoc";

            this.btnLoc.Size =
                new System.Drawing.Size(100, 40);

            this.btnLoc.Text =
                "Lọc";

            this.btnLoc.UseVisualStyleBackColor =
                false;

            this.btnLoc.Click +=
                new System.EventHandler(
                    this.btnLoc_Click
                );


            // btnXuatExcel
            this.btnXuatExcel.BackColor =
                System.Drawing.Color.DarkRed;

            this.btnXuatExcel.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10.2F
                );

            this.btnXuatExcel.ForeColor =
                System.Drawing.Color.White;

            this.btnXuatExcel.Location =
                new System.Drawing.Point(1790, 25);

            this.btnXuatExcel.Name =
                "btnXuatExcel";

            this.btnXuatExcel.Size =
                new System.Drawing.Size(140, 40);

            this.btnXuatExcel.Text =
                "Xuất Excel";

            this.btnXuatExcel.UseVisualStyleBackColor =
                false;

            this.btnXuatExcel.Click +=
                new System.EventHandler(
                    this.btnXuatExcel_Click
                );

            // ===== lblTongQuan =====

            this.lblTongQuan.AutoSize = false;

            this.lblTongQuan.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    11F,
                    System.Drawing.FontStyle.Bold
                );

            this.lblTongQuan.ForeColor =
                System.Drawing.Color.DarkRed;

            this.lblTongQuan.Location =
                new System.Drawing.Point(200, 70);

            this.lblTongQuan.Name =
                "lblTongQuan";

            this.lblTongQuan.Size =
                new System.Drawing.Size(520, 25);

            this.lblTongQuan.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTongQuan.Text =
                "TỔNG QUAN HIỆN TẠI";


            // ===== lblTheoKy =====

            this.lblTheoKy.AutoSize = false;

            this.lblTheoKy.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    11F,
                    System.Drawing.FontStyle.Bold
                );

            this.lblTheoKy.ForeColor =
                System.Drawing.Color.DarkRed;

            this.lblTheoKy.Location =
                new System.Drawing.Point(1200, 70);

            this.lblTheoKy.Name =
                "lblTheoKy";

            this.lblTheoKy.Size =
                new System.Drawing.Size(520, 25);

            this.lblTheoKy.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTheoKy.Text =
                "THỐNG KÊ THEO KỲ LỌC";


            // ===== panelTongSanPham =====

            this.panelTongSanPham.Location =
                new System.Drawing.Point(30, 95);


            // ===== panelTongNCC =====

            this.panelTongNCC.Location =
                new System.Drawing.Point(300, 95);


            // ===== panelTongTonKho =====

            this.panelTongTonKho.Location =
                new System.Drawing.Point(570, 95);


            // ===== panelTongDonHang =====

            this.panelTongDonHang.Location =
                new System.Drawing.Point(900, 95);


            // ===== panelTongPhieuNhap =====

            this.panelTongPhieuNhap.Location =
                new System.Drawing.Point(1190, 95);


            // ===== panelTongDoanhThu =====

            this.panelTongDoanhThu.Location =
                new System.Drawing.Point(1480, 95);

            // Các panel tổng giữ nguyên
            this.panelTongSanPham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongSanPham.Controls.Add(this.lblTongSanPham);
            this.panelTongSanPham.Controls.Add(this.lblTextTongSanPham);
            this.panelTongSanPham.Location = new System.Drawing.Point(30, 95);
            this.panelTongSanPham.Size = new System.Drawing.Size(250, 95);

            this.lblTextTongSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongSanPham.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongSanPham.Size = new System.Drawing.Size(242, 25);
            this.lblTextTongSanPham.Text = "📦 Tổng sản phẩm";
            this.lblTextTongSanPham.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTongSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblTongSanPham.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTongSanPham.Location = new System.Drawing.Point(3, 45);
            this.lblTongSanPham.Size = new System.Drawing.Size(242, 35);
            this.lblTongSanPham.Text = "0";
            this.lblTongSanPham.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelTongNCC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongNCC.Controls.Add(this.lblTongNCC);
            this.panelTongNCC.Controls.Add(this.lblTextTongNCC);
            this.panelTongNCC.Location = new System.Drawing.Point(320, 95);
            this.panelTongNCC.Size = new System.Drawing.Size(250, 95);

            this.lblTextTongNCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongNCC.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongNCC.Size = new System.Drawing.Size(242, 25);
            this.lblTextTongNCC.Text = "🏬 Nhà cung cấp";
            this.lblTextTongNCC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTongNCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblTongNCC.ForeColor = System.Drawing.Color.Green;
            this.lblTongNCC.Location = new System.Drawing.Point(3, 45);
            this.lblTongNCC.Size = new System.Drawing.Size(242, 35);
            this.lblTongNCC.Text = "0";
            this.lblTongNCC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelTongDonHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongDonHang.Controls.Add(this.lblTongDonHang);
            this.panelTongDonHang.Controls.Add(this.lblTextTongDonHang);
            this.panelTongDonHang.Location = new System.Drawing.Point(1030, 95);
            this.panelTongDonHang.Size = new System.Drawing.Size(250, 95);

            this.lblTextTongDonHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongDonHang.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongDonHang.Size = new System.Drawing.Size(242, 25);
            this.lblTextTongDonHang.Text = "🛒 Đơn hàng";
            this.lblTextTongDonHang.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTongDonHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblTongDonHang.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTongDonHang.Location = new System.Drawing.Point(3, 45);
            this.lblTongDonHang.Size = new System.Drawing.Size(242, 35);
            this.lblTongDonHang.Text = "0";
            this.lblTongDonHang.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelTongPhieuNhap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongPhieuNhap.Controls.Add(this.lblTongPhieuNhap);
            this.panelTongPhieuNhap.Controls.Add(this.lblTextTongPhieuNhap);
            this.panelTongPhieuNhap.Location = new System.Drawing.Point(1320, 95);
            this.panelTongPhieuNhap.Size = new System.Drawing.Size(250, 95);

            this.lblTextTongPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongPhieuNhap.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongPhieuNhap.Size = new System.Drawing.Size(242, 25);
            this.lblTextTongPhieuNhap.Text = "📥 Phiếu nhập";
            this.lblTextTongPhieuNhap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTongPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblTongPhieuNhap.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblTongPhieuNhap.Location = new System.Drawing.Point(3, 45);
            this.lblTongPhieuNhap.Size = new System.Drawing.Size(242, 35);
            this.lblTongPhieuNhap.Text = "0";
            this.lblTongPhieuNhap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelTongTonKho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongTonKho.Controls.Add(this.lblTongTonKho);
            this.panelTongTonKho.Controls.Add(this.lblTextTongTonKho);
            this.panelTongTonKho.Location = new System.Drawing.Point(610, 95);
            this.panelTongTonKho.Size = new System.Drawing.Size(250, 95);

            this.lblTextTongTonKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongTonKho.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongTonKho.Size = new System.Drawing.Size(242, 25);
            this.lblTextTongTonKho.Text = "📦 Tồn kho";
            this.lblTextTongTonKho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTongTonKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblTongTonKho.ForeColor = System.Drawing.Color.Purple;
            this.lblTongTonKho.Location = new System.Drawing.Point(3, 45);
            this.lblTongTonKho.Size = new System.Drawing.Size(242, 35);
            this.lblTongTonKho.Text = "0";
            this.lblTongTonKho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelTongDoanhThu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongDoanhThu.Controls.Add(this.lblTongDoanhThu);
            this.panelTongDoanhThu.Controls.Add(this.lblTextTongDoanhThu);
            this.panelTongDoanhThu.Location = new System.Drawing.Point(1610, 95);
            this.panelTongDoanhThu.Size = new System.Drawing.Size(280, 95);

            this.lblTextTongDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongDoanhThu.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongDoanhThu.Size = new System.Drawing.Size(272, 25);
            this.lblTextTongDoanhThu.Text = "💰 Tổng doanh thu";
            this.lblTextTongDoanhThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTongDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThu.ForeColor = System.Drawing.Color.Brown;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(3, 45);
            this.lblTongDoanhThu.Size = new System.Drawing.Size(272, 35);
            this.lblTongDoanhThu.Text = "0 VNĐ";
            this.lblTongDoanhThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // chartDoanhThu
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);

            this.chartDoanhThu.Anchor =
                ((System.Windows.Forms.AnchorStyles)(
                ((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));

            this.chartDoanhThu.Location = new System.Drawing.Point(30, 225);
            this.chartDoanhThu.Name = "chartDoanhThu";
            this.chartDoanhThu.Size = new System.Drawing.Size(860, 280);
            this.chartDoanhThu.TabIndex = 20;
            this.chartDoanhThu.Text = "chart1";
            // chartNhapHang
            chartArea2.Name = "ChartArea1";
            this.chartNhapHang.ChartAreas.Add(chartArea2);

            this.chartNhapHang.Anchor =
                ((System.Windows.Forms.AnchorStyles)(
                ((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));

            this.chartNhapHang.Location = new System.Drawing.Point(910, 225);
            this.chartNhapHang.Name = "chartNhapHang";
            this.chartNhapHang.Size = new System.Drawing.Size(900, 280);
            this.chartNhapHang.TabIndex = 21;
            this.chartNhapHang.Text = "chart2";

            // chartTopSanPham
            chartArea3.Name = "ChartArea1";
            this.chartTopSanPham.ChartAreas.Add(chartArea3);

            legend1.Name = "Legend1";
            this.chartTopSanPham.Legends.Add(legend1);

            this.chartTopSanPham.Anchor =
                ((System.Windows.Forms.AnchorStyles)(
                ((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)));

            this.chartTopSanPham.Location = new System.Drawing.Point(30, 530);
            this.chartTopSanPham.Name = "chartTopSanPham";
            this.chartTopSanPham.Size = new System.Drawing.Size(560, 380);
            this.chartTopSanPham.TabIndex = 22;
            this.chartTopSanPham.Text = "chart3";

            // dgvTopSanPham
            this.dgvTopSanPham.Anchor =
                ((System.Windows.Forms.AnchorStyles)(
                ((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right));

            this.dgvTopSanPham.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvTopSanPham.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvTopSanPham.Location = new System.Drawing.Point(620, 530);
            this.dgvTopSanPham.Name = "dgvTopSanPham";
            this.dgvTopSanPham.ReadOnly = true;
            this.dgvTopSanPham.RowHeadersWidth = 51;
            this.dgvTopSanPham.RowTemplate.Height = 24;
            this.dgvTopSanPham.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvTopSanPham.Size = new System.Drawing.Size(1180, 380);
            this.dgvTopSanPham.TabIndex = 23;

            // frmThongKe
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1837, 1025);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmThongKe";
            this.Text = "frmThongKe";
            this.Load += new System.EventHandler(this.frmThongKe_Load);

            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelTongSanPham.ResumeLayout(false);
            this.panelTongNCC.ResumeLayout(false);
            this.panelTongDonHang.ResumeLayout(false);
            this.panelTongPhieuNhap.ResumeLayout(false);
            this.panelTongTonKho.ResumeLayout(false);
            this.panelTongDoanhThu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNhapHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSanPham)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnXuatExcel;

        private System.Windows.Forms.Panel panelTongSanPham;
        private System.Windows.Forms.Label lblTongSanPham;
        private System.Windows.Forms.Label lblTextTongSanPham;

        private System.Windows.Forms.Panel panelTongNCC;
        private System.Windows.Forms.Label lblTongNCC;
        private System.Windows.Forms.Label lblTextTongNCC;

        private System.Windows.Forms.Panel panelTongDonHang;
        private System.Windows.Forms.Label lblTongDonHang;
        private System.Windows.Forms.Label lblTextTongDonHang;

        private System.Windows.Forms.Panel panelTongPhieuNhap;
        private System.Windows.Forms.Label lblTongPhieuNhap;
        private System.Windows.Forms.Label lblTextTongPhieuNhap;

        private System.Windows.Forms.Panel panelTongTonKho;
        private System.Windows.Forms.Label lblTongTonKho;
        private System.Windows.Forms.Label lblTextTongTonKho;

        private System.Windows.Forms.Panel panelTongDoanhThu;
        private System.Windows.Forms.Label lblTongDoanhThu;
        private System.Windows.Forms.Label lblTextTongDoanhThu;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNhapHang;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTopSanPham;
        private System.Windows.Forms.DataGridView dgvTopSanPham;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblRoleAdmin;
        private System.Windows.Forms.Label lblCapNhatLuc;
        private System.Windows.Forms.Label lblTongQuan;
        private System.Windows.Forms.Label lblTheoKy;
        private System.Windows.Forms.Panel panelTongQuanBox;
        private System.Windows.Forms.Panel panelTheoKyBox;
        private System.Windows.Forms.Label lblGhiChuLoc;
    }
}