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

            this.Load += F_Product_Load;

            button2.Click += button2_Click; // Thêm
            button3.Click += button3_Click; // Sửa
            button4.Click += button4_Click; // Xóa
            button5.Click += button5_Click; // Làm mới

            button6.Click += (s, e) => ThucHienLocChung();
            dataGridView1.CellClick += dataGridView1_CellClick;
            timkiem.TextChanged += (s, e) => ThucHienLocChung();
        }

        private void F_Product_Load(object sender, EventArgs e)
        {
            // Khóa ô ID không cho người dùng tự gõ
            textBox1.ReadOnly = true;

            LoadComboboxTrangThai();
            LoadComboboxDanhMuc();
            LoadComboboxLocDanhMuc();
            LoadData();

            // Hiển thị ID Sản phẩm tiếp theo khi vừa mở Form
            LoadNextId();
        }

        #region Các hàm hỗ trợ nạp dữ liệu (Helpers)

        // HÀM MỚI: Load ID tiếp theo lên giao diện
        private void LoadNextId()
        {
            int nextId = productController.GetNextProductId();
            textBox1.Text = nextId.ToString();
        }

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

        #region HÀM KIỂM TRA LỖI NHẬP LIỆU (VALIDATION)

        private bool ValidateData()
        {
            // 1. Kiểm tra Tên sản phẩm
            if (string.IsNullOrWhiteSpace(tensp.Text))
            {
                MessageBox.Show("Vui lòng nhập [Tên Sản Phẩm]!", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tensp.Focus();
                return false;
            }

            // 2. Kiểm tra Giá tiền (Không để trống + Phải là số hợp lệ + Lớn hơn 0)
            if (string.IsNullOrWhiteSpace(gia.Text))
            {
                MessageBox.Show("Vui lòng nhập [Giá Tiền]!", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gia.Focus();
                return false;
            }

            decimal checkGia;
            if (!decimal.TryParse(gia.Text, out checkGia) || checkGia < 0)
            {
                MessageBox.Show("Giá tiền phải là một con số hợp lệ và lớn hơn hoặc bằng 0!", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gia.Focus();
                return false;
            }

            // 3. Kiểm tra Màu sắc
            if (string.IsNullOrWhiteSpace(mau.Text))
            {
                MessageBox.Show("Vui lòng nhập [Màu Sắc]!", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mau.Focus();
                return false;
            }

            // 4. Kiểm tra Kích cỡ
            if (string.IsNullOrWhiteSpace(kichco.Text))
            {
                MessageBox.Show("Vui lòng nhập [Kích Cỡ]!", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kichco.Focus();
                return false;
            }

            // 5. Kiểm tra Chọn Danh Mục
            if (listdm.SelectedIndex == -1 || listdm.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn [Danh Mục] cho sản phẩm!", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listdm.Focus();
                return false;
            }

            // 6. Kiểm tra Chọn Trạng Thái
            if (listtt.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn [Trạng Thái] cho sản phẩm!", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listtt.Focus();
                return false;
            }

            return true; // Dữ liệu hoàn hảo!
        }

        #endregion

        #region XỬ LÝ LỌC KÉP (Tìm kiếm + Bộ lọc danh mục)

        private void ThucHienLocChung()
        {
            if (comboBox1.SelectedValue == null) return;

            DataTable dtProducts = productController.GetAllProducts();
            string stringLoc = "";
            string keyword = timkiem.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                stringLoc = string.Format("tenSP LIKE '%{0}%'", keyword);
            }

            int idChon;
            if (int.TryParse(comboBox1.SelectedValue.ToString(), out idChon))
            {
                if (idChon != 0)
                {
                    string tenDMLoc = comboBox1.Text;
                    if (string.IsNullOrEmpty(stringLoc))
                    {
                        stringLoc = string.Format("tenDanhMuc = '{0}'", tenDMLoc);
                    }
                    else
                    {
                        stringLoc += string.Format(" AND tenDanhMuc = '{0}'", tenDMLoc);
                    }
                }
            }

            dtProducts.DefaultView.RowFilter = stringLoc;
            dataGridView1.DataSource = dtProducts.DefaultView;
        }

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
        // Thêm
        private void button2_Click(object sender, EventArgs e)
        {
            // --- BƯỚC CHẶN 1: KIỂM TRA TRẠNG THÁI FORM ---
            // Nếu idSanPhamHienTai > 0 nghĩa là người dùng đang click chọn 1 sản phẩm cũ trong bảng
            if (idSanPhamHienTai > 0)
            {
                MessageBox.Show("Bạn đang chọn một sản phẩm đã có sẵn!\n- Nếu muốn thay đổi thông tin, hãy bấm nút [Sửa].\n- Nếu muốn thêm sản phẩm mới hoàn toàn, hãy bấm nút [Làm mới] trước khi thêm.", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Dừng lại luôn, không chạy code thêm bên dưới nữa
            }

            // --- BƯỚC CHẶN 2: KIỂM TRA LỖI NHẬP LIỆU (VALIDATION) ---
            if (!ValidateData()) return;

            string ten = tensp.Text.Trim();
            decimal giaTien = decimal.Parse(gia.Text.Trim());
            string mauSac = mau.Text.Trim();
            string kichThuoc = kichco.Text.Trim();
            int idDanhMuc = Convert.ToInt32(listdm.SelectedValue);
            string trangThai = listtt.SelectedItem.ToString();

            tenAnhLuuDB = XulyLuuAnh();

            bool isSuccess = productController.AddProduct(ten, giaTien, tenAnhLuuDB, mauSac, kichThuoc, idDanhMuc, trangThai);

            if (isSuccess)
            {
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                button5_Click(sender, e); // Xóa trắng và Load lại ID mới
            }
            else
            {
                MessageBox.Show("Thêm thất bại. Vui lòng kiểm tra lại thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa
        private void button3_Click(object sender, EventArgs e)
        {
            if (idSanPhamHienTai <= 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ bảng bên dưới để sửa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // BƯỚC CHẶN KIỂM TRA LỖI
            if (!ValidateData()) return;

            string ten = tensp.Text.Trim();
            decimal giaTien = decimal.Parse(gia.Text.Trim());
            string mauSac = mau.Text.Trim();
            string kichThuoc = kichco.Text.Trim();
            int idDanhMuc = Convert.ToInt32(listdm.SelectedValue);
            string trangThai = listtt.SelectedItem.ToString();

            tenAnhLuuDB = XulyLuuAnh();

            bool isSuccess = productController.UpdateProduct(idSanPhamHienTai, ten, giaTien, tenAnhLuuDB, mauSac, kichThuoc, idDanhMuc, trangThai);

            if (isSuccess)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                button5_Click(sender, e);
            }
        }

        // Xóa
        // Xóa
        private void button4_Click(object sender, EventArgs e)
        {
            if (idSanPhamHienTai <= 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    // BƯỚC 1: TIẾN HÀNH XÓA SẢN PHẨM TRONG DATABASE TRƯỚC
                    if (productController.DeleteProduct(idSanPhamHienTai))
                    {
                        // BƯỚC 2: NẾU DB XÓA THÀNH CÔNG -> TIẾN HÀNH XÓA ẢNH VẬT LÝ
                        if (!string.IsNullOrEmpty(tenAnhLuuDB))
                        {
                            // 2.1: Gỡ ảnh ra khỏi PictureBox trước để Windows cho phép xóa file
                            if (picture.Image != null)
                            {
                                picture.Image.Dispose();
                                picture.Image = null;
                            }

                            // 2.2: Tìm đường dẫn tới bức ảnh đó trong thư mục Images
                            string thuMucGocProject = Directory.GetParent(Application.StartupPath).Parent.FullName;
                            string duongDanAnhCuaSP = Path.Combine(thuMucGocProject, "Images", tenAnhLuuDB);

                            // 2.3: Nếu file tồn tại thì xóa nó đi
                            if (File.Exists(duongDanAnhCuaSP))
                            {
                                File.Delete(duongDanAnhCuaSP);
                            }
                        }

                        MessageBox.Show("Xóa thành công sản phẩm và dọn sạch ảnh trong thư mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        button5_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    // Lỗi 547: Sản phẩm đã nằm trong hóa đơn
                    if (sqlEx.Number == 547)
                    {
                        MessageBox.Show("Không thể xóa sản phẩm này vì nó đã phát sinh Giao dịch / Nằm trong Đơn hàng cũ!\n\nGiải pháp: Hãy chọn nút [Sửa] và đổi Trạng thái thành Inactive (Ngừng kinh doanh).", "Lỗi Ràng Buộc Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi cơ sở dữ liệu: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            timkiem.TextChanged -= (s, ev) => ThucHienLocChung();
            timkiem.Clear();
            timkiem.TextChanged += (s, ev) => ThucHienLocChung();

            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;

            picture.Image = null;
            duongDanAnhGoc = "";
            tenAnhLuuDB = "";
            idSanPhamHienTai = -1;

            LoadData();

            // CẬP NHẬT LẠI ID MỚI VÀO TEXTBOX
            LoadNextId();
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

                    // HIỂN THỊ ID LÊN TEXTBOX KHI CLICK
                    textBox1.Text = idSanPhamHienTai.ToString();

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

        // --- Các hàm rác (Giữ lại) ---
        private void button6_Click_1(object sender, EventArgs e) { ThucHienLocChung(); }
        private void label8_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        #endregion
    }
}