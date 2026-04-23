using System;
using System.Data;
using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.model;

namespace QuanLyCuaHangGiay.controller
{
    internal class CategoryController
    {
        private CategoryRepository repo = new CategoryRepository();

        //  Lấy danh sách thể loại
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

        // Lấy ID tự tăng tiếp theo
        public int GetNextCategoryId()
        {
            return repo.GetNextCategoryId();
        }

        //  Thêm thể loại mới
        public bool AddCategory(string tenDanhMuc, string trangThai)
        {
            if (string.IsNullOrWhiteSpace(tenDanhMuc))
            {
                return false;
            }

            Categories cat = new Categories(tenDanhMuc, trangThai);
            int result = repo.Insert(cat);

            return result > 0;
        }

        //  Cập nhật thể loại
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

        //  XÓA THỂ LOẠI (Đã được nâng cấp kiểm tra ràng buộc)
        public bool DeleteCategory(int id)
        {
            if (id <= 0) return false;

            //  Gọi hàm CheckHasProduct từ Repository
            if (repo.CheckHasProduct(id))
            {
                // Nếu có sản phẩm, ném ra lỗi này để Form bắt được và hiện MessageBox
                throw new Exception("Không thể thực hiện! Danh mục này vẫn đang chứa sản phẩm.");
            }

            // NẾU AN TOÀN (Không có sản phẩm) -> Tiến hành xóa
            if (UserSession.Role == "Staff")
            {
                // Nhân viên xóa mềm (đổi trạng thái)
                int result = repo.ChangeStatus(id, "Inactive");
                return result > 0;
            }
            else
            {
                // Admin xóa cứng khỏi database
                int result = repo.Delete(id);
                return result > 0;
            }
        }

        // 5. Tìm kiếm thể loại
        public DataTable SearchCategory(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetAllCategories();
            }
            return repo.Search(keyword);
        }

        // 6. Lấy danh mục Active cho ComboBox bên form Sản phẩm
        public DataTable GetActiveCategories()
        {
            return repo.GetActiveCategories();
        }
    }
}