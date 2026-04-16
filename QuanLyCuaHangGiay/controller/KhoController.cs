using QuanLyCuaHangGiay.database.repository;
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
    public class KhoController
    {
        private KhoRepository repo = new KhoRepository();

        public DataTable GetAllKho()
        {
            return repo.getAllKho();
        }

        public DataTable FilterByDanhMuc(int danhMucID)
        {
            return repo.filterByDanhMuc(danhMucID);
        }

        public DataTable Search(string keyword)
        {
            return repo.search(keyword);
        }

        public DataTable GetDanhMuc()
        {
            return repo.getDanhMuc();
        }

        public DataTable GetLowStock(int threshold = 5)
        {
            return repo.getLowStock(threshold);
        }
    }
}
