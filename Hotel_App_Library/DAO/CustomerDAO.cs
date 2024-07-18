using Hotel_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_App_Library.DAO
{
    public class CustomerDAO
    {
        public List<Customer> get_customer_list()
        {
            using(var context = new HotelManagementContext())
            {
                return context.Customers.ToList();
            }
        }

        public Customer getCustomerById(int id)
        {
            using( var context = new HotelManagementContext())
            {
                return context.Customers.Where(x=>x.CustomerId == id).FirstOrDefault();
            }
        }

        public Customer getCustomerByEmailpassword(string mail, string password)
        {
            using(var context = new HotelManagementContext())
            {
                return context.Customers.Where(x=>x.Email==mail && x.Password==password).FirstOrDefault();   
            }
        }
    }
}
