using System;
using System.Data;
using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.model;
using QuanLyCuaHangGiay.util;

namespace QuanLyCuaHangGiay.controller
{
    internal class CategoryController
    {
        private CategoryRepository repo = new CategoryRepository();

        public CategoryController()
        {
            if (!Authorization.IsAdmin() && !Authorization.IsStaff())
            {
                throw new UnauthorizedAccessException("Không có quyền truy cập hệ thống.");
            }
        }

        // Lấy danh sách thể loại (Admin & Staff đều xem được)
        public DataTable GetAllCategories()
        {
            return repo.GetAll();
        }

        // Lấy ID tự tăng
        public int GetNextCategoryId()
        {
            return repo.GetNextCategoryId();
        }

        // Thêm thể loại
        public bool AddCategory(string tenDanhMuc, string trangThai)
        {
            if (!Authorization.IsAdmin()) throw new UnauthorizedAccessException("Chỉ Admin mới có quyền thêm.");
            if (string.IsNullOrWhiteSpace(tenDanhMuc)) return false;

            Categories cat = new Categories(tenDanhMuc, trangThai);
            return repo.Insert(cat) > 0;
        }

        // Cập nhật thể loại
        public bool UpdateCategory(int id, string tenDanhMuc, string trangThai)
        {
            if (!Authorization.IsAdmin()) throw new UnauthorizedAccessException("Chỉ Admin mới có quyền cập nhật.");
            if (id <= 0 || string.IsNullOrWhiteSpace(tenDanhMuc)) return false;

            Categories cat = new Categories(id, tenDanhMuc, DateTime.Now, trangThai);
            return repo.Update(cat) > 0;
        }

        // Xóa thể loại
        public bool DeleteCategory(int id)
        {
            if (!Authorization.IsAdmin()) throw new UnauthorizedAccessException("Chỉ Admin mới có quyền xóa.");
            if (id <= 0) return false;

            if (repo.CheckHasProduct(id))
            {
                throw new Exception("Không thể thực hiện! Danh mục này vẫn đang chứa sản phẩm.");
            }

            return repo.Delete(id) > 0;
        }

        // TÌM KIẾM VÀ LỌC
        public DataTable SearchCategory(string keyword, string status)
        {
            if (keyword == null) keyword = "";
            if (status == null) status = "Tất cả";

            return repo.Search(keyword, status);
        }

        public DataTable GetActiveCategories()
        {
            return repo.GetActiveCategories();
        }
    }
}