using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.model
{
    internal class Users
    {
        public int id { get; set; }
        public string hoTen { get; set; }
        public string email { get; set; }
        public string matKhau { get; set; }
        public string sdt { get; set; }
        public string diaChi { get; set; }
        public string quyen { get; set; }
        public DateTime ngayTao { get; set; }
        public string trangThai { get; set; }
    }
}
