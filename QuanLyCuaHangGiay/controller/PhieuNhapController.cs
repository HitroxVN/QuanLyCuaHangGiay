using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangGiay.controller
{
    public class PhieuNhapController
    {
        private PhieuNhapRepository repo = new PhieuNhapRepository();

        public DataTable GetAll()
        {
            return repo.getAll();
        }

        public DataTable Filter(DateTime from, DateTime to)
        {
            return repo.filter(from, to);
        }

        public DataTable GetNhaCungCap()
        {
            return repo.getNCC();
        }

        public DataTable GetDanhMuc()
        {
            return repo.getDanhMuc();
        }

        public DataTable GetSanPhamByDanhMuc(int dmID)
        {
            return repo.getSanPhamByDanhMuc(dmID);
        }

        public bool Insert(int spID, int nccID, int soLuong, decimal giaNhap, int userID, string ghiChu)
        {
            if (spID <= 0 || nccID <= 0 || soLuong <= 0)
                return false;

            return repo.insert(spID, nccID, soLuong, giaNhap, userID, ghiChu);
        }

        public bool Update(int id, int spID, int nccID, int soLuong, decimal giaNhap, string ghiChu)
        {
            if (id <= 0 || spID <= 0)
                return false;

            return repo.update(id, spID, nccID, soLuong, giaNhap, ghiChu);
        }

        public bool Delete(int id)
        {
            if (id <= 0) return false;

            return repo.delete(id);
        }

        public DataTable GetPhieuNhapReport(DateTime time, int nccID)
        {
            return repo.GetPhieuNhap(time, nccID);
        }
    }
}
