using QuanLyCuaHangGiay.Database;
using QuanLyCuaHangGiay.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.database.repository
{
    internal class UserRepository
    {
        public Users getByEmail(string email)
        {
            string query = "SELECT * FROM TaiKhoan WHERE email = @email";

            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@email", email)
            };

            DataTable dt = DBConnection.GetDataTable(query, p);

            if(dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Users
                {
                    id = (int)row["id"],
                    hoTen = row["hoTen"].ToString(),
                    email = row["email"].ToString(),
                    matKhau = row["matKhau"].ToString(),
                    sdt = row["sdt"].ToString(),
                    diaChi = row["diaChi"].ToString(),
                    quyen = row["quyen"].ToString(),
                    ngayTao = row["ngayTao"].ToString(),
                    trangThai = row["trangThai"].ToString()
                };
            }

            return null;
        }

        public bool addUser(Users u)
        {
            string query = @"insert into TaiKhoan (hoTen, email, matKhau, sdt, diaChi, quyen, trangThai) values (@ht, @e, @mk, @sdt, @dc, @q, @tt)";

            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@ht", u.hoTen),
                new SqlParameter("@e", u.email),
                new SqlParameter("@mk", u.matKhau),
                new SqlParameter("@sdt", u.sdt),
                new SqlParameter("@dc", u.diaChi),
                new SqlParameter("@q", u.quyen),
                new SqlParameter("@tt", u.trangThai)
            };

            return DBConnection.ExecuteNonQuery(query, p) > 0;
        }
    }
}
