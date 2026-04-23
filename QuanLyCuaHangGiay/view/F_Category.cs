using System;
using System.Data;
using System.Windows.Forms;
using QuanLyCuaHangGiay.controller;

namespace QuanLyCuaHangGiay.view
{
    public partial class F_Category : Form
    {
        // Khởi tạo Controller
        private CategoryController categoryController = new CategoryController();

        // Biến lưu ID danh mục khi click vào bảng để Sửa/Xóa
        private int idDanhMucHienTai = -1;

        public F_Category()
        {
            InitializeComponent();

            // Gắn sự kiện Load form
            this.Load += F_Category_Load;

            // Gắn sự kiện cho các nút bấm
            button2.Click += button2_Click; // Thêm
            button3.Click += button3_Click; // Sửa
            button4.Click += button4_Click; // Xóa
            button5.Click += button5_Click; // Làm mới

            // Tìm kiếm tự động ngay khi gõ
            timkiem.TextChanged += timkiem_TextChanged;

            // Sự kiện click vào bảng
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void F_Category_Load(object sender, EventArgs e)
        {
            // KHÓA Ô ID: Chuyển ô textBox1 thành chỉ đọc (Chỉ xem, không sửa)
            textBox1.ReadOnly = true;
            // Hoặc có thể dùng: textBox1.Enabled = false; (Nếu bạn muốn nó mờ đi)

            LoadComboboxTrangThai();
            LoadData();

            // Tự động hiển thị ID tiếp theo khi vừa mở Form
            LoadNextId();
        }

        #region Các hàm hỗ trợ nạp dữ liệu

        // HÀM MỚI: Hiển thị ID tiếp theo sẽ được tạo
        private void LoadNextId()
        {
            int nextId = categoryController.GetNextCategoryId();
            textBox1.Text = nextId.ToString();
        }

        private void LoadComboboxTrangThai()
        {
            listtt.Items.Clear();
            listtt.Items.Add("active");
            listtt.Items.Add("inactive");
            listtt.SelectedIndex = 0; // Mặc định chọn Active
        }

        private void LoadData()
        {
            dataGridView1.DataSource = categoryController.GetAllCategories();

            // Đổi tên cột cho đẹp
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["id"].HeaderText = "Mã Danh Mục";
                dataGridView1.Columns["tenDanhMuc"].HeaderText = "Tên Danh Mục";
                dataGridView1.Columns["ngayTao"].HeaderText = "Ngày Tạo";
                dataGridView1.Columns["trangthai"].HeaderText = "Trạng Thái";

                // Chỉnh độ rộng cột cho đẹp
                dataGridView1.Columns["tenDanhMuc"].Width = 200;
            }
        }
        #endregion

        #region HÀM KIỂM TRA DỮ LIỆU ĐẦU VÀO (VALIDATION)

        // Trả về true nếu dữ liệu chuẩn, false nếu có lỗi
        private bool ValidateData()
        {
            // 1. Kiểm tra để trống Tên danh mục
            if (string.IsNullOrWhiteSpace(tendm.Text))
            {
                MessageBox.Show("Ô [Tên Danh Mục] đang bị trống!\nVui lòng nhập tên danh mục trước khi lưu.", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tendm.Focus(); // Tự động đưa con trỏ chuột nhấp nháy vào ô bị lỗi
                return false;
            }

            // 2. Kiểm tra độ dài Tên danh mục
            if (tendm.Text.Length > 100)
            {
                MessageBox.Show("Ô [Tên Danh Mục] quá dài!\nVui lòng nhập dưới 100 ký tự.", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tendm.Focus();
                return false;
            }

            // 3. Kiểm tra Trạng thái
            if (listtt.SelectedIndex == -1 || listtt.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn [Trạng Thái] cho danh mục này!", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listtt.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Sự kiện Nút bấm (CRUD)

        // Nút: Thêm
        private void button2_Click(object sender, EventArgs e)
        {
            // BƯỚC CHẶN 1: Nếu đang chọn danh mục cũ thì không cho thêm
            if (idDanhMucHienTai > 0)
            {
                MessageBox.Show("Bạn đang chọn một danh mục đã có sẵn!\n- Nếu muốn thay đổi thông tin, hãy bấm nút [Sửa].\n- Nếu muốn thêm mới hoàn toàn, hãy bấm nút [Làm mới] trước khi thêm.", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // BƯỚC CHẶN 2: Validation
            if (!ValidateData()) return;

            string ten = tendm.Text.Trim();
            string trangThai = listtt.SelectedItem.ToString();

            bool isSuccess = categoryController.AddCategory(ten, trangThai);

            if (isSuccess)
            {
                MessageBox.Show("Thêm danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                button5_Click(sender, e); // Xóa trắng và Load lại ID mới
            }
            else
            {
                MessageBox.Show("Thêm thất bại. Vui lòng kiểm tra lại thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút: Sửa
        private void button3_Click(object sender, EventArgs e)
        {
            if (idDanhMucHienTai <= 0)
            {
                MessageBox.Show("Vui lòng click chọn một danh mục từ bảng bên dưới để sửa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateData()) return;

            string ten = tendm.Text.Trim();
            string trangThai = listtt.SelectedItem.ToString();

            bool isSuccess = categoryController.UpdateCategory(idDanhMucHienTai, ten, trangThai);

            if (isSuccess)
            {
                MessageBox.Show("Cập nhật danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                button5_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút: Xóa
        private void button4_Click(object sender, EventArgs e)
        {
            if (idDanhMucHienTai <= 0)
            {
                MessageBox.Show("Vui lòng click chọn một danh mục từ bảng bên dưới để xóa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa danh mục này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    if (categoryController.DeleteCategory(idDanhMucHienTai))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        button5_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi Ràng Buộc Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Nút: Làm mới
        private void button5_Click(object sender, EventArgs e)
        {
            tendm.Clear();
            listtt.SelectedIndex = 0;
            timkiem.Clear();
            idDanhMucHienTai = -1;

            LoadData(); // Nạp lại bảng
            LoadNextId(); // GỌI LẠI HÀM NÀY ĐỂ SHOW SỐ ID MỚI NHẤT
        }

        // Sự kiện: Tìm kiếm tự động
        private void timkiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = timkiem.Text.Trim();
            dataGridView1.DataSource = categoryController.SearchCategory(keyword);
        }

        // Sự kiện: Click vào dòng trên DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["id"].Value != DBNull.Value && row.Cells["id"].Value != null)
                {
                    idDanhMucHienTai = Convert.ToInt32(row.Cells["id"].Value);

                    // HIỂN THỊ ID LÊN TEXTBOX KHI CLICK
                    textBox1.Text = idDanhMucHienTai.ToString();

                    tendm.Text = row.Cells["tenDanhMuc"].Value.ToString();
                    listtt.Text = row.Cells["trangthai"].Value.ToString();
                }
            }
        }

        // Các sự kiện rác trên giao diện
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        #endregion

    }
}