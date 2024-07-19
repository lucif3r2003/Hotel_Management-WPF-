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

namespace Hotel_App_View.Staff
{
    /// <summary>
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        private int staffId;    
        public StaffWindow(int id)
        {
            InitializeComponent();
            this.staffId = id;
            loadBooking();
        }

        private void loadBooking()
        {
            BookingDAO bDAO = new BookingDAO();
            var list = bDAO.getBookingListStaff();
            lvBooking.ItemsSource = list;
            lvBooking.Items.Refresh();
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            BookingListlView.Visibility = Visibility.Collapsed;
            ProfileView.Visibility = Visibility.Visible;
            loadProfile(staffId);

        }
        private void loadProfile(int id)
        {
            using(var context = new HotelManagementContext())
            {
                StaffDAO sDAO = new StaffDAO();
                var s =sDAO.getStaffByID(id);
                txtFirstName.Text = s.FirstName;
                txtLastName.Text = s.LastName;
                txtMail.Text = s.Email;
                txtPhone.Text = s.PhoneNumber;
                pwbPw.Password = s.Password;
            }
        }

        private void lvBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookingListlView.Visibility = Visibility.Collapsed;
            ProfileView.Visibility = Visibility.Collapsed;

        }
    }
}
