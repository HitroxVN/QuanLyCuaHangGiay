using System;
using System.Data;
using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.model;
using QuanLyCuaHangGiay.util;

namespace QuanLyCuaHangGiay.controller
{
    internal class ProductController
    {
        private ProductRepository repo = new ProductRepository();

        public ProductController()
        {
            if (!Authorization.IsAdmin() && !Authorization.IsStaff())
            {
                throw new UnauthorizedAccessException("Không có quyền truy cập hệ thống.");
            }
        }


        public DataTable SearchAndFilter(string keyword, int categoryId, string status)
        {
            if (keyword == null) keyword = "";
            if (status == null) status = "Tất cả";

            // ĐÃ XÓA ĐOẠN ÉP CỨNG STAFF XEM ACTIVE: Giờ đây Staff có thể xem cả "Tất cả", "active" và "inactive"
            return repo.SearchAndFilter(keyword, categoryId, status);
        }

        public DataTable GetAllProducts()
        {
            return SearchAndFilter("", 0, "Tất cả");
        }


        public bool AddProduct(string tenSP, decimal gia, string anh, string mau, string kichCo, int danhMucID, string trangThai)
        {
            // Cho phép cả Admin và Staff thêm sản phẩm
            if (string.IsNullOrWhiteSpace(tenSP) || gia < 0 || danhMucID <= 0) return false;

            // Đảm bảo an toàn: Nếu là Staff thêm mới, mặc định trạng thái luôn là "active"
            if (Authorization.IsStaff()) trangThai = "active";

            Products sp = new Products(tenSP, gia, anh, mau, kichCo, danhMucID, trangThai);
            return repo.Insert(sp) > 0;
        }

        public bool UpdateProduct(int id, string tenSP, decimal gia, string anh, string mau, string kichCo, int danhMucID, string trangThai, int soLuong)
        {
            // Cho phép cả Admin và Staff sửa sản phẩm
            if (id <= 0 || string.IsNullOrWhiteSpace(tenSP) || gia < 0 || danhMucID <= 0) return false;

            Products sp = new Products(id, tenSP, gia, anh, mau, kichCo, danhMucID, trangThai, DateTime.Now, soLuong);
            return repo.Update(sp) > 0;
        }

        public bool DeleteProduct(int id)
        {
            if (id <= 0) return false;

            if (Authorization.IsStaff())
            {
                // YÊU CẦU CỦA BẠN: Nhân viên xóa -> Xóa mềm (đổi thành inactive)
                return repo.ChangeStatus(id, "inactive") > 0;
            }
            else
            {
                // Admin xóa -> Xóa cứng mất khỏi Database
                return repo.Delete(id) > 0;
            }
        }

        public int GetNextProductId()
        {
            return repo.GetNextProductId();
        }
    }
}