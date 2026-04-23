using System;
using System.Collections.Generic;
using System.Data;
using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.model;

namespace QuanLyCuaHangGiay.controller
{
    public class ThongKeController
    {
        private ThongKeRepository repository = new ThongKeRepository();

        public ThongKe LayTongQuan(DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayTongQuan(tuNgay, denNgay);
        }

        public List<BieuDoThongKe> LayDoanhThuTheoThang(int nam, DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayDoanhThuTheoThang(nam, tuNgay, denNgay);
        }

        public List<BieuDoThongKe> LayNhapHangTheoThang(int nam, DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayNhapHangTheoThang(nam, tuNgay, denNgay);
        }

        public List<BieuDoThongKe> LayTop5SanPhamBanChay(DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayTop5SanPhamBanChay(tuNgay, denNgay);
        }

        public DataTable LayBangTopSanPhamBanChay(DateTime tuNgay, DateTime denNgay)
        {
            return repository.LayBangTopSanPhamBanChay(tuNgay, denNgay);
        }
    }
}