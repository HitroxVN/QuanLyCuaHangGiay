using System;
using System.Data;
using System.Windows.Forms;
using QuanLyCuaHangGiay.controller; // Gọi Controller

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
            LoadComboboxTrangThai();
            LoadData();
        }

        #region Các hàm hỗ trợ
        private void LoadComboboxTrangThai()
        {
            listtt.Items.Clear();
            listtt.Items.Add("Active");
            listtt.Items.Add("Inactive");
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

        #region Sự kiện Nút bấm (CRUD)

        // Nút: Thêm
        private void button2_Click(object sender, EventArgs e)
        {
            string ten = tendm.Text;
            string trangThai = listtt.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(ten))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!");
                return;
            }

            bool isSuccess = categoryController.AddCategory(ten, trangThai);

            if (isSuccess)
            {
                MessageBox.Show("Thêm danh mục thành công!");
                LoadData();
                button5_Click(sender, e); // Làm mới form
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }

        // Nút: Sửa
        private void button3_Click(object sender, EventArgs e)
        {
            if (idDanhMucHienTai <= 0)
            {
                MessageBox.Show("Vui lòng chọn một danh mục từ bảng để sửa!");
                return;
            }

            string ten = tendm.Text;
            string trangThai = listtt.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(ten))
            {
                MessageBox.Show("Tên danh mục không được để trống!");
                return;
            }

            bool isSuccess = categoryController.UpdateCategory(idDanhMucHienTai, ten, trangThai);

            if (isSuccess)
            {
                MessageBox.Show("Cập nhật danh mục thành công!");
                LoadData();
                button5_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        // Nút: Xóa
        private void button4_Click(object sender, EventArgs e)
        {
            if (idDanhMucHienTai <= 0)
            {
                MessageBox.Show("Vui lòng chọn một danh mục để xóa!");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xóa danh mục này? (Lưu ý: Nếu danh mục đã có sản phẩm thì không thể xóa)", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    if (categoryController.DeleteCategory(idDanhMucHienTai))
                    {
                        MessageBox.Show("Xóa thành công!");
                        LoadData();
                        button5_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa danh mục này vì đang có sản phẩm thuộc danh mục này!\nChi tiết: " + ex.Message, "Lỗi Ràng Buộc Dữ Liệu");
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

            LoadData(); // Nạp lại toàn bộ bảng
        }

        // Sự kiện: Tìm kiếm tự động khi đang gõ
        private void timkiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = timkiem.Text;
            dataGridView1.DataSource = categoryController.SearchCategory(keyword);
        }

        // Sự kiện: Click vào dòng trên DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                idDanhMucHienTai = Convert.ToInt32(row.Cells["id"].Value);
                tendm.Text = row.Cells["tenDanhMuc"].Value.ToString();
                listtt.Text = row.Cells["trangthai"].Value.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)

        {
        }
        private void label2_Click(object sender, EventArgs e)

        {

        }
        #endregion
    }
}