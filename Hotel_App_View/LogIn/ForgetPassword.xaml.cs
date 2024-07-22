using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Hotel_App_Library.Models;
using Hotel_App_Library.DAO;

namespace Hotel_App_View.LogIn
{
    /// <summary>
    /// Interaction logic for ForgetPassword.xaml
    /// </summary>
    public partial class ForgetPassword : Window
    {
        private string email;
        public ForgetPassword(string email)
        {
            InitializeComponent();
            this.email = email;
        }


        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            // Get the stored OTP
            int storedOtp = (int)Application.Current.Properties["ForgetOTP"];
            int enteredOtp;

            if (int.TryParse(txtOTP.Text, out enteredOtp))
            {
                if (enteredOtp == storedOtp)
                {
                    OTPView.Visibility = Visibility.Collapsed;
                    NewPwView.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Invalid OTP. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid OTP.");
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            CustomerDAO customerDAO = new CustomerDAO();
            var c= customerDAO.GetCustomerByEmail(email);
            var context = new HotelManagementContext();
            if ((txtNewPw.Text) != (txtNewPwCf.Text))
            {
                MessageBox.Show("Password does not match, please try again!");
                return;
            }
            if (c != null)
            {
                c.Password = txtNewPw.Text;
                try
                {
                    context.Customers.Update(c);
                    context.SaveChanges();
                    MessageBox.Show($"Change password successfully, please log in {c.Email} ");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving changes: {ex.Message}");
                }
                
            }
        }
    }

}
