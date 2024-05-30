namespace RelaxingKoala
{
    public class Customer 
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Requirements { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

        // Constructor to initialize a new customer with essential details
        public Customer( string name, string contactNumber, string requirements = "")
        {
            CustomerId++;
            Name = name;
            ContactNumber = contactNumber;
            Requirements = requirements;
        }

        // Method to add an order for the customer
        public void AddOrder(Order order) => Orders.Add(order);

        // Method to display customer details along with reservations and orders
        public void DisplayCustomerDetails(ReservationManager reservationManager)
        {
            Console.WriteLine($"Customer ID: {CustomerId}, Name: {Name}, Contact: {ContactNumber}");
            Console.WriteLine("Reservations:");
            Reservation? Reservations = reservationManager.GetReservationByCustomer(CustomerId);
            Console.WriteLine($" Reservation ID: {Reservations.ReservationId}, Table ID: {Reservations.Table.TableID}, Time: {Reservations.ReservationTime}, Guests: {Reservations.NumberOfGuests}");
            Console.WriteLine("Orders:");
            foreach (var order in Orders)
            {
                Console.WriteLine($"   Order ID: {order.OrderId}, Total: ${order.TotalAmount}");
                foreach (var item in order.Items)
                {
                    Console.WriteLine($"      Item: {item.Name}, Price: ${item.Price}");
                }
            }
        }

        public void Pay()
        {
            Console.WriteLine("Please select the order number you wish to pay for:");
            DisplayOrders();
            int selectedOrder = int.Parse(Console.ReadLine() ?? "0");
        
            Order? requiredOrder = Orders.SingleOrDefault(o => o.OrderId == selectedOrder);
            if (requiredOrder == null)
            {
                Console.WriteLine("That order could not be found");
            }
            else
            {
                Console.WriteLine("Please input amount being paid:");
                decimal amount = decimal.Parse(Console.ReadLine() ?? "0");
                requiredOrder.Pay(amount);
            }
        }

        public void DisplayOrders()
        {
            foreach (Order order in Orders)
            {
                Console.WriteLine($"   Order ID: {order.OrderId}, Total: ${order.TotalAmount}");
                foreach (var item in order.Items)
                {
                    Console.WriteLine($"      Item: {item.Name}, Price: ${item.Price}");
                }
            }
        }

        public Customer? GetCustomerByID(int customerID)
        {
            if (CustomerId == customerID)
            {
                return this;
            }
            else
            {
                return null;
            }
        }
    }
}
