using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyCuaHangGiay.model;
using QuanLyCuaHangGiay.Database;

namespace QuanLyCuaHangGiay.database.repository
{
    internal class ProductRepository
    {
        public DataTable SearchAndFilter(string keyword, int categoryId, string status)
        {
            // Câu SQL gốc có JOIN để lấy được tên Danh Mục
            string sql = @"SELECT sp.id, sp.tenSP, sp.gia, sp.anh, sp.mau, sp.kichco, sp.soLuong, 
                                  sp.trangthai, sp.ngayTao, dm.tenDanhMuc 
                           FROM SanPham sp
                           INNER JOIN DanhMuc dm ON sp.danhmucID = dm.id
                           WHERE 1=1"; // 1=1 là mẹo để nối các lệnh AND bên dưới dễ dàng

            List<SqlParameter> paramList = new List<SqlParameter>();

            // Nếu có gõ từ khóa
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND sp.tenSP LIKE @keyword";
                paramList.Add(new SqlParameter("@keyword", "%" + keyword + "%"));
            }

            // Nếu có chọn Danh mục (Khác "Tất cả" - mặc định id sẽ > 0)
            if (categoryId > 0)
            {
                sql += " AND sp.danhmucID = @categoryId";
                paramList.Add(new SqlParameter("@categoryId", categoryId));
            }

            // Nếu có chọn Trạng thái (Khác "Tất cả")
            if (status != "Tất cả" && !string.IsNullOrEmpty(status))
            {
                sql += " AND sp.trangthai = @status";
                paramList.Add(new SqlParameter("@status", status));
            }

            // Sắp xếp mã sản phẩm mới nhất lên đầu tiên
            sql += " ORDER BY sp.id DESC";

            // Chuyển List parameter thành mảng và thực thi
            return DBConnection.GetDataTable(sql, paramList.ToArray());
        }


        // 2. Thêm sản phẩm mới 
        public int Insert(Products sp)
        {
            string sql = @"INSERT INTO SanPham (tenSP, gia, anh, mau, kichco, danhmucID, trangthai, soLuong) 
                           VALUES (@tenSP, @gia, @anh, @mau, @kichco, @danhmucID, @trangthai, 0)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@tenSP", sp.TenSP),
                new SqlParameter("@gia", sp.Gia),
                new SqlParameter("@anh", (object)sp.Anh ?? DBNull.Value),
                new SqlParameter("@mau", sp.Mau),
                new SqlParameter("@kichco", sp.KichCo),
                new SqlParameter("@danhmucID", sp.DanhMucID),
                new SqlParameter("@trangthai", sp.TrangThai)
            };
            return DBConnection.ExecuteNonQuery(sql, parameters);
        }

        // 3. Cập nhật thông tin sản phẩm
        public int Update(Products sp)
        {
            string sql = @"UPDATE SanPham 
                           SET tenSP = @tenSP, gia = @gia, anh = @anh, mau = @mau, 
                               kichco = @kichco, danhmucID = @danhmucID, trangthai = @trangthai, soLuong = @soLuong 
                           WHERE id = @id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", sp.Id),
                new SqlParameter("@tenSP", sp.TenSP),
                new SqlParameter("@gia", sp.Gia),
                new SqlParameter("@anh", (object)sp.Anh ?? DBNull.Value),
                new SqlParameter("@mau", sp.Mau),
                new SqlParameter("@kichco", sp.KichCo),
                new SqlParameter("@danhmucID", sp.DanhMucID),
                new SqlParameter("@trangthai", sp.TrangThai),
                new SqlParameter("@soLuong", sp.SoLuong)
            };
            return DBConnection.ExecuteNonQuery(sql, parameters);
        }

        // 4. Xóa cứng sản phẩm
        public int Delete(int id)
        {
            string sql = "DELETE FROM SanPham WHERE id = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            return DBConnection.ExecuteNonQuery(sql, parameters);
        }

        // 5. Xóa mềm (Đổi trạng thái thành Inactive)
        public int ChangeStatus(int id, string status)
        {
            string sql = "UPDATE SanPham SET trangthai = @status WHERE id = @id";
            SqlParameter[] p = {
                new SqlParameter("@id", id),
                new SqlParameter("@status", status)
            };
            return DBConnection.ExecuteNonQuery(sql, p);
        }

        // 6. Lấy ID tiếp theo sẽ được tạo
        public int GetNextProductId()
        {
            string sql = "SELECT MAX(id) FROM SanPham";
            DataTable dt = DBConnection.GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                return Convert.ToInt32(dt.Rows[0][0]) + 1;
            }
            return 1;
        }
    }
}