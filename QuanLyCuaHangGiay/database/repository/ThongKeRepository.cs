using QuanLyCuaHangGiay.Database;
using QuanLyCuaHangGiay.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCuaHangGiay.database.repository
{
    public class ThongKeRepository
    {
        public ThongKe LayTongQuan(DateTime tuNgay, DateTime denNgay)
        {
            ThongKe tk = new ThongKe();

            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT 
                        (SELECT COUNT(*) FROM SanPham) AS TongSanPham,
                        (SELECT COUNT(*) FROM NhaCungCap) AS TongNhaCungCap,
                        (SELECT COUNT(*) 
                         FROM DonHang 
                         WHERE ngayTao >= @TuNgay AND ngayTao < DATEADD(DAY, 1, @DenNgay)) AS TongDonHang,
                        (SELECT COUNT(*) 
                         FROM PhieuNhap 
                         WHERE thoiGian >= @TuNgay AND thoiGian < DATEADD(DAY, 1, @DenNgay)) AS TongPhieuNhap,
                        (SELECT ISNULL(SUM(soLuong), 0) FROM Kho) AS TongSoLuongTon,
                        (SELECT ISNULL(SUM(tongTien), 0) 
                         FROM DonHang 
                         WHERE ngayTao >= @TuNgay AND ngayTao < DATEADD(DAY, 1, @DenNgay)) AS TongDoanhThu
                ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tk.TongSanPham = Convert.ToInt32(reader["TongSanPham"]);
                            tk.TongNhaCungCap = Convert.ToInt32(reader["TongNhaCungCap"]);
                            tk.TongDonHang = Convert.ToInt32(reader["TongDonHang"]);
                            tk.TongPhieuNhap = Convert.ToInt32(reader["TongPhieuNhap"]);
                            tk.TongSoLuongTon = Convert.ToInt32(reader["TongSoLuongTon"]);
                            tk.TongDoanhThu = Convert.ToDecimal(reader["TongDoanhThu"]);
                        }
                    }
                }
            }

            return tk;
        }

        public List<BieuDoThongKe> LayDoanhThuTheoThang(int nam, DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds = new List<BieuDoThongKe>();

            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT MONTH(ngayTao) AS Thang, ISNULL(SUM(tongTien), 0) AS DoanhThu
                    FROM DonHang
                    WHERE YEAR(ngayTao) = @Nam 
                      AND ngayTao >= @TuNgay AND ngayTao < DATEADD(DAY, 1, @DenNgay)
                    GROUP BY MONTH(ngayTao)
                    ORDER BY MONTH(ngayTao)
                ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nam", nam);
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ds.Add(new BieuDoThongKe
                            {
                                Nhan = "Tháng " + reader["Thang"],
                                GiaTri = Convert.ToDecimal(reader["DoanhThu"])
                            });
                        }
                    }
                }
            }

            return ds;
        }

        public List<BieuDoThongKe> LayNhapHangTheoThang(int nam, DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds = new List<BieuDoThongKe>();

            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT MONTH(thoiGian) AS Thang, ISNULL(SUM(soLuong), 0) AS TongNhap
                    FROM PhieuNhap
                    WHERE YEAR(thoiGian) = @Nam 
                      AND thoiGian >= @TuNgay AND thoiGian < DATEADD(DAY, 1, @DenNgay)
                    GROUP BY MONTH(thoiGian)
                    ORDER BY MONTH(thoiGian)
                ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nam", nam);
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ds.Add(new BieuDoThongKe
                            {
                                Nhan = "Tháng " + reader["Thang"],
                                GiaTri = Convert.ToDecimal(reader["TongNhap"])
                            });
                        }
                    }
                }
            }

            return ds;
        }

        public List<BieuDoThongKe> LayTop5SanPhamBanChay(DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds = new List<BieuDoThongKe>();

            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT TOP 5 sp.tenSP, SUM(ct.soLuong) AS TongBan
                    FROM ChiTietDonHang ct
                    INNER JOIN DonHang dh ON ct.donhangID = dh.id
                    INNER JOIN SanPham sp ON ct.sanphamID = sp.id
                    WHERE dh.ngayTao >= @TuNgay AND dh.ngayTao < DATEADD(DAY, 1, @DenNgay)
                    GROUP BY sp.tenSP
                    ORDER BY TongBan DESC
                ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ds.Add(new BieuDoThongKe
                            {
                                Nhan = reader["tenSP"].ToString(),
                                GiaTri = Convert.ToDecimal(reader["TongBan"])
                            });
                        }
                    }
                }
            }

            return ds;
        }

        public DataTable LayBangTopSanPhamBanChay(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT TOP 5 sp.tenSP AS N'Sản phẩm', SUM(ct.soLuong) AS N'Số lượng bán'
                FROM ChiTietDonHang ct
                INNER JOIN DonHang dh ON ct.donhangID = dh.id
                INNER JOIN SanPham sp ON ct.sanphamID = sp.id
                WHERE dh.ngayTao >= @TuNgay AND dh.ngayTao < DATEADD(DAY, 1, @DenNgay)
                GROUP BY sp.tenSP
                ORDER BY SUM(ct.soLuong) DESC
            ";

            SqlParameter[] pa = new SqlParameter[]
            {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };

            return DBConnection.GetDataTable(sql, pa);
        }
    }
}