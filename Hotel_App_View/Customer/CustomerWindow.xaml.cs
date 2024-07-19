using Hotel_App_Library.DAO;
using Hotel_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Hotel_App_View.Customer
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private int customerId;
        public CustomerWindow(int id)
        {
            this.customerId = id;
            InitializeComponent();
            loadRoom();
        }
//------------------------------------------------------------ROOM_LIST--------------------------------------------------------------------------
        private void loadRoom()
        {
            RoomDAO roomDAO = new RoomDAO();
            var list = roomDAO.getListAvailableRoom();
            var type =roomDAO.getListRoomType();
            cbbType.ItemsSource = type;
            cbbType.DisplayMemberPath = "TypeName";
            cbbType.SelectedValuePath = "RoomTypeId";
            icRoom.ItemsSource = list;
            icRoom.Items.Refresh();
            FilerRoom.Visibility = Visibility.Visible;
        }
        private void btnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            FilerRoom.Visibility = Visibility.Collapsed;
            RoomDAO roomsDAO = new RoomDAO();
            Button btn = sender as Button;
            if (btn != null)
            {
                int id = (int)btn.Tag;
                var room = roomsDAO.getRoomById(id);
                txtId.Text = id.ToString();
                txtName.Text = room.RoomNumber.ToString();
                txtPrice.Text = room.Price.ToString();
                txtType.Text = roomsDAO.getRoomTypeById(id);
                transactionHistoryView.Visibility = Visibility.Collapsed;
                ProfileView.Visibility = Visibility.Collapsed;
                RoomListView.Visibility = Visibility.Collapsed;
                RoomDetailView.Visibility = Visibility.Visible;
                btnBook.Tag = id;
                MessageBox.Show($"btnBook Tag set to: {btnBook.Tag}");
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            loadRoom();
            FilerRoom.Visibility = Visibility.Visible;
            RoomListView.Visibility = Visibility.Visible;
            RoomDetailView.Visibility = Visibility.Collapsed;
            transactionHistoryView.Visibility = Visibility.Collapsed;
            ProfileView.Visibility = Visibility.Collapsed;
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            loadRoom();
        }

        private void cbbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int? id = cbbType.SelectedValue as int?;
            if(id != null)
            {
                RoomDAO roomDAO = new RoomDAO();
                var list = roomDAO.getListRoomByTypeId(id.Value);
                icRoom.ItemsSource = list;
                icRoom.Items.Refresh();
            }
        }


        //------------------------------------------------ROOM_DETAIL---------------------------------------------------------------------------------
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FilerRoom.Visibility = Visibility.Visible;
            RoomListView.Visibility = Visibility.Visible;
            RoomDetailView.Visibility = Visibility.Collapsed;
            transactionHistoryView.Visibility = Visibility.Collapsed;
            ProfileView.Visibility= Visibility.Collapsed;
        }

        

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if(btn != null)
            {
                int id = (int)btn.Tag;
                BookingWindow bookWin = new BookingWindow(customerId, id);
                bookWin.Show();
                
            }
        }

        private void loadHistory(int id)
        {
            BookingDAO bookingDAO = new BookingDAO();
            var list = bookingDAO.getListBookingById(id);
            lvBooking.ItemsSource = list;
            lvBooking.Items.Refresh();
        }

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            ProfileView.Visibility = Visibility.Collapsed;
            FilerRoom.Visibility= Visibility.Collapsed;
            RoomListView.Visibility = Visibility.Collapsed;
            RoomDetailView.Visibility = Visibility.Collapsed;
            transactionHistoryView.Visibility = Visibility.Visible;
            loadHistory(customerId);
        }
        //------------------------------------------------------------PROFILE-----------------------------------------------------------------------------------------------

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileView.Visibility = Visibility.Visible;
            FilerRoom.Visibility = Visibility.Collapsed;
            RoomListView.Visibility = Visibility.Collapsed;
            RoomDetailView.Visibility = Visibility.Collapsed;
            transactionHistoryView.Visibility = Visibility.Collapsed;
            loadProfile(customerId);
        }

        private void loadProfile(int id)
        {
            CustomerDAO customerDAO = new CustomerDAO();
            var customer = customerDAO.getCustomerById(id);
            tblId.Text = customer.CustomerId.ToString();
            txtFirstName.Text = customer.FirstName;
            txtLastName.Text = customer.LastName;
            txtMail.Text = customer.Email;
            txtPhone.Text = customer.Phone;
            txtAddress.Text = customer.Address;
            pwbPw.Password = customer.Password;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using(var context = new HotelManagementContext())
            {
                int id = Convert.ToInt32(tblId.Text);
                CustomerDAO customerDAO = new CustomerDAO();
                var customer = customerDAO.getCustomerById(id);
                if (customer == null)
                {
                    MessageBox.Show("Customer not found.");
                    return;
                }
                if (IsValidPhoneNumber(txtPhone.Text) && isValidEmail(txtMail.Text))
                {
                    customer.FirstName = txtFirstName.Text; customer.LastName = txtLastName.Text;
                    customer.Address = txtAddress.Text;
                    customer.Phone = txtPhone.Text;
                    customer.Email = txtMail.Text;
                    customer.Password = pwbPw.Password;
                    try
                    {
                        Debug.WriteLine(context.Entry(customer).State); // Should output "Modified"
                        context.SaveChanges();
                        Debug.WriteLine(context.Entry(customer).State); // Should output "Unchanged" after SaveChanges
                        MessageBox.Show($"Update information successfully {customerId}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while saving changes: {ex.Message}");
                    }

                }
                else
                {
                    MessageBox.Show("Invalid email or phone number");
                }
            }           
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginWindow win = new LoginWindow();
            win.Show();
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
