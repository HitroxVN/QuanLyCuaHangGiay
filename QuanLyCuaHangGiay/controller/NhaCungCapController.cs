using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.model;
using QuanLyCuaHangGiay.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangGiay.controller
{
    internal class NhaCungCapController
    {
        public NhaCungCapController()
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
        }
        private NhaCungCapRepository repo = new NhaCungCapRepository();

        // ================= ADD =================
        public bool add(NhaCungCap ncc, out string msg)
        {
            if (string.IsNullOrWhiteSpace(ncc.tenNCC))
            {
                msg = "Tên nhà cung cấp không được trống!";
                return false;
            }

            if (repo.add(ncc))
            {
                msg = "Thêm thành công!";
                return true;
            }

            msg = "Thêm thất bại!";
            return false;
        }

        // ================= UPDATE =================
        public bool update(NhaCungCap ncc, out string msg)
        {
            if (ncc.id <= 0)
            {
                msg = "ID không hợp lệ!";
                return false;
            }

            if (repo.update(ncc))
            {
                msg = "Cập nhật thành công!";
                return true;
            }

            msg = "Cập nhật thất bại!";
            return false;
        }

        // ================= DELETE =================
        public bool delete(int id, out string msg)
        {
            msg = "";
            if (id <= 0) 
            {
                msg = "ID không hợp lệ!";
                return false;
            }
            try
            {
                if (repo.delete(id))
                {
                    msg = "Xóa thành công!";
                    return true;
                }
                else
                {
                    msg = "Không tìm thấy nhà cung cấp để xóa!";
                    return false;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key constraint violation
                {
                    msg = "Không thể xóa nhà cung cấp này vì đã có dữ liệu liên quan (như phiếu nhập). Vui lòng chuyển trạng thái sang ngưng hoạt động thay vì xóa.";
                }
                else
                {
                    msg = "Lỗi cơ sở dữ liệu: " + ex.Message;
                }
                return false;
            }
            catch (Exception ex)
            {
                msg = "Lỗi hệ thống: " + ex.Message;
                return false;
            }
        }

        // ================= GET ALL =================
        public List<NhaCungCap> getAll()
        {
            return repo.getAll();
        }

        // ================= SEARCH =================
        public List<NhaCungCap> search(string keyword)
        {
            return repo.search(keyword);
        }
    }
}
