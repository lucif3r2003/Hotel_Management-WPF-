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
            //load combobox:
            var type = roomDAO.getListRoomType();
            cbbTypeRoom.ItemsSource = type;
            cbbTypeRoom.SelectedValuePath = "RoomTypeID";
            cbbTypeRoom.DisplayMemberPath = "TypeName";
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
        private void cbbTypeRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterRoom();
        }
        private void FilterRoom()
        {
            //get type room and status:
            int? typeId = cbbTypeRoom.SelectedValue as int?;
            RadioButton rdb = spStatus.Children.OfType<RadioButton>().FirstOrDefault(x => x.IsChecked == true);
            int? statusId = rdb.Tag as int?;
            //filter:
            using(var context = new HotelManagementContext())
            {
                var query = context.Rooms.Include(x=>x.RoomType).Include(x=>x.Status).AsQueryable();
                if (typeId != null)
                {
                    query = query.Where(x=>x.RoomTypeId== typeId);
                }
                if (statusId != null)
                {
                    query = query.Where(x=>x.StatusId== statusId);
                }
                var list = query.ToList();
                dgvRoom.ItemsSource = list;
                dgvRoom.Items.Refresh();
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


    }
}
