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

        // 5. Tìm kiếm danh mục theo tên VÀ lọc theo trạng thái
        public DataTable Search(string keyword, string status)
        {
            string sql = "SELECT * FROM DanhMuc WHERE tenDanhMuc LIKE @keyword";

            if (status != "Tất cả" && !string.IsNullOrEmpty(status))
            {
                sql += " AND trangthai = @status";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@keyword", "%" + keyword + "%"),
                    new SqlParameter("@status", status)
                };
                return DBConnection.GetDataTable(sql, parameters);
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@keyword", "%" + keyword + "%")
                };
                return DBConnection.GetDataTable(sql, parameters);
            }
        }

        // 6. Kiểm tra danh mục có chứa sản phẩm không
        public bool CheckHasProduct(int idDanhMuc)
        {
            string sql = "SELECT COUNT(*) FROM SanPham WHERE danhmucID = @id";
            SqlParameter[] p = { new SqlParameter("@id", idDanhMuc) };
            DataTable dt = DBConnection.GetDataTable(sql, p);

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]) > 0;
            }
            return false;
        }

        // 7. Lấy ID tự tăng tiếp theo
        public int GetNextCategoryId()
        {
            string sql = "SELECT MAX(id) FROM DanhMuc";
            DataTable dt = DBConnection.GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                return Convert.ToInt32(dt.Rows[0][0]) + 1;
            }
            return 1;
        }

        // 8. Lấy danh mục đang Active (Dành riêng cho Form Sản Phẩm gọi tới)
        public DataTable GetActiveCategories()
        {
            string sql = "SELECT * FROM DanhMuc WHERE trangthai = 'Active'";
            return DBConnection.GetDataTable(sql);
        }
    }
}