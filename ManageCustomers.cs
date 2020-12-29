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
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
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

        private void ManageCustomers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerId.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            customername.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
            customerPhone.Text = CustomersGV.SelectedRows[0].Cells[2].Value.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into CustomerTable values('" + CustomerId.Text + "','" + customername.Text + "', '" + customerPhone.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer has been added");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }


        private void delete_Click(object sender, EventArgs e)
        {
            if (CustomerId.Text == "")
            {
                MessageBox.Show("Enter Customers ID number");
            }
            else
            {
                Con.Open();
                string myQuery = "delete from CustomerTable where CustId='" + CustomerId.Text + "'";
                SqlCommand cmd = new SqlCommand(myQuery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE CustomerTable SET Custname='" + customername.Text + "', CustPhone='" + customerPhone.Text + "' WHERE CustId='" + CustomerId.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Succesfully Updated");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void home_Click(object sender, EventArgs e)
        {

        }

    }
}
