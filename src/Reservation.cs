namespace RelaxingKoala
{
    public class Reservation
    {

        public Reservation(Customer customer, Table table, DateTime reservationTime, int numberOfGuests)
        {
            ReservationId++;
            Customer = customer;
            Table = table;
            ReservationTime = reservationTime;
            NumberOfGuests = numberOfGuests;
        }

        public int ReservationId { get; set; }
        public Customer Customer { get; set; }
        public Table Table { get; set;}
        public DateTime ReservationTime { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
