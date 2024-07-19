using Hotel_App_Library.DAO;
using Hotel_App_Library.Models;
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

namespace Hotel_App_View.Admin
{
    /// <summary>
    /// Interaction logic for CustomerDetail.xaml
    /// </summary>
    public partial class CustomerDetail : Window
    {
        private int cusId;
        public CustomerDetail(int id)
        {
            InitializeComponent();
            this.cusId = id;
            CustomerDAO cDAO = new CustomerDAO();
            var c = cDAO.getCustomerById(id);
            txtEmail.Text = c.Email;
            txtPhone.Text = c.Phone;
            txtId.Text = c.CustomerId.ToString();
            txtFirstName.Text = c.FirstName.ToString();
            txtLastName.Text = c.LastName.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = new HotelManagementContext();
                var c= context.Customers.SingleOrDefault(c => c.CustomerId == cusId);
                c.FirstName = txtFirstName.Text;
                c.LastName = txtLastName.Text;
                c.Email = txtEmail.Text;
                c.Phone = txtPhone.Text;
                context.SaveChanges();
                MessageBox.Show($"Customer details updated successfully! {c.CustomerId} {c.LastName} {c.Email}");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
    }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
