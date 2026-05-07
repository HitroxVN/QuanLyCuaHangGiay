using QuanLyCuaHangGiay.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.database.repository
{
    internal class KhoRepository
    {
        public DataTable GetTenKhoDuyNhat()
        {
            string sql = "SELECT DISTINCT tenKho FROM Kho";
            DataTable dt = DBConnection.GetDataTable(sql);

            // Xử lý thực tế: Nếu hệ thống mới tinh chưa có kho nào trong DB
            if (dt.Rows.Count == 0)
            {
                dt.Columns.Add("tenKho");
                dt.Rows.Add("Kho Tổng");
                dt.Rows.Add("Kho Chi Nhánh 1");
            }

            return dt;
        }

        /// <summary>
        /// Lấy chi tiết tồn kho để hiển thị trên Form Quản lý kho.
        /// Kết hợp với bảng SanPham để lấy tên sản phẩm.
        /// </summary>
        public DataTable GetTonKhoChiTiet(string tenKho = "")
        {
            string sql = @"SELECT k.id AS [Mã Dòng], 
                                  k.tenKho AS [Tên Kho], 
                                  s.tenSP AS [Tên Sản Phẩm], 
                                  k.soLuongTrongKho AS [Số Lượng], 
                                  k.diaChi AS [Hà Nội],
                                  k.ngayCapNhat AS [Ngày Cập Nhật cuối]
                           FROM Kho k
                           JOIN SanPham s ON k.sanphamID = s.id";

            // Nếu người dùng chọn lọc theo một kho cụ thể
            if (!string.IsNullOrEmpty(tenKho) && tenKho != "Tất cả")
            {
                sql += " WHERE k.tenKho = @tenKho";
                SqlParameter[] pa = { new SqlParameter("@tenKho", tenKho) };
                return DBConnection.GetDataTable(sql, pa);
            }

            return DBConnection.GetDataTable(sql);
        }

        public bool CapNhatDiaChi(int idKho, string diaChiMoi)
        {
            string sql = "UPDATE Kho SET diaChi = @diaChi WHERE id = @id";
            SqlParameter[] pa = {
                new SqlParameter("@diaChi", diaChiMoi),
                new SqlParameter("@id", idKho)
            };
            return DBConnection.ExecuteNonQuery(sql, pa) > 0;
        }
    }
}
