using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.Database;
using QuanLyCuaHangGiay.model;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCuaHangGiay.controller
{
    public class BanHangController
    {
        private BanHangRepository repo = new BanHangRepository();

        public DataTable LayDanhSachSanPhamBan()
        {
            return repo.LayDanhSachSanPhamBan();
        }

        public int ThanhToanDonHang(int taikhoanID, decimal tongGiaGoc, decimal tienChietKhau, decimal tongTienSauGiam,
                            string sdt, string pttt, string ghiChu, int diemDaDung, BindingList<ChiTietGioHang> gioHang)
        {
            int? khachHangID = null;
            if (!string.IsNullOrEmpty(sdt) && sdt.Length >= 10)
            {
                DataTable dtKhach = TimKhachHangTheoSDT(sdt);
                if (dtKhach != null && dtKhach.Rows.Count > 0)
                {
                    khachHangID = Convert.ToInt32(dtKhach.Rows[0]["id"]);
                }
            }

            string query = @"INSERT INTO DonHang 
            (taikhoanID, ngayTao, tongTien, tongGia, trangThai, chietKhau, pttt, ghiChu, khachhangID, diemSuDung) 
            VALUES 
            (@taikhoanID, GETDATE(), @tongTien, @tongGia, @trangThai, @chietKhau, @pttt, @ghiChu, @khachHangID, @diemSuDung);
            SELECT SCOPE_IDENTITY();";

            SqlParameter[] pa = new SqlParameter[]
            {
        new SqlParameter("@taikhoanID", taikhoanID),
        new SqlParameter("@tongTien", tongTienSauGiam),
        new SqlParameter("@tongGia", tongGiaGoc),
        new SqlParameter("@trangThai", "Đã thanh toán"),
        new SqlParameter("@chietKhau", tienChietKhau),
        new SqlParameter("@pttt", pttt),
        new SqlParameter("@ghiChu", string.IsNullOrEmpty(ghiChu) ? (object)DBNull.Value : ghiChu),
        new SqlParameter("@khachHangID", khachHangID.HasValue ? (object)khachHangID.Value : DBNull.Value),
        new SqlParameter("@diemSuDung", diemDaDung)
            };

            System.Data.DataTable dt = DBConnection.GetDataTable(query, pa);
            int maDonHang = -1;

            if (dt != null && dt.Rows.Count > 0)
            {
                maDonHang = Convert.ToInt32(dt.Rows[0][0]);
            }

            if (maDonHang > 0)
            {
                if (khachHangID.HasValue && diemDaDung > 0)
                {
                    string updateDiem = "UPDATE KhachHang SET diemTichLuy = diemTichLuy - @diemDaDung WHERE id = @khachHangID";
                    SqlParameter[] paDiem = new SqlParameter[] {
                new SqlParameter("@diemDaDung", diemDaDung),
                new SqlParameter("@khachHangID", khachHangID.Value)
            };
                    DBConnection.ExecuteNonQuery(updateDiem, paDiem);
                }

                foreach (ChiTietGioHang item in gioHang)
                {
                    string insertChiTiet = @"INSERT INTO ChiTietDonHang (donhangID, sanphamID, soLuong, giamGia, thanhTien) 
                             VALUES (@donhangID, @sanphamID, @soLuong, @giamGia, @thanhTien)";

                    SqlParameter[] paChiTiet = new SqlParameter[] {
                new SqlParameter("@donhangID", maDonHang),
                new SqlParameter("@sanphamID", item.MaSanPham),
                new SqlParameter("@soLuong", item.SoLuongMua),
                new SqlParameter("@giamGia", (object)0),
                new SqlParameter("@thanhTien", item.ThanhTien)
            };

                    DBConnection.ExecuteNonQuery(insertChiTiet, paChiTiet);

                    string updateKho = "UPDATE SanPham SET soLuong = soLuong - @soLuongMua WHERE id = @idSanPham";

                    SqlParameter[] paKho = new SqlParameter[] {
                new SqlParameter("@soLuongMua", item.SoLuongMua),
                new SqlParameter("@idSanPham", item.MaSanPham)
            };
                    DBConnection.ExecuteNonQuery(updateKho, paKho);
                }

                if (khachHangID.HasValue)
                {
                    int diemTichLuyMoi = Convert.ToInt32(tongTienSauGiam / 100000);

                    if (diemTichLuyMoi > 0)
                    {
                        string congDiemSql = "UPDATE KhachHang SET diemTichLuy = diemTichLuy + @diemCong WHERE id = @khachHangID";
                        SqlParameter[] paCongDiem = new SqlParameter[] {
                    new SqlParameter("@diemCong", diemTichLuyMoi),
                    new SqlParameter("@khachHangID", khachHangID.Value)
                };
                        DBConnection.ExecuteNonQuery(congDiemSql, paCongDiem);
                    }
                }
            }

            return maDonHang;
        }

        public DataTable TimKhachHangTheoSDT(string sdt)
        {
            string query = "SELECT id, hoTen, diemTichLuy FROM KhachHang WHERE sdt = @sdt AND trangThai = 1";

            SqlParameter[] pa = new SqlParameter[]
            {
        new SqlParameter("@sdt", sdt)
            };

            return DBConnection.GetDataTable(query, pa);
        }

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