using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.Database;
using QuanLyCuaHangGiay.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.controller
{
    public class KhoController
    {
        private KhoRepository _repo;
        public KhoController()
        {
            if (
                !Authorization.IsAdmin() &&
                !Authorization.IsStaff()
               )
            {
                throw new UnauthorizedAccessException(
                    "Không có quyền"
                );
            }
            _repo = new KhoRepository();
        }

        public DataTable LayDanhSachKho()
        {
            return _repo.GetTenKhoDuyNhat();
        }

        public DataTable LayChiTietTonKho(string tenKho = "Tất cả")
        {
            return _repo.GetTonKhoChiTiet(tenKho);
        }

        public string CapNhatDiaChi(int idKho, string diaChiMoi)
        {
            if (idKho <= 0)
                return "Vui lòng chọn một dòng sản phẩm trong kho để cập nhật!";

            if (string.IsNullOrWhiteSpace(diaChiMoi))
                return "Vui lòng nhập địa chỉ mới!";

            bool isSuccess = _repo.CapNhatDiaChi(idKho, diaChiMoi);
            return isSuccess ? "Success" : "Có lỗi khi cập nhật địa chỉ trong cơ sở dữ liệu.";
        }
    }
}
