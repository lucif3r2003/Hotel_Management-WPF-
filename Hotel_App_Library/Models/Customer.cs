using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hotel_App_Library.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }
}
