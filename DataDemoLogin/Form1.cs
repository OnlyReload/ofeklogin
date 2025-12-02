using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDemoLogin
{
    //version of tusday
    public partial class Form1 : Form
    {
        static string error = "";
        public Form1()
        {
            InitializeComponent();
        }

        DataHandle DataHandle = new DataHandle();
        private void registbtn_Click(object sender, EventArgs e)
        {
            //connection string path
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Source\Repos\ofeklogin\DataDemoLogin\Database1.mdf;Integrated Security=True";
            // connection object
            SqlConnection connection = new SqlConnection(connectionString);
            // command object
            SqlCommand cmd = new SqlCommand();
            // match command to connection
            cmd.Connection = connection;
            if (cities.SelectedIndex < 0)
            {
                cities.SelectedIndex = 0;
                error += "City automatically set. ";
            }

            string username1 = txbusername.Text;
            string password1 = txbpassword.Text;
            string fname1 = txbfname.Text;
            string lname1 = txblname.Text;
            string email1 = txbemail.Text;
            string city1 = cities.SelectedItem.ToString();
            string gender1;
            if (male.Checked) { gender1 = "0"; }
            else if (female.Checked) { gender1 = "1"; }
            else
            {
                { gender1 = "-1"; }
            }
            DataHandle.AddUser(username1, password1, fname1, lname1, email1, city1, gender1);

        }
            /*if (IsBasicEmailFormat(email1)) { success++; } else
            {
                error += "Email is not valid. ";
            }
            if (IsPasswordGood(password1)) { success++; }
            else
            {
                error += "Password is not strong enough. ";
            }*/
            /*int x = 0;
            if (success == 3)
            {
                cmd.CommandText = $"INSERT INTO Users (username,password,fname,lname,email,city,gender) VALUES ('{username1}','{password1}','{fname1}','{lname1}','{email1}','{city1}','{gender1}')";
                connection.Open();
                x = cmd.ExecuteNonQuery();
                connection.Close();
            }

            if (x > 0 && success == 3)
            {
                MessageBox.Show("regist ok");
            }

            else
                MessageBox.Show("regist not ok: " + error);
        }*/

        private void loginbtn_Click(object sender, EventArgs e)
        {
            //connection string path
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Source\Repos\ofeklogin\DataDemoLogin\Database1.mdf;Integrated Security=True";
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
        /*public static bool IsBasicEmailFormat(string email)
        {
            String BasicEmailPattern = @"^[^@]+@[^@]+\.[^@\.]*[a-zA-Z0-9]$";


            if (string.IsNullOrWhiteSpace(email))
            {

                return false;
            }

            // The Regex.IsMatch method checks if the input string contains a match for the pattern.
            return Regex.IsMatch(email, BasicEmailPattern);
        }

        public static bool IsPasswordGood(string password)
        {
            // The regex pattern uses positive lookaheads to enforce criteria.
            // ^                        # Start of the string
            // (?=.*[0-9])              # Lookahead: Must contain at least one digit
            // (?=.*[A-Z])              # Lookahead: Must contain at least one uppercase letter
            // (?=.*[a-z])              # Lookahead: Must contain at least one lowercase letter
            // (?=.*[^a-zA-Z0-9\s])     # Lookahead: Must contain at least one special character
            // .{8,}                    # Match: Any character, 8 or more times
            // $                        # End of the string

            string pattern = @"^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[^a-zA-Z0-9\s]).{8,}$";

            if (string.IsNullOrEmpty(password))
            {

                return false;
            }

            // The Match method returns a match object if the entire string satisfies the pattern.
            return Regex.IsMatch(password, pattern);
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {
            cities.SelectedIndex = 1;
        }
    }
}
