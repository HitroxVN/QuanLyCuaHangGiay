using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.model;
using QuanLyCuaHangGiay.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.controller
{
    internal class UserController
    {
        private UserRepository repo = new UserRepository();
        private ValidateUser validate = new ValidateUser();

        public bool addUser(Users u, out string m)
        {
            if(!validate.validateForAdd(u, out m))
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

            if(repo.addUser(u))
            {
                m = "Thêm người dùng thành công.";
                return true;
            }
            else
            {
                m = "Thêm người dùng thất bại.";
                return false;
            }
            //return repo.addUser(u);
        }

        public bool updateUser(Users u, out string m)
        {
            if(!validate.validateForUpdate(u, out m))
            {
                return false;
            }

            if (repo.updateUser(u))
            {
                m = "Cập nhật người dùng thành công.";
                return true;

            }
            else
            {
                m = "Cập nhật người dùng thất bại.";
                return false;
            }
        }

        public bool deleteUser(int id)
        {
            if (id <= 0) return false;

            return repo.removeUser(id);
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
