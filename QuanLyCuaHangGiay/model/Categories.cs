using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.model
{
    internal class Categories
    {
        public int Id { get; set; }
        public string TenDanhMuc { get; set; }
        public DateTime NgayTao { get; set; }
        public string TrangThai { get; set; }

        // Constructor mặc định (cần thiết khi truy vấn dữ liệu và map vào object)
        public Categories()
        {
        }

        // Constructor đầy đủ tham số
        public Categories(int id, string tenDanhMuc, DateTime ngayTao, string trangThai)
        {
            Id = id;
            TenDanhMuc = tenDanhMuc;
            NgayTao = ngayTao;
            TrangThai = trangThai;
        }

        // Constructor không có Id và NgayTao (dùng khi thêm mới, vì SQL tự tăng ID và gán ngày mặc định)
        public Categories(string tenDanhMuc, string trangThai)
        {
            TenDanhMuc = tenDanhMuc;
            TrangThai = trangThai;
        }
    }
}