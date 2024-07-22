using Hotel_App_Library.DAO;
using Hotel_App_View.Admin;
using Hotel_App_View.Customer;
using Hotel_App_View.Staff;
using Microsoft.Extensions.Configuration;
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

namespace Hotel_App_View.LogIn
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            CustomerDAO customerDAO = new CustomerDAO();
            StaffDAO sDAO = new StaffDAO();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string email = txtEmail.Text;
            string pw = pwbPw.Password;
            if (email == null || pw == null)
            {
                MessageBox.Show("Invalid email or password 1");
                return;
            }
            else
            {
                var cus = customerDAO.getCustomerByEmailpassword(email, pw);
                var s = sDAO.getStaffByEmailPW(email, pw);
                if (cus != null)
                {
                    int id = cus.CustomerId;
                    CustomerWindow cusWin = new CustomerWindow(id);
                    this.Hide();
                    cusWin.Show();

                }
                else if (s != null)
                {
                    int id = s.StaffId;
                    StaffWindow staffWindow = new StaffWindow(id);
                    this.Hide();
                    staffWindow.Show();
                }
                else if (email == config["AdminAccount:Email"] && pw == config["AdminAccount:Password"])
                {
                    AdminWindow adWin = new AdminWindow();
                    adWin.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid email or password 2");
                    return;
                }
            }

        }

        private void btnForgetPw_Click(object sender, RoutedEventArgs e)
        {
            btnLogin.Visibility = Visibility.Collapsed;
            labelLogin.Visibility = Visibility.Collapsed;
            pwbPw.Visibility = Visibility.Collapsed;
            lbPw.Visibility = Visibility.Collapsed;
            btnForgetPw.Visibility = Visibility.Collapsed;
            labelForget.Visibility = Visibility.Visible;
            btnTrigger.Visibility = Visibility.Visible;
        }

        private void btnTrigger_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            int otp = rand.Next(100000, 999999);
            SendOTPEmail(txtEmail.Text, otp);

            // Store the OTP in a secure way for later verification
            Application.Current.Properties["ForgetOTP"] = otp;
            string email = txtEmail.Text;
            ForgetPassword forgetPassword = new ForgetPassword(email);
            if (email != null)
            {
                forgetPassword.Show();
                btnLogin.Visibility = Visibility.Visible;
                labelLogin.Visibility = Visibility.Visible;
                pwbPw.Visibility = Visibility.Visible;
                lbPw.Visibility = Visibility.Visible;
                btnForgetPw.Visibility = Visibility.Visible;
                labelForget.Visibility = Visibility.Collapsed;
                btnTrigger.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Please enter your email");
            }
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            SigUpWindow sigUpWindow = new SigUpWindow();
            sigUpWindow.Show();
        }

        private void SendOTPEmail(string toEmail, int otp)
        {
            try
            {
                string fromEmail = "hotel20102003@gmail.com";
                string fromPassword = "ecfn jsan wyrl pxzp";

                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = "Your OTP Code";
                mail.Body = "Your OTP code is " + otp;

                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(fromEmail, fromPassword);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                MessageBox.Show("OTP has been sent to your email.");
            }
            catch (SmtpException smtpEx)
            {
                MessageBox.Show("SMTP Error: " + smtpEx.Message);
            }
            catch (AuthenticationException authEx)
            {
                MessageBox.Show("Authentication Error: " + authEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("General Error: " + ex.Message);
            }
        }
    }
}
