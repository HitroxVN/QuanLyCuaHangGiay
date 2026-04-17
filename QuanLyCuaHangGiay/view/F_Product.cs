using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyCuaHangGiay.controller;

namespace QuanLyCuaHangGiay.view
{
    public partial class F_Product : Form
    {
        // Khởi tạo các Controller
        private ProductController productController = new ProductController();
        private CategoryController categoryController = new CategoryController();

        // Các biến lưu trữ tạm thời
        private string duongDanAnhGoc = "";
        private string tenAnhLuuDB = "";
        private int idSanPhamHienTai = -1;

        public F_Product()
        {
            InitializeComponent();

            // Gắn sự kiện Load form
            this.Load += F_Product_Load;

            // Gắn sự kiện cho các nút bấm
            button2.Click += button2_Click; // Thêm
            button3.Click += button3_Click; // Sửa
            button4.Click += button4_Click; // Xóa
            button5.Click += button5_Click; // Làm mới

            // Nút Tìm kiếm cũng gọi chung hàm Lọc Kép
            button6.Click += (s, e) => ThucHienLocChung();

            // Click vào bảng
            dataGridView1.CellClick += dataGridView1_CellClick;

            // Bắt sự kiện khi đang gõ chữ tìm kiếm -> Gọi hàm Lọc Kép
            timkiem.TextChanged += (s, e) => ThucHienLocChung();
        }

        private void F_Product_Load(object sender, EventArgs e)
        {
            LoadComboboxTrangThai();
            LoadComboboxDanhMuc();
            LoadComboboxLocDanhMuc(); // Nạp dữ liệu cho ô Lọc
            LoadData();
        }

        #region Các hàm hỗ trợ nạp dữ liệu (Helpers)

        private void LoadComboboxTrangThai()
        {
            listtt.Items.Clear();
            listtt.Items.Add("Active");
            listtt.Items.Add("Inactive");
            listtt.SelectedIndex = 0;
        }

        private void LoadComboboxDanhMuc()
        {
            DataTable dt = categoryController.GetActiveCategories();
            listdm.DataSource = dt;
            listdm.DisplayMember = "tenDanhMuc";
            listdm.ValueMember = "id";
        }

        // Hàm nạp dữ liệu cho ô Lọc (Có thêm dòng "--- Tất cả ---")
        private void LoadComboboxLocDanhMuc()
        {
            DataTable dtLoc = categoryController.GetActiveCategories();

            DataRow rowAll = dtLoc.NewRow();
            rowAll["id"] = 0;
            rowAll["tenDanhMuc"] = "--- Tất cả ---";
            dtLoc.Rows.InsertAt(rowAll, 0);

            comboBox1.DataSource = dtLoc;
            comboBox1.DisplayMember = "tenDanhMuc";
            comboBox1.ValueMember = "id";
        }

        private void LoadData()
        {
            dataGridView1.DataSource = productController.GetAllProducts();

            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["id"].HeaderText = "Mã SP";
                dataGridView1.Columns["tenSP"].HeaderText = "Tên Sản Phẩm";
                dataGridView1.Columns["gia"].HeaderText = "Giá";
                dataGridView1.Columns["tenDanhMuc"].HeaderText = "Danh Mục";
            }
        }

