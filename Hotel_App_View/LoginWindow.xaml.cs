using Hotel_App_Library.DAO;
using Hotel_App_Library.Models;
using Hotel_App_View.Admin;
using Hotel_App_View.Customer;
using Hotel_App_View.Staff;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Hotel_App_View
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
            if(email ==null || pw ==null)
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
                else if(email == config["AdminAccount:Email"] && pw == config["AdminAccount:Password"])
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
    }
}
