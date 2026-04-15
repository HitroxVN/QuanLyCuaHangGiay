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
        // ================= GET ALL =================
        public DataTable getAllKho()
        {
            string sql = @"SELECT k.id,
                                  sp.id AS sanphamID,
                                  sp.tenSP,
                                  sp.mau,
                                  sp.kichco,
                                  dm.tenDanhMuc,
                                  k.soLuong
                           FROM Kho k
                           JOIN SanPham sp ON k.sanphamID = sp.id
                           JOIN DanhMuc dm ON sp.danhmucID = dm.id";

            return DBConnection.GetDataTable(sql);
        }

        // ================= FILTER =================
        public DataTable filterByDanhMuc(int danhMucID)
        {
            string sql = @"SELECT k.id,
                                  sp.id AS sanphamID,
                                  sp.tenSP,
                                  sp.mau,
                                  sp.kichco,
                                  dm.tenDanhMuc,
                                  k.soLuong
                           FROM Kho k
                           JOIN SanPham sp ON k.sanphamID = sp.id
                           JOIN DanhMuc dm ON sp.danhmucID = dm.id
                           WHERE dm.id = @dm";

            SqlParameter[] pa = {
                new SqlParameter("@dm", danhMucID)
            };

            return DBConnection.GetDataTable(sql, pa);
        }

        // ================= SEARCH =================
        public DataTable search(string keyword)
        {
            string sql = @"SELECT k.id,
                                  sp.id AS sanphamID,
                                  sp.tenSP,
                                  sp.mau,
                                  sp.kichco,
                                  dm.tenDanhMuc,
                                  k.soLuong
                           FROM Kho k
                           JOIN SanPham sp ON k.sanphamID = sp.id
                           JOIN DanhMuc dm ON sp.danhmucID = dm.id
                           WHERE sp.tenSP LIKE @kw";

            SqlParameter[] pa = {
                new SqlParameter("@kw", "%" + keyword + "%")
            };

            return DBConnection.GetDataTable(sql, pa);
        }

        // ================= DANH MUC =================
        public DataTable getDanhMuc()
        {
            string sql = "SELECT id, tenDanhMuc FROM DanhMuc WHERE trangthai = 'Active'";
            return DBConnection.GetDataTable(sql);
        }

        // ================= LOW STOCK =================
        public DataTable getLowStock(int threshold)
        {
            string sql = @"SELECT k.id,
                                  sp.tenSP,
                                  sp.mau,
                                  sp.kichco,
                                  k.soLuong
                           FROM Kho k
                           JOIN SanPham sp ON k.sanphamID = sp.id
                           WHERE k.soLuong <= @sl";

            SqlParameter[] pa = {
                new SqlParameter("@sl", threshold)
            };

            return DBConnection.GetDataTable(sql, pa);
        }
    }
}
