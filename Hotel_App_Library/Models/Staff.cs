using System;
using System.Collections.Generic;

namespace Hotel_App_Library.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Position { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public DateOnly? HireDate { get; set; }

    public decimal? Salary { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
