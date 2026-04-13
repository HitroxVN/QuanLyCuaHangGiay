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
    internal class AuthController
    {
        private UserRepository repo = new UserRepository();

        public Users login(string email, string password)
        {
            Users u = repo.getByEmail(email);

            if(u == null) return null;
            if(u.trangThai.ToLower() != "active") return null;

            string hashed = HashPassword.hashPassword(password);
            if (u.matKhau == hashed) return u;

            return null;
        }

        public bool register(Users u)
        {
            Users checkEmail = repo.getByEmail(u.email);
            if (checkEmail != null) return false;

            u.quyen = "user";
            u.ngayTao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            u.trangThai = "active";

            u.matKhau = HashPassword.hashPassword(u.matKhau);

            return repo.addUser(u);
        }

        public void logout()
        {
            Session.user = null;
        }
    }
}
