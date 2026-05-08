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
            // MẶC ĐỊNH BẰNG 0: Mình truyền cứng số 0 vào câu SQL luôn để đảm bảo khi thêm mới chắc chắn số lượng là 0
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
            // Đã bổ sung soLuong = @soLuong để sau này làm chức năng nhập kho
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
                
                // Thuộc tính sp.SoLuong này sẽ lấy từ class model Products
                new SqlParameter("@soLuong", sp.SoLuong)
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

        // 6. Lấy danh sách sản phẩm kèm tên danh mục
        public DataTable GetAllWithCategoryName()
        {
            // BỔ SUNG: sp.soLuong vào câu SELECT để hiển thị lên bảng
            string sql = @"SELECT sp.id, sp.tenSP, sp.gia, sp.anh, sp.mau, sp.kichco, sp.soLuong, 
                                  sp.trangthai, sp.ngayTao, dm.tenDanhMuc 
                           FROM SanPham sp
                           INNER JOIN DanhMuc dm ON sp.danhmucID = dm.id";
            return DBConnection.GetDataTable(sql);
        }

        // Hàm 1: Chỉ lấy sản phẩm Active (dành cho Staff)
        public DataTable GetActiveProductsWithCategoryName()
        {
            // BỔ SUNG: sp.soLuong vào câu SELECT để hiển thị lên bảng
            string sql = @"SELECT sp.id, sp.tenSP, sp.gia, sp.anh, sp.mau, sp.kichco, sp.soLuong, 
                                  sp.trangthai, sp.ngayTao, dm.tenDanhMuc 
                           FROM SanPham sp
                           INNER JOIN DanhMuc dm ON sp.danhmucID = dm.id
                           WHERE sp.trangthai = 'active'";
            return DBConnection.GetDataTable(sql);
        }

        // Hàm 2: Xóa mềm (Chỉ đổi trạng thái thành Inactive)
        public int ChangeStatus(int id, string status)
        {
            string sql = "UPDATE SanPham SET trangthai = @status WHERE id = @id";
            SqlParameter[] p = {
                new SqlParameter("@id", id),
                new SqlParameter("@status", status)
            };
            return DBConnection.ExecuteNonQuery(sql, p);
        }

        // Lấy ID tiếp theo sẽ được tạo cho Sản phẩm (MAX + 1)
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