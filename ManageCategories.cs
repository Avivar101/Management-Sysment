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
    public partial class ManageCategories : Form
    {
        public ManageCategories()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from CategoryTbl";
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

        private void ManageCategories_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into CategoryTbl values('" + categoryID.Text + "','" + categoryName.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category has been added");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE CategoryTbl SET Catname='" + categoryName.Text + "' WHERE CatId='" + categoryID.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Succesfully Updated");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (categoryID.Text == "")
            {
                MessageBox.Show("Enter Category ID number");
            }
            else
            {
                Con.Open();
                string myQuery = "delete from CategoryTbl where CatId='" + categoryID.Text + "'";
                SqlCommand cmd = new SqlCommand(myQuery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            categoryID.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            categoryName.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
        }
    }
}
