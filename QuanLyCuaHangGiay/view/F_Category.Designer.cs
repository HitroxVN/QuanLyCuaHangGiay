namespace QuanLyCuaHangGiay.view
{
    partial class F_Category
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listtt = new System.Windows.Forms.ComboBox();
            this.tendm = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timkiem = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên danh mục :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Trạng thái :";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // listtt
            // 
            this.listtt.FormattingEnabled = true;
            this.listtt.Location = new System.Drawing.Point(120, 244);
            this.listtt.Name = "listtt";
            this.listtt.Size = new System.Drawing.Size(191, 24);
            this.listtt.TabIndex = 2;
            // 
            // tendm
            // 
            this.tendm.Location = new System.Drawing.Point(120, 167);
            this.tendm.Name = "tendm";
            this.tendm.Size = new System.Drawing.Size(191, 22);
            this.tendm.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(49, 428);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(550, 84);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chức năng";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(438, 21);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(91, 57);
            this.button5.TabIndex = 3;
            this.button5.Text = "Làm mới";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(309, 21);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 57);
            this.button4.TabIndex = 2;
            this.button4.Text = "Xóa";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(165, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 57);
            this.button3.TabIndex = 1;
            this.button3.Text = "Sửa";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(28, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 57);
            this.button2.TabIndex = 0;
            this.button2.Text = "Thêm";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // timkiem
            // 
            this.timkiem.Location = new System.Drawing.Point(328, 102);
            this.timkiem.Name = "timkiem";
            this.timkiem.Size = new System.Drawing.Size(348, 22);
            this.timkiem.TabIndex = 21;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(703, 102);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(121, 22);
            this.button6.TabIndex = 20;
            this.button6.Text = "Tìm kiếm";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(328, 167);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(802, 255);
            this.dataGridView1.TabIndex = 22;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Firebrick;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1194, 73);
            this.panel1.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Firebrick;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Lavender;
            this.label7.Location = new System.Drawing.Point(13, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(386, 32);
            this.label7.TabIndex = 0;
            this.label7.Text = "Quản lý thông tin danh mục";
            // 
            // F_Category
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 551);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.timkiem);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tendm);
            this.Controls.Add(this.listtt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "F_Category";
            this.Text = "F_Category";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox listtt;
        private System.Windows.Forms.TextBox tendm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox timkiem;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
    }
}