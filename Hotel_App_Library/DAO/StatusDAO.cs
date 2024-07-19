using Hotel_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_App_Library.DAO
{
    public class StatusDAO
    {
        public List<RoomStatus> getListStatus()
        {
            using(var context = new HotelManagementContext())
            {
                return context.RoomStatuses.ToList();
            }
        }
    }
}
