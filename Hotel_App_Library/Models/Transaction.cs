using System;
using System.Collections.Generic;

namespace Hotel_App_Library.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? RoomId { get; set; }

    public int? CustomerId { get; set; }

    public int? StaffId { get; set; }

    public DateOnly? TransactionDate { get; set; }

    public decimal? Amount { get; set; }

    public string? Description { get; set; }

    public int? BookingId { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Staff? Staff { get; set; }
}
