using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_store.model
{
    public class DonHang
    {
        public int Id { get; set; }
        public int TaikhoanID { get; set; }
        public DateTime NgayTao { get; set; }
        public decimal TongTien { get; set; }
        public decimal TongGia { get; set; }
        public string TrangThai { get; set; }
        public decimal ChietKhau { get; set; }
        public string Pttt { get; set; }
        public string GhiChu { get; set; }
        // Sử dụng int? (nullable) vì khách hàng vãng lai sẽ có khachhangID là null
        public int? KhachhangID { get; set; }
        public int DiemSuDung { get; set; }

        public DonHang()
        {
        }

        public DonHang(int id, int taikhoanID, DateTime ngayTao, decimal tongTien, decimal tongGia, string trangThai, decimal chietKhau, string pttt, string ghiChu, int? khachhangID, int diemSuDung)
        {
            Id = id;
            TaikhoanID = taikhoanID;
            NgayTao = ngayTao;
            TongTien = tongTien;
            TongGia = tongGia;
            TrangThai = trangThai;
            ChietKhau = chietKhau;
            Pttt = pttt;
            GhiChu = ghiChu;
            KhachhangID = khachhangID;
            DiemSuDung = diemSuDung;
        }
    }
}
