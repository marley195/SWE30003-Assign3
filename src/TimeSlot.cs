using RelaxingKoala;

public class TimeSlot
{
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Table.Status BookingStatus { get; set;}

    public TimeSlot() {}
    public TimeSlot(DateTime startTime)
    {
        Date = startTime.Date;
        StartTime = startTime;
        EndTime = startTime.AddHours(2);
        BookingStatus = Table.Status.Available;
    }
}