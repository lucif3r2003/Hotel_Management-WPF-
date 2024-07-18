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

        private void loadRoom()
        {
            RoomDAO roomDAO = new RoomDAO();
            var list = roomDAO.getListAvailableRoom();
            icRoom.ItemsSource = list;
            icRoom.Items.Refresh();
            
        }

        private void loadHistory(int id)
        {
            BookingDAO bookingDAO = new BookingDAO();
            var list = bookingDAO.getListBookingById(id);
            lvBooking.ItemsSource = list;
            lvBooking.Items.Refresh();
        }

        private void btnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            RoomDAO roomsDAO = new RoomDAO();
            Button btn = sender as Button;
            if(btn != null)
            {
                int id = (int)btn.Tag;
                var room = roomsDAO.getRoomById(id);
                txtId.Text = id.ToString();
                txtName.Text = room.RoomNumber.ToString();
                txtPrice.Text = room.Price.ToString();
                txtType.Text = roomsDAO.getRoomTypeById(id);
                RoomListView.Visibility = Visibility.Collapsed;
                RoomDetailView.Visibility = Visibility.Visible;
                btnBook.Tag = id;
                MessageBox.Show($"btnBook Tag set to: {btnBook.Tag}");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
            RoomListView.Visibility = Visibility.Visible;
            RoomDetailView.Visibility = Visibility.Collapsed;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            loadRoom();
            RoomListView.Visibility = Visibility.Visible;
            RoomDetailView.Visibility = Visibility.Collapsed;
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

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            RoomListView.Visibility = Visibility.Collapsed;
            transactionHistoryView.Visibility = Visibility.Visible;
            loadHistory(customerId);
        }
    }
}
