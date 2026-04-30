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

        // Lấy danh sách sản phẩm (Sử dụng hàm Join để hiển thị tên danh mục thay vì ID)
        public DataTable GetAllProducts()
        {
            // Nếu là Nhân viên -> Chỉ cho xem sản phẩm Active
            if (Authorization.IsStaff())
            {
                return repo.GetActiveProductsWithCategoryName();
            }
            // Nếu là Admin -> Cho xem tất cả
            return repo.GetAllWithCategoryName();
        }

        // Thêm sản phẩm mới
        public bool AddProduct(string tenSP, decimal gia, string anh, string mau, string kichCo, int danhMucID, string trangThai)
        {
            // Kiểm tra dữ liệu đầu vào bắt buộc
            if (string.IsNullOrWhiteSpace(tenSP) || gia < 0 || danhMucID <= 0)
            {
                return false;
            }

            Products sp = new Products(tenSP, gia, anh, mau, kichCo, danhMucID, trangThai);
            int result = repo.Insert(sp);

            return result > 0;
        }

        // Cập nhật sản phẩm
        public bool UpdateProduct(int id, string tenSP, decimal gia, string anh, string mau, string kichCo, int danhMucID, string trangThai)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(tenSP) || gia < 0 || danhMucID <= 0)
            {
                return false;
            }

            Products sp = new Products(id, tenSP, gia, anh, mau, kichCo, danhMucID, trangThai, DateTime.Now);
            int result = repo.Update(sp);

            return result > 0;
        }

        // Xóa sản phẩm
        public bool DeleteProduct(int id)
        {
            if (id <= 0) return false;

            if (Authorization.IsStaff())
            {
                // Nhân viên -> Bấm xóa là Xóa mềm (Đổi sang Inactive)
                int result = repo.ChangeStatus(id, "inactive");
                return result > 0;
            }
            else
            {
                // Admin -> Xóa cứng (Mất luôn khỏi CSDL)
                // (Hoặc bạn có thể cho Admin xóa mềm luôn nếu muốn giữ lịch sử dữ liệu)
                int result = repo.Delete(id);
                return result > 0;
            }
        }

        // Tìm kiếm sản phẩm
        public DataTable SearchProduct(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetAllProducts();
            }
            return repo.Search(keyword);
        }

        public int GetNextProductId()
        {
            return repo.GetNextProductId();
        }

        // Cập nhật sản phẩm (Thêm tham số int soLuong vào cuối)
        public bool UpdateProduct(int id, string tenSP, decimal gia, string anh, string mau, string kichCo, int danhMucID, string trangThai, int soLuong)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(tenSP) || gia < 0 || danhMucID <= 0)
            {
                return false;
            }

            // Đưa thêm soLuong vào để truyền xuống Repository
            Products sp = new Products(id, tenSP, gia, anh, mau, kichCo, danhMucID, trangThai, DateTime.Now, soLuong);
            int result = repo.Update(sp);

            return result > 0;
        }
    }
}