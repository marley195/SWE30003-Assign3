namespace RelaxingKoala
{
    public class ReservationManager
    {
        private List<Reservation> Reservations;

        public ReservationManager()
        {
            Reservations = new List<Reservation>();
        }

        public void CreateReservation(Customer customer, TableManager tableManager, DateTime dateTime, int numberOfGuests)
        {
            Table? table = tableManager.FindAvailableTable(numberOfGuests, dateTime);
            if (table != null)
            {
                Reservation reservation = new Reservation(customer, table, dateTime, numberOfGuests);
                Reservations.Add(reservation);
                table.TableStatus=Table.Status.Reserved;
            }
        }
        public void OccupyTable(Customer customer, TableManager tableManager, DateTime dateTime, int numberOfGuests)
        {
            Table? table = tableManager.FindAvailableTable(numberOfGuests, dateTime);
            if (table != null)
            {
                Reservation reservation = new Reservation(customer, table, dateTime, numberOfGuests);
                Reservations.Add(reservation);
                table.TableStatus=Table.Status.Occupied;
            }

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