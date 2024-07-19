using Hotel_App_Library.Models;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_App_Library.DAO
{
    public class StaffDAO
    {
        public Staff getStaffByID(int id)
        {
            using(var context = new HotelManagementContext())
            {
                var s = context.Staff.Where(x => x.StaffId == id).FirstOrDefault();
                return s;
            }
        }

        public Staff getStaffByEmailPW(string email, string pw)
        {
            using (var context = new HotelManagementContext())
            {
                var s = context.Staff.Where(x=>x.Email == email && x.Password == pw).FirstOrDefault();
                return s;
            }
        }
    }
}
