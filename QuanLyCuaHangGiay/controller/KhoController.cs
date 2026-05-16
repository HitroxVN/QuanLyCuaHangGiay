using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.Database;
using QuanLyCuaHangGiay.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.controller
{
    public class KhoController
    {
        private KhoRepository _repo;
        public KhoController()
        {
            if (
                !Authorization.IsAdmin() &&
                !Authorization.IsStaff()
               )
            {
                throw new UnauthorizedAccessException(
                    "Không có quyền"
                );
            }
            _repo = new KhoRepository();
        }

        public DataTable LayDanhSachNhaCungCap()
        {
            return _repo.GetDanhSachNhaCungCap();
        }

        public DataTable LayLichSuNhapHang(DateTime tuNgay, DateTime denNgay, string nhaCungCapID, string tuKhoa)
        {
            if (tuNgay.Date > denNgay.Date)
            {
                throw new ArgumentException("Thời gian 'Từ ngày' không thể lớn hơn 'Đến ngày'.");
            }

            return _repo.GetLichSuNhapHang(tuNgay, denNgay, nhaCungCapID, tuKhoa);
        }

        public DataTable GetPhieuNhapByTime(DateTime thoiGian)
        {
            return _repo.GetPhieuNhapByTime(thoiGian);
        }
    }
}
