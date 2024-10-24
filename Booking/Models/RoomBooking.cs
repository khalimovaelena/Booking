namespace Booking.Models;

public class RoomBooking
{
    public Guid ID { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Title { get; set; }

    public Guid CustomerID { get; set; }

    public Guid RoomID { get; set; }

    public Guid UserID { get; set; }
}

