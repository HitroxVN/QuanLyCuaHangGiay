using QuanLyCuaHangGiay.controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace QuanLyCuaHangGiay.view
{
    public partial class F_Category : Form
    {
        private CategoryController categoryController = new CategoryController();
        private int idDanhMucHienTai = -1;

        public F_Category()
        {
            InitializeComponent();

            // gọi hàm load 
            this.Load += F_Category_Load;
            button2.Click += button2_Click; // Thêm
            button3.Click += button3_Click; // Sửa
            button4.Click += button4_Click; // Xóa
            button5.Click += button5_Click; // Làm mới

            // Sự kiện tìm kiếm và lọc
            timkiem.TextChanged += timkiem_TextChanged;
            // chọn lọc 
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            // Sự kiện click vào ô trong DataGridView để hiển thị thông tin lên form
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void F_Category_Load(object sender, EventArgs e)
        {
            // không cho sửa ô id 
            textBox1.ReadOnly = true;

            // Load trạng thái cho Thêm/Sửa
            listtt.Items.Clear();
            listtt.Items.Add("active");
            listtt.Items.Add("inactive");
            listtt.SelectedIndex = 0;

            // Load trạng thái cho chức năng Lọc (comboBox1)
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Tất cả");
            comboBox1.Items.Add("active");
            comboBox1.Items.Add("inactive");
            comboBox1.SelectedIndex = 0;

            LoadData();
            LoadNextId();

            // PHÂN QUYỀN GIAO DIỆN NHÂN VIÊN
            if (QuanLyCuaHangGiay.util.Authorization.IsStaff())
            {
                button2.Visible = false; 
                button3.Visible = false; 
                button4.Visible = false; 
                button5.Visible = false;
                groupBox1.Visible = false;

                tendm.Enabled = false;   
                listtt.Enabled = false;  
            }
        }

        private void LoadNextId()
        {
            textBox1.Text = categoryController.GetNextCategoryId().ToString();
        }

        private void LoadData()
        {
            dataGridView1.DataSource = categoryController.GetAllCategories();
            FormatGrid();
        }

        private void FormatGrid()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["id"].HeaderText = "Mã Danh Mục";
                dataGridView1.Columns["tenDanhMuc"].HeaderText = "Tên Danh Mục";
                dataGridView1.Columns["ngayTao"].HeaderText = "Ngày Tạo";
                dataGridView1.Columns["trangthai"].HeaderText = "Trạng Thái";
                dataGridView1.Columns["tenDanhMuc"].Width = 200;
            }
        }

        // HÀM CHUNG CHO CẢ TÌM KIẾM VÀ LỌC TRẠNG THÁI
        private void LocVaTimKiem()
        {
            string keyword = timkiem.Text.Trim();
            if (keyword == "Tìm kiếm theo tên danh mục ...")
            {
                keyword = ""; 
            }
            string status = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "Tất cả";

            dataGridView1.DataSource = categoryController.SearchCategory(keyword, status);
        }

        private void timkiem_TextChanged(object sender, EventArgs e)
        {
            LocVaTimKiem();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocVaTimKiem();
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(tendm.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tendm.Focus();
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (idDanhMucHienTai > 0)
            {
                MessageBox.Show("Đang chọn danh mục cũ. Vui lòng bấm [Làm mới] trước khi thêm.", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateData()) return;

            string ten = tendm.Text.Trim();

            // Kiểm tra trùng lặp Tên Danh Mục
            DataTable dtAll = categoryController.GetAllCategories();
            foreach (DataRow row in dtAll.Rows)
            {
                if (row["tenDanhMuc"].ToString().Equals(ten, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Tên danh mục này ĐÃ TỒN TẠI!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tendm.Focus();
                    return;
                }
            }

            string trangThai = listtt.SelectedItem.ToString();
            if (categoryController.AddCategory(ten, trangThai))
            {
                MessageBox.Show("Thêm danh mục thành công!");
                button5_Click(sender, e);
            }
            else MessageBox.Show("Thêm thất bại!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (idDanhMucHienTai <= 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục để sửa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateData()) return;

            string ten = tendm.Text.Trim();

            // Kiểm tra trùng lặp
            DataTable dtAll = categoryController.GetAllCategories();
            foreach (DataRow row in dtAll.Rows)
            {
                if (Convert.ToInt32(row["id"]) == idDanhMucHienTai) continue;
                if (row["tenDanhMuc"].ToString().Equals(ten, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Tên danh mục này ĐÃ TRÙNG với danh mục khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tendm.Focus();
                    return;
                }
            }

            string trangThai = listtt.SelectedItem.ToString();
            if (categoryController.UpdateCategory(idDanhMucHienTai, ten, trangThai))
            {
                MessageBox.Show("Cập nhật thành công!");
                button5_Click(sender, e);
            }
            else MessageBox.Show("Cập nhật thất bại!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (idDanhMucHienTai <= 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục để xóa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (categoryController.DeleteCategory(idDanhMucHienTai))
                    {
                        MessageBox.Show("Xóa thành công!");
                        button5_Click(sender, e);
                    }
                    else MessageBox.Show("Xóa thất bại!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi vì danh mục đang chứa sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tendm.Clear();
            listtt.SelectedIndex = 0;
            timkiem.Clear();
            comboBox1.SelectedIndex = 0; // Reset cả bộ lọc trạng thái
            idDanhMucHienTai = -1;

            LoadData();
            LoadNextId();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (row.Cells["id"].Value != DBNull.Value && row.Cells["id"].Value != null)
                {
                    idDanhMucHienTai = Convert.ToInt32(row.Cells["id"].Value);
                    textBox1.Text = idDanhMucHienTai.ToString();
                    tendm.Text = row.Cells["tenDanhMuc"].Value.ToString();
                    listtt.Text = row.Cells["trangthai"].Value.ToString();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void timkiem_Enter(object sender, EventArgs e)
        {
            // Nếu chữ trong ô đang là chữ gợi ý thì xóa đi và đổi màu chữ thành đen
            if (timkiem.Text == "Tìm kiếm theo tên danh mục ...")
            {
                timkiem.Text = "";
                timkiem.ForeColor = Color.Black;
            }
        }

        private void timkiem_Leave(object sender, EventArgs e)
        {
            // Nếu người dùng không nhập gì cả (ô text trống) thì hiển thị lại chữ gợi ý
            if (string.IsNullOrWhiteSpace(timkiem.Text))
            {
                timkiem.Text = "Tìm kiếm theo tên danh mục ...";
                timkiem.ForeColor = Color.Gray;
            }
        }
    }
}