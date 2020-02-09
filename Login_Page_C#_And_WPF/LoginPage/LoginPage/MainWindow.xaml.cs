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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace LoginPage
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); //initialize MainWindow for Registration.
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(); //object for login class.
            login.Show(); //show login window for user login.
            Close(); //close the Registration winodw.
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset(); //rest function to reset the registration form.
        }

        public void Reset()
        {
            FirstNameInput.Text = ""; //firstname text from xaml tag
            LastNameInput.Text = ""; //lastname text from xaml tag
            EmailInput.Text = ""; //email text from xaml tag
            AddressInput.Text = ""; //address text from xaml tag
            passwordInput.Password = ""; //password text from xaml tag
            passwordConfirmInput.Password = ""; //confirm password text from xaml tag
            //double quotes will make the input empty.
        }
        private void Cancel_Click(object sender, RoutedEventArgs e) //cancel button function.
        {
            Close(); //close the registration window.
        }

        private void Submit_Click(object sender, RoutedEventArgs e) //submit button function.
        {
            //listing contraints for email and password using if and Regex.
            if (EmailInput.Text.Length == 0)
            {
                errormessage.Text = "Enter an email."; //show error message.
                EmailInput.Focus(); //will focus the email after the popup of error message.
            }
            else if (!Regex.IsMatch(EmailInput.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")) //regex constraints for email validation.
            {
                errormessage.Text = "Enter a valid email.";
                EmailInput.Select(0, EmailInput.Text.Length);
                EmailInput.Focus();
            }
            else
            {
                //assigning varibales for xaml input tags
                string firstname = FirstNameInput.Text;
                string lastname = LastNameInput.Text;
                string email = EmailInput.Text;
                string password = passwordInput.Password;
                //constraints for password using if
                if (passwordInput.Password.Length == 0)
                {
                    errormessage.Text = "Enter password.";
                    passwordInput.Focus();
                }
                else if (passwordConfirmInput.Password.Length == 0)
                {
                    errormessage.Text = "Enter Confirm password.";
                    passwordConfirmInput.Focus();
                }
                else if (passwordInput.Password != passwordConfirmInput.Password)
                {
                    errormessage.Text = "Confirm password must be same as password.";
                    passwordConfirmInput.Focus();
                }
                else
                {
                    errormessage.Text = "";
                    string address = AddressInput.Text;
                    //sql connection --- SSMS --> Server type = Database engine ; Auth --> Windows Auth ;
                    SqlConnection con = new SqlConnection(@"Data Source=PRAVEENRAMESH;Initial Catalog=Login_DB;Integrated Security=True;");
                    con.Open(); //create connection
                    //insert values
                    SqlCommand cmd = new SqlCommand("Insert into [Users] (FirstName,LastName,Email,Password,Address) values('" + firstname + "','" + lastname + "','" + email + "','" + password + "','" + address + "')", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery(); //executenonquery to insert values 
                    con.Close(); //close connection after data inseration
                    errormessage.Text = "You have Registered successfully.";
                    Reset(); //reset the registration form
                }
            }
        }
    }
}
