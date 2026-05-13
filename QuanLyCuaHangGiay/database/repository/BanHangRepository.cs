using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCuaHangGiay.Database; // Gọi DBConnection của nhóm
using QuanLyCuaHangGiay.util;
namespace QuanLyCuaHangGiay.database.repository
{
    internal class BanHangRepository
    {
        // =================================================================
        // HÀM 1: LẤY DANH SÁCH SẢN PHẨM HIỆN LÊN FORM BÁN HÀNG
        // =================================================================
        public DataTable LayDanhSachSanPhamBan()
        {
            string sql = @"SELECT sp.id AS MaSP,
                                  sp.tenSP AS TenSP,
                                  sp.gia AS GiaBan,
                                  sp.mau AS MauSac,
                                  sp.kichco AS KichCo,
                                  sp.soLuong AS TonKho,
                                  dm.tenDanhMuc AS DanhMuc,
                                  sp.anh AS Anh
                           FROM SanPham sp
                           LEFT JOIN DanhMuc dm ON sp.danhmucID = dm.id
                           WHERE sp.trangthai = 'active'";
            return DBConnection.GetDataTable(sql);
        }

        // =================================================================
        // HÀM 2: THANH TOÁN ĐƠN HÀNG (ĐÃ CẬP NHẬT PTTT VÀ TRỪ ĐIỂM)
        // =================================================================
        public int ThanhToanDonHang(decimal tongGia, decimal chietKhau, decimal tongTien, string sdtKhach, string pttt, int diemSuDung, System.ComponentModel.BindingList<model.ChiTietGioHang> gioHang)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // --- VIỆC 1: TÌM ID KHÁCH HÀNG, CỘNG ĐIỂM MỚI VÀ TRỪ ĐIỂM ĐÃ DÙNG ---
                        object khachHangID = DBNull.Value;
                        // Chỉ cộng điểm dựa trên số tiền THỰC TẾ khách trả cuối cùng
                        int diemCongThem = (int)(tongTien / 100000);

                        if (!string.IsNullOrEmpty(sdtKhach))
                        {
                            string sqlKhach = "SELECT id FROM KhachHang WHERE sdt = @sdt";
                            using (SqlCommand cmdK = new SqlCommand(sqlKhach, conn, trans))
                            {
                                cmdK.Parameters.AddWithValue("@sdt", sdtKhach);
                                object result = cmdK.ExecuteScalar();
                                if (result != null)
                                {
                                    khachHangID = result;
                                    // Cập nhật điểm: Điểm cũ + Điểm mới mua - Điểm vừa xài
                                    string sqlCongDiem = "UPDATE KhachHang SET diemTichLuy = diemTichLuy + @diemMoi - @diemTru WHERE id = @idKhach";
                                    using (SqlCommand cmdDiem = new SqlCommand(sqlCongDiem, conn, trans))
                                    {
                                        cmdDiem.Parameters.AddWithValue("@diemMoi", diemCongThem);
                                        cmdDiem.Parameters.AddWithValue("@diemTru", diemSuDung);
                                        cmdDiem.Parameters.AddWithValue("@idKhach", khachHangID);
                                        cmdDiem.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        // --- VIỆC 2: LƯU VÀO BẢNG DonHang ---
                        string sqlDonHang = @"INSERT INTO DonHang (taikhoanID, ngayTao, tongTien, tongGia, trangThai, chietKhau, pttt, khachhangID, diemSuDung) 
                                              OUTPUT INSERTED.id 
                                              VALUES (@tkID, GETDATE(), @tTien, @tGia, @tThai, @cKhau, @pttt, @khID, @dSuDung)";
                        int donHangIDMoiTao = 0;
                        using (SqlCommand cmdDH = new SqlCommand(sqlDonHang, conn, trans))
                        {
                            cmdDH.Parameters.AddWithValue("@tkID", Session.user.id); // ID tài khoản (sẽ update sau khi làm Đăng nhập)
                            cmdDH.Parameters.AddWithValue("@tTien", tongTien);
                            cmdDH.Parameters.AddWithValue("@tGia", tongGia);
                            cmdDH.Parameters.AddWithValue("@tThai", "Đã thanh toán");
                            cmdDH.Parameters.AddWithValue("@cKhau", chietKhau);
                            cmdDH.Parameters.AddWithValue("@pttt", pttt); // Lưu PTTT thật
                            cmdDH.Parameters.AddWithValue("@khID", khachHangID);
                            cmdDH.Parameters.AddWithValue("@dSuDung", diemSuDung); // Lưu điểm khách đã xài
                            donHangIDMoiTao = (int)cmdDH.ExecuteScalar();
                        }

                        // --- VIỆC 3: LƯU VÀO ChiTietDonHang VÀ TRỪ SỐ LƯỢNG SẢN PHẨM ---
                        foreach (var item in gioHang)
                        {
                            string sqlChiTiet = @"DECLARE @kID INT;
                                                  
                                                  INSERT INTO ChiTietDonHang (donhangID, soLuong, giamGia, sanphamID, khoID, thanhTien) 
                                                  VALUES (@dhID, @sl, @gg, @spID, @kID, @ttienCT);
                                                  
                                                  UPDATE SanPham SET soLuong = soLuong - @sl WHERE id = @spID;";

                            using (SqlCommand cmdCT = new SqlCommand(sqlChiTiet, conn, trans))
                            {
                                cmdCT.Parameters.AddWithValue("@dhID", donHangIDMoiTao);
                                cmdCT.Parameters.AddWithValue("@sl", item.SoLuongMua);
                                cmdCT.Parameters.AddWithValue("@gg", item.GiamGia);
                                cmdCT.Parameters.AddWithValue("@spID", item.MaSanPham);
                                cmdCT.Parameters.AddWithValue("@ttienCT", item.ThanhTien);
                                cmdCT.ExecuteNonQuery();
                            }
                        }
                        trans.Commit();
                        return donHangIDMoiTao; 
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Lỗi khi thanh toán: " + ex.Message);
                    }
                }
            }
        }
        // =================================================================
        // HÀM 3: TÌM KIẾM KHÁCH HÀNG BẰNG SỐ ĐIỆN THOẠI
        // =================================================================
        public DataTable TimKhachHangTheoSDT(string sdt)
        {
            // Chỉ cần lấy id, họ tên và điểm tích lũy ra để hiển thị và xử lý
            string sql = @"SELECT id, hoTen, diemTichLuy 
                           FROM KhachHang 
                           WHERE sdt = @sdt";

            SqlParameter[] pa = { new SqlParameter("@sdt", sdt) };

            // Trả về DataTable (nếu có khách sẽ có 1 dòng dữ liệu, nếu không có sẽ rỗng)
            return DBConnection.GetDataTable(sql, pa);
        }

