using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.Database;
using QuanLyCuaHangGiay.util;
using QuanLyCuaHangGiay.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyCuaHangGiay.database.repository.PhieuNhapRepository;

namespace QuanLyCuaHangGiay.controller
{
    public class PhieuNhapController
    {

        private PhieuNhapRepository _repository;

        public PhieuNhapController()
        {
            if (
                !Authorization.IsAdmin() &&
                !Authorization.IsStaff()
               )
            {
                throw new UnauthorizedAccessException(
                    "Không có quyền"
                );
            }
            _repository = new PhieuNhapRepository();
        }

        public string LuuPhieuNhap(List<PhieuNhap> danhSach, string tenKho, DateTime thoiGianLuu)
        {
            if (danhSach == null || danhSach.Count == 0)
            {
                return "Phiếu nhập không có sản phẩm nào!";
            }

            try
            {
                bool isSuccess = _repository.NhapHangVaoKho(danhSach, tenKho, thoiGianLuu);
                return isSuccess ? "Success" : "Có lỗi trong quá trình lưu dữ liệu.";
            }
            catch (Exception ex)
            {
                return ex.Message; 
            }
        }

        public DataTable GetPhieuNhapReport(DateTime time, int nccID)
        {
            return _repository.GetPhieuNhap(time, nccID);
        }


        public DataTable LayNCC() => _repository.LoadCbNCC();
        public DataTable LaySanPham() => _repository.LoadCbSP();
        public DataTable LayKho() => _repository.GetTenKhoDuyNhat();
    }
}
