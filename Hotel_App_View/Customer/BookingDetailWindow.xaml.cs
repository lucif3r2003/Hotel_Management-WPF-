using Hotel_App_Library.DAO;
using Hotel_App_Library.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for BookingDetailWindow.xaml
    /// </summary>
    public partial class BookingDetailWindow : Window
    {
        private int cusId;
        private int bookId;
        public BookingDetailWindow(int id, int cusId)
        {
            InitializeComponent();
            this.cusId = cusId;
            this.bookId = id;
            load();
        }

        private void load()
        {
            BookingDAO bookingDAO = new BookingDAO();
            RoomDAO roomDAO = new RoomDAO();
            BookingStatusDAO bookingStatusDAO = new BookingStatusDAO();
            var b = bookingDAO.getBookingById(bookId);
            var r = roomDAO.getRoomById(b.RoomId);
            var stt = bookingStatusDAO.getBookingStatusById(b.StatusId.Value);
            txtBookingId.Text = b.BookingId.ToString();
            txtCheckIn.Text = b.CheckInDate.ToString();
            txtCheckOut.Text = b.CheckOutDate.ToString();
            txtRoomID.Text = b.RoomId.ToString();
            txtPrice.Text = r.Price.ToString();
            txtStatus.Text = stt.Description.ToString();
            if(b.StatusId == 0)
            {
                btnCancelBooking.Visibility = Visibility.Visible;
            }
            else
            {
                btnCancelBooking.Visibility = Visibility.Collapsed;
            }
            
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnCancelBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {
                    var result = MessageBox.Show("Do you want to cancel this booking?", "Confirm", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        var booking = context.Bookings.FirstOrDefault(b => b.BookingId == bookId);

                        if (booking != null)
                        {
                            booking.StatusId = 2; // Assuming 2 represents 'Cancelled' status

                            // Log before saving
                            MessageBox.Show("Attempting to save changes...");

                            context.SaveChanges();

                            // Log after saving
                            MessageBox.Show("Changes saved. Verifying...");

                            // Verify the change by re-fetching from the database
                            var updatedBooking = context.Bookings.AsNoTracking().FirstOrDefault(b => b.BookingId == bookId);
                            if (updatedBooking != null && updatedBooking.StatusId == 2)
                            {
                                MessageBox.Show("Booking cancelled successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Failed to verify booking cancellation.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Booking not found.");
                        }
                    }
                    this.Close();
                }
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Database update error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


    }
}
