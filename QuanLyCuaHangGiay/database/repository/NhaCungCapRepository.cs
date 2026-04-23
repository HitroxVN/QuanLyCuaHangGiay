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
    internal class NhaCungCapRepository
    {
        // ================= GET ALL =================
        public List<NhaCungCap> getAll()
        {
            string query = "SELECT * FROM NhaCungCap";
            DataTable dt = DBConnection.GetDataTable(query);

            List<NhaCungCap> list = new List<NhaCungCap>();

            foreach (DataRow row in dt.Rows)
            {
                NhaCungCap ncc = new NhaCungCap
                {
                    id = (int)row["id"],
                    tenNCC = row["tenNCC"].ToString(),
                    email = row["email"].ToString(),
                    diaChi = row["diaChi"].ToString(),
                    sdt = row["sdt"].ToString(),
                    trangthai = row["trangthai"].ToString()
                };
                list.Add(ncc);
            }

            return list;
        }

        // ================= INSERT =================
        public bool add(NhaCungCap ncc)
        {
            string query = @"INSERT INTO NhaCungCap(tenNCC, email, diaChi, sdt, trangthai)
                             VALUES(@ten, @email, @dc, @sdt, @tt)";

            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@ten", ncc.tenNCC),
                new SqlParameter("@email", ncc.email),
                new SqlParameter("@dc", ncc.diaChi),
                new SqlParameter("@sdt", ncc.sdt),
                new SqlParameter("@tt", ncc.trangthai)
            };

            return DBConnection.ExecuteNonQuery(query, p) > 0;
        }

        // ================= UPDATE =================
        public bool update(NhaCungCap ncc)
        {
            string query = @"UPDATE NhaCungCap
                             SET tenNCC=@ten, email=@email, diaChi=@dc, sdt=@sdt, trangthai=@tt
                             WHERE id=@id";

            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@ten", ncc.tenNCC),
                new SqlParameter("@email", ncc.email),
                new SqlParameter("@dc", ncc.diaChi),
                new SqlParameter("@sdt", ncc.sdt),
                new SqlParameter("@tt", ncc.trangthai),
                new SqlParameter("@id", ncc.id)
            };

            return DBConnection.ExecuteNonQuery(query, p) > 0;
        }

        // ================= DELETE =================
        public bool delete(int id)
        {
            string query = "DELETE FROM NhaCungCap WHERE id=@id";

            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            return DBConnection.ExecuteNonQuery(query, p) > 0;
        }

        // ================= SEARCH =================
        public List<NhaCungCap> search(string keyword)
        {
            string query = @"SELECT * FROM NhaCungCap
                             WHERE tenNCC LIKE @kw OR sdt LIKE @kw";

            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@kw", "%" + keyword + "%")
            };

            DataTable dt = DBConnection.GetDataTable(query, p);

            List<NhaCungCap> list = new List<NhaCungCap>();

            foreach (DataRow row in dt.Rows)
            {
                NhaCungCap ncc = new NhaCungCap
                {
                    id = (int)row["id"],
                    tenNCC = row["tenNCC"].ToString(),
                    email = row["email"].ToString(),
                    diaChi = row["diaChi"].ToString(),
                    sdt = row["sdt"].ToString(),
                    trangthai = row["trangthai"].ToString()
                };
                list.Add(ncc);
            }

            return list;
        }
    }
}
