using QuanLyCuaHangGiay.database.repository;
using QuanLyCuaHangGiay.model;
using QuanLyCuaHangGiay.util;
using System;
using System.Collections.Generic;
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
        public bool delete(int id)
        {
            if (id <= 0) return false;
            return repo.delete(id);
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
