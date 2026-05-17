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

        // =======================================================================
        // 1. CHỨC NĂNG XEM VÀ LỌC (Cả Admin và Staff đều được dùng)
        // =======================================================================
        public DataTable SearchAndFilter(string keyword, int categoryId, string status)
        {
            if (keyword == null) keyword = "";
            if (status == null) status = "Tất cả";

            return repo.SearchAndFilter(keyword, categoryId, status);
        }

        public DataTable GetAllProducts()
        {
            return SearchAndFilter("", 0, "Tất cả");
        }

        // =======================================================================
        // 2. CHỨC NĂNG THÊM, SỬA, XÓA (CHỈ ADMIN MỚI ĐƯỢC DÙNG)
        // =======================================================================
        public bool AddProduct(string tenSP, decimal gia, string anh, string mau, string kichCo, int danhMucID, string trangThai)
        {
            // Chặn đứng Staff
            if (!Authorization.IsAdmin()) throw new UnauthorizedAccessException("Chỉ Admin mới được thêm sản phẩm.");

            if (string.IsNullOrWhiteSpace(tenSP) || gia < 0 || danhMucID <= 0) return false;

            Products sp = new Products(tenSP, gia, anh, mau, kichCo, danhMucID, trangThai);
            return repo.Insert(sp) > 0;
        }

        public bool UpdateProduct(int id, string tenSP, decimal gia, string anh, string mau, string kichCo, int danhMucID, string trangThai, int soLuong)
        {
            // Chặn đứng Staff
            if (!Authorization.IsAdmin()) throw new UnauthorizedAccessException("Chỉ Admin mới được cập nhật sản phẩm.");

            if (id <= 0 || string.IsNullOrWhiteSpace(tenSP) || gia < 0 || danhMucID <= 0) return false;

            Products sp = new Products(id, tenSP, gia, anh, mau, kichCo, danhMucID, trangThai, DateTime.Now, soLuong);
            return repo.Update(sp) > 0;
        }

        public bool DeleteProduct(int id)
        {
            // Chặn đứng Staff không cho xóa (kể cả xóa mềm)
            if (!Authorization.IsAdmin()) throw new UnauthorizedAccessException("Chỉ Admin mới được xóa sản phẩm.");

            if (id <= 0) return false;

            // Admin xóa cứng
            return repo.Delete(id) > 0;
        }

        public int GetNextProductId()
        {
            return repo.GetNextProductId();
        }
    }
}