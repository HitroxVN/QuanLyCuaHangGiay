using QuanLyCuaHangGiay.database.repository;
using System;
using System.Data;

namespace QuanLyCuaHangGiay.controller
{
    public class KhachHangController
    {
        private KhachHangRepository repo = new KhachHangRepository();

        public DataTable GetDanhSachKhachHang(string tuKhoa)
        {
            return repo.GetDanhSachKhachHang(tuKhoa);
        }

        public bool UpdateKhachHang(int id, string hoTen, string sdt)
        {
            return repo.UpdateKhachHang(id, hoTen, sdt);
        }

        public bool DeleteKhachHang(int id)
        {
            return repo.DeleteKhachHang(id);
        }

        public bool ThemKhachHang(string hoTen, string sdt)
        {
            return repo.ThemKhachHang(hoTen, sdt);
        }
    }
}