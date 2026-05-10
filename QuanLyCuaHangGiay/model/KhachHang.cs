using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_store.model
{
    public class KhachHang
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string Sdt { get; set; }
        public int DiemTichLuy { get; set; }
        public DateTime NgayTao { get; set; }

        public KhachHang()
        {
        }

        public KhachHang(int id, string hoTen, string sdt, int diemTichLuy, DateTime ngayTao)
        {
            Id = id;
            HoTen = hoTen;
            Sdt = sdt;
            DiemTichLuy = diemTichLuy;
            NgayTao = ngayTao;
        }
    }
}
