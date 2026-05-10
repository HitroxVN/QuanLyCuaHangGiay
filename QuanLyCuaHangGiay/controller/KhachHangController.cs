using QuanLyCuaHangGiay.Database;
using System;
using System.Data;
using System.Data.SqlClient;

namespace shoe_store.controller
{
    public class KhachHangController
    {
        public DataTable GetDanhSachKhachHang(string tuKhoa)
        {
            string query = @"SELECT id as [Mã KH], 
                                    hoTen as [Họ tên], 
                                    sdt as [Số điện thoại], 
                                    diemTichLuy as [Điểm tích lũy], 
                                    ngayTao as [Ngày tạo]
                             FROM KhachHang
                             WHERE trangThai = 1 
                               AND (@tuKhoa = '' OR id LIKE @tuKhoa OR sdt LIKE @tuKhoa)
                             ORDER BY id DESC";

            SqlParameter pTuKhoa = new SqlParameter("@tuKhoa", SqlDbType.NVarChar);
            pTuKhoa.Value = string.IsNullOrEmpty(tuKhoa) ? "" : "%" + tuKhoa.Trim() + "%";

            return DBConnection.GetDataTable(query, new SqlParameter[] { pTuKhoa });
        }

        public bool UpdateKhachHang(int id, string hoTen, string sdt)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                string checkSql = "SELECT id FROM KhachHang WHERE sdt = @sdt AND id != @id";
                using (SqlCommand cmdCheck = new SqlCommand(checkSql, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@sdt", sdt);
                    cmdCheck.Parameters.AddWithValue("@id", id);
                    if (cmdCheck.ExecuteScalar() != null)
                    {
                        throw new Exception("Số điện thoại này đã được đăng ký cho một khách hàng khác!");
                    }
                }
                string query = "UPDATE KhachHang SET hoTen = @hoTen, sdt = @sdt WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@hoTen", hoTen);
                    cmd.Parameters.AddWithValue("@sdt", sdt);
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteKhachHang(int id)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                string query = "UPDATE KhachHang SET trangThai = 0 WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ThemKhachHang(string hoTen, string sdt)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                string sqlCheck = "SELECT id, trangThai FROM KhachHang WHERE sdt = @sdt";
                int? idKhach = null;
                int trangThai = -1;

                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@sdt", sdt);
                    using (SqlDataReader reader = cmdCheck.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idKhach = Convert.ToInt32(reader["id"]);
                            trangThai = reader["trangThai"] != DBNull.Value ? Convert.ToInt32(reader["trangThai"]) : 1;
                        }
                    }
                }

                if (idKhach.HasValue)
                {
                    if (trangThai == 1)
                    {
                        throw new Exception("Số điện thoại này đã tồn tại trong hệ thống!");
                    }
                    else
                    {
                        string sqlReactivate = @"UPDATE KhachHang 
                                                 SET hoTen = @hoTen, trangThai = 1, diemTichLuy = 0, ngayTao = GETDATE() 
                                                 WHERE id = @id";
                        using (SqlCommand cmdUpdate = new SqlCommand(sqlReactivate, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@hoTen", hoTen);
                            cmdUpdate.Parameters.AddWithValue("@id", idKhach.Value);
                            return cmdUpdate.ExecuteNonQuery() > 0;
                        }
                    }
                }
                else
                {
                    string sqlInsert = @"INSERT INTO KhachHang (hoTen, sdt, diemTichLuy, trangThai, ngayTao) 
                                         VALUES (@hoTen, @sdt, 0, 1, GETDATE())";
                    using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@hoTen", hoTen);
                        cmdInsert.Parameters.AddWithValue("@sdt", sdt);
                        return cmdInsert.ExecuteNonQuery() > 0;
                    }
                }
            }
        }
    }
}