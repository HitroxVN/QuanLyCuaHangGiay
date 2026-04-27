using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.util
{
    internal class Authorization
    {
        public static bool Logged()
        {
            return Session.user != null;
        }

        public static bool IsAdmin()
        {
            return Logged() && Session.user.quyen.ToLower() == "admin";
        }

        public static bool IsStaff()
        {
            return Logged() && Session.user.quyen.ToLower() == "staff";
        }

    }
}
