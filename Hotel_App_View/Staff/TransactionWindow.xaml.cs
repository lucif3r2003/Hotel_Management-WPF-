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
    /// Interaction logic for TransactionWindow.xaml
    /// </summary>
    public partial class TransactionWindow : Window
    {
        private int bookingID;
        private int staffID;
        public TransactionWindow(int id, int staffId)
        {
            InitializeComponent();
            this.bookingID = id;
            this.staffID = staffId;
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

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var context = new HotelManagementContext();
            BookingDAO bookingDAO = new BookingDAO();
            var b = bookingDAO.getBookingById(bookingID);
            Transaction t = new Transaction();
            t.CustomerId = b.CustomerId;
            t.RoomId = b.RoomId;
            t.StaffId = staffID;
            t.TransactionDate = DateOnly.FromDateTime(DateTime.Now);
            t.Amount = Convert.ToDecimal(txtTotal.Text);
            t.Description =txtDescription.Text;
            t.BookingId = b.BookingId;
            context.Add(t);
            context.SaveChanges();
            MessageBox.Show("Create Transaction Successfully!");
            this.Close();
        }
    }
}
