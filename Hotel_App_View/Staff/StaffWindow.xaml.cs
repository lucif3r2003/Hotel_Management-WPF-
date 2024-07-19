using Hotel_App_Library.DAO;
using Hotel_App_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            TransactionDAO transactionDAO = new TransactionDAO();
            var listT = transactionDAO.getListTransaction();
            var list = bDAO.getBookingListStaff();
            lvBooking.ItemsSource = list;
            lvTransaction.ItemsSource = listT;
            lvBooking.Items.Refresh();
            lvTransaction.Items.Refresh();
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            lvBooking.SelectedItem = null;
            BookingListlView.Visibility = Visibility.Visible;
            ProfileView.Visibility = Visibility.Collapsed;
            DetailView.Visibility = Visibility.Collapsed;
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            BookingListlView.Visibility = Visibility.Collapsed;
            ProfileView.Visibility = Visibility.Visible;
            DetailView.Visibility = Visibility.Collapsed;
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
            DetailView.Visibility = Visibility.Visible;
            if (lvBooking.SelectedItem is Booking selectedBooking)
            {
                // Retrieve the BookingId
                int bookingId = selectedBooking.BookingId;
                BookingDAO bookingDAO = new BookingDAO();
                CustomerDAO customerDAO = new CustomerDAO();  
                RoomDAO roomDAO = new RoomDAO();               
                var b = bookingDAO.getBookingById(bookingId);
                var r = roomDAO.getRoomById(b.RoomId);
                var c = customerDAO.getCustomerById(b.CustomerId);
                txtBookingId.Text = bookingId.ToString();
                txtCheckIn.Text = b.CheckInDate.ToString();
                txtCheckOut.Text = b.CheckOutDate.ToString();
                txtCusName.Text = c.FullName;
                txtRoomID.Text = b.RoomId.ToString();
                txtPrice.Text = r.Price.ToString(); 
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var context = new HotelManagementContext();
            string name = txtSearch.Text;
            CustomerDAO customerDAO = new CustomerDAO();   
            BookingDAO bookingDAO = new BookingDAO();
            var c = customerDAO.getCustomerByName(name);
            List<Booking> list = context.Bookings.Include(x=>x.Room).Include(x=>x.Customer).Where(x=>x.CustomerId==c.CustomerId).ToList();
            lvBooking.ItemsSource = list;
            lvBooking.Items.Refresh();
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            loadBooking();
            lvBooking.SelectedItem = null;
        }

        private void btnCreateTransaction_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(txtBookingId.Text);
            TransactionWindow transactionWindow = new TransactionWindow(id, staffId);
            transactionWindow.Show();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var context = new HotelManagementContext();
            var s = context.Staff.SingleOrDefault(x => x.StaffId == staffId);
            if (isValidEmail(txtMail.Text) && IsValidPhoneNumber(txtPhone.Text))
            {
                s.FirstName = txtFirstName.Text;
                s.LastName = txtLastName.Text;
                s.PhoneNumber = txtPhone.Text;
                s.Email = txtMail.Text;
                s.Password = pwbPw.Password;
                context.SaveChanges();
                MessageBox.Show("Update Information sucessfully!");
            }
            else
            {
                MessageBox.Show("Invalid email or phone number");
            }
            
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Regular expression to validate phone number
            string pattern = @"^[0-9+]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        private bool isValidEmail(string email)
        {
            return email.Contains("@");
        }
    }
}
