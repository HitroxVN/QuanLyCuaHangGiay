using System;
using System.Collections.Generic;
using System.Data;
using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.model;
using QuanLyCuaHangGiay.util;

namespace QuanLyCuaHangGiay.controller
{
    public class ThongKeController
    {
        private ThongKeRepository repository = new ThongKeRepository();

        public ThongKeController()
        {
            if (!Authorization.IsAdmin() && !Authorization.IsStaff())
            {
                throw new UnauthorizedAccessException("Không có quyền xem thống kê");
            }
        }

        public ThongKe LayTongQuan(DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayTongQuan(tuNgay, denNgay);
        }

        public List<BieuDoThongKe> LayDoanhThuTheoThang(DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayDoanhThuTheoThang(tuNgay, denNgay);
        }

        public List<BieuDoThongKe> LayNhapHangTheoThang(DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayNhapHangTheoThang(tuNgay, denNgay);
        }

        public List<BieuDoThongKe> LayTop5SanPhamBanChay(DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayTop5SanPhamBanChay(tuNgay, denNgay);
        }

        public DataTable LayBangTopSanPhamBanChay(DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayBangTopSanPhamBanChay(tuNgay, denNgay);
        }

        public DataTable LayDanhSachSanPham()
        {
            return repository.LayDanhSachSanPham();
        }

        public DataTable LayDanhSachNhaCungCap()
        {
            return repository.LayDanhSachNhaCungCap();
        }

        public DataTable LayDanhSachDonHang(DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayDanhSachDonHang(tuNgay, denNgay);
        }

        public DataTable LayDanhSachPhieuNhap(DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayDanhSachPhieuNhap(tuNgay, denNgay);
        }

        public DataTable LayTonKhoLauNhat()
        {
            return repository.LayTonKhoLauNhat();
        }

        public DataTable LayDanhSachDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayDanhSachDoanhThu(tuNgay, denNgay);
        }
    }
}