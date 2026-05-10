namespace QuanLyCuaHangGiay.view
{
    partial class ucSanPham
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.picAnh = new System.Windows.Forms.PictureBox();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.cboMau = new System.Windows.Forms.ComboBox();
            this.cboSize = new System.Windows.Forms.ComboBox();
            this.lblGia = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            this.SuspendLayout();
            // 
            // picAnh
            // 
            this.picAnh.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picAnh.Location = new System.Drawing.Point(0, 0);
            this.picAnh.Name = "picAnh";
            this.picAnh.Size = new System.Drawing.Size(163, 140);
            this.picAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnh.TabIndex = 0;
            this.picAnh.TabStop = false;
            // 
            // lblTonKho
            // 
            this.lblTonKho.AutoSize = true;
            this.lblTonKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(230)))));
            this.lblTonKho.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTonKho.Location = new System.Drawing.Point(106, 10);
            this.lblTonKho.Name = "lblTonKho";
            this.lblTonKho.Padding = new System.Windows.Forms.Padding(3);
            this.lblTonKho.Size = new System.Drawing.Size(50, 22);
            this.lblTonKho.TabIndex = 1;
            this.lblTonKho.Text = "Tồn: 0";
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenSP.Location = new System.Drawing.Point(2, 146);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(110, 18);
            this.lblTenSP.TabIndex = 2;
            this.lblTenSP.Text = "Tên Sản Phẩm";
            // 
            // cboMau
            // 
            this.cboMau.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMau.FormattingEnabled = true;
            this.cboMau.Location = new System.Drawing.Point(6, 168);
            this.cboMau.Name = "cboMau";
            this.cboMau.Size = new System.Drawing.Size(51, 24);
            this.cboMau.TabIndex = 3;
            // 
            // cboSize
            // 
            this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSize.FormattingEnabled = true;
            this.cboSize.Location = new System.Drawing.Point(63, 168);
            this.cboSize.Name = "cboSize";
            this.cboSize.Size = new System.Drawing.Size(49, 24);
            this.cboSize.TabIndex = 4;
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblGia.ForeColor = System.Drawing.Color.DarkRed;
            this.lblGia.Location = new System.Drawing.Point(5, 206);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(38, 22);
            this.lblGia.TabIndex = 5;
            this.lblGia.Text = "0 đ";
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.Brown;
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(88, 198);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(68, 30);
            this.btnThem.TabIndex = 6;
            this.btnThem.Text = "+ Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(118, 170);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(38, 22);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // ucSanPham
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblTonKho);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.lblGia);
            this.Controls.Add(this.cboSize);
            this.Controls.Add(this.cboMau);
            this.Controls.Add(this.lblTenSP);
            this.Controls.Add(this.picAnh);
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "ucSanPham";
            this.Size = new System.Drawing.Size(162, 231);
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Khai báo các "đồ chơi" có trên Form
        public System.Windows.Forms.PictureBox picAnh;
        public System.Windows.Forms.Label lblTonKho;
        public System.Windows.Forms.Label lblTenSP;
        public System.Windows.Forms.ComboBox cboMau;
        public System.Windows.Forms.ComboBox cboSize;
        public System.Windows.Forms.Label lblGia;
        public System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnReset;
    }
}