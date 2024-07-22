using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using System.Security.Authentication;

namespace Hotel_App_View.LogIn
{
    /// <summary>
    /// Interaction logic for SigUpWindow.xaml
    /// </summary>
    public partial class SigUpWindow : Window
    {
        public SigUpWindow()
        {
            InitializeComponent();
        }
        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(pwbPw.Password) ||
                string.IsNullOrEmpty(pwbConfirm.Password) ||
                pwbPw.Password != pwbConfirm.Password)
            {
                MessageBox.Show("Please fill all fields correctly.");
                return;
            }
            if((pwbPw.Password)!= (pwbConfirm.Password))
            {
                MessageBox.Show("Password does not match, please try again.");
                return;
            }
            
            Random rand = new Random();
            int otp = rand.Next(100000, 999999);

            // Send the OTP to the user's email
            SendOTPEmail(txtEmail.Text, otp);

            // Store the OTP in a secure way for later verification
            Application.Current.Properties["OTP"] = otp;

            // Hide SignUpView and show OTPView
            SignUpView.Visibility = Visibility.Collapsed;
            OTPView.Visibility = Visibility.Visible;
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
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            // Get the stored OTP
            int storedOtp = (int)Application.Current.Properties["OTP"];
            int enteredOtp;

            if (int.TryParse(txtOTP.Text, out enteredOtp))
            {
                if (enteredOtp == storedOtp)
                {
                    var context = new HotelManagementContext();
                    Hotel_App_Library.Models.Customer c = new Hotel_App_Library.Models.Customer();
                    c.Email = txtEmail.Text;
                    c.Password = pwbPw.Password;
                    c.FirstName = txtFirstName.Text;
                    c.LastName = txtLastName.Text;
                    c.Phone = txtPhone.Text;
                    c.Address = txtAddress.Text;
                    context.Add(c);
                    context.SaveChanges();
                    MessageBox.Show("Sign Up Sucessfully, please log in.");
                    this.Close();
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
    }
}
