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

        public Table(int tableID, int capacity)
        {
            this.TableID = tableID;
            this.Capacity = capacity;
            this.TableStatus = Status.Available;
        }

        public Status TableStatus {get; set;}
        public int TableID { get; set; }
        public int Capacity { get; set; }
    }
}