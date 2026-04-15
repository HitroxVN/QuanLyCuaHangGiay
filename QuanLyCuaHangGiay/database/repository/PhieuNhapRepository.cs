using QuanLyCuaHangGiay.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangGiay.database.repository
{
    internal class PhieuNhapRepository
    {
        //Hiển thị
        public DataTable getAll()
        {
            string sql = @"SELECT   pn.id,
                                    dm.tenDanhMuc,
                                    sp.tenSP,
                                    sp.kichco,
                                    sp.mau,
                                    ncc.tenNCC,
                                    pn.soLuong,
                                    pn.giaDonNhap,
                                    pn.thoiGian,
                                    pn.ghiChu,
                                    pn.sanphamID,
                                    pn.nhacungcapID,
                                    sp.danhmucID
                           FROM PhieuNhap pn
                           JOIN SanPham sp ON pn.sanphamID = sp.id
                           JOIN DanhMuc dm ON sp.danhmucID = dm.id
                           JOIN NhaCungCap ncc ON pn.nhacungcapID = ncc.id
                           ORDER BY pn.id DESC";

            return DBConnection.GetDataTable(sql);
        }


        //Lọc
        public DataTable filter(DateTime from, DateTime to)
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

        //LOAD COMBOBOX
        public DataTable getNCC()
        {
            string sql = "SELECT id, tenNCC FROM NhaCungCap WHERE trangthai='Active'";
            return DBConnection.GetDataTable(sql);
        }

        public DataTable getDanhMuc()
        {
            string sql = "SELECT id, tenDanhMuc FROM DanhMuc WHERE trangthai='Active'";
            return DBConnection.GetDataTable(sql);
        }

        public DataTable getSanPhamByDanhMuc(int dmID)
        {
            string sql = @"SELECT id, tenSP, mau, kichco
                   FROM SanPham
                   WHERE danhmucID=@dm AND trangthai='Active'";

            SqlParameter[] pa = {
                new SqlParameter("@dm", dmID)
            };

            return DBConnection.GetDataTable(sql, pa);
        }

        //Thêm + Cập nhật Kho
        public bool insert(int spID, int nccID, int soLuong, decimal giaNhap, int userID, string ghiChu)
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

        //update + Cập nhật Kho
        public bool update(int id, int spID, int nccID, int soLuong, decimal giaNhap, string ghiChu)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                //MessageBox.Show("spID = " + spID);
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    int oldSP = 0;
                    int oldSL = 0;

                    // 1. Lấy dữ liệu cũ
                    string getSql = "SELECT sanphamID, soLuong FROM PhieuNhap WHERE id=@id";
                    SqlCommand cmdGet = new SqlCommand(getSql, conn, tran);
                    cmdGet.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmdGet.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            oldSP = Convert.ToInt32(reader["sanphamID"]);
                            oldSL = Convert.ToInt32(reader["soLuong"]);
                        }
                        else
                        {
                            throw new Exception("Không tìm thấy phiếu nhập!");
                        }
                    }

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

                    int rows = cmd2.ExecuteNonQuery();

                    if (rows == 0)
                        throw new Exception("Update thất bại!");

                    // 4. Nếu chưa có trong Kho → insert
                    string checkKho = "SELECT COUNT(*) FROM Kho WHERE sanphamID=@sp";
                    SqlCommand cmdCheck = new SqlCommand(checkKho, conn, tran);
                    cmdCheck.Parameters.AddWithValue("@sp", spID);

                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count == 0)
                    {
                        string insertKho = "INSERT INTO Kho(sanphamID, soLuong) VALUES(@sp, @sl)";
                        SqlCommand cmdInsert = new SqlCommand(insertKho, conn, tran);
                        cmdInsert.Parameters.AddWithValue("@sp", spID);
                        cmdInsert.Parameters.AddWithValue("@sl", soLuong);
                        cmdInsert.ExecuteNonQuery();
                    }
                    else
                    {
                        // 5. Cộng kho mới
                        string congKho = "UPDATE Kho SET soLuong = soLuong + @sl WHERE sanphamID=@sp";
                        SqlCommand cmd3 = new SqlCommand(congKho, conn, tran);
                        cmd3.Parameters.AddWithValue("@sl", soLuong);
                        cmd3.Parameters.AddWithValue("@sp", spID);
                        cmd3.ExecuteNonQuery();
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Lỗi update: " + ex.Message);
                    return false;
                }
            }
        }
        //Xóa + Trừ Kho
        public bool delete(int phieuNhapID)
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
    }
}
