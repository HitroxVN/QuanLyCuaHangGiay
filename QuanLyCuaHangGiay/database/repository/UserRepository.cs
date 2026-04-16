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
                    ngayTao = (DateTime)row["ngayTao"],
                    trangThai = row["trangThai"].ToString()
                };
            }

            return null;
        }

        public bool addUser(Users u)
        {
            string query = @"insert into TaiKhoan (hoTen, email, matKhau, sdt, diaChi, quyen, trangThai, ngayTao) values (@ht, @e, @mk, @sdt, @dc, @q, @tt, @nt)";

            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@ht", u.hoTen),
                new SqlParameter("@e", u.email),
                new SqlParameter("@mk", u.matKhau),
                new SqlParameter("@sdt", u.sdt),
                new SqlParameter("@dc", u.diaChi),
                new SqlParameter("@q", u.quyen),
                new SqlParameter("@tt", u.trangThai),
                new SqlParameter("@nt", u.ngayTao)
            };

            return DBConnection.ExecuteNonQuery(query, p) > 0;
        }

        public bool updateUser(Users u)
        {
            string query = @"update TaiKhoan 
                     set hoTen=@ht, sdt=@sdt, diaChi=@dc, quyen=@q, trangThai=@tt
                     where id=@id";

            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@ht", u.hoTen),
                new SqlParameter("@sdt", u.sdt),
                new SqlParameter("@dc", u.diaChi),
                new SqlParameter("@q", u.quyen),
                new SqlParameter("@tt", u.trangThai),
                new SqlParameter("@id", u.id)
            };

            return DBConnection.ExecuteNonQuery(query, p) > 0;
        }

        public bool removeUser(int id)
        {
            string query = "delete from TaiKhoan where id = @id";
            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            return DBConnection.ExecuteNonQuery(query, p) > 0;
        }

        public List<Users> getAllUsers()
        {
            string query = "SELECT * FROM TaiKhoan";
            DataTable dt = DBConnection.GetDataTable(query);

            List<Users> list = new List<Users>();

            foreach(DataRow row in dt.Rows)
            {
                Users u = new Users
                {
                    id = (int)row["id"],
                    hoTen = row["hoTen"].ToString(),
                    email = row["email"].ToString(),
                    matKhau = row["matKhau"].ToString(),
                    sdt = row["sdt"].ToString(),
                    diaChi = row["diaChi"].ToString(),
                    quyen = row["quyen"].ToString(),
                    ngayTao = (DateTime)row["ngayTao"],
                    trangThai = row["trangThai"].ToString()
                };
                list.Add(u);
            }

            return list;
        }

        public List<Users> searchUsers(string keyword)
        {
            string query = "SELECT * FROM TaiKhoan WHERE hoTen LIKE @kw OR email LIKE @kw";
            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@kw", "%" + keyword + "%")
            };
            DataTable dt = DBConnection.GetDataTable(query, p);
            List<Users> list = new List<Users>();
            foreach (DataRow row in dt.Rows)
            {
                Users u = new Users
                {
                    id = (int)row["id"],
                    hoTen = row["hoTen"].ToString(),
                    email = row["email"].ToString(),
                    matKhau = row["matKhau"].ToString(),
                    sdt = row["sdt"].ToString(),
                    diaChi = row["diaChi"].ToString(),
                    quyen = row["quyen"].ToString(),
                    ngayTao = (DateTime)row["ngayTao"],
                    trangThai = row["trangThai"].ToString()
                };
                list.Add(u);
            }
            return list;
        }

        public List<Users> filterByRole(string role)
        {
            string query = "SELECT * FROM TaiKhoan WHERE quyen = @role";
            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@role", role)
            };
            DataTable dt = DBConnection.GetDataTable(query, p);
            List<Users> list = new List<Users>();
            foreach (DataRow row in dt.Rows)
            {
                Users u = new Users
                {
                    id = (int)row["id"],
                    hoTen = row["hoTen"].ToString(),
                    email = row["email"].ToString(),
                    matKhau = row["matKhau"].ToString(),
                    sdt = row["sdt"].ToString(),
                    diaChi = row["diaChi"].ToString(),
                    quyen = row["quyen"].ToString(),
                    ngayTao = (DateTime)row["ngayTao"],
                    trangThai = row["trangThai"].ToString()
                };
                list.Add(u);
            }
            return list;
        }

        public bool checkPassword(int userId, string password)
        {
            string query = "SELECT matKhau FROM TaiKhoan WHERE id = @id";
            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@id", userId)
            };
            DataTable dt = DBConnection.GetDataTable(query, p);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["matKhau"].ToString() == password;
            }
            return false;
        }

        public bool changePassword(int id, string newPass)
        {
            string query = "UPDATE TaiKhoan SET matKhau = @mk WHERE id = @id";

            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@mk", newPass),
                new SqlParameter("@id", id)
            };

            return DBConnection.ExecuteNonQuery(query, p) > 0;
        }

    }
}
