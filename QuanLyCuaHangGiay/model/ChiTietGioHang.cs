using System;

namespace QuanLyCuaHangGiay.model
{
    public class ChiTietGioHang
    {
        // Các thông tin cơ bản của 1 dòng trong giỏ hàng
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MauSac { get; set; }
        public string KichCo { get; set; }

        public int SoLuongMua { get; set; }
        public decimal DonGia { get; set; }

        // Tiền giảm giá trực tiếp cho 1 đôi giày (Ví dụ: giảm 50k)
        public decimal GiamGia { get; set; }

        // Thành tiền sẽ tự động tính = (Giá - Giảm) x Số lượng mua
        // Chỗ này chỉ có hàm get (chỉ đọc), không cho phép thu ngân sửa thẳng thành tiền
        public decimal ThanhTien
        {
            get
            {
                return (DonGia - GiamGia) * SoLuongMua;
            }
        }
    }
}