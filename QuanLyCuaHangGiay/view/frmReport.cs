using Microsoft.Reporting.WinForms;
using QuanLyCuaHangGiay.database.repository;
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
    public partial class frmReport : Form
    {
        public frmReport(string reportName, DataTable dt, string nguoiTao = null)
        {
            InitializeComponent();


            reportViewer1.LocalReport.ReportEmbeddedResource = reportName;

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);


            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            if (!string.IsNullOrEmpty(nguoiTao))
            {
                ReportParameter[] parameters = new ReportParameter[] { new ReportParameter("NguoiTao", nguoiTao) };
                reportViewer1.LocalReport.SetParameters(parameters);
            }

            reportViewer1.RefreshReport();
        }
    }
}
