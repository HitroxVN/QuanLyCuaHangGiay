using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCuaHangGiay.Database
{
    internal class DBConnection
    {
        private static readonly String connectionString = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=shoe_shop;Integrated Security=True";

        public static SqlConnection GetDBConnection()
        {
            return new SqlConnection(connectionString);
        }

        //hàm thực thi SQL => ExecuteNonQuery
        public static int ExecuteNonQuery(string sql, SqlParameter[] pa = null)
        {
            using (SqlConnection conn = GetDBConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    if (pa != null)
                        cmd.Parameters.AddRange(pa);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        //hàm trả về DataTable
        public static DataTable GetDataTable(string sql, SqlParameter[] pa = null)
        {
            using (SqlConnection conn = GetDBConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (pa != null)
                        cmd.Parameters.AddRange(pa);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        ///

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetDBConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
