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
        public List <TimeSlot> TimeSlots {get; set;}
        public Status TableStatus {get; set;}
        public int TableID { get; set; }
        public int Capacity { get; set; }

        public Table()
        {
            TimeSlots = new List<TimeSlot>();
            for(int i = 8; i < 20; i++)
            {
                AddTimeSlot(new DateTime(2024, 29, 5, i, 0, 0));
                i++;
            }
        }
        public Table(int tableID, int capacity)
        {
            TableID = tableID;
            TimeSlots = new List<TimeSlot>();
            TableStatus = Status.Available;
            for(int i = 8; i < 20; i++)
            {
                AddTimeSlot(new DateTime(2024, 1, 1, i, 0, 0));
                i++;
            }
        }

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
    }
}