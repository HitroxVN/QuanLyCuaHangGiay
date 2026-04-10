using QuanLyCuaHangGiay.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangGiay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool test = DBConnection.TestConnection();
            if (test)
            {
                MessageBox.Show("oke");
            }
            else
            {
                MessageBox.Show("false");
            }
        }

    }
}
