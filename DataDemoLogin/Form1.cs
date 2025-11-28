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

namespace DataDemoLogin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void registbtn_Click(object sender, EventArgs e)
        {
            //connection string path
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ofek\Downloads\DataDemoLogin\DataDemoLogin\DataDemoLogin\Database2.mdf;Integrated Security=True";
            // connection object
            SqlConnection connection = new SqlConnection(connectionString);
            // command object
            SqlCommand cmd = new SqlCommand();
            // match command to connection
            cmd.Connection = connection;

            string username1 = txbusername.Text;
            string password1 = txbpassword.Text;
            string fname1 = txbfname.Text;
            string lname1 = txblname.Text;
            string email1 = txbemail.Text;
            string city1 = txbcity.Text;
            //cmd.CommandText = "INSERT INTO UsersDetails VALUES('"+ 4 +"','" + username + "', '" + password + "', '" + fname + "', '" + lname + "', '" + email + "', '" + city + "')";

            cmd.CommandText = $"INSERT INTO Users (username,password,fname,lname,email,city) VALUES ('{username1}','{password1}','{fname1}','{lname1}','{email1}','{city1}')";
            connection.Open();
            int x = cmd.ExecuteNonQuery();
            connection.Close();
            if (x > 0)
                MessageBox.Show("regist ok");
            else
                MessageBox.Show("regist not ok");
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            //connection string path
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ofek\Downloads\DataDemoLogin\DataDemoLogin\DataDemoLogin\Database2.mdf;Integrated Security=True";
            // connection object
            SqlConnection connection = new SqlConnection(connectionString);
            // command object
            SqlCommand cmd = new SqlCommand();
            // match command to connection
            cmd.Connection = connection;

            string username1 = txbuserlogin.Text;
            string password1 = txbpasslogin.Text;

            cmd.CommandText = $"SELECT COUNT(*) FROM Users WHERE username='{username1}' AND password='{password1}'";
            connection.Open();
            int c = (int)cmd.ExecuteScalar();
            connection.Close();

            if (c > 0)
            {
                MessageBox.Show("login ok");
                Form2 f2 = new Form2();
                f2.Show();
            }
            else
                MessageBox.Show("login not ok");
        }
    }
}
