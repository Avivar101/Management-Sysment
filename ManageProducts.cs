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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");


        //to fill in the product categories added in category page into thr product dropDowm
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
                CatDropDown.ValueMember = "Catname";
                CatDropDown.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }
        }

        void fillsearch()
        {
            string query = "select * from CategoryTbl where Catname='"+ searchDD.SelectedValue.ToString() +"'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("Catname", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatDropDown.ValueMember = "Catname";
                CatDropDown.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }
        }

        void populate()
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

        private void ManageProducts_Load(object sender, EventArgs e)
        {
            fillcategory();
            populate();
            fillsearch();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into ProductTbl values('" + productIdTbl.Text + "','" + productNameTb.Text + "', '"+ productQtyTB.Text +"', '"+ productPriceTb.Text +"', '"+ productDescriptionTb.Text +"', '"+ CatDropDown.SelectedValue.ToString() +"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product has been added");
                Con.Close();
                 populate();
            }
            catch
            {

            }
        }

        private void ProductGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            productIdTbl.Text = ProductGV.SelectedRows[0].Cells[0].Value.ToString();
            productNameTb.Text = ProductGV.SelectedRows[0].Cells[1].Value.ToString();
            productQtyTB.Text = ProductGV.SelectedRows[0].Cells[2].Value.ToString();
            productPriceTb.Text = ProductGV.SelectedRows[0].Cells[3].Value.ToString();
            productDescriptionTb.Text = ProductGV.SelectedRows[0].Cells[4].Value.ToString();
            CatDropDown.SelectedValue = ProductGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (productIdTbl.Text == "")
            {
                MessageBox.Show("Enter the Product's ID");
            }
            else
            {
                Con.Open();
                string myQuery = "delete from ProductTbl where ProductId='" + productIdTbl.Text + "'";
                SqlCommand cmd = new SqlCommand(myQuery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ProductTbl SET ProductName='" + productNameTb.Text + "', ProductQty='" + productQtyTB.Text + "', ProductPrice='" + productPriceTb.Text + "', description='"+ productDescriptionTb.Text +"', ProductCategory='"+ CatDropDown.SelectedValue.ToString() +"' WHERE ProductId='" + productIdTbl.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Succesfully Updated");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void search_Click(object sender, EventArgs e)
        {
            fillsearch();
        }
    }
}
