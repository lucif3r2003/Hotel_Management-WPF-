using System;
using System.Collections.Generic;

namespace Hotel_App_Library.Models;

public partial class BookingStatus
{
    public int StatusId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
