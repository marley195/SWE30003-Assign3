namespace RelaxingKoala
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Whatuppp booo");
            // Create sample data
            var customer = new Customer(1, "John Doe", "555-1234");
            var reservation = new Reservation { ReservationId = 1, CustomerId = 1, TableId = 5, ReservationTime = DateTime.Now.AddHours(2), NumberOfGuests = 4 };
            var order = new Order { OrderId = 1, CustomerId = 1 };
            var menuItem = new MenuItem { MenuItemId = 1, Name = "Pizza", Price = 15.99M };

            // Adding reservation and order to customer
            customer.AddReservation(reservation);
            order.AddItem(menuItem);
            customer.AddOrder(order);

            // Display customer details
            customer.DisplayCustomerDetails();
        }
    } 
}