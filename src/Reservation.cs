namespace RelaxingKoala
{
    public class Reservation
    {

        public Reservation(int ID, int customerID, int tableID, DateTime reservationTime, int numberOfGuests)
        {
            this.ReservationId = ID;
            this.CustomerId = customerID;
            this.TableId = tableID;
            this.ReservationTime = reservationTime;
            this.NumberOfGuests = numberOfGuests;
        }

        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NumberOfGuests { get; set; }
    }
}