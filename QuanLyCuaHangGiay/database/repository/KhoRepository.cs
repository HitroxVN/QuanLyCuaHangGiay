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
        // 1. Lấy danh sách Nhà cung cấp đổ vào ComboBox
        public DataTable GetDanhSachNhaCungCap()
        {
            string sql = "SELECT id, tenNCC FROM NhaCungCap";
            return DBConnection.GetDataTable(sql);
        }

        // 2. Lấy lịch sử nhập hàng theo các tiêu chí lọc
        public DataTable GetLichSuNhapHang(DateTime tuNgay, DateTime denNgay, string nhaCungCapID, string tuKhoa)
        {
            // Thiết lập đến cuối ngày của mốc "Đến ngày" để lấy trọn vẹn dữ liệu
            denNgay = denNgay.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            string sql = @"SELECT pn.id AS [Mã Phiếu], 
                                  pn.thoiGian AS [Ngày Nhập], 
                                  sp.tenSP AS [Tên Sản Phẩm], 
                                  ncc.tenNCC AS [Nhà Cung Cấp], 
                                  pn.soLuong AS [Số Lượng Nhập], 
                                  pn.giaDonNhap AS [Đơn Giá],
                                  (pn.soLuong * pn.giaDonNhap) AS [Thành Tiền],
                                  pn.ghiChu AS [Ghi Chú]
                           FROM PhieuNhap pn
                           JOIN SanPham sp ON pn.sanphamID = sp.id
                           JOIN NhaCungCap ncc ON pn.nhacungcapID = ncc.id
                           WHERE pn.thoiGian >= @tuNgay AND pn.thoiGian <= @denNgay";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@tuNgay", tuNgay),
                new SqlParameter("@denNgay", denNgay)
            };

            // Lọc theo nhà cung cấp nếu có chọn (khác "Tất cả")
            if (!string.IsNullOrEmpty(nhaCungCapID) && nhaCungCapID != "0")
            {
                sql += " AND pn.nhacungcapID = @nccID";
                parameters.Add(new SqlParameter("@nccID", nhaCungCapID));
            }

            // Lọc theo tên sản phẩm nếu có nhập từ khóa
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                sql += " AND sp.tenSP LIKE @tuKhoa";
                parameters.Add(new SqlParameter("@tuKhoa", "%" + tuKhoa + "%"));
            }

            sql += " ORDER BY pn.thoiGian DESC"; 

            return DBConnection.GetDataTable(sql, parameters.ToArray());
        }
    }
}
