using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace LoginPage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent(); //initailize login window.
        }
        MainWindow registration = new MainWindow(); //object for mainwindow class
        Welcome welcome = new Welcome(); //object for welcome class

        private void Login_Click(object sender, RoutedEventArgs e) //login button function
        {
            //constraints for login using if and regex
            if (EmailInput.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                EmailInput.Focus();
            }
            else if (!Regex.IsMatch(EmailInput.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                EmailInput.Select(0, EmailInput.Text.Length);
                EmailInput.Focus();
            }
            else
            {
                string email = EmailInput.Text;
                string password = passwordInput.Password;
                //sql connection --> to know env see MainWindow.xaml.cs sql comment line 94.
                SqlConnection con = new SqlConnection("Data Source=PRAVEENRAMESH;Initial Catalog=Login_DB;Integrated Security=True;");
                con.Open(); //create connection
                //select login values from db.
                SqlCommand cmd = new SqlCommand("Select * from dbo.Users where Email='" + email + "'  and password='" + password + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                //check in db for data.
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string username = dataSet.Tables[0].Rows[0]["FirstName"].ToString() + " " + dataSet.Tables[0].Rows[0]["LastName"].ToString();
                    welcome.TextBlockName.Text = username;//Sending value from one form to another form.
                    welcome.Show(); //shows welcome window
                    Close(); //close login window
                }
                else
                {
                    errormessage.Text = "Sorry! Please enter existing emailid/password.";
                }
                con.Close(); //close the connection.
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            registration.Show();
            Close();
        }

    }
}
