using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.model
{
    public class Kho
    {
        public int id { get; set; }
        public int sanphamID { get; set; }
        public int soLuong { get; set; }

        public Kho() { }

        public Kho(int id, int sanphamID, int soLuong)
        {
            this.id = id;
            this.sanphamID = sanphamID;
            this.soLuong = soLuong;
        }
    }
}
