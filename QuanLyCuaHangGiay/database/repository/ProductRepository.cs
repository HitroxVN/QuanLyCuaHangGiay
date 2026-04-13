using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCuaHangGiay.model;
using QuanLyCuaHangGiay.Database;

namespace QuanLyCuaHangGiay.database.repository
{
    internal class ProductRepository
    {
        // 1. Lấy tất cả sản phẩm
        public DataTable GetAll()
        {
            string sql = "SELECT * FROM SanPham";
            return DBConnection.GetDataTable(sql);
        }

        // 2. Thêm sản phẩm mới
        public int Insert(Products sp)
        {
            string sql = @"INSERT INTO SanPham (tenSP, gia, anh, mau, kichco, danhmucID, trangthai) 
                           VALUES (@tenSP, @gia, @anh, @mau, @kichco, @danhmucID, @trangthai)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@tenSP", sp.TenSP),
                new SqlParameter("@gia", sp.Gia),
                new SqlParameter("@anh", (object)sp.Anh ?? DBNull.Value), // Xử lý nếu không có ảnh
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
                               kichco = @kichco, danhmucID = @danhmucID, trangthai = @trangthai 
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
                new SqlParameter("@trangthai", sp.TrangThai)
            };
            return DBConnection.ExecuteNonQuery(sql, parameters);
        }

        // 4. Xóa sản phẩm
        public int Delete(int id)
        {
            string sql = "DELETE FROM SanPham WHERE id = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            return DBConnection.ExecuteNonQuery(sql, parameters);
        }

        // 5. Tìm kiếm sản phẩm theo tên
        public DataTable Search(string keyword)
        {
            string sql = "SELECT * FROM SanPham WHERE tenSP LIKE @keyword";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };
            return DBConnection.GetDataTable(sql, parameters);
        }

        // 6. Lấy danh sách sản phẩm kèm tên danh mục (Dùng để hiển thị lên View cho đẹp)
        public DataTable GetAllWithCategoryName()
        {
            string sql = @"SELECT sp.id, sp.tenSP, sp.gia, sp.anh, sp.mau, sp.kichco, 
                                  sp.trangthai, sp.ngayTao, dm.tenDanhMuc 
                           FROM SanPham sp
                           INNER JOIN DanhMuc dm ON sp.danhmucID = dm.id";
            return DBConnection.GetDataTable(sql);
        }
    }
}