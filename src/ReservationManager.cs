namespace RelaxingKoala
{
    public class ReservationManager
    {
        private List<Reservation> Reservations;

        public ReservationManager()
        {
            Reservations = new List<Reservation>();

        }

        public Reservation CreateReservation(int customerID, TableManger tableManger, DateTime reservationTime, int numberOfGuests)
        {
            Table table = tableManger.FindAvailableTable(numberOfGuests);
            Reservation reservation = new Reservation( customerID, table.TableID, reservationTime, numberOfGuests);

            Reservations.Add(reservation);
            return reservation;
        }


        public void RemoveReservation(int reservationID)
        {
            Reservations.RemoveAll(res => res.ReservationId == reservationID);
        }

        public void DisplayReservations()
        {
            foreach (var res in Reservations)
            {
                Console.WriteLine($"Reservation ID: {res.ReservationId}, Customer ID: {res.CustomerId}, Table ID: {res.TableId}, Time: {res.ReservationTime}, Guests: {res.NumberOfGuests}");
            }
        }

        public Reservation? GetReservationByID(int reservationID)
        {
            return Reservations.SingleOrDefault(res => res.ReservationId == reservationID);
        }

        public Reservation? GetReservationByCustomer(int customerID)
        {
            return Reservations.SingleOrDefault(res => res.CustomerId == customerID);
        }
    }
}