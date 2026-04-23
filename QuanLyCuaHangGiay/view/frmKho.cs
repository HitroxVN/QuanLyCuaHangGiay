using QuanLyCuaHangGiay.controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangGiay.view
{
    public partial class frmKho : Form
    {
        KhoController _controller = new KhoController();
        bool isLoading = true;
        public frmKho()
        {
            InitializeComponent();
        }

        

        private void frmKho_Load(object sender, EventArgs e)
        {
            isLoading = true;

            LoadDanhMuc();
            LoadData();

            isLoading = false;
        }

        private void LoadData()
        {
            dgvKho.DataSource = _controller.GetAllKho();
            HighlightLowStock();
        }

        private void LoadDanhMuc()
        {
            cbDanhMuc.DataSource = _controller.GetDanhMuc();
            cbDanhMuc.DisplayMember = "tenDanhMuc";
            cbDanhMuc.ValueMember = "id";

            cbDanhMuc.SelectedIndex = -1;
        }

        private void HighlightLowStock()
        {
            foreach (DataGridViewRow row in dgvKho.Rows)
            {
                if (row.Cells["Column7"].Value != null)
                {
                    int sl = Convert.ToInt32(row.Cells["Column7"].Value);

                    if (sl <= 5)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (cbDanhMuc.SelectedValue != null)
            {

                txtSearch.Clear();

                int dmID = Convert.ToInt32(cbDanhMuc.SelectedValue);
                dgvKho.DataSource = _controller.FilterByDanhMuc(dmID);

                HighlightLowStock();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            isLoading = true;
            cbDanhMuc.SelectedIndex = -1;
            isLoading = false;
            dgvKho.DataSource = _controller.Search(txtSearch.Text);

            HighlightLowStock();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            isLoading = true;

            txtSearch.Clear();
            cbDanhMuc.SelectedIndex = -1;

            isLoading = false;

            LoadData();
        }

        private void dgvKho_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            HighlightLowStock();
        }

        private void cbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            txtSearch.Clear();

            if (cbDanhMuc.SelectedValue != null)
            {
                int dmID = Convert.ToInt32(cbDanhMuc.SelectedValue);
                dgvKho.DataSource = _controller.FilterByDanhMuc(dmID);
            }
        }
    }
}
