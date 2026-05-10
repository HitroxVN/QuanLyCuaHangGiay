using QuanLyCuaHangGiay.Database;
using System;
using System.Data;
using System.Data.SqlClient;

namespace shoe_store.controller
{
    public class DonHangController
    {
        public DataTable GetDanhSachDonHang(DateTime? tuNgay, DateTime? denNgay, string tuKhoa)
        {
            string query = @"SELECT dh.id as [Mã HĐ], dh.ngayTao as [Ngày bán], 
                            ISNULL(kh.sdt, N'Khách vãng lai') as [Khách hàng], 
                            tk.hoTen as [Nhân viên], dh.tongTien as [Thành tiền], dh.trangThai as [Trạng thái]
                     FROM DonHang dh
                     LEFT JOIN KhachHang kh ON dh.khachhangID = kh.id
                     LEFT JOIN TaiKhoan tk ON dh.taikhoanID = tk.id
                     WHERE (@tuKhoa = '' OR dh.id LIKE @tuKhoa OR kh.sdt LIKE @tuKhoa)
                       AND (@tuNgay IS NULL OR dh.ngayTao >= @tuNgay)
                       AND (@denNgay IS NULL OR dh.ngayTao <= @denNgay)
                     ORDER BY dh.ngayTao DESC";

            SqlParameter pTuNgay = new SqlParameter("@tuNgay", SqlDbType.DateTime);
            pTuNgay.Value = tuNgay.HasValue ? (object)tuNgay.Value.Date : DBNull.Value;

            SqlParameter pDenNgay = new SqlParameter("@denNgay", SqlDbType.DateTime);
            pDenNgay.Value = denNgay.HasValue ? (object)denNgay.Value.Date.AddDays(1).AddTicks(-1) : DBNull.Value;

            SqlParameter pTuKhoa = new SqlParameter("@tuKhoa", SqlDbType.NVarChar);
            pTuKhoa.Value = string.IsNullOrEmpty(tuKhoa) ? "" : "%" + tuKhoa.Trim() + "%";

            return DBConnection.GetDataTable(query, new SqlParameter[] { pTuNgay, pDenNgay, pTuKhoa });
        }

        private void HoanKhoVaDiem(int maDonHang, SqlConnection conn, SqlTransaction trans)
        {
            string sqlHoanKho = @"UPDATE SanPham 
                                  SET soLuong = SanPham.soLuong + ct.soLuong
                                  FROM SanPham 
                                  INNER JOIN ChiTietDonHang ct ON SanPham.id = ct.sanphamID
                                  WHERE ct.donhangID = @donhangID";
            using (SqlCommand cmdKho = new SqlCommand(sqlHoanKho, conn, trans))
            {
                cmdKho.Parameters.AddWithValue("@donhangID", maDonHang);
                cmdKho.ExecuteNonQuery();
            }

            string sqlCheckDiem = "SELECT khachhangID, diemSuDung FROM DonHang WHERE id = @id";
            int? khachID = null;
            int diemDaDung = 0;

            using (SqlCommand cmdCheck = new SqlCommand(sqlCheckDiem, conn, trans))
            {
                cmdCheck.Parameters.AddWithValue("@id", maDonHang);
                using (SqlDataReader reader = cmdCheck.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        khachID = reader["khachhangID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["khachhangID"]) : null;
                        diemDaDung = reader["diemSuDung"] != DBNull.Value ? Convert.ToInt32(reader["diemSuDung"]) : 0;
                    }
                }
            }

            if (khachID.HasValue && diemDaDung > 0)
            {
                string sqlHoanDiem = "UPDATE KhachHang SET diemTichLuy = diemTichLuy + @diem WHERE id = @khID";
                using (SqlCommand cmdDiem = new SqlCommand(sqlHoanDiem, conn, trans))
                {
                    cmdDiem.Parameters.AddWithValue("@diem", diemDaDung);
                    cmdDiem.Parameters.AddWithValue("@khID", khachID.Value);
                    cmdDiem.ExecuteNonQuery();
                }
            }
        }

        public bool HuyDonHang(int maDonHang)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlCheck = "SELECT trangThai FROM DonHang WHERE id = @id";
                        using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, trans))
                        {
                            cmdCheck.Parameters.AddWithValue("@id", maDonHang);
                            object result = cmdCheck.ExecuteScalar();
                            if (result == null) throw new Exception("Không tìm thấy đơn hàng!");
                            if (result.ToString() == "Đã hủy")
                                throw new Exception("Đơn hàng này đã được hủy trước đó!");
                        }
                        HoanKhoVaDiem(maDonHang, conn, trans);
                        string sqlUpdate = "UPDATE DonHang SET trangThai = N'Đã hủy' WHERE id = @id";
                        using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, trans))
                        {
                            cmdUpdate.Parameters.AddWithValue("@id", maDonHang);
                            cmdUpdate.ExecuteNonQuery();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Hủy đơn hàng thất bại: " + ex.Message);
                    }
                }
            }
        }

        public bool XoaVinhVienDonHang(int maDonHang)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlCheck = "SELECT trangThai FROM DonHang WHERE id = @id";
                        string trangThai = "";
                        using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, trans))
                        {
                            cmdCheck.Parameters.AddWithValue("@id", maDonHang);
                            object result = cmdCheck.ExecuteScalar();
                            if (result == null) throw new Exception("Không tìm thấy đơn hàng!");
                            trangThai = result.ToString();
                        }
                        if (trangThai != "Đã hủy")
                        {
                            HoanKhoVaDiem(maDonHang, conn, trans);
                        }
                        string sqlXoaChiTiet = "DELETE FROM ChiTietDonHang WHERE donhangID = @id";
                        using (SqlCommand cmdChiTiet = new SqlCommand(sqlXoaChiTiet, conn, trans))
                        {
                            cmdChiTiet.Parameters.AddWithValue("@id", maDonHang);
                            cmdChiTiet.ExecuteNonQuery();
                        }
                        string sqlXoaCha = "DELETE FROM DonHang WHERE id = @id";
                        using (SqlCommand cmdCha = new SqlCommand(sqlXoaCha, conn, trans))
                        {
                            cmdCha.Parameters.AddWithValue("@id", maDonHang);
                            cmdCha.ExecuteNonQuery();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Xóa vĩnh viễn thất bại: " + ex.Message);
                    }
                }
            }
        }
    }
}