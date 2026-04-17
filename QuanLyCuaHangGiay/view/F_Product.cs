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
        private string duongDanAnhGoc = ""; // Lưu đường dẫn tuyệt đối khi chọn ảnh
        private string tenAnhLuuDB = "";    // Lưu tên ảnh để nạp vào DB
        private int idSanPhamHienTai = -1;  // Lưu ID sản phẩm khi click vào bảng để Sửa/Xóa

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
            button6.Click += button6_Click; // Tìm kiếm
            dataGridView1.CellClick += dataGridView1_CellClick; // Click vào bảng

            // Bắt sự kiện khi đang gõ chữ tìm kiếm
            timkiem.TextChanged += timkiem_TextChanged;
        }

        private void F_Product_Load(object sender, EventArgs e)
        {
            LoadComboboxTrangThai();
            LoadComboboxDanhMuc();
            LoadData();
        }

        #region Các hàm hỗ trợ nạp dữ liệu (Helpers)

        private void LoadComboboxTrangThai()
        {
            listtt.Items.Clear();
            listtt.Items.Add("Active");
            listtt.Items.Add("Inactive");
            listtt.SelectedIndex = 0; // Mặc định chọn Active
        }

        private void LoadComboboxDanhMuc()
        {
            DataTable dt = categoryController.GetActiveCategories();
            listdm.DataSource = dt;
            listdm.DisplayMember = "tenDanhMuc"; // Cột hiện chữ
            listdm.ValueMember = "id";           // Cột lấy ID ẩn
        }

        private void LoadData()
        {
            dataGridView1.DataSource = productController.GetAllProducts();

            // Đổi tên cột cho đẹp
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["id"].HeaderText = "Mã SP";
                dataGridView1.Columns["tenSP"].HeaderText = "Tên Sản Phẩm";
                dataGridView1.Columns["gia"].HeaderText = "Giá";
                dataGridView1.Columns["tenDanhMuc"].HeaderText = "Danh Mục";
                // Có thể ẩn cột ID danh mục đi cho đỡ rối
                //dataGridView1.Columns["danhmucID"].Visible = false;
            }
        }

        // Hàm xử lý copy ảnh vào thư mục Project
        // Hàm xử lý copy ảnh vào thư mục Project gốc
        private string XulyLuuAnh()
        {
            // Nếu không chọn ảnh mới, giữ nguyên tên ảnh cũ
            if (string.IsNullOrEmpty(duongDanAnhGoc)) return tenAnhLuuDB;

            // Lấy đường dẫn thư mục gốc của Project (Lùi 2 cấp từ bin/Debug)
            string thuMucGocProject = Directory.GetParent(Application.StartupPath).Parent.FullName;

            // Trỏ thẳng vào thư mục Images của Project
            string thuMucDich = Path.Combine(thuMucGocProject, "Images");

            // Tạo thư mục nếu chưa có
            if (!Directory.Exists(thuMucDich))
            {
                Directory.CreateDirectory(thuMucDich);
            }

            string tenFile = Path.GetFileName(duongDanAnhGoc);
            string duongDanMoi = Path.Combine(thuMucDich, tenFile);

            try
            {
                // Copy đè nếu file đã tồn tại
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

        #region Các sự kiện Nút bấm (Events)

        // Nút: Chọn Ảnh
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
                ofd.Title = "Chọn ảnh sản phẩm";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    duongDanAnhGoc = ofd.FileName; // Lưu đường dẫn tuyệt đối
                    picture.Image = Image.FromFile(duongDanAnhGoc);
                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        //Thêm
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

                // Xử lý lưu ảnh vật lý vào máy và lấy tên file
                tenAnhLuuDB = XulyLuuAnh();

                bool isSuccess = productController.AddProduct(ten, giaTien, tenAnhLuuDB, mauSac, kichThuoc, idDanhMuc, trangThai);

                if (isSuccess)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    LoadData();
                    button5_Click(sender, e); // Gọi hàm làm mới form
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

        //  Sửa
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

                // Lưu ảnh mới (nếu có chọn)
                tenAnhLuuDB = XulyLuuAnh();

                bool isSuccess = productController.UpdateProduct(idSanPhamHienTai, ten, giaTien, tenAnhLuuDB, mauSac, kichThuoc, idDanhMuc, trangThai);

                if (isSuccess)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                    button5_Click(sender, e); // Xóa trắng form
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

        //  Làm mới 
        private void button5_Click(object sender, EventArgs e)
        {
            tensp.Clear();
            gia.Clear();
            mau.Clear();
            kichco.Clear();
            listdm.SelectedIndex = 0;
            listtt.SelectedIndex = 0;
            timkiem.Clear();

            // Xóa ảnh
            picture.Image = null;
            duongDanAnhGoc = "";
            tenAnhLuuDB = "";
            idSanPhamHienTai = -1;

            LoadData(); // Load lại toàn bộ bảng (bỏ tìm kiếm)
        }

        // Tìm kiếm
        private void button6_Click(object sender, EventArgs e)
        {
            string keyword = timkiem.Text;
            dataGridView1.DataSource = productController.SearchProduct(keyword);
        }

        // Sự kiện: Tự động tìm kiếm ngay khi đang gõ từng chữ
        private void timkiem_TextChanged(object sender, EventArgs e)
        {
            // Lấy từ khóa hiện tại trong ô Textbox
            string keyword = timkiem.Text;

            // Lập tức gọi hàm tìm kiếm và đổ lại dữ liệu vào DataGridView
            dataGridView1.DataSource = productController.SearchProduct(keyword);
        }

        // Sự kiện: Bấm vào 1 dòng trên DataGridView để hiển thị lên trên
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //  e.RowIndex >= 0 VÀ không phải là dòng trống cuối cùng (!IsNewRow)
            if (e.RowIndex >= 0 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Chắc chắn rằng ô ID có chứa dữ liệu
                if (row.Cells["id"].Value != DBNull.Value && row.Cells["id"].Value != null)
                {
                    idSanPhamHienTai = Convert.ToInt32(row.Cells["id"].Value);
                    tensp.Text = row.Cells["tenSP"].Value.ToString();
                    gia.Text = row.Cells["gia"].Value.ToString();
                    mau.Text = row.Cells["mau"].Value.ToString();
                    kichco.Text = row.Cells["kichco"].Value.ToString();

                    // Chọn đúng combobox theo Value (ẩn) hoặc theo chữ (hiển thị)
                    listdm.Text = row.Cells["tenDanhMuc"].Value.ToString();
                    listtt.Text = row.Cells["trangthai"].Value.ToString();

                    // Hiển thị ảnh
                    tenAnhLuuDB = row.Cells["anh"].Value.ToString();
                    duongDanAnhGoc = ""; // Reset đường dẫn gốc vì mình đang load ảnh từ DB lên

                    if (!string.IsNullOrEmpty(tenAnhLuuDB))
                    {
                        // Lùi 2 cấp để tìm ảnh trong thư mục gốc
                        string thuMucGocProject = Directory.GetParent(Application.StartupPath).Parent.FullName;
                        string duongDanLoadLen = Path.Combine(thuMucGocProject, "Images", tenAnhLuuDB);

                        if (File.Exists(duongDanLoadLen))
                        {
                            // Dùng FileStream để không bị lock file khi xóa/sửa
                            using (FileStream fs = new FileStream(duongDanLoadLen, FileMode.Open, FileAccess.Read))
                            {
                                picture.Image = Image.FromStream(fs);
                            }
                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            picture.Image = null; // Nếu file ảnh bị mất trong ổ cứng thì bỏ trống
                        }
                    }
                    else
                    {
                        picture.Image = null;
                    }
                }
            }
        }
        #endregion
    }
}