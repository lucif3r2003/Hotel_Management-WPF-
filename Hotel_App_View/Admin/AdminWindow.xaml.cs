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
using Hotel_App_Library.Models;
using Hotel_App_Library.DAO;
using Microsoft.EntityFrameworkCore;
using Hotel_App_View.LogIn;

namespace Hotel_App_View.Admin
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            loadRoom();
            loadCus();
            loadBooking();
            loadStaff();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------ROOM_MANAGEMENT--------------------------------------------------------------------------------------------
        private void loadRoom()
        {
            RoomDAO roomDAO = new RoomDAO();
            //load datagrid:
            var list = roomDAO.getListRoom();
            dgvRoom.ItemsSource = list;
            dgvRoom.Items.Refresh();
            //load radio button:
            RadioButton rdbAvailable = new RadioButton()
            {
                Content = "Available",
                Tag = 1,
            };
            RadioButton rdbOccupied = new RadioButton()
            {
                Content = "Occupied",
                Tag = 2,
            };
            RadioButton rdbReserved = new RadioButton()
            {
                Content = "Reserved",
                Tag = 3,
            };
            spStatus.Children.Add(rdbAvailable);
            spStatus.Children.Add(rdbOccupied);
            spStatus.Children.Add(rdbReserved);
            rdbAvailable.Checked +=(s,e)=> FilterRoom();
            rdbOccupied.Checked += (s, e) => FilterRoom();
            rdbReserved.Checked += (s, e) => FilterRoom();
        }
        private void FilterRoom()
        {
            //get type room and status:
            RadioButton rdb = spStatus.Children.OfType<RadioButton>().FirstOrDefault(x => x.IsChecked == true);
            int statusId = Convert.ToInt32( rdb.Tag) ;
            //filter:
            using(var context = new HotelManagementContext())
            {
                var query = context.Rooms.Include(x=>x.RoomType).Include(x=>x.Status).AsQueryable();
                if (statusId != null)
                {
                    query = query.Where(x=>x.StatusId== statusId);
                }
                var list = query.ToList();
                dgvRoom.ItemsSource = list;
                dgvRoom.Items.Refresh();
            }
        }
        private void btnclearFilter_Click(object sender, RoutedEventArgs e)
        {
            foreach (var rdb in spStatus.Children.OfType<RadioButton>())
            {
                rdb.IsChecked = false;
            }
            RoomDAO roomDAO = new RoomDAO();
            var list = roomDAO.getListRoom();
            dgvRoom.ItemsSource = list;
            dgvRoom.Items.Refresh();
        }
        private void btnViewRoom_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if(btn != null)
            {
                int? id = btn.Tag as int?;
                if (id != null)
                {
                    RoomDetail roomDetail = new RoomDetail(id.Value);
                    roomDetail.Show();
                }
            }
        }

        private void btnclearFilter_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int? id = btn.Tag as int?;
                if (id != null)
                {
                    RoomDetail roomDetail = new RoomDetail(id.Value);
                    roomDetail.Show();
                }
            }
        }


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------CUSTOMER_MANAGEMENT----------------------------------------------------------------------------------------
        private void loadCus()
        {
            CustomerDAO customerDAO = new CustomerDAO();
            var context = new HotelManagementContext();
            var list = customerDAO.get_customer_list();
            dgvCustomer.ItemsSource = list;
            dgvCustomer.Items.Refresh();    
        }

        private void btnViewCusDetail_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (bt != null)
            {
                int? id = bt.Tag as int?;
                if (id != null)
                {
                    CustomerDetail customerDetail = new CustomerDetail(id.Value);
                    customerDetail.Show();
                }
            }
        }

        //-----------------------------------------------------BOOKING MANAGEMENT---------------------------------------------------------------------------------------

        private void loadBooking()
        {
            BookingDAO bookingDAO = new BookingDAO();
            var list = bookingDAO.getBookingList();
            dgvBooking.ItemsSource = list;
            dgvBooking.Items.Refresh();
        }

       

        private void btnViewBooking_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int? id = btn.Tag as int?;
                if(id != null)
                {
                    BookingDetail bookingDetail = new BookingDetail(id.Value);
                    bookingDetail.Show();
                }
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginWindow loginWindow = new LoginWindow(); 
            loginWindow.Show();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow  reportWindow = new ReportWindow();
            reportWindow.Show();
        }

        //----------------------------------------------------------------------Staff------------------------------------------------------------------------------------

        private void loadStaff()
        {
            var context = new HotelManagementContext(); 
            var list = context.Staff.ToList();
            dgvStaff.ItemsSource = list;
            dgvStaff.Items.Refresh();
        }
        private void btnViewStaffDetail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
