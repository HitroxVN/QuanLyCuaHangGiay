using QuanLyCuaHangGiay.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.util
{
    internal class ValidateUser
    {
        public bool validateForAdd(Users u, out string m)
        {
            m = "";

            if (string.IsNullOrWhiteSpace(u.email))
            {
                m = "Email không được để trống.";
                return false;
            }

            if (!u.email.Contains("@"))
            {
                m = "Email không hợp lệ.";
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(u.email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                m = "Email không hợp lệ.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(u.matKhau))
            {
                m = "Mật khẩu không được để trống.";
                return false;
            }

            if (u.matKhau.Length < 6)
            {
                m = "Mật khẩu phải >= 6 ký tự.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(u.hoTen))
            {
                m = "Họ tên không được để trống.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(u.sdt))
            {
                m = "Số điện thoại không được để trống.";
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(u.sdt, @"^\d{10}$"))
            {
                m = "Số điện thoại phải có 10 chữ số.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(u.diaChi))
            {
                m = "Địa chỉ không được để trống.";
                return false;
            }

            return true;
        }

        public bool validateForPasswordChange(string pass, out string m)
        {
            m = "";
            if (string.IsNullOrWhiteSpace(pass))
            {
                m = "Mật khẩu mới không được để trống.";
                return false;
            }
            if (pass.Length < 6)
            {
                m = "Mật khẩu mới phải >= 6 ký tự.";
                return false;
            }
            return true;
        }

        public bool validateForUpdate(Users u, out string m)
        {
            m = "";
            if (u.id <= 0)
            {
                m = "ID không hợp lệ.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(u.hoTen))
            {
                m = "Họ tên không được để trống.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(u.sdt))
            {
                m = "Số điện thoại không được để trống.";
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(u.sdt, @"^\d{10}$"))
            {
                m = "Số điện thoại phải có 10 chữ số.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(u.diaChi))
            {
                m = "Địa chỉ không được để trống.";
                return false;
            }
            return true;
        }
    }
}