        private string XulyLuuAnh()
        {
            if (string.IsNullOrEmpty(duongDanAnhGoc)) return tenAnhLuuDB;

            string thuMucGocProject = Directory.GetParent(Application.StartupPath).Parent.FullName;
            string thuMucDich = Path.Combine(thuMucGocProject, "Images");

            if (!Directory.Exists(thuMucDich))
            {
                Directory.CreateDirectory(thuMucDich);
            }

            string tenFile = Path.GetFileName(duongDanAnhGoc);
            string duongDanMoi = Path.Combine(thuMucDich, tenFile);

            try
            {
                if (duongDanAnhGoc != duongDanMoi)
                {
                    File.Copy(duongDanAnhGoc, duongDanMoi, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu ảnh: " + ex.Message);
            }

            return tenFile;
        }

        #endregion

        #region XỬ LÝ LỌC KÉP (Tìm kiếm + Bộ lọc danh mục)

        // HÀM ĐÃ ĐƯỢC SỬA LỖI: Chuyển logic tìm kiếm lên giao diện (Memory)
        private void ThucHienLocChung()
        {
            // Tránh lỗi khi Form đang mở lên, ComboBox chưa kịp có dữ liệu
            if (comboBox1.SelectedValue == null) return;

            // Bước 1: Lấy TẤT CẢ sản phẩm (Dùng GetAllProducts vì nó chắc chắn có chứa cột tenDanhMuc)
            DataTable dtProducts = productController.GetAllProducts();

            string stringLoc = "";
            string keyword = timkiem.Text.Trim();

            // Bước 2: Lọc theo Tên SP (Từ khóa gõ vào ô tìm kiếm)
            if (!string.IsNullOrEmpty(keyword))
            {
                stringLoc = string.Format("tenSP LIKE '%{0}%'", keyword);
            }

            // Bước 3: Ép thêm bộ Lọc danh mục
            int idChon;
            if (int.TryParse(comboBox1.SelectedValue.ToString(), out idChon))
            {
                if (idChon != 0) // Nếu KHÔNG PHẢI là "--- Tất cả ---"
                {
                    string tenDMLoc = comboBox1.Text;

                    if (string.IsNullOrEmpty(stringLoc))
                    {
                        // Nếu chưa có từ khóa tìm kiếm -> Chỉ lọc theo danh mục
                        stringLoc = string.Format("tenDanhMuc = '{0}'", tenDMLoc);
                    }
                    else
                    {
                        // Nếu đã có từ khóa -> Nối thêm điều kiện lọc bằng chữ AND
                        stringLoc += string.Format(" AND tenDanhMuc = '{0}'", tenDMLoc);
                    }
                }
            }

            // Bước 4: Áp dụng bộ lọc kép vào bảng
            dtProducts.DefaultView.RowFilter = stringLoc;
            dataGridView1.DataSource = dtProducts.DefaultView;
        }

        // Sự kiện: Khi chọn danh mục trong ô Lọc -> Gọi hàm Lọc Kép
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThucHienLocChung();
        }

        #endregion

        #region Các sự kiện Nút bấm (CRUD)

        // Chọn Ảnh
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
                ofd.Title = "Chọn ảnh sản phẩm";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    duongDanAnhGoc = ofd.FileName;
                    picture.Image = Image.FromFile(duongDanAnhGoc);
                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        // Thêm
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ten = tensp.Text;
                decimal giaTien = decimal.Parse(gia.Text);
                string mauSac = mau.Text;
                string kichThuoc = kichco.Text;
                int idDanhMuc = Convert.ToInt32(listdm.SelectedValue);
                string trangThai = listtt.SelectedItem.ToString();

                tenAnhLuuDB = XulyLuuAnh();

                bool isSuccess = productController.AddProduct(ten, giaTien, tenAnhLuuDB, mauSac, kichThuoc, idDanhMuc, trangThai);

                if (isSuccess)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    LoadData();
                    button5_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Thêm thất bại. Vui lòng kiểm tra lại thông tin!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho Giá! Lỗi: " + ex.Message);
            }
        }

        // Sửa
        private void button3_Click(object sender, EventArgs e)
        {
            if (idSanPhamHienTai <= 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ bảng bên dưới để sửa!");
                return;
            }

            try
            {
                string ten = tensp.Text;
                decimal giaTien = decimal.Parse(gia.Text);
                string mauSac = mau.Text;
                string kichThuoc = kichco.Text;
                int idDanhMuc = Convert.ToInt32(listdm.SelectedValue);
                string trangThai = listtt.SelectedItem.ToString();

                tenAnhLuuDB = XulyLuuAnh();

                bool isSuccess = productController.UpdateProduct(idSanPhamHienTai, ten, giaTien, tenAnhLuuDB, mauSac, kichThuoc, idDanhMuc, trangThai);

                if (isSuccess)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                    button5_Click(sender, e);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi dữ liệu đầu vào. Vui lòng kiểm tra lại!");
            }
        }

        // Xóa
        private void button4_Click(object sender, EventArgs e)
        {
            if (idSanPhamHienTai <= 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa!");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                if (productController.DeleteProduct(idSanPhamHienTai))
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
        }

        // Làm mới 
        private void button5_Click(object sender, EventArgs e)
        {
            tensp.Clear();
            gia.Clear();
            mau.Clear();
            kichco.Clear();
            listdm.SelectedIndex = 0;
            listtt.SelectedIndex = 0;

            // Xóa ô tìm kiếm mà không kích hoạt gọi database quá nhiều lần
            timkiem.TextChanged -= (s, ev) => ThucHienLocChung();
            timkiem.Clear();
            timkiem.TextChanged += (s, ev) => ThucHienLocChung();

            // Đặt ô lọc về "Tất cả"
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;

            picture.Image = null;
            duongDanAnhGoc = "";
            tenAnhLuuDB = "";
            idSanPhamHienTai = -1;

            LoadData();
        }

        // Bấm vào bảng
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["id"].Value != DBNull.Value && row.Cells["id"].Value != null)
                {
                    idSanPhamHienTai = Convert.ToInt32(row.Cells["id"].Value);
                    tensp.Text = row.Cells["tenSP"].Value.ToString();
                    gia.Text = row.Cells["gia"].Value.ToString();
                    mau.Text = row.Cells["mau"].Value.ToString();
                    kichco.Text = row.Cells["kichco"].Value.ToString();

                    listdm.Text = row.Cells["tenDanhMuc"].Value.ToString();
                    listtt.Text = row.Cells["trangthai"].Value.ToString();

                    tenAnhLuuDB = row.Cells["anh"].Value.ToString();
                    duongDanAnhGoc = "";

                    if (!string.IsNullOrEmpty(tenAnhLuuDB))
                    {
                        string thuMucGocProject = Directory.GetParent(Application.StartupPath).Parent.FullName;
                        string duongDanLoadLen = Path.Combine(thuMucGocProject, "Images", tenAnhLuuDB);

                        if (File.Exists(duongDanLoadLen))
                        {
                            using (FileStream fs = new FileStream(duongDanLoadLen, FileMode.Open, FileAccess.Read))
                            {
                                picture.Image = Image.FromStream(fs);
                            }
                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            picture.Image = null;
                        }
                    }
                    else
                    {
                        picture.Image = null;
                    }
                }
            }
        }

        // --- Các hàm bắt sự kiện rác trên Form Design (Giữ lại để không lỗi Form) ---
        private void button6_Click_1(object sender, EventArgs e) { ThucHienLocChung(); }
        private void label8_Click(object sender, EventArgs e) { }

        #endregion
    }
}