using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCuaHangGiay.controller;

namespace QuanLyCuaHangGiay.view
{
    public partial class F_Category : Form
    {
        private CategoryController categoryController = new CategoryController();
        private int idDanhMucHienTai = -1;

        // BIẾN LƯU QUYỀN
        private string _quyen = "";

        // HÀM KHỞI TẠO CÓ NHẬN 1 THAM SỐ (ĐỂ SỬA LỖI CS1729)
        public F_Category(string quyenDangNhap = "admin")
        {
            InitializeComponent();

            _quyen = quyenDangNhap.ToLower();

            this.Load += F_Category_Load;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
            button5.Click += button5_Click;
            timkiem.TextChanged += timkiem_TextChanged;
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void F_Category_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;

            // CHIẾN THUẬT MỚI: Nếu không phải "admin" thì cấm đụng vào mọi thứ
            if (_quyen != "admin")
            {
                // 1. Khóa luôn các ô nhập liệu để cấm gõ
                tendm.Enabled = false;
                listtt.Enabled = false;

                // 2. Tắt chức năng của các nút
                button2.Enabled = false; // Khóa Thêm
                button3.Enabled = false; // Khóa Sửa
                button4.Enabled = false; // Khóa Xóa
                button5.Enabled = false; // Khóa Xóa

                // 3. Đổi màu nút thành xám xịt cho người dùng biết là đã bị cấm
                button2.BackColor = Color.LightGray;
                button3.BackColor = Color.LightGray;
                button4.BackColor = Color.LightGray;
            }

            LoadComboboxTrangThai();
            LoadData();
            LoadNextId();
        }

        #region Nạp dữ liệu

        private void LoadNextId()
        {
            textBox1.Text = categoryController.GetNextCategoryId().ToString();
        }

        private void LoadComboboxTrangThai()
        {
            listtt.Items.Clear();
            listtt.Items.Add("active");
            listtt.Items.Add("inactive");
            listtt.SelectedIndex = 0;
        }

        private void LoadData()
        {
            dataGridView1.DataSource = categoryController.GetAllCategories();
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["id"].HeaderText = "Mã Danh Mục";
                dataGridView1.Columns["tenDanhMuc"].HeaderText = "Tên Danh Mục";
                dataGridView1.Columns["ngayTao"].HeaderText = "Ngày Tạo";
                dataGridView1.Columns["trangthai"].HeaderText = "Trạng Thái";
                dataGridView1.Columns["tenDanhMuc"].Width = 200;
            }
        }

        #endregion

        #region Validation

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(tendm.Text))
            {
                MessageBox.Show("Ô [Tên Danh Mục] đang bị trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tendm.Focus();
                return false;
            }
            if (tendm.Text.Length > 100)
            {
                MessageBox.Show("Tên danh mục quá dài (tối đa 100 ký tự)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tendm.Focus();
                return false;
            }
            if (listtt.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Trạng Thái!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listtt.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region Sự kiện nút bấm

        private void button2_Click(object sender, EventArgs e)
        {
            if (idDanhMucHienTai > 0)
            {
                MessageBox.Show("Đang chọn danh mục cũ. Bấm [Làm mới] nếu muốn thêm mới.", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateData()) return;

            string ten = tendm.Text.Trim();
            foreach (DataRow row in categoryController.GetAllCategories().Rows)
            {
                if (row["tenDanhMuc"].ToString().Equals(ten, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Tên danh mục đã tồn tại!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tendm.Focus();
                    return;
                }
            }

            if (categoryController.AddCategory(ten, listtt.SelectedItem.ToString()))
            {
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button5_Click(sender, e);
            }
            else
                MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (idDanhMucHienTai <= 0)
            {
                MessageBox.Show("Vui lòng chọn một danh mục từ bảng để sửa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateData()) return;

            string ten = tendm.Text.Trim();
            foreach (DataRow row in categoryController.GetAllCategories().Rows)
            {
                if (Convert.ToInt32(row["id"]) == idDanhMucHienTai) continue;
                if (row["tenDanhMuc"].ToString().Equals(ten, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Tên danh mục đã bị trùng!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tendm.Focus();
                    return;
                }
            }

            if (categoryController.UpdateCategory(idDanhMucHienTai, ten, listtt.SelectedItem.ToString()))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button5_Click(sender, e);
            }
            else
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (idDanhMucHienTai <= 0)
            {
                MessageBox.Show("Vui lòng chọn một danh mục từ bảng để xóa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn xóa danh mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (categoryController.DeleteCategory(idDanhMucHienTai))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button5_Click(sender, e);
                    }
                    else
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tendm.Clear();
            listtt.SelectedIndex = 0;
            timkiem.Clear();
            idDanhMucHienTai = -1;
            LoadData();
            LoadNextId();
        }

        private void timkiem_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = categoryController.SearchCategory(timkiem.Text.Trim());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Rows[e.RowIndex].IsNewRow) return;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if (row.Cells["id"].Value == null || row.Cells["id"].Value == DBNull.Value) return;

            idDanhMucHienTai = Convert.ToInt32(row.Cells["id"].Value);
            textBox1.Text = idDanhMucHienTai.ToString();
            tendm.Text = row.Cells["tenDanhMuc"].Value.ToString();
            listtt.Text = row.Cells["trangthai"].Value.ToString();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        #endregion

    }
}