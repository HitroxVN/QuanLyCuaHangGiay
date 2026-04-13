using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.model
{
    internal class Products
    {
        public int Id { get; set; }
        public string TenSP { get; set; }
        public decimal Gia { get; set; }
        public string Anh { get; set; }
        public string Mau { get; set; }
        public string KichCo { get; set; }
        public int DanhMucID { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }

        // Constructor mặc định
        public Products()
        {
        }

        // Constructor đầy đủ tham số
        public Products(int id, string tenSP, decimal gia, string anh, string mau, string kichCo, int danhMucID, string trangThai, DateTime ngayTao)
        {
            Id = id;
            TenSP = tenSP;
            Gia = gia;
            Anh = anh;
            Mau = mau;
            KichCo = kichCo;
            DanhMucID = danhMucID;
            TrangThai = trangThai;
            NgayTao = ngayTao;
        }

        // Constructor không có Id và NgayTao (dùng khi thực thi lệnh INSERT INTO)
        public Products(string tenSP, decimal gia, string anh, string mau, string kichCo, int danhMucID, string trangThai)
        {
            TenSP = tenSP;
            Gia = gia;
            Anh = anh;
            Mau = mau;
            KichCo = kichCo;
            DanhMucID = danhMucID;
            TrangThai = trangThai;
        }
    }
}