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
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }

        //connection to sql database
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
                string Myquery = "select * from UserTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                UsersGV.DataSource = ds.Tables[0];
                Con.Close();           
            }
            catch
            {

            }
        }
        private void ManageUsers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //adding user data to db 
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into UserTbl values('"+unameTb.Text+"','"+passwordTb.Text+"', '"+fullnameTb.Text+"', '"+telephoneTb.Text+"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Succesfully Added");
                Con.Close();
                populate();  
            }
            catch
            {

            }
        }

        private void unameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
            if(telephoneTb.Text == "")
            {
                MessageBox.Show("Enter the user's Phone number");
            }
            else
            {
                Con.Open();
                string myQuery = "delete from UserTbl where Uphone='" + telephoneTb.Text + "'";
                SqlCommand cmd = new SqlCommand(myQuery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            unameTb.Text = UsersGV.SelectedRows[0].Cells[0].Value.ToString();
            passwordTb.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
            fullnameTb.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
            telephoneTb.Text = UsersGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE UserTbl SET Uname='"+unameTb.Text+"', Upassword='"+passwordTb.Text+"', Ufullname='"+fullnameTb.Text+"' WHERE Uphone='"+telephoneTb.Text+"'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Succesfully Updated");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }
    }
}
