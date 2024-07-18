using Hotel_App_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_App_Library.DAO
{
    public class RoomDAO
    {
        public List<Room> getListRoom()
        {
            var context = new HotelManagementContext();
            var list = new List<Room>();
            list = context.Rooms.Include(x=>x.RoomType).Include(x=>x.Status).ToList();  
            return list;
        }
        public List<Room> getListAvailableRoom()
        {
            var context = new HotelManagementContext();
            var list = new List<Room>();
            list = context.Rooms.Include(x => x.RoomType).Include(x => x.Status).Where(x=>x.StatusId==1).ToList();
            return list;
        }

        public Room getRoomById(int id)
        {
            using(var context = new HotelManagementContext())
            {
                var room = context.Rooms.Include(x=>x.Status).Where(x=>x.RoomId==id).FirstOrDefault();
                return room;
            }
        }
        public String getRoomTypeById(int id)
        {
            using(var context = new HotelManagementContext())
            {
                Room room = getRoomById(id);
                int? typeId = room.RoomTypeId;
                RoomType type = context.RoomTypes.Where(x=>x.RoomTypeId==typeId).FirstOrDefault();
                string s = type.TypeName;
                return s;
            }
        }
        public List<RoomType> getListRoomType() 
        {
            using( var context = new HotelManagementContext())
            {
                var list = new List<RoomType>();
                list = context.RoomTypes.ToList();
                return list;
            }
        }

        public void UpdateRoomStatus(int roomId, int statusId)
        {
            using (var context = new HotelManagementContext())
            {
                var room = context.Rooms.FirstOrDefault(x => x.RoomId == roomId);
                if (room != null)
                {
                    room.StatusId = statusId;
                    context.SaveChanges();
                }
            }
        }
    }
}
