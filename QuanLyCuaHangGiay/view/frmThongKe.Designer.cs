namespace QuanLyCuaHangGiay.view
{
    partial class frmThongKe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.cboNam = new System.Windows.Forms.ComboBox();
            this.lblNam = new System.Windows.Forms.Label();
            this.panelTongSanPham = new System.Windows.Forms.Panel();
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
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(62)))), ((int)(((byte)(15)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1837, 80);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(750, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "THỐNG KÊ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnXuatExcel);
            this.panel2.Controls.Add(this.btnLoc);
            this.panel2.Controls.Add(this.btnTaiLai);
            this.panel2.Controls.Add(this.dtpDenNgay);
            this.panel2.Controls.Add(this.lblDenNgay);
            this.panel2.Controls.Add(this.dtpTuNgay);
            this.panel2.Controls.Add(this.lblTuNgay);
            this.panel2.Controls.Add(this.cboNam);
            this.panel2.Controls.Add(this.lblNam);
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
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnXuatExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btnXuatExcel.Location = new System.Drawing.Point(1620, 26);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(140, 40);
            this.btnXuatExcel.TabIndex = 18;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btnLoc.Location = new System.Drawing.Point(1490, 26);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(100, 40);
            this.btnLoc.TabIndex = 17;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTaiLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btnTaiLai.Location = new System.Drawing.Point(1360, 26);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(100, 40);
            this.btnTaiLai.TabIndex = 16;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.UseVisualStyleBackColor = false;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(1115, 35);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 27);
            this.dtpDenNgay.TabIndex = 15;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.lblDenNgay.Location = new System.Drawing.Point(1025, 39);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(84, 20);
            this.lblDenNgay.TabIndex = 14;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(790, 35);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 27);
            this.dtpTuNgay.TabIndex = 13;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.lblTuNgay.Location = new System.Drawing.Point(710, 39);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(74, 20);
            this.lblTuNgay.TabIndex = 12;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // cboNam
            // 
            this.cboNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.cboNam.FormattingEnabled = true;
            this.cboNam.Location = new System.Drawing.Point(560, 34);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new System.Drawing.Size(121, 28);
            this.cboNam.TabIndex = 11;
            this.cboNam.SelectedIndexChanged += new System.EventHandler(this.cboNam_SelectedIndexChanged);
            // 
            // lblNam
            // 
            this.lblNam.AutoSize = true;
            this.lblNam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.lblNam.Location = new System.Drawing.Point(500, 38);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(47, 20);
            this.lblNam.TabIndex = 10;
            this.lblNam.Text = "Năm:";
            // 
            // panelTongSanPham
            // 
            this.panelTongSanPham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongSanPham.Controls.Add(this.lblTongSanPham);
            this.panelTongSanPham.Controls.Add(this.lblTextTongSanPham);
            this.panelTongSanPham.Location = new System.Drawing.Point(30, 95);
            this.panelTongSanPham.Name = "panelTongSanPham";
            this.panelTongSanPham.Size = new System.Drawing.Size(250, 95);
            this.panelTongSanPham.TabIndex = 0;
            // 
            // lblTongSanPham
            // 
            this.lblTongSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblTongSanPham.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTongSanPham.Location = new System.Drawing.Point(3, 45);
            this.lblTongSanPham.Name = "lblTongSanPham";
            this.lblTongSanPham.Size = new System.Drawing.Size(242, 35);
            this.lblTongSanPham.TabIndex = 1;
            this.lblTongSanPham.Text = "0";
            this.lblTongSanPham.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTextTongSanPham
            // 
            this.lblTextTongSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongSanPham.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongSanPham.Name = "lblTextTongSanPham";
            this.lblTextTongSanPham.Size = new System.Drawing.Size(242, 25);
            this.lblTextTongSanPham.TabIndex = 0;
            this.lblTextTongSanPham.Text = "Tổng sản phẩm";
            this.lblTextTongSanPham.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTongNCC
            // 
            this.panelTongNCC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongNCC.Controls.Add(this.lblTongNCC);
            this.panelTongNCC.Controls.Add(this.lblTextTongNCC);
            this.panelTongNCC.Location = new System.Drawing.Point(320, 95);
            this.panelTongNCC.Name = "panelTongNCC";
            this.panelTongNCC.Size = new System.Drawing.Size(250, 95);
            this.panelTongNCC.TabIndex = 1;
            // 
            // lblTongNCC
            // 
            this.lblTongNCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblTongNCC.ForeColor = System.Drawing.Color.Green;
            this.lblTongNCC.Location = new System.Drawing.Point(3, 45);
            this.lblTongNCC.Name = "lblTongNCC";
            this.lblTongNCC.Size = new System.Drawing.Size(242, 35);
            this.lblTongNCC.TabIndex = 1;
            this.lblTongNCC.Text = "0";
            this.lblTongNCC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTextTongNCC
            // 
            this.lblTextTongNCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongNCC.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongNCC.Name = "lblTextTongNCC";
            this.lblTextTongNCC.Size = new System.Drawing.Size(242, 25);
            this.lblTextTongNCC.TabIndex = 0;
            this.lblTextTongNCC.Text = "Nhà cung cấp";
            this.lblTextTongNCC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTongDonHang
            // 
            this.panelTongDonHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongDonHang.Controls.Add(this.lblTongDonHang);
            this.panelTongDonHang.Controls.Add(this.lblTextTongDonHang);
            this.panelTongDonHang.Location = new System.Drawing.Point(610, 95);
            this.panelTongDonHang.Name = "panelTongDonHang";
            this.panelTongDonHang.Size = new System.Drawing.Size(250, 95);
            this.panelTongDonHang.TabIndex = 2;
            // 
            // lblTongDonHang
            // 
            this.lblTongDonHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblTongDonHang.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTongDonHang.Location = new System.Drawing.Point(3, 45);
            this.lblTongDonHang.Name = "lblTongDonHang";
            this.lblTongDonHang.Size = new System.Drawing.Size(242, 35);
            this.lblTongDonHang.TabIndex = 1;
            this.lblTongDonHang.Text = "0";
            this.lblTongDonHang.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTextTongDonHang
            // 
            this.lblTextTongDonHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongDonHang.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongDonHang.Name = "lblTextTongDonHang";
            this.lblTextTongDonHang.Size = new System.Drawing.Size(242, 25);
            this.lblTextTongDonHang.TabIndex = 0;
            this.lblTextTongDonHang.Text = "Đơn hàng";
            this.lblTextTongDonHang.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTongPhieuNhap
            // 
            this.panelTongPhieuNhap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongPhieuNhap.Controls.Add(this.lblTongPhieuNhap);
            this.panelTongPhieuNhap.Controls.Add(this.lblTextTongPhieuNhap);
            this.panelTongPhieuNhap.Location = new System.Drawing.Point(900, 95);
            this.panelTongPhieuNhap.Name = "panelTongPhieuNhap";
            this.panelTongPhieuNhap.Size = new System.Drawing.Size(250, 95);
            this.panelTongPhieuNhap.TabIndex = 3;
            // 
            // lblTongPhieuNhap
            // 
            this.lblTongPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblTongPhieuNhap.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblTongPhieuNhap.Location = new System.Drawing.Point(3, 45);
            this.lblTongPhieuNhap.Name = "lblTongPhieuNhap";
            this.lblTongPhieuNhap.Size = new System.Drawing.Size(242, 35);
            this.lblTongPhieuNhap.TabIndex = 1;
            this.lblTongPhieuNhap.Text = "0";
            this.lblTongPhieuNhap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTextTongPhieuNhap
            // 
            this.lblTextTongPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongPhieuNhap.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongPhieuNhap.Name = "lblTextTongPhieuNhap";
            this.lblTextTongPhieuNhap.Size = new System.Drawing.Size(242, 25);
            this.lblTextTongPhieuNhap.TabIndex = 0;
            this.lblTextTongPhieuNhap.Text = "Phiếu nhập";
            this.lblTextTongPhieuNhap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTongTonKho
            // 
            this.panelTongTonKho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongTonKho.Controls.Add(this.lblTongTonKho);
            this.panelTongTonKho.Controls.Add(this.lblTextTongTonKho);
            this.panelTongTonKho.Location = new System.Drawing.Point(1190, 95);
            this.panelTongTonKho.Name = "panelTongTonKho";
            this.panelTongTonKho.Size = new System.Drawing.Size(250, 95);
            this.panelTongTonKho.TabIndex = 4;
            // 
            // lblTongTonKho
            // 
            this.lblTongTonKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblTongTonKho.ForeColor = System.Drawing.Color.Purple;
            this.lblTongTonKho.Location = new System.Drawing.Point(3, 45);
            this.lblTongTonKho.Name = "lblTongTonKho";
            this.lblTongTonKho.Size = new System.Drawing.Size(242, 35);
            this.lblTongTonKho.TabIndex = 1;
            this.lblTongTonKho.Text = "0";
            this.lblTongTonKho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTextTongTonKho
            // 
            this.lblTextTongTonKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongTonKho.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongTonKho.Name = "lblTextTongTonKho";
            this.lblTextTongTonKho.Size = new System.Drawing.Size(242, 25);
            this.lblTextTongTonKho.TabIndex = 0;
            this.lblTextTongTonKho.Text = "Tồn kho";
            this.lblTextTongTonKho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTongDoanhThu
            // 
            this.panelTongDoanhThu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTongDoanhThu.Controls.Add(this.lblTongDoanhThu);
            this.panelTongDoanhThu.Controls.Add(this.lblTextTongDoanhThu);
            this.panelTongDoanhThu.Location = new System.Drawing.Point(1480, 95);
            this.panelTongDoanhThu.Name = "panelTongDoanhThu";
            this.panelTongDoanhThu.Size = new System.Drawing.Size(280, 95);
            this.panelTongDoanhThu.TabIndex = 5;
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThu.ForeColor = System.Drawing.Color.Brown;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(3, 45);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(272, 35);
            this.lblTongDoanhThu.TabIndex = 1;
            this.lblTongDoanhThu.Text = "0 VNĐ";
            this.lblTongDoanhThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTextTongDoanhThu
            // 
            this.lblTextTongDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTextTongDoanhThu.Location = new System.Drawing.Point(3, 10);
            this.lblTextTongDoanhThu.Name = "lblTextTongDoanhThu";
            this.lblTextTongDoanhThu.Size = new System.Drawing.Size(272, 25);
            this.lblTextTongDoanhThu.TabIndex = 0;
            this.lblTextTongDoanhThu.Text = "Tổng doanh thu";
            this.lblTextTongDoanhThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chartDoanhThu
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            this.chartDoanhThu.Location = new System.Drawing.Point(30, 225);
            this.chartDoanhThu.Name = "chartDoanhThu";
            this.chartDoanhThu.Size = new System.Drawing.Size(870, 280);
            this.chartDoanhThu.TabIndex = 6;
            this.chartDoanhThu.Text = "chartDoanhThu";
            // 
            // chartNhapHang
            // 
            chartArea2.Name = "ChartArea1";
            this.chartNhapHang.ChartAreas.Add(chartArea2);
            this.chartNhapHang.Location = new System.Drawing.Point(930, 225);
            this.chartNhapHang.Name = "chartNhapHang";
            this.chartNhapHang.Size = new System.Drawing.Size(830, 280);
            this.chartNhapHang.TabIndex = 7;
            this.chartNhapHang.Text = "chartNhapHang";
            // 
            // chartTopSanPham
            // 
            chartArea3.Name = "ChartArea1";
            this.chartTopSanPham.ChartAreas.Add(chartArea3);
            legend1.Name = "Legend1";
            this.chartTopSanPham.Legends.Add(legend1);
            this.chartTopSanPham.Location = new System.Drawing.Point(30, 530);
            this.chartTopSanPham.Name = "chartTopSanPham";
            this.chartTopSanPham.Size = new System.Drawing.Size(700, 360);
            this.chartTopSanPham.TabIndex = 8;
            this.chartTopSanPham.Text = "chartTopSanPham";
            // 
            // dgvTopSanPham
            // 
            this.dgvTopSanPham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopSanPham.Location = new System.Drawing.Point(760, 530);
            this.dgvTopSanPham.Name = "dgvTopSanPham";
            this.dgvTopSanPham.ReadOnly = true;
            this.dgvTopSanPham.RowHeadersWidth = 51;
            this.dgvTopSanPham.RowTemplate.Height = 24;
            this.dgvTopSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopSanPham.Size = new System.Drawing.Size(1000, 360);
            this.dgvTopSanPham.TabIndex = 9;
            // 
            // frmThongKe
            // 
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNam;
        private System.Windows.Forms.ComboBox cboNam;
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
    }
}