using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCuaHangGiay.model;
using QuanLyCuaHangGiay.Database;

namespace QuanLyCuaHangGiay.database.repository
{
    internal class CategoryRepository
    {
        // 1. Lấy tất cả danh mục
        public DataTable GetAll()
        {
            string sql = "SELECT * FROM DanhMuc";
            return DBConnection.GetDataTable(sql);
        }

        // 2. Thêm danh mục mới
        public int Insert(Categories cat)
        {
            string sql = "INSERT INTO DanhMuc (tenDanhMuc, trangthai) VALUES (@tenDanhMuc, @trangthai)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@tenDanhMuc", cat.TenDanhMuc),
                new SqlParameter("@trangthai", cat.TrangThai)
            };
            return DBConnection.ExecuteNonQuery(sql, parameters);
        }

        // 3. Cập nhật danh mục
        public int Update(Categories cat)
        {
            string sql = "UPDATE DanhMuc SET tenDanhMuc = @tenDanhMuc, trangthai = @trangthai WHERE id = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", cat.Id),
                new SqlParameter("@tenDanhMuc", cat.TenDanhMuc),
                new SqlParameter("@trangthai", cat.TrangThai)
            };
            return DBConnection.ExecuteNonQuery(sql, parameters);
        }

        // 4. Xóa danh mục
        public int Delete(int id)
        {
            string sql = "DELETE FROM DanhMuc WHERE id = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            return DBConnection.ExecuteNonQuery(sql, parameters);
        }

        // 5. Tìm kiếm danh mục theo tên
        public DataTable Search(string keyword)
        {
            string sql = "SELECT * FROM DanhMuc WHERE tenDanhMuc LIKE @keyword";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };
            return DBConnection.GetDataTable(sql, parameters);
        }

        // Hàm mới: Chỉ lấy các danh mục có trạng thái là 'Active'
        public DataTable GetActiveCategories()
        {
            string sql = "SELECT * FROM DanhMuc WHERE trangthai = 'Active'";
            return DBConnection.GetDataTable(sql);
        }

        // Xóa mềm danh mục
        public int ChangeStatus(int id, string status)
        {
            string sql = "UPDATE DanhMuc SET trangthai = @status WHERE id = @id";
            SqlParameter[] p = {
                new SqlParameter("@id", id),
                new SqlParameter("@status", status)
            };
            return DBConnection.ExecuteNonQuery(sql, p);
        }
    }
}