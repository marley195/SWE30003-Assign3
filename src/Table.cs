namespace RelaxingKoala
{
    public class Table
    {
        public enum Status
        {
            Available,
            Reserved,
            Occupied
        }
        public Status TableStatus { get; set; }
        public int TableID { get; set; }
        public int Capacity { get; set; }

        public Table()
        { }
        public Table(int tableID, int capacity)
        {
            TableID = tableID;
            Capacity = capacity;
            TableStatus = Status.Available;
        }
        /*
                public void AddTimeSlot(DateTime startTime)
                {
                    TimeSlot TimeSlot = new TimeSlot(startTime);
                    if(!TimeSlots.Any(TimeSlot => TimeSlot.StartTime == startTime))
                    TimeSlots.Add(TimeSlot);
                }

                public bool TimeSlot(DateTime dateTime)
                {
                    var TimeSlot = TimeSlots.FirstOrDefault(TimeSlot => TimeSlot.StartTime == dateTime);
                    if(TimeSlot != null)
                    {
                        TimeSlot.BookingStatus = Status.Reserved;
                        return true;
                    }
                    return false;
                }

                public bool RemoveTimeSlot(DateTime dateTime)
                {
                    var TimeSlot = TimeSlots.FirstOrDefault(TimeSlot => TimeSlot.StartTime == dateTime);
                    if(TimeSlot != null)
                    {
                        TimeSlots.Remove(TimeSlot);
                        return true;
                    }
                    return false;
                }
                */
    }
}