        // =================================================================
        // HÀM 4: THÊM KHÁCH HÀNG MỚI
        // =================================================================
        public void ThemKhachHangMoi(string hoTen, string sdt)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                string sqlCheck = "SELECT id, trangThai FROM KhachHang WHERE sdt = @sdt";
                int? idKhach = null;
                int trangThai = -1;

                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@sdt", sdt);
                    using (SqlDataReader reader = cmdCheck.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idKhach = Convert.ToInt32(reader["id"]);
                            trangThai = reader["trangThai"] != DBNull.Value ? Convert.ToInt32(reader["trangThai"]) : 1;
                        }
                    }
                }

                if (idKhach.HasValue)
                {
                    if (trangThai == 1)
                    {
                        throw new Exception("Số điện thoại này đã tồn tại trong hệ thống!");
                    }
                    else
                    {
                        string sqlReactivate = @"UPDATE KhachHang 
                                         SET hoTen = @hoTen, trangThai = 1, diemTichLuy = 0, ngayTao = GETDATE() 
                                         WHERE id = @id";
                        using (SqlCommand cmdUpdate = new SqlCommand(sqlReactivate, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@hoTen", hoTen);
                            cmdUpdate.Parameters.AddWithValue("@id", idKhach.Value);
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    string sqlInsert = @"INSERT INTO KhachHang (hoTen, sdt, diemTichLuy, trangThai, ngayTao) 
                                 VALUES (@hoTen, @sdt, 0, 1, GETDATE())";
                    using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@hoTen", hoTen);
                        cmdInsert.Parameters.AddWithValue("@sdt", sdt);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }
        }

        // =================================================================
        // HÀM 5: TẠO ĐƠN HÀNG MỚI
        // =================================================================
        public int TaoDonHangMoi(int taiKhoanID, decimal tongTien, decimal tongGia, string trangThai,
                                 decimal chietKhau, string pttt, string ghiChu, int khachHangID, int diemSuDung)
        {
            string query = @"INSERT INTO DonHang 
                    (taikhoanID, ngayTao, tongTien, tongGia, trangThai, chietKhau, pttt, ghiChu, khachhangID, diemSuDung) 
                    VALUES 
                    (@taiKhoanID, GETDATE(), @tongTien, @tongGia, @trangThai, @chietKhau, @pttt, @ghiChu, @khachHangID, @diemSuDung);
                    SELECT SCOPE_IDENTITY();";

            SqlParameter[] pa = new SqlParameter[]
            {
        new SqlParameter("@taiKhoanID", taiKhoanID),
        new SqlParameter("@tongTien", tongTien),
        new SqlParameter("@tongGia", tongGia),
        new SqlParameter("@trangThai", trangThai),
        new SqlParameter("@chietKhau", chietKhau),
        new SqlParameter("@pttt", pttt),
        new SqlParameter("@ghiChu", string.IsNullOrEmpty(ghiChu) ? "" : ghiChu), 
        new SqlParameter("@khachHangID", khachHangID > 0 ? (object)khachHangID : DBNull.Value),
        new SqlParameter("@diemSuDung", diemSuDung)
            };
            DataTable dt = DBConnection.GetDataTable(query, pa);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]); 
            }
            return -1; 
        }
    }
}