using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.model;
using QuanLyCuaHangGiay.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangGiay.controller
{
    internal class UserController
    {
        private UserRepository repo = new UserRepository();
        private ValidateUser validate = new ValidateUser();

        public bool addUser(Users u, out string m)
        {
            if(!Authorization.IsAdmin())
                {
                m = "Bạn không có quyền thực hiện hành động này.";
                return false;
            }

            if (!validate.validateForAdd(u, out m))
            {
                return false;
            }

            Users check = repo.getByEmail(u.email);

            if (check != null)
            {
                m = "Email đã tồn tại.";
                return false;
            }

            u.matKhau = HashPassword.hashPassword(u.matKhau);

            bool rs = repo.addUser(u);
            m = rs ? "Thêm người dùng thành công." : "Thêm người dùng thất bại.";
            return rs;
        }

        public bool updateUser(Users u, out string m)
        {
            if (!Authorization.IsAdmin())
            {
                m = "Bạn không có quyền thực hiện hành động này.";
                return false;
            }
            if (!validate.validateForUpdate(u, out m))
            {
                return false;
            }

            bool rs = repo.updateUser(u);
            m = rs ? "Cập nhật người dùng thành công." : "Cập nhật người dùng thất bại.";
            return rs;
        }

        public bool deleteUser(int id)
        {
            if (!Authorization.IsAdmin())
            {
                MessageBox.Show("Bạn không có quyền thực hiện hành động này.");
                return false;
            }

            if (id <= 0) return false;

            return repo.removeUser(id);
        }

        public bool changePassword(int id, string newPass, string oldPass, out string m)
        {
            if (!validate.validateForPasswordChange(newPass, out m))
            {
                return false;
            }
            string newhashed = HashPassword.hashPassword(newPass);
            string oldhashed = HashPassword.hashPassword(oldPass);

            if (!repo.checkPassword(id, oldhashed))
            {
                m = "Mật khẩu cũ không đúng.";
                return false;
            }
            
            bool rs = repo.changePassword(id, newhashed);
            m = rs ? "Đổi mật khẩu thành công." : "Đổi mật khẩu thất bại.";
            return rs;
        }

        public List<Users> getAllUsers()
        {
            return repo.getAllUsers();
        }

        public List<Users> searchUsers(string keyword)
        {
            return repo.searchUsers(keyword);
        }

        public List<Users> filterUsersByRole(string role)
        {
            return repo.filterByRole(role);
        }
    }
}
