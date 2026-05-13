using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCuaHangGiay.controller
{
    public class DonHangController
    {
        public DonHangController()
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
        }

        private DonHangRepository repo = new DonHangRepository();

        public DataTable GetDanhSachDonHang(DateTime? tuNgay, DateTime? denNgay, string tuKhoa)
        {
            return repo.GetDanhSachDonHang(tuNgay, denNgay, tuKhoa);
        }

        public bool HuyDonHang(int maDonHang)
        {
            return repo.HuyDonHang(maDonHang);
        }

        public bool XoaVinhVienDonHang(int maDonHang)
        {
            return repo.XoaVinhVienDonHang(maDonHang);
        }
    }
}