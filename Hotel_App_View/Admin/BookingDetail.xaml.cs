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
    /// Interaction logic for BookingDetail.xaml
    /// </summary>
    public partial class BookingDetail : Window
    {
        private int BookID;
        public BookingDetail(int id)
        {
            InitializeComponent();
            BookingDAO bookingDAO = new BookingDAO();
            CustomerDAO customerDAO = new CustomerDAO();
            RoomDAO roomDAO = new RoomDAO();
            var b = bookingDAO.getBookingById(id);
            var r = roomDAO.getRoomById(b.RoomId);
            var c = customerDAO.getCustomerById(b.CustomerId);
            txtBookingId.Text = id.ToString();
            txtCheckIn.Text = b.CheckInDate.ToString();
            txtCheckOut.Text = b.CheckOutDate.ToString();
            txtCusName.Text = c.FullName;
            txtRoomID.Text = b.RoomId.ToString();
            txtPrice.Text = r.Price.ToString();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
