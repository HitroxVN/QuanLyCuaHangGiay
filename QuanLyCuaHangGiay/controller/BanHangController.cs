using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.util;
using QuanLyCuaHangGiay.model;
using System;
using System.ComponentModel;
using System.Data;

namespace QuanLyCuaHangGiay.controller
{
    public class BanHangController
    {
        public BanHangController()
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
        
        private BanHangRepository repo = new BanHangRepository();

        public DataTable LayDanhSachSanPhamBan()
        {
            return repo.LayDanhSachSanPhamBan();
        }

        public int ThanhToanDonHang(int taikhoanID, decimal tongGiaGoc, decimal tienChietKhau, decimal tongTienSauGiam,
                            string sdt, string pttt, string ghiChu, int diemDaDung, BindingList<ChiTietGioHang> gioHang)
        {
            return repo.ThanhToanDonHang(tongGiaGoc, tienChietKhau, tongTienSauGiam, sdt, pttt, diemDaDung, gioHang);
        }

        public DataTable TimKhachHangTheoSDT(string sdt)
        {
            return repo.TimKhachHangTheoSDT(sdt);
        }

        public void ThemKhachHangMoi(string hoTen, string sdt)
        {
            repo.ThemKhachHangMoi(hoTen, sdt);
        }

        public int TaoDonHangMoi(int taiKhoanID, decimal tongTien, decimal tongGia, string trangThai,
                                 decimal chietKhau, string pttt, string ghiChu, int khachHangID, int diemSuDung)
        {
            return repo.TaoDonHangMoi(taiKhoanID, tongTien, tongGia, trangThai, chietKhau, pttt, ghiChu, khachHangID, diemSuDung);
        }
    }
}