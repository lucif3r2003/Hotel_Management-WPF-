using System;
using System.Collections.Generic;

namespace Hotel_App_Library.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string? RoomNumber { get; set; }

    public int? RoomTypeId { get; set; }

    public int? StatusId { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual RoomType? RoomType { get; set; }

    public virtual RoomStatus? Status { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
