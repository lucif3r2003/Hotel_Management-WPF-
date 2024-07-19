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

namespace Hotel_App_View.Admin
{
    /// <summary>
    /// Interaction logic for RoomDetail.xaml
    /// </summary>
    public partial class RoomDetail : Window
    {
        private int RoomID; 
        public RoomDetail(int id)
        {
            InitializeComponent();
            this.RoomID = id;
            RoomDAO rDAO = new RoomDAO();
            StatusDAO statusDAO = new StatusDAO();
            var r =rDAO.getRoomById(id);
            var type = rDAO.getListRoomType();
            var status = statusDAO.getListStatus();
            if (r != null)
            {
                txtId.Text = r.RoomId.ToString();
                txtName.Text = r.RoomNumber.ToString();
                txtPrice.Text = r.RoomNumber.ToString();
                cbbType.ItemsSource = type;
                cbbType.DisplayMemberPath = "TypeName";
                cbbType.SelectedValuePath = "RoomTypeId";
                cbbType.SelectedValue = r.RoomTypeId.ToString();
                cbbStatus.ItemsSource = status;
                cbbStatus.DisplayMemberPath = "StatusName";
                cbbStatus.SelectedValuePath = "StatusId";
                cbbStatus.SelectedValue = r.StatusId.ToString();
            }
        }



        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var context = new HotelManagementContext();
            var r = context.Rooms.Include(r => r.RoomType).SingleOrDefault(x => x.RoomId == RoomID);
            if (r != null)
            {
                r.StatusId = (int)cbbStatus.SelectedValue;
                r.RoomTypeId =(int)cbbType.SelectedValue;
                r.Price = Decimal.Parse(txtPrice.Text);
                context.SaveChanges();
            }
        }
    }
}
