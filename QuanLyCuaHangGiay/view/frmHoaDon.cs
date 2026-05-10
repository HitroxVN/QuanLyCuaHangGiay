using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using QuanLyCuaHangGiay.Database;
namespace QuanLyCuaHangGiay.view
{
    public partial class frmHoaDon : Form
    {
        private int maDonHangID;

        public frmHoaDon(int maDonHang)
        {
            InitializeComponent();
            maDonHangID = maDonHang;

            LoadDuLieuHoaDonThucTe();
        }
        private void LoadDuLieuHoaDonThucTe()
        {
            try
            {
                // 1. Cấu hình kích thước hiển thị
                this.Width = 720;
                rtbPreview.Width = 680;

                string bill = "";
                bill += "                         CỬA HÀNG GIÀY 4TC\n";
                bill += "                     Điện thoại: 0988.888.888\n";
                bill += "====================================================================\n";
                bill += "                         HÓA ĐƠN THANH TOÁN\n";
                bill += "====================================================================\n";

                using (SqlConnection conn = DBConnection.GetDBConnection())
                {
                    conn.Open();

                    // 1. ĐÃ BỔ SUNG CỘT "dh.ghiChu" VÀO LỆNH SELECT
                    string sqlPhieu = @"SELECT dh.ngayTao, dh.tongTien, dh.tongGia, dh.chietKhau, dh.diemSuDung, dh.pttt, dh.ghiChu,
                               kh.hoTen as tenKhach,
                               tk.hoTen as tenNhanVien
                        FROM DonHang dh 
                        LEFT JOIN KhachHang kh ON dh.khachhangID = kh.id 
                        LEFT JOIN TaiKhoan tk ON dh.taikhoanID = tk.id 
                        WHERE dh.id = @id";

                    using (SqlCommand cmd = new SqlCommand(sqlPhieu, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", maDonHangID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bill += string.Format(" Mã HĐ             : #{0}\n", maDonHangID);
                                bill += string.Format(" Thời gian mua     : {0:dd/MM/yyyy HH:mm}\n", reader["ngayTao"]);
                                bill += string.Format(" Khách hàng        : {0}\n", reader["tenKhach"] != DBNull.Value ? reader["tenKhach"] : "Khách vãng lai");
                                bill += string.Format(" Hình thức TT      : {0}\n", reader["pttt"]);
                                bill += string.Format(" Nhân viên TT      : {0}\n", reader["tenNhanVien"] != DBNull.Value ? reader["tenNhanVien"] : "Hệ thống");

                                // 2. XỬ LÝ DÒNG GHI CHÚ
                                // Kiểm tra xem ghi chú có bị NULL không, nếu không NULL thì gán vào biến
                                string ghiChu = reader["ghiChu"] != DBNull.Value ? reader["ghiChu"].ToString().Trim() : "";

                                // Nếu biến có chữ (không rỗng) thì mới in ra dòng "Ghi chú:" trên hóa đơn
                                if (!string.IsNullOrEmpty(ghiChu))
                                {
                                    bill += string.Format(" Ghi chú           : {0}\n", ghiChu);
                                }
                            }
                        }
                    }

                    // 3. VẼ ĐẦU BẢNG (Đầy đủ các cột như mẫu sếp muốn)
                    bill += "+----------------------+------+----+-----------+--+----------+----------+\n";
                    bill += "| TÊN SẢN PHẨM         | MÀU  |SIZE|  ĐƠN GIÁ  |SL| GIẢM GIÁ |THÀNH TIỀN|\n";
                    bill += "+----------------------+------+----+-----------+--+----------+----------+\n";

                    // 4. LẤY CHI TIẾT GIÀY (Dữ liệu thật từ DB)
                    string sqlChiTiet = @"SELECT sp.tenSP, sp.mau, sp.kichco, sp.gia, ct.soLuong, ct.giamGia, ct.thanhTien 
                                  FROM ChiTietDonHang ct 
                                  JOIN SanPham sp ON ct.sanphamID = sp.id 
                                  WHERE ct.donhangID = @id";

                    using (SqlCommand cmdCT = new SqlCommand(sqlChiTiet, conn))
                    {
                        cmdCT.Parameters.AddWithValue("@id", maDonHangID);
                        using (SqlDataReader r = cmdCT.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                string ten = r["tenSP"].ToString();
                                if (ten.Length > 20) ten = ten.Substring(0, 17) + "..."; // Cắt tên nếu quá dài

                                bill += string.Format("|{0,-22}|{1,-6}|{2,-4}|{3,11:N0}|{4,2}|{5,10:N0}|{6,10:N0}|\n",
                                    ten, r["mau"], r["kichco"], r["gia"], r["soLuong"], r["giamGia"], r["thanhTien"]);
                            }
                        }
                    }
                    bill += "+----------------------+------+----+-----------+--+----------+----------+\n";

                    // 5. PHẦN TỔNG KẾT (Căn lề phải cho đẹp)
                    decimal tgGoc = 0, ckHĐ = 0, tCuoi = 0;
                    int diem = 0;

                    using (SqlCommand cmdLayTong = new SqlCommand(sqlPhieu, conn))
                    {
                        cmdLayTong.Parameters.AddWithValue("@id", maDonHangID);
                        using (SqlDataReader r = cmdLayTong.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                tgGoc = Convert.ToDecimal(r["tongGia"]);
                                ckHĐ = Convert.ToDecimal(r["chietKhau"]);
                                tCuoi = Convert.ToDecimal(r["tongTien"]);
                                diem = Convert.ToInt32(r["diemSuDung"]);
                            }
                        }
                    }

                    bill += string.Format(" Tổng tiền hàng:                                     {0,15:N0}\n", tgGoc);
                    bill += string.Format(" Giảm giá hóa đơn:                                 - {0,15:N0}\n", ckHĐ);
                    bill += string.Format(" Dùng điểm ({0,-3}):                                 - {1,15:N0}\n", diem, diem * 1000);
                    bill += "--------------------------------------------------------------------\n";
                    bill += string.Format(" KHÁCH HÀNG PHẢI TRẢ                                 {0,15:N0}\n", tCuoi);
                    bill += "====================================================================\n";
                    bill += "                 CẢM ƠN QUÝ KHÁCH VÀ HẸN GẶP LẠI !\n";
                }

                // CHỐT HẠ: Gán dữ liệu vào RichTextBox (Thiếu dòng này là Form trắng bóc)
                rtbPreview.Text = bill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị hóa đơn: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // 2. CÁC SỰ KIỆN NÚT BẤM VÀ MÁY IN (Giữ nguyên)
        // =========================================================
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnInHoaDon_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
                MessageBox.Show("Đã gửi lệnh in hóa đơn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(rtbPreview.Text, new Font("Courier New", 10, FontStyle.Regular), Brushes.Black, new PointF(10, 10));
        }
    }
}