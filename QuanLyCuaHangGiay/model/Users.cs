using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.model
{
    internal class Users
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string role { get; set; }
        public string created_at { get; set; }
        public string status { get; set; }
    }
}
