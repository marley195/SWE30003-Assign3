namespace RelaxingKoala
{
    class Program
    {
        public static void Main()
        {
            ReservationManager reservationManager = new ReservationManager();
            // Create sample data
            var customer = new Customer(1, "John Doe", "555-1234");
            reservationManager.CreateReservation(customer.CustomerId, 1, DateTime.Now.AddHours(2), 4);
            var order = new Order (1,1);
            var menuItem = new MenuItem { MenuItemId = 1, Name = "Pizza", Price = 15.99M };

            order.AddItem(menuItem);
            customer.AddOrder(order);
            
            // Display customer details
            customer.DisplayCustomerDetails(reservationManager);
        }
    } 
}