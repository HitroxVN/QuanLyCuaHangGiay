using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.model
{
    public class PhieuNhap
    {
        public int id {  get; set; }    
        public int sanPhamID { get; set; }
        public int nhaCungCapID { get; set; }
        public int soLuong {  get; set; }
        public decimal giaNhap { get; set; }
        public DateTime thoiGian { get; set; }
        public string ghiChu { get; set; }

        public PhieuNhap() { }

        public PhieuNhap(int id, int sanPhamID, int nhaCungCapID, int soLuong, decimal giaNhap, DateTime thoiGian, string ghiChu)
        {
            this.id = id;
            this.sanPhamID = sanPhamID;
            this.nhaCungCapID = nhaCungCapID;
            this.soLuong = soLuong;
            this.giaNhap = giaNhap;
            this.thoiGian = thoiGian;
            this.ghiChu = ghiChu;
        }
    }
}
