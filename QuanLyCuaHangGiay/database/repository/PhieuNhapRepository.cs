using QuanLyCuaHangGiay.Database;
using QuanLyCuaHangGiay.model;
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
    public class PhieuNhapRepository
    {
        // Hàm này nhận vào một danh sách các mặt hàng cần nhập và tên kho đích đến
        public bool NhapHangVaoKho(List<PhieuNhap> danhSachNhap, DateTime thoiGianLuu)
        {
            using (SqlConnection conn = DBConnection.GetDBConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in danhSachNhap)
                        {

                            //string checkKhoSql = "SELECT id FROM Kho WHERE tenKho = @tenKho AND sanphamID = @spID";
                            //using (SqlCommand cmdCheck = new SqlCommand(checkKhoSql, conn, trans))
                            //{
                            //    cmdCheck.Parameters.AddWithValue("@spID", item.sanPhamID);
                            //    object result = cmdCheck.ExecuteScalar();

                            //    if (result != null)
                            //    {
                            //        idKhoHienTai = Convert.ToInt32(result);
                            //        string updateKhoSql = "UPDATE Kho SET soLuongTrongKho = soLuongTrongKho + @sl WHERE id = @idKho";
                            //        using (SqlCommand cmdUpdKho = new SqlCommand(updateKhoSql, conn, trans))
                            //        {
                            //            cmdUpdKho.Parameters.AddWithValue("@sl", item.soLuong);
                            //            cmdUpdKho.Parameters.AddWithValue("@idKho", idKhoHienTai);
                            //            cmdUpdKho.ExecuteNonQuery();
                            //        }
                            //    }
                            //    else
                            //    {
                            //        string insertKhoSql = "INSERT INTO Kho (tenKho, sanphamID, soLuongTrongKho) OUTPUT INSERTED.id VALUES (@tenKho, @spID, @sl)";
                            //        using (SqlCommand cmdInsKho = new SqlCommand(insertKhoSql, conn, trans))
                            //        {
                            //            cmdInsKho.Parameters.AddWithValue("@spID", item.sanPhamID);
                            //            cmdInsKho.Parameters.AddWithValue("@sl", item.soLuong);
                            //            idKhoHienTai = (int)cmdInsKho.ExecuteScalar();
                            //        }
                            //    }
                            //}

                            string insertPhieuSql = "INSERT INTO PhieuNhap (thoiGian, soLuong, giaDonNhap, nhacungcapID, sanphamID, ghiChu) VALUES (@tg, @sl, @gia, @nccID, @spID, @ghiChu)";
                            using (SqlCommand cmdInsPhieu = new SqlCommand(insertPhieuSql, conn, trans))
                            {
                                cmdInsPhieu.Parameters.AddWithValue("@tg", thoiGianLuu);
                                cmdInsPhieu.Parameters.AddWithValue("@sl", item.soLuong);
                                cmdInsPhieu.Parameters.AddWithValue("@gia", item.giaNhap);
                                cmdInsPhieu.Parameters.AddWithValue("@nccID", item.nhaCungCapID);
                                cmdInsPhieu.Parameters.AddWithValue("@spID", item.sanPhamID);
                                cmdInsPhieu.Parameters.AddWithValue("@ghiChu", string.IsNullOrEmpty(item.ghiChu) ? (object)DBNull.Value : item.ghiChu);
                                cmdInsPhieu.ExecuteNonQuery();
                            }

                            string updateSpSql = "UPDATE SanPham SET soLuong = soLuong + @sl WHERE id = @spID";
                            using (SqlCommand cmdUpdSp = new SqlCommand(updateSpSql, conn, trans))
                            {
                                cmdUpdSp.Parameters.AddWithValue("@sl", item.soLuong);
                                cmdUpdSp.Parameters.AddWithValue("@spID", item.sanPhamID);
                                cmdUpdSp.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Lỗi giao dịch nhập hàng: " + ex.Message);
                    }
                }
            }
        }

        public DataTable LoadCbNCC()
        {
            // Chỉ lấy ID và Tên để load vào combo cho nhẹ
            return DBConnection.GetDataTable("SELECT id, tenNCC FROM NhaCungCap WHERE trangthai = N'Active'");
        }

        public DataTable LoadCbSP()
        {
            return DBConnection.GetDataTable("SELECT id, tenSP FROM SanPham");
        }

        public DataTable GetPhieuNhap(DateTime time, int nccID)
        {
            string query = @"
                    SELECT 
                        pn.id AS PhieuNhapID,
                        pn.thoiGian,

                        ncc.tenNCC,
                        ncc.diaChi,
                        ncc.sdt,

                        sp.tenSP,
                        sp.mau,
                        sp.kichco,
                        dm.tenDanhMuc,

                        pn.soLuong,
                        pn.giaDonNhap,
                        (pn.soLuong * pn.giaDonNhap) AS ThanhTien

                    FROM PhieuNhap pn
                    JOIN NhaCungCap ncc ON pn.nhacungcapID = ncc.id
                    JOIN SanPham sp ON pn.sanphamID = sp.id
                    JOIN DanhMuc dm ON sp.danhmucID = dm.id

                    WHERE pn.thoiGian = @thoiGian 
                    AND pn.nhacungcapID = @nccID
                ";

            SqlParameter[] pa = {
                new SqlParameter("@thoiGian", time),
                new SqlParameter("@nccID", nccID)
            };

            return DBConnection.GetDataTable(query, pa);
        }
    }
}
