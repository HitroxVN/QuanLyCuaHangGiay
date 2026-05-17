using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyCuaHangGiay.controller;

namespace QuanLyCuaHangGiay.view
{
    public partial class F_Product : Form, QuanLyCuaHangGiay.util.IBaseForm
    {
        private ProductController productController = new ProductController();
        private CategoryController categoryController = new CategoryController();

        private string duongDanAnhGoc = "";
        private string tenAnhLuuDB = "";
        private int idSanPhamHienTai = -1;

        public F_Product()
        {
            InitializeComponent();
            this.Load += F_Product_Load;

            // Gắn sự kiện nút bấm
            button1.Click += button1_Click; // Chọn ảnh
            button2.Click += button2_Click; // Thêm
            button3.Click += button3_Click; // Sửa
            button4.Click += button4_Click; // Xóa
            button5.Click += button5_Click; // Làm mới
            button6.Click += (s, e) => LocVaTimKiem();

            // Gắn sự kiện lọc tự động
            timkiem.TextChanged += (s, e) => LocVaTimKiem();
            comboBox1.SelectedIndexChanged += (s, e) => LocVaTimKiem(); // Lọc danh mục
            comboBox2.SelectedIndexChanged += (s, e) => LocVaTimKiem(); // Lọc trạng thái

            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
        }

        private void F_Product_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            soluong.ReadOnly = true;
            soluong.Text = "0";

            LoadAllComboboxes();
            LocVaTimKiem(); // Load dữ liệu lần đầu
            LoadNextId();

            // PHÂN QUYỀN: Staff không được đổi trạng thái
            if (QuanLyCuaHangGiay.util.Authorization.IsStaff())
            {
                listtt.Visible = false; 
                label6.Visible = false; 
            }
        }

        #region 1. HÀM HỖ TRỢ & NẠP DỮ LIỆU
        private void LoadNextId()
        {
            textBox1.Text = productController.GetNextProductId().ToString();
        }

        private void LoadAllComboboxes()
        {
            // 1. Trạng thái Thêm/Sửa (listtt)
            listtt.Items.AddRange(new string[] { "active", "inactive" });
            listtt.SelectedIndex = 0;

            // 2. Danh mục Thêm/Sửa (listdm)
            listdm.DataSource = categoryController.GetActiveCategories();
            listdm.DisplayMember = "tenDanhMuc";
            listdm.ValueMember = "id";

            // 3. Lọc Danh mục (comboBox1)
            DataTable dtLoc = categoryController.GetActiveCategories();
            DataRow rowAll = dtLoc.NewRow();
            rowAll["id"] = 0;
            rowAll["tenDanhMuc"] = "--- Tất cả ---";
            dtLoc.Rows.InsertAt(rowAll, 0);
            comboBox1.DataSource = dtLoc;
            comboBox1.DisplayMember = "tenDanhMuc";
            comboBox1.ValueMember = "id";

            // 4. Lọc Trạng thái (comboBox2)
            comboBox2.Items.AddRange(new string[] { "Tất cả", "active", "inactive" });
            comboBox2.SelectedIndex = 0;
        }

        // Thay thế hoàn toàn ThucHienLocChung cồng kềnh cũ
        private void LocVaTimKiem()
        {
            if (comboBox1.SelectedValue == null) return; // Bỏ qua nếu form đang khởi tạo

            string keyword = timkiem.Text.Trim();
            int idDanhMuc = 0;
            int.TryParse(comboBox1.SelectedValue.ToString(), out idDanhMuc);
            string status = comboBox2.SelectedItem?.ToString() ?? "Tất cả";

            // Đẩy xuống SQL xử lý 1 chạm
            dataGridView1.DataSource = productController.SearchAndFilter(keyword, idDanhMuc, status);
            FormatGrid();
        }

        public void ReloadData()
        {
            LocVaTimKiem();
        }

