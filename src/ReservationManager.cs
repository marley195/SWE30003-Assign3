namespace RelaxingKoala
{
    public class ReservationManager
    {
        private List<Reservation> Reservations;

        public ReservationManager()
        {
            Reservations = new List<Reservation>();
        }

        public Reservation CreateReservation(Customer customer, TableManger tableManger, DateTime reservationTime, int numberOfGuests)
        {
            Reservation reservation = new Reservation(customer, tableManger.ReserveTable(numberOfGuests), reservationTime, numberOfGuests);
            Reservations.Add(reservation);
            Console.WriteLine("Reservation created, Details: " + reservation.Table.TableID + " Reservation Time: " + reservation.ReservationTime + " Number of Guests: " + reservation.NumberOfGuests + " Customer Name: " + reservation.Customer.Name + " Contact Number: " + reservation.Customer.ContactNumber);
            return reservation;
        }


        public void RemoveReservation(int reservationID)
        {
            Reservations.RemoveAll(res => res.ReservationId == reservationID);
        }

        public Reservation? GetReservationByID(int reservationID)
        {
            return Reservations.SingleOrDefault(res => res.ReservationId == reservationID);
        }

        public Reservation? GetReservationByCustomer(int customerID)
        {
            return Reservations.SingleOrDefault(res => res.Customer.CustomerId == customerID);
        }
    }
}