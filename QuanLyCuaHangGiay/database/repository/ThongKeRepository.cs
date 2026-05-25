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
                 WHERE ngayTao >= @TuNgay 
                 AND ngayTao < DATEADD(DAY,1,@DenNgay)) AS TongDonHang,

                (SELECT COUNT(*) 
                 FROM PhieuNhap 
                 WHERE thoiGian >= @TuNgay 
                 AND thoiGian < DATEADD(DAY,1,@DenNgay)) AS TongPhieuNhap,

                (SELECT ISNULL(SUM(soLuong),0) 
                 FROM SanPham) AS TongSoLuongTon,

                (SELECT ISNULL(SUM(tongTien),0) 
                 FROM DonHang 
                 WHERE ngayTao >= @TuNgay 
                 AND ngayTao < DATEADD(DAY,1,@DenNgay)) AS TongDoanhThu
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

        // ==================== CHART DOANH THU ====================

        public List<BieuDoThongKe> LayDoanhThuTheoThang(DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds = new List<BieuDoThongKe>();

            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();

                string sql = @"
            SELECT 
                MONTH(ngayTao) AS Thang,
                ISNULL(SUM(tongTien),0) AS DoanhThu
            FROM DonHang
            WHERE ngayTao >= @TuNgay
            AND ngayTao < DATEADD(DAY,1,@DenNgay)
            GROUP BY MONTH(ngayTao)
            ORDER BY MONTH(ngayTao)
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
                                Nhan = "Tháng " + reader["Thang"].ToString(),
                                GiaTri = Convert.ToDecimal(reader["DoanhThu"])
                            });
                        }
                    }
                }
            }

            return ds;
        }
        // ==================== CHART NHẬP HÀNG ====================

        public List<BieuDoThongKe> LayNhapHangTheoThang(DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds = new List<BieuDoThongKe>();

            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT 
                        MONTH(thoiGian) AS Thang,
                        ISNULL(SUM(soLuong),0) AS TongNhap
                    FROM PhieuNhap
                    WHERE thoiGian >= @TuNgay
                    AND thoiGian < DATEADD(DAY,1,@DenNgay)
                    GROUP BY MONTH(thoiGian)
                    ORDER BY MONTH(thoiGian)
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
                                Nhan = "Tháng " + reader["Thang"].ToString(),
                                GiaTri = Convert.ToDecimal(reader["TongNhap"])
                            });
                        }
                    }
                }
            }

            return ds;
        }

        // ==================== TOP 5 BÁN CHẠY ====================

        public List<BieuDoThongKe> LayTop5SanPhamBanChay(DateTime tuNgay, DateTime denNgay)
        {
            List<BieuDoThongKe> ds = new List<BieuDoThongKe>();

            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT TOP 5 
                        sp.tenSP,
                        SUM(ct.soLuong) AS TongBan
                    FROM ChiTietDonHang ct
                    INNER JOIN DonHang dh ON ct.donhangID = dh.id
                    INNER JOIN SanPham sp ON ct.sanphamID = sp.id
                    WHERE dh.ngayTao >= @TuNgay
                    AND dh.ngayTao < DATEADD(DAY,1,@DenNgay)
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

        // ==================== BẢNG TOP BÁN CHẠY ====================

        public DataTable LayBangTopSanPhamBanChay(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
                SELECT TOP 5
                    sp.tenSP AS N'Sản phẩm',
                    SUM(ct.soLuong) AS N'Số lượng bán'
                FROM ChiTietDonHang ct
                INNER JOIN DonHang dh ON ct.donhangID = dh.id
                INNER JOIN SanPham sp ON ct.sanphamID = sp.id
                WHERE dh.ngayTao >= @TuNgay
                AND dh.ngayTao < DATEADD(DAY,1,@DenNgay)
                GROUP BY sp.tenSP
                ORDER BY SUM(ct.soLuong) DESC
            ";

            SqlParameter[] pa =
            {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };

            return DBConnection.GetDataTable(sql, pa);
        }

        // ==================== DANH SÁCH SẢN PHẨM ====================

        public DataTable LayDanhSachSanPham()
        {
            string sql = @"
        SELECT 
            id AS N'Mã SP',
            tenSP AS N'Tên sản phẩm',
            soLuong AS N'Tồn kho',
            gia AS N'Giá',
            mau AS N'Màu',
            kichco AS N'Kích cỡ',
            ngayTao AS N'Ngày tạo'
        FROM SanPham
    ";

            return DBConnection.GetDataTable(sql);
        }

        // ==================== NHÀ CUNG CẤP ====================

        public DataTable LayDanhSachNhaCungCap()
        {
            string sql = @"
                SELECT 
                    id AS N'Mã NCC',
                    tenNCC AS N'Tên nhà cung cấp',
                    sdt AS N'SĐT',
                    diaChi AS N'Địa chỉ'
                FROM NhaCungCap
            ";

            return DBConnection.GetDataTable(sql);
        }

        // ==================== ĐƠN HÀNG ====================

        public DataTable LayDanhSachDonHang(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
        SELECT
            dh.id AS N'Mã đơn',
            dh.ngayTao AS N'Ngày bán',
            tk.sdt AS N'Khách hàng',
            tk.hoTen AS N'Nhân viên',
            dh.tongTien AS N'Thành tiền',
            dh.trangThai AS N'Trạng thái'
        FROM DonHang dh
        LEFT JOIN TaiKhoan tk ON dh.taikhoanID = tk.id
        WHERE dh.ngayTao >= @TuNgay
        AND dh.ngayTao < DATEADD(DAY,1,@DenNgay)
        ORDER BY dh.ngayTao DESC
    ";

            SqlParameter[] pa =
            {
        new SqlParameter("@TuNgay", tuNgay.Date),
        new SqlParameter("@DenNgay", denNgay.Date)
    };

            return DBConnection.GetDataTable(sql, pa);
        }

        // ==================== PHIẾU NHẬP ====================

        public DataTable LayDanhSachPhieuNhap(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
        SELECT
            id AS N'Mã phiếu nhập',

            thoiGian AS N'Thời gian',

            soLuong AS N'Số lượng nhập',

            giaDonNhap AS N'Giá nhập',

            (soLuong * giaDonNhap) AS N'Thành tiền'

        FROM PhieuNhap

        WHERE thoiGian >= @TuNgay
        AND thoiGian < DATEADD(DAY,1,@DenNgay)
    ";

            SqlParameter[] pa =
            {
        new SqlParameter("@TuNgay", tuNgay.Date),
        new SqlParameter("@DenNgay", denNgay.Date)
    };

            return DBConnection.GetDataTable(sql, pa);
        }
        // ==================== TỒN KHO LÂU NHẤT ====================

        public DataTable LayTonKhoLauNhat()
        {
            string sql = @"
        SELECT TOP 10
            tenSP AS N'Sản phẩm',
            soLuong AS N'Số lượng tồn',
            gia AS N'Giá',
            ngayTao AS N'Ngày nhập',
            mau AS N'Màu',
            kichco AS N'Kích cỡ'
        FROM SanPham
        ORDER BY soLuong DESC, ngayTao ASC
    ";

            return DBConnection.GetDataTable(sql);
        }


        // ==================== DOANH THU ====================

        public DataTable LayDanhSachDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
        SELECT
            dh.id AS N'Mã đơn',
            dh.ngayTao AS N'Ngày bán',
            tk.sdt AS N'Khách hàng',
            tk.hoTen AS N'Nhân viên',
            dh.tongTien AS N'Thành tiền',
            dh.trangThai AS N'Trạng thái'
        FROM DonHang dh
        LEFT JOIN TaiKhoan tk ON dh.taikhoanID = tk.id
        WHERE dh.ngayTao >= @TuNgay
        AND dh.ngayTao < DATEADD(DAY,1,@DenNgay)
        ORDER BY dh.ngayTao DESC
    ";

            SqlParameter[] pa =
            {
        new SqlParameter("@TuNgay", tuNgay.Date),
        new SqlParameter("@DenNgay", denNgay.Date)
    };

            return DBConnection.GetDataTable(sql, pa);
        }
    }
}