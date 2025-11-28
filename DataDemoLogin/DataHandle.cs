
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
        connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ofek\Downloads\DataDemoLogin\DataDemoLogin\DataDemoLogin\Database2.mdf;Integrated Security=True";
        connection = new SqlConnection(connectionString);
        cmd = new SqlCommand();

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

    public void AddUser()//adds user's info to the query
    {
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Users VALUES('1234, 'aa'')";
        connection.Open();
        cmd.ExecuteNonQuery();
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
