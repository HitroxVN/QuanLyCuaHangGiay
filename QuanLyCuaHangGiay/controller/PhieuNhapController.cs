using QuanLyCuaHangGiay.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.controller
{
    public class PhieuNhapController
    {
        //Hiển thị
        public DataTable GetAll()
        {
            string sql = @"SELECT pn.id,

                                  sp.tenSP,
                                  sp.mau,
                                  sp.kichco,
                                  dm.tenDanhMuc,
                                  ncc.tenNCC,

                                  pn.soLuong,
                                  pn.giaDonNhap,
                                  pn.thoiGian,
                                  pn.ghiChu
                           FROM PhieuNhap pn
                           JOIN SanPham sp ON pn.sanphamID = sp.id
                           JOIN DanhMuc dm ON sp.danhmucID = dm.id
                           JOIN NhaCungCap ncc ON pn.nhacungcapID = ncc.id
                           ORDER BY pn.id DESC";

            return DBConnection.GetDataTable(sql);
        }

        //Lọc
        public DataTable Filter(DateTime from, DateTime to)
        {
            string sql = @"SELECT pn.id,
                                  sp.tenSP,
                                  ncc.tenNCC,
                                  pn.soLuong,
                                  pn.giaDonNhap,
                                  pn.thoiGian,
                                  pn.ghiChu
                           FROM PhieuNhap pn
                           JOIN SanPham sp ON pn.sanphamID = sp.id
                           JOIN NhaCungCap ncc ON pn.nhacungcapID = ncc.id
                           WHERE pn.thoiGian BETWEEN @from AND @to
                           ORDER BY pn.id DESC";

            SqlParameter[] pa = {
                new SqlParameter("@from", from),
                new SqlParameter("@to", to)
            };

            return DBConnection.GetDataTable(sql, pa);
        }

        //Load cboNCC
        public DataTable GetNhaCungCap()
        {
            string sql = @"SELECT id, tenNCC FROM NhaCungCap WHERE trangthai = 'Active'";
            return DBConnection.GetDataTable(sql);
        }
        //Thêm + Cập nhật Kho
        public bool Insert(int spID, int nccID, int soLuong, decimal giaNhap, int userID, string ghiChu)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    // 1. Thêm phiếu nhập
                    string sqlInsert = @"INSERT INTO PhieuNhap
                        (thoiGian, soLuong, giaDonNhap, nhacungcapID, sanphamID, taikhoanID, ghiChu)
                        VALUES(GETDATE(), @sl, @gia, @ncc, @sp, @tk, @gc)";

                    SqlCommand cmd1 = new SqlCommand(sqlInsert, conn, tran);
                    cmd1.Parameters.AddWithValue("@sl", soLuong);
                    cmd1.Parameters.AddWithValue("@gia", giaNhap);
                    cmd1.Parameters.AddWithValue("@ncc", nccID);
                    cmd1.Parameters.AddWithValue("@sp", spID);
                    cmd1.Parameters.AddWithValue("@tk", userID);
                    cmd1.Parameters.AddWithValue("@gc", ghiChu);
                    cmd1.ExecuteNonQuery();

                    // 2. Kiểm tra tồn kho
                    string checkSql = "SELECT COUNT(*) FROM Kho WHERE sanphamID = @sp";
                    SqlCommand cmdCheck = new SqlCommand(checkSql, conn, tran);
                    cmdCheck.Parameters.AddWithValue("@sp", spID);

                    int exists = (int)cmdCheck.ExecuteScalar();

                    if (exists > 0)
                    {
                        // update
                        string updateSql = "UPDATE Kho SET soLuong = soLuong + @sl WHERE sanphamID = @sp";
                        SqlCommand cmd2 = new SqlCommand(updateSql, conn, tran);
                        cmd2.Parameters.AddWithValue("@sl", soLuong);
                        cmd2.Parameters.AddWithValue("@sp", spID);
                        cmd2.ExecuteNonQuery();
                    }
                    else
                    {
                        // insert mới
                        string insertKho = "INSERT INTO Kho(sanphamID, soLuong) VALUES(@sp, @sl)";
                        SqlCommand cmd3 = new SqlCommand(insertKho, conn, tran);
                        cmd3.Parameters.AddWithValue("@sp", spID);
                        cmd3.Parameters.AddWithValue("@sl", soLuong);
                        cmd3.ExecuteNonQuery();
                    }

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
        //Update + Cập nhật Kho
        public bool Update(int id, int spID, int nccID, int soLuong, decimal giaNhap, string ghiChu)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    // 1. Lấy dữ liệu cũ
                    string getSql = "SELECT sanphamID, soLuong FROM PhieuNhap WHERE id=@id";
                    SqlCommand cmdGet = new SqlCommand(getSql, conn, tran);
                    cmdGet.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmdGet.ExecuteReader();

                    int oldSP = 0;
                    int oldSL = 0;

                    if (reader.Read())
                    {
                        oldSP = Convert.ToInt32(reader["sanphamID"]);
                        oldSL = Convert.ToInt32(reader["soLuong"]);
                    }
                    reader.Close();

                    // 2. Trừ kho cũ
                    string truKho = "UPDATE Kho SET soLuong = soLuong - @sl WHERE sanphamID=@sp";
                    SqlCommand cmd1 = new SqlCommand(truKho, conn, tran);
                    cmd1.Parameters.AddWithValue("@sl", oldSL);
                    cmd1.Parameters.AddWithValue("@sp", oldSP);
                    cmd1.ExecuteNonQuery();

                    // 3. Update phiếu
                    string update = @"UPDATE PhieuNhap 
                              SET sanphamID=@sp, nhacungcapID=@ncc, soLuong=@sl, giaDonNhap=@gia, ghiChu=@gc
                              WHERE id=@id";

                    SqlCommand cmd2 = new SqlCommand(update, conn, tran);
                    cmd2.Parameters.AddWithValue("@sp", spID);
                    cmd2.Parameters.AddWithValue("@ncc", nccID);
                    cmd2.Parameters.AddWithValue("@sl", soLuong);
                    cmd2.Parameters.AddWithValue("@gia", giaNhap);
                    cmd2.Parameters.AddWithValue("@gc", ghiChu);
                    cmd2.Parameters.AddWithValue("@id", id);
                    cmd2.ExecuteNonQuery();

                    // 4. Cộng lại kho mới
                    string congKho = "UPDATE Kho SET soLuong = soLuong + @sl WHERE sanphamID=@sp";
                    SqlCommand cmd3 = new SqlCommand(congKho, conn, tran);
                    cmd3.Parameters.AddWithValue("@sl", soLuong);
                    cmd3.Parameters.AddWithValue("@sp", spID);
                    cmd3.ExecuteNonQuery();

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
        //Xóa + Trừ Kho
        public bool Delete(int phieuNhapID)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    // Lấy thông tin phiếu nhập
                    string getSql = "SELECT sanphamID, soLuong FROM PhieuNhap WHERE id=@id";
                    SqlCommand cmdGet = new SqlCommand(getSql, conn, tran);
                    cmdGet.Parameters.AddWithValue("@id", phieuNhapID);

                    SqlDataReader reader = cmdGet.ExecuteReader();

                    int spID = 0;
                    int soLuong = 0;

                    if (reader.Read())
                    {
                        spID = Convert.ToInt32(reader["sanphamID"]);
                        soLuong = Convert.ToInt32(reader["soLuong"]);
                    }
                    reader.Close();

                    // Trừ kho
                    string updateKho = "UPDATE Kho SET soLuong = soLuong - @sl WHERE sanphamID = @sp";
                    SqlCommand cmd1 = new SqlCommand(updateKho, conn, tran);
                    cmd1.Parameters.AddWithValue("@sl", soLuong);
                    cmd1.Parameters.AddWithValue("@sp", spID);
                    cmd1.ExecuteNonQuery();

                    // Xóa phiếu
                    string deleteSql = "DELETE FROM PhieuNhap WHERE id=@id";
                    SqlCommand cmd2 = new SqlCommand(deleteSql, conn, tran);
                    cmd2.Parameters.AddWithValue("@id", phieuNhapID);
                    cmd2.ExecuteNonQuery();

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
        //Xem tồn kho
        public DataTable GetKho()
        {
            string sql = @"SELECT k.id, sp.tenSP, k.soLuong
                           FROM Kho k
                           JOIN SanPham sp ON k.sanphamID = sp.id";

            return DBConnection.GetDataTable(sql);
        }
        //Load danh mục
        public DataTable GetDanhMuc()
        {
            string sql = "SELECT id, tenDanhMuc FROM DanhMuc WHERE trangthai = 'Active'";
            return DBConnection.GetDataTable(sql);
        }
        //Load sản phẩm theo danh mục
        public DataTable GetSanPhamByDanhMuc(int danhMucID)
        {
            string sql = @"SELECT id, tenSP, mau, kichco 
                   FROM SanPham 
                   WHERE danhmucID = @dm AND trangthai = 'Active'";

            SqlParameter[] pa = {
                new SqlParameter("@dm", danhMucID)
            };

            return DBConnection.GetDataTable(sql, pa);
        }

        
    }
}
