
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Xml.Linq;

/// <summary>
/// Summary description for DataHandle
/// </summary>
public class DataHandle
{

    string connectionString;//the path to the database
    SqlConnection connection;//opens and closes the connection between the database and the server
    SqlCommand cmd;//executes the sql commands

    public DataHandle()//constructor
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Source\Repos\ofeklogin\DataDemoLogin\Database1.mdf;Integrated Security=True";
        connection = new SqlConnection(connectionString);
        cmd = new SqlCommand();
        cmd.Connection = connection;

    }

    public bool isExist(string username, string password, string email)//checkes if the info exists in the query
    {
        cmd.Connection = connection;
        cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE UserName='" + username + "' AND password='" + password + "' AND Email='" + email + "'";
        connection.Open();
        int c = (int)cmd.ExecuteScalar();
        connection.Close();
        return (c == 1);
    }

    public int GetKey(string username, string password, string email)//returns the key of the user with the matching info
    {
        cmd.Connection = connection;
        cmd.CommandText = "SELECT Id FROM Users WHERE UserName='" + username + "' AND password='" + password + "' AND Email='" + email + "'";
        connection.Open();
        int c = (int)cmd.ExecuteScalar();
        connection.Close();
        return (c);

    }

    public void AddUser(string username, string password, string fname, string lname, string email, string city, string gender)//adds user's info to the query
    {

        string error = "";
        int success = 0;
        if (fname == null || lname == null || username == null || password == null || email == null || city == null || gender == null)
        {
            error += "You must fill all fields!";
            success--;
        }
        if(gender == "-1")
        {
            error += "Pick a gender. ";
            success--;
        }
        if (!IsBasicEmailFormat(email))
        {
            error += "Email is not valid. ";
            success--;

        }
        if (!IsPasswordGood(password))
        {
            error += "Password is not strong enough. ";
            success--;
        }
        int x = 0;
        if (success == 0)
        {
            cmd.CommandText = $"INSERT INTO Users (username,password,fname,lname,email,city,gender) VALUES ('{username}','{password}','{fname}','{lname}','{email}','{city}','{gender}')";
            connection.Open();
            x = cmd.ExecuteNonQuery();
            connection.Close();
        }

        if (x > 0 && success == 0)
        {
            MessageBox.Show("regist ok");
        }

        else
            MessageBox.Show("regist not ok: " + error);
        success = 0;
    



}


    public static bool IsBasicEmailFormat(string email)
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
    }




}

/* 
 CREATE TABLE [dbo].[Users] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [username] NVARCHAR (10) NULL,
    [password] NVARCHAR (10) NULL,
    [fname]    NVARCHAR (10) NULL,
    [lname]    NVARCHAR (10) NULL,
    [email]    NVARCHAR (10) NULL,
    [city]     NVARCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

-------------------------------------------
i gotta remember to change connection string !!!!!!!
 
 
 
 
 */
