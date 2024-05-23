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

        public Status TableStatus {get; set;}
        public int TableID { get; set; }
        public int Capacity { get; set; }

        public Table()
        {
        }
        public Table(int tableID, int capacity)
        {
            TableID = tableID;
            Capacity = capacity;
            TableStatus = Status.Available;
        }

        public Table(int tableID, string tStatus, int capacity)
        {
            TableID = tableID;
            Capacity = capacity;
            TableStatus = (Status)Enum.Parse(typeof(Status), tStatus);
        }
    }
}