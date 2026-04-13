using System;
using System.Data;
using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.model;

namespace QuanLyCuaHangGiay.controller
{
    internal class CategoryController
    {

        // Thêm chữ "static" để biến class này thành toàn cục, dùng được ở mọi nơi
        public static class UserSession
        {
            // Giả lập bạn đang đăng nhập bằng tài khoản Staff (Nhân viên)
            // Bạn có thể đổi chữ "Staff" thành "Admin" để test quyền của Giám đốc nhé!
            public static string Role = "Staff";
        }





        private CategoryRepository repo = new CategoryRepository();

        // Lấy danh sách thể loại
        public DataTable GetAllCategories()
        {
            // Nhân viên chỉ xem danh mục Active
            if (UserSession.Role == "Staff")
            {
                return repo.GetActiveCategories();
            }
            // Admin xem toàn bộ
            return repo.GetAll();
        }

        // Thêm thể loại mới
        public bool AddCategory(string tenDanhMuc, string trangThai)
        {
            // Kiểm tra tính hợp lệ của dữ liệu
            if (string.IsNullOrWhiteSpace(tenDanhMuc))
            {
                return false; // Trả về false nếu tên danh mục trống
            }

            Categories cat = new Categories(tenDanhMuc, trangThai);
            int result = repo.Insert(cat);

            return result > 0; // Nếu insert thành công, số dòng tác động > 0
        }

        // Cập nhật thể loại
        public bool UpdateCategory(int id, string tenDanhMuc, string trangThai)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(tenDanhMuc))
            {
                return false;
            }

            Categories cat = new Categories(id, tenDanhMuc, DateTime.Now, trangThai);
            int result = repo.Update(cat);

            return result > 0;
        }

        // Xóa thể loại
        public bool DeleteCategory(int id)
        {
            if (id <= 0) return false;

            int result = repo.Delete(id);
            return result > 0;
        }

        // Tìm kiếm thể loại
        public DataTable SearchCategory(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetAllCategories();
            }
            return repo.Search(keyword);
        }

        // Hàm mới: Gọi xuống Repository để lấy danh mục Active
        public DataTable GetActiveCategories()
        {
            return repo.GetActiveCategories(); // (Nếu biến của bạn tên là repo thì xóa dấu gạch dưới đi nhé)
        }


    }
}