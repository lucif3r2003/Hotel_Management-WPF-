using System;
using System.Collections.Generic;

namespace Hotel_App_Library.Models;

public partial class RoomStatus
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
