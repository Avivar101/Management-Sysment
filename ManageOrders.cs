using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Management_Sysment
{
    public partial class ManageOrders : Form
    {

        int num = 0;
        int uprice, totprice, qty;
        string product;


        public ManageOrders()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

       
        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from CustomerTable";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomersGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        void populateproducts()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        void fillcategory()
        {
            string query = "select * from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("Catname", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                searchDD.ValueMember = "Catname";
                searchDD.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }
        }

        private void ManageOrders_Load(object sender, EventArgs e)
        {
            populate();
            populateproducts();
            fillcategory();
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            custID.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void ProductGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            product = ProductGV.SelectedRows[0].Cells[1].Value.ToString();
            qty = ProductGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void searchDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTbl where ProductCategory='" + searchDD.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            populateproducts();
        }

        private void add_Click(object sender, EventArgs e)
        {

        }
    }
}
