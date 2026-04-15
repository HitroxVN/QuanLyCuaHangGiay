using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.model
{
    public class NhaCungCap
    {
        public int id { get; set; }
        public string tenNCC { get; set; }
        public string email { get; set; }
        public string diaChi { get; set; }
        public string sdt { get; set; }
        public string trangthai { get; set; }

        public NhaCungCap() { }

        public NhaCungCap(int id, string tenNCC, string email, string diaChi, string sdt, string trangthai)
        {
            this.id = id;
            this.tenNCC = tenNCC;
            this.email = email;
            this.diaChi = diaChi;
            this.sdt = sdt;
            this.trangthai = trangthai;
        }
    }
}
