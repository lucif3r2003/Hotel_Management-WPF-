using Hotel_App_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hotel_App_Library.DAO
{
    public class TransactionDAO
    {
        public List<Transaction> getTransactionbyCusId(int id)
        {
            using(var context = new HotelManagementContext())
            {
                var list = context.Transactions.Include(x=>x.Room).Include(x=>x.Staff).Include(x=>x.Customer).Where(x => x.CustomerId == id).ToList();
                return list;
            }
        }

        public List<Transaction> getListTransaction()
        {
            using (var context = new HotelManagementContext())
            {
                var list = context.Transactions.Include(x => x.Room).Include(x => x.Staff).Include(x => x.Customer).ToList();
                return list;
            }
        }
    }
}
