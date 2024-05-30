namespace RelaxingKoala
{
    public class Customer
    {
        private static int nextCustomerId = 1;
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Requirements { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

        // Constructor to initialize a new customer with essential details
        public Customer(string name, string contactNumber, string requirements = "")
        {
            CustomerId = nextCustomerId++;
            Name = name;
            ContactNumber = contactNumber;
            Requirements = requirements;
        }

        // Method to add an order for the customer
        public void AddOrder(Order order) 
        {
            Orders.Add(order);
            order.OrderState=Order.State.OrderPlaced;
        } 

        // Method to display customer details along with reservations and orders
        public void DisplayCustomerDetails(ReservationManager reservationManager)
        {
            Console.WriteLine($"Customer ID: {CustomerId}, Name: {Name}, Contact: {ContactNumber}");
            Console.Write("Reservations:");
            Reservation? Reservations = reservationManager.GetReservationByCustomer(this.CustomerId);
            if(Reservations != null)
            {
                Console.WriteLine($" Reservation ID: {Reservations.ReservationId}, Table ID: {Reservations.Table.TableID}, Time: {Reservations.ReservationTime}, Guests: {Reservations.NumberOfGuests}");

            }
            else { Console.WriteLine(" no reservations"); }
            if (Orders.Count>0)
            {
                Console.WriteLine("Orders:");
                foreach (var order in Orders)
                {
                    Console.WriteLine($"   Order ID: {order.OrderId}, Total: ${order.TotalAmount}, Amount Owed: ${order.getAmountOwed}");
                    foreach (var item in order.Items)
                    {
                        Console.WriteLine($"      Item: {item.Name}, Price: ${item.Price}");
                    }
                }

            }
           
        }

        public void Pay()
        {
            int selectedOrder = 0;
            Order requiredOrder = null;
            decimal amount = 0;
            DisplayOrders();
            Console.WriteLine("Please select the order number you wish to pay for:");
            if (int.TryParse(Console.ReadLine() ?? "0", out int result))
            {
                selectedOrder = result;

                foreach (Order order in Orders)
                {
                    if (order.OrderId == selectedOrder)
                    {
                        requiredOrder = order;
                    }
                }

                if (requiredOrder == null)
                {
                    Console.WriteLine("That order could not be found");
                }
                else
                {
                    Console.WriteLine("Please input amount being paid:");
                    if (decimal.TryParse(Console.ReadLine() ?? "0.0", out decimal result2))
                    {
                        amount = result2;
                        requiredOrder.Pay((int)amount);
                    }
                    else
                    {
                        Console.WriteLine("A valid amount was not inputted. Please input an amount in the format dollars.cents with no special characters");
                    }
                }
            }
            else
            {
                Console.WriteLine("A valid order ID was not submitted.");
            }
        }

        public void DisplayOrders()
        {
            foreach (Order order in Orders)
            {
                Console.WriteLine($"   Order ID: {order.OrderId}, Total: ${order.TotalAmount}, Amount Owed: ${order.getAmountOwed}");
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