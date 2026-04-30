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
            // Khóa ô ID và ô Số lượng không cho người dùng tự gõ
            textBox1.ReadOnly = true;
            soluong.ReadOnly = true;
            soluong.Text = "0"; // Mặc định hiển thị là 0

            LoadComboboxTrangThai();
            LoadComboboxDanhMuc();
            LoadComboboxLocDanhMuc();
            LoadData();

            // Hiển thị ID Sản phẩm tiếp theo khi vừa mở Form
            LoadNextId();
        }

        #region Các hàm hỗ trợ nạp dữ liệu (Helpers)

        private void LoadNextId()
        {
            int nextId = productController.GetNextProductId();
            textBox1.Text = nextId.ToString();
        }

        private void LoadComboboxTrangThai()
        {
            listtt.Items.Clear();
            listtt.Items.Add("active");
            listtt.Items.Add("inactive");
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
            // 1. Đổ dữ liệu từ SQL vào bảng
            dataGridView1.DataSource = productController.GetAllProducts();

            // 2. Tiến hành đổi tên cột (Viết Tiếng Việt có dấu)
            if (dataGridView1.Columns.Count > 0)
            {
                // Cú pháp: dataGridView1.Columns["Tên_cột_trong_SQL"].HeaderText = "Tên hiển thị trên giao diện";
                dataGridView1.Columns["id"].HeaderText = "Mã SP";
                dataGridView1.Columns["tenSP"].HeaderText = "Tên Sản Phẩm";
                dataGridView1.Columns["gia"].HeaderText = "Giá Tiền";
                dataGridView1.Columns["mau"].HeaderText = "Màu Sắc";
                dataGridView1.Columns["kichco"].HeaderText = "Kích Cỡ";
                dataGridView1.Columns["trangthai"].HeaderText = "Trạng Thái";
                dataGridView1.Columns["ngayTao"].HeaderText = "Ngày Tạo";
                dataGridView1.Columns["tenDanhMuc"].HeaderText = "Danh Mục";
                dataGridView1.Columns["anh"].HeaderText = "Tên Ảnh ";

                // Cột số lượng (Kiểm tra xem CSDL đã có cột này chưa rồi mới đổi tên để tránh lỗi)
                if (dataGridView1.Columns.Contains("soLuong"))
                {
                    dataGridView1.Columns["soLuong"].HeaderText = "Số Lượng";
                }

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

            // 2. Kiểm tra Giá tiền
            decimal checkGia;
            if (string.IsNullOrWhiteSpace(gia.Text) || !decimal.TryParse(gia.Text, out checkGia) || checkGia < 0)
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

            // 4. Kiểm tra Kích Cỡ (Khoảng 20-50, cho phép thập phân)
            decimal checkKichCo;
            if (string.IsNullOrWhiteSpace(kichco.Text) || !decimal.TryParse(kichco.Text.Trim(), out checkKichCo) || checkKichCo < 20 || checkKichCo > 50)
            {
                MessageBox.Show("Kích cỡ giày không hợp lệ!\nVui lòng nhập số trong khoảng từ 20 đến 50 (VD: 39 hoặc 39.5).", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kichco.Focus();
                return false;
            }

            //  Ràng buộc nghiêm ngặt chỉ cho phép tối đa 1 số sau dấu phẩy
            int decimalPlaces = BitConverter.GetBytes(decimal.GetBits(checkKichCo)[3])[2];
            if (decimalPlaces > 1)
            {
                MessageBox.Show("Kích cỡ chỉ được phép có tối đa 1 số sau dấu phẩy (VD: 39.5)!", "Cảnh báo nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            return true;
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
            if (idSanPhamHienTai > 0)
            {
                MessageBox.Show("Bạn đang chọn một sản phẩm đã có sẵn!\n- Nếu muốn thay đổi thông tin, hãy bấm nút [Sửa].\n- Nếu muốn thêm sản phẩm mới hoàn toàn, hãy bấm nút [Làm mới] trước khi thêm.", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
                button5_Click(sender, e);
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

            if (!ValidateData()) return;

            string ten = tensp.Text.Trim();
            decimal giaTien = decimal.Parse(gia.Text.Trim());
            string mauSac = mau.Text.Trim();
            string kichThuoc = kichco.Text.Trim();
            int idDanhMuc = Convert.ToInt32(listdm.SelectedValue);
            string trangThai = listtt.SelectedItem.ToString();

            // ĐỌC LẠI SỐ LƯỢNG TỪ Ô TEXTBOX ĐỂ TRUYỀN XUỐNG CONTROLLER
            int soLuongCu = 0;
            if (!string.IsNullOrEmpty(soluong.Text))
            {
                soLuongCu = Convert.ToInt32(soluong.Text);
            }

            tenAnhLuuDB = XulyLuuAnh();

            // GỌI HÀM UPDATE VÀ TRUYỀN THÊM soLuongCu VÀO CUỐI
            bool isSuccess = productController.UpdateProduct(idSanPhamHienTai, ten, giaTien, tenAnhLuuDB, mauSac, kichThuoc, idDanhMuc, trangThai, soLuongCu);

            if (isSuccess)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                button5_Click(sender, e);
            }
        }

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
                    if (productController.DeleteProduct(idSanPhamHienTai))
                    {
                        if (!string.IsNullOrEmpty(tenAnhLuuDB))
                        {
                            if (picture.Image != null)
                            {
                                picture.Image.Dispose();
                                picture.Image = null;
                            }

                            string thuMucGocProject = Directory.GetParent(Application.StartupPath).Parent.FullName;
                            string duongDanAnhCuaSP = Path.Combine(thuMucGocProject, "Images", tenAnhLuuDB);

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

            // LÀM MỚI SỐ LƯỢNG VỀ 0
            soluong.Text = "0";

            timkiem.TextChanged -= (s, ev) => ThucHienLocChung();
            timkiem.Clear();
            timkiem.TextChanged += (s, ev) => ThucHienLocChung();

            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;

            picture.Image = null;
            duongDanAnhGoc = "";
            tenAnhLuuDB = "";
            idSanPhamHienTai = -1;

            LoadData();
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

                    textBox1.Text = idSanPhamHienTai.ToString();
                    tensp.Text = row.Cells["tenSP"].Value.ToString();
                    gia.Text = row.Cells["gia"].Value.ToString();
                    mau.Text = row.Cells["mau"].Value.ToString();
                    kichco.Text = row.Cells["kichco"].Value.ToString();

                    listdm.Text = row.Cells["tenDanhMuc"].Value.ToString();
                    listtt.Text = row.Cells["trangthai"].Value.ToString();

                    // HIỂN THỊ SỐ LƯỢNG LÊN Ô TEXTBOX
                    if (row.Cells["soLuong"].Value != null)
                    {
                        soluong.Text = row.Cells["soLuong"].Value.ToString();
                    }

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
        private void label10_Click(object sender, EventArgs e) { }

        #endregion
    }
}