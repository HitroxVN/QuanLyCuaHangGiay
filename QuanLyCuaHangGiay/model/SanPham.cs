using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.model
{
    public class SanPham
    {
        public int id { get; set; }
        public string tenSP { get; set; }
        public decimal gia { get; set; }
        public string anh { get; set; }
        public string mau { get; set; }
        public string kichco { get; set; }
        public int danhmucID { get; set; }
        public string trangthai { get; set; }
        public DateTime ngayTao { get; set; }

        public SanPham() { }

        public SanPham(int id, string tenSP, decimal gia, string anh, string mau, string kichco, int danhmucID, string trangthai, DateTime ngayTao)
        {
            this.id = id;
            this.tenSP = tenSP;
            this.gia = gia;
            this.anh = anh;
            this.mau = mau;
            this.kichco = kichco;
            this.danhmucID = danhmucID;
            this.trangthai = trangthai;
            this.ngayTao = ngayTao;
        }
    }
}