        private void FormatGrid()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["id"].HeaderText = "Mã SP";
                dataGridView1.Columns["tenSP"].HeaderText = "Tên Sản Phẩm";
                dataGridView1.Columns["gia"].HeaderText = "Giá Tiền";
                dataGridView1.Columns["mau"].HeaderText = "Màu Sắc";
                dataGridView1.Columns["kichco"].HeaderText = "Kích Cỡ";
                dataGridView1.Columns["trangthai"].HeaderText = "Trạng Thái";
                dataGridView1.Columns["ngayTao"].HeaderText = "Ngày Tạo";
                dataGridView1.Columns["tenDanhMuc"].HeaderText = "Danh Mục";
                if (dataGridView1.Columns.Contains("anh")) dataGridView1.Columns["anh"].HeaderText = "Tên Ảnh";
                if (dataGridView1.Columns.Contains("soLuong")) dataGridView1.Columns["soLuong"].HeaderText = "Số Lượng";
            }
        }

        private string XulyLuuAnh()
        {
            if (string.IsNullOrEmpty(duongDanAnhGoc)) return tenAnhLuuDB;

            string thuMucDich = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.FullName, "Images");
            if (!Directory.Exists(thuMucDich)) Directory.CreateDirectory(thuMucDich);

            string tenFile = Path.GetFileName(duongDanAnhGoc);
            string duongDanMoi = Path.Combine(thuMucDich, tenFile);

            try
            {
                if (duongDanAnhGoc != duongDanMoi) File.Copy(duongDanAnhGoc, duongDanMoi, true);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu ảnh: " + ex.Message); }

            return tenFile;
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(tensp.Text)) { MessageBox.Show("Nhập Tên Sản Phẩm!"); tensp.Focus(); return false; }

            if (!decimal.TryParse(gia.Text, out decimal checkGia) || checkGia < 0)
            { MessageBox.Show("Giá tiền không hợp lệ!"); gia.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(mau.Text)) { MessageBox.Show("Nhập Màu Sắc!"); mau.Focus(); return false; }

            if (!decimal.TryParse(kichco.Text.Trim(), out decimal checkKichCo) || checkKichCo < 20 || checkKichCo > 50)
            { MessageBox.Show("Kích cỡ từ 20 đến 50!"); kichco.Focus(); return false; }

            if (BitConverter.GetBytes(decimal.GetBits(checkKichCo)[3])[2] > 1)
            { MessageBox.Show("Kích cỡ tối đa 1 số thập phân (VD: 39.5)!"); kichco.Focus(); return false; }

            if (listdm.SelectedIndex == -1) { MessageBox.Show("Chọn Danh Mục!"); listdm.Focus(); return false; }
            if (listtt.SelectedIndex == -1) { MessageBox.Show("Chọn Trạng Thái!"); listtt.Focus(); return false; }

            return true;
        }
        #endregion

        #region 2. SỰ KIỆN NÚT BẤM (CRUD)
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif", Title = "Chọn ảnh" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    duongDanAnhGoc = ofd.FileName;
                    picture.Image = Image.FromFile(duongDanAnhGoc);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // THÊM
        {
            if (idSanPhamHienTai > 0) { MessageBox.Show("Đang chọn SP cũ, hãy bấm Làm mới trước khi thêm!"); return; }
            if (!ValidateData()) return;

            string ten = tensp.Text.Trim(), mauSac = mau.Text.Trim(), kichThuoc = kichco.Text.Trim();

            // Chặn trùng lặp
            foreach (DataRow row in productController.GetAllProducts().Rows)
            {
                if (row["tenSP"].ToString().Equals(ten, StringComparison.OrdinalIgnoreCase) &&
                    row["mau"].ToString().Equals(mauSac, StringComparison.OrdinalIgnoreCase) &&
                    row["kichco"].ToString().Equals(kichThuoc, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Sản phẩm (Tên, Màu, Size) đã tồn tại! Vui lòng nhập kho thay vì tạo mới."); return;
                }
            }

            if (productController.AddProduct(ten, decimal.Parse(gia.Text), XulyLuuAnh(), mauSac, kichThuoc, Convert.ToInt32(listdm.SelectedValue), listtt.Text))
            {
                MessageBox.Show("Thêm thành công!"); button5_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e) // SỬA
        {
            if (idSanPhamHienTai <= 0) { MessageBox.Show("Chọn sản phẩm để sửa!"); return; }
            if (!ValidateData()) return;

            string ten = tensp.Text.Trim(), mauSac = mau.Text.Trim(), kichThuoc = kichco.Text.Trim();

            // Chặn trùng lặp khi sửa
            foreach (DataRow row in productController.GetAllProducts().Rows)
            {
                if (Convert.ToInt32(row["id"]) == idSanPhamHienTai) continue;
                if (row["tenSP"].ToString().Equals(ten, StringComparison.OrdinalIgnoreCase) &&
                    row["mau"].ToString().Equals(mauSac, StringComparison.OrdinalIgnoreCase) &&
                    row["kichco"].ToString().Equals(kichThuoc, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Trùng thông tin với sản phẩm khác!"); return;
                }
            }

            int soLuongCu = string.IsNullOrEmpty(soluong.Text) ? 0 : Convert.ToInt32(soluong.Text);

            if (productController.UpdateProduct(idSanPhamHienTai, ten, decimal.Parse(gia.Text), XulyLuuAnh(), mauSac, kichThuoc, Convert.ToInt32(listdm.SelectedValue), listtt.Text, soLuongCu))
            {
                MessageBox.Show("Cập nhật thành công!"); button5_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e) // XÓA
        {
            if (idSanPhamHienTai <= 0) { MessageBox.Show("Chọn sản phẩm để xóa!"); return; }

            string message = QuanLyCuaHangGiay.util.Authorization.IsStaff() 
                ? "Bạn có muốn ngừng kinh doanh sản phẩm này? (Sản phẩm sẽ chuyển sang trạng thái Inactive)" 
                : "Bạn có chắc chắn muốn xóa vĩnh viễn sản phẩm này?";

            if (MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (productController.DeleteProduct(idSanPhamHienTai))
                    {
                        // Dọn ảnh cũ (Chỉ dọn khi là Admin xóa cứng, Staff xóa mềm DB vẫn giữ nên kệ)
                        string imgPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.FullName, "Images", tenAnhLuuDB);
                        if (!QuanLyCuaHangGiay.util.Authorization.IsStaff() && !string.IsNullOrEmpty(tenAnhLuuDB) && File.Exists(imgPath))
                        {
                            if (picture.Image != null) { picture.Image.Dispose(); picture.Image = null; }
                            File.Delete(imgPath);
                        }
                        if (QuanLyCuaHangGiay.util.Authorization.IsStaff())
                        {
                            MessageBox.Show("Sản phẩm đã được chuyển sang trạng thái ngừng kinh doanh (Inactive).");
                        }
                        else
                        {
                            MessageBox.Show("Xóa thành công!");
                        }
                        button5_Click(sender, e);
                    }
                }
                catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 547)
                {
                    MessageBox.Show("Sản phẩm đã có lịch sử nhập/bán, hãy đổi trạng thái Inactive thay vì xóa!");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e) // LÀM MỚI
        {
            tensp.Clear(); gia.Clear(); mau.Clear(); kichco.Clear();
            listdm.SelectedIndex = 0; listtt.SelectedIndex = 0; soluong.Text = "0";

            // Tạm ngắt sự kiện để tránh gọi SQL liên tục khi reset combo
            comboBox1.SelectedIndexChanged -= (s, ev) => LocVaTimKiem();
            comboBox2.SelectedIndexChanged -= (s, ev) => LocVaTimKiem();

            timkiem.Clear();
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;

            comboBox1.SelectedIndexChanged += (s, ev) => LocVaTimKiem();
            comboBox2.SelectedIndexChanged += (s, ev) => LocVaTimKiem();

            picture.Image = null; duongDanAnhGoc = ""; tenAnhLuuDB = ""; idSanPhamHienTai = -1;
            button4.Enabled = true;

            LocVaTimKiem();
            LoadNextId();
        }
        #endregion

        #region 3. SỰ KIỆN LƯỚI & RÁC
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (row.Cells["id"].Value != DBNull.Value)
                {
                    idSanPhamHienTai = Convert.ToInt32(row.Cells["id"].Value);
                    textBox1.Text = idSanPhamHienTai.ToString();
                    tensp.Text = row.Cells["tenSP"].Value.ToString();
                    gia.Text = row.Cells["gia"].Value.ToString();
                    mau.Text = row.Cells["mau"].Value.ToString();
                    kichco.Text = row.Cells["kichco"].Value.ToString();
                    listdm.Text = row.Cells["tenDanhMuc"].Value.ToString();
                    listtt.Text = row.Cells["trangthai"].Value.ToString();
                    soluong.Text = row.Cells["soLuong"].Value?.ToString() ?? "0";

                    // Vô hiệu hóa nút xóa nếu sản phẩm đã inactive
                    button4.Enabled = (row.Cells["trangthai"].Value.ToString() == "active");

                    tenAnhLuuDB = row.Cells["anh"].Value.ToString();
                    duongDanAnhGoc = "";
                    if (!string.IsNullOrEmpty(tenAnhLuuDB))
                    {
                        string imgPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.FullName, "Images", tenAnhLuuDB);
                        if (File.Exists(imgPath))
                        {
                            using (FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                            {
                                picture.Image = Image.FromStream(fs);
                            }
                        }
                        else picture.Image = null;
                    }
                    else picture.Image = null;
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns.Contains("soLuong") && e.RowIndex >= 0)
            {
                var cellValue = dataGridView1.Rows[e.RowIndex].Cells["soLuong"].Value;
                if (cellValue != DBNull.Value && cellValue != null && Convert.ToInt32(cellValue) < 10)
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e) { LocVaTimKiem(); }
        private void label8_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void soluong_TextChanged(object sender, EventArgs e) { }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        #endregion
    }
}