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
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        private int customerId;
        private int roomId;
        public BookingWindow(int customerId, int id)
        {
            InitializeComponent();
            txtId.Text = id.ToString();
            this.customerId = customerId;
            this.roomId = id;

        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            RoomDAO roomDAO = new RoomDAO();
            var context = new HotelManagementContext();
            Booking booking = new Booking
            {
                CustomerId = customerId,
                RoomId = roomId,
                CheckInDate = DateOnly.FromDateTime(dpkStart.SelectedDate.Value),
                CheckOutDate = DateOnly.FromDateTime(dpkEnd.SelectedDate.Value),
                BookingDate = DateOnly.FromDateTime(DateTime.Now),
                StatusId = 0
            };
            if (booking.CheckInDate >  booking.CheckOutDate)
            {
                MessageBox.Show("Invalid Date");
            }
            else if(booking.CheckInDate == null || booking.CheckOutDate == null)
            {
                MessageBox.Show("Invalid Date");
            }
            else
            {
                context.Bookings.Add(booking);
                roomDAO.UpdateRoomStatus(roomId, 3);
                context.SaveChanges();
                MessageBox.Show("Booking complete, please go to hotel on check-in date to pay the bill");
                this.Close();
            }
        }
    }
}
