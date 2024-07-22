using Hotel_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_App_Library.DAO
{
    public class BookingStatusDAO
    {
        public BookingStatus getBookingStatusById(int id)
        {
            using(var context = new HotelManagementContext())
            {
                return context.BookingStatuses.FirstOrDefault(x => x.StatusId == id);
            }
        }
    }
}
