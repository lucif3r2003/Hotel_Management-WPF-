using Hotel_App_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_App_Library.DAO
{
    public class BookingDAO
    {
        public List<Booking> getBookingList()
        {
            using (var context = new HotelManagementContext())
            {
                var list = context.Bookings.Include(x => x.Room).Include(x=>x.Customer).ToList();
                return list;
            }
        }

        public List<Booking> getBookingListStaff()
        {
            using (var context = new HotelManagementContext())
            {
                var list = context.Bookings.Include(x => x.Room).Include(x => x.Customer).Where(x=>x.CheckOutDate > DateOnly.FromDateTime(DateTime.Now)).ToList();
                return list;
            }
        }
        public List<Booking> getListBookingById(int id)
        {
            using(var context = new HotelManagementContext())
            {
                var list = context.Bookings.Include(x=>x.Room).Where(x => x.CustomerId == id).ToList();
                return list;
            }
        }
    }
}
