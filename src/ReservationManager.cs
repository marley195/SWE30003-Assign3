namespace RelaxingKoala
{
    public class ReservationManager
    {
        private List<Reservation> Reservations;
        private int ReservationID;

        public ReservationManager()
        {
            Reservations = new List<Reservation>();
            ReservationID = 1;
        }

        public Reservation CreateReservation(int customerID, int tableID, DateTime reservationTime, int numberOfGuests)
        {
            Reservation reservation = new Reservation(ReservationID, customerID, tableID, reservationTime, numberOfGuests);
            Reservations.Add(reservation);
            ReservationID++;
            return reservation;
        }

        public void DisplayReservations()
        {
            foreach (var res in Reservations)
            {
                Console.WriteLine($"Reservation ID: {res.ReservationId}, Customer ID: {res.CustomerId}, Table ID: {res.TableId}, Time: {res.ReservationTime}, Guests: {res.NumberOfGuests}");
            }
        }

        public Reservation GetReservationByID(int reservationID)
        {
            return Reservations.SingleOrDefault(res => res.ReservationId == reservationID);
        }

        public Reservation GetReservationByCustomer(int customerID)
        {
            return Reservations.SingleOrDefault(res => res.CustomerId == customerID);
        }

        public void RemoveReservation(int reservationID)
        {
            Reservation reservation = GetReservationByID(reservationID);
            if (reservation != null)
            {
                Reservations.Remove(reservation);
            }


        }
    }
}