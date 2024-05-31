using System.Security.Cryptography;

namespace RelaxingKoala
{
    class Program
    {

        public static void Main()
        {
            //Object creation
            ReservationManager reservationManager = new ReservationManager();
            TableManager tableManager = new TableManager();
            Menu menu = new Menu();
            Kitchen Kitchen = new Kitchen();

            bool exit = false;
            string option;
            List<Customer> Customers = new List<Customer>();
            // Create sample data
            menu.AddMenuItem(new MenuItem(10, "Burger", MenuItem.FoodCategory.MainCourse));
            menu.AddMenuItem(new MenuItem(5, "Fries", MenuItem.FoodCategory.Appetizer));
            menu.AddMenuItem(new MenuItem(15, "Steak", MenuItem.FoodCategory.MainCourse));
            menu.AddMenuItem(new MenuItem(5, "Salad", MenuItem.FoodCategory.Appetizer));
            menu.AddMenuItem(new MenuItem(5, "Ice Cream", MenuItem.FoodCategory.Dessert));
            menu.AddMenuItem(new MenuItem(5, "Soda", MenuItem.FoodCategory.Drink));
            menu.AddMenuItem(new MenuItem(15, "Wine", MenuItem.FoodCategory.Drink));
            //Temporary to Generate Tables for testing
            for (int i = 0; i < 2; i++)
            {
                tableManager.CreateTable(i, RandomNumberGenerator.GetInt32(2, 10));
            }
            //tableManager.readTables();

            //Loop for Reservation + table system.
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Relaxing Koala!");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Show Customer Details");
                Console.WriteLine("2. Reserve Table");
                Console.WriteLine("3. Place an order");
                Console.WriteLine("4. Display Tables");
                Console.WriteLine("5. List Available Tables");
                Console.WriteLine("6. Find Available Table");
                Console.WriteLine("7. Pay For An Order");
                Console.WriteLine("8. Display Menu");
                Console.WriteLine("9. Exit");
                Console.Write("Select an option: ");
                option = Console.ReadLine() ?? "";
                switch (option)
                {
                    case "1":
                        {
                            if (Customers.Count>0)
                            {
                                foreach (Customer customer1 in Customers)
                                {
                                    {
                                        customer1.DisplayCustomerDetails(reservationManager);

                                    }
                                }

                            }
                            else
                            {
                                Console.WriteLine("No customer details found");
                            }
                           
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "2":
                        {
                            Console.Write("Enter number of guests: ");
                            int guests = int.Parse(Console.ReadLine() ?? "");
                            Table? table = tableManager.FindAvailableTable(guests, DateTime.Today);
                            if (table != null)
                            {
                                Console.WriteLine("Table found\nEnter you name: ");
                                string name = Console.ReadLine() ?? "";
                                Console.WriteLine("Enter your contact number: ");
                                string contact = Console.ReadLine() ?? "";
                                Customers.Add(new Customer(name, contact, "No requirements"));
                                reservationManager.CreateReservation(Customers[Customers.Count-1], tableManager, DateTime.Now.AddHours(2), guests);
                            }
                            else
                            {
                                Console.WriteLine("No tables available");
                            }
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "3":
                        {
                            Customer? customer = null;
                            Table? _table = null;
                            int guest;

                            Console.WriteLine("----- Order Menu -----");
                            Console.WriteLine("Do you have a reservation? (y/n)");

                            if (Console.ReadLine()?.ToLower() == "y")
                            {
                                Console.WriteLine("Enter your name: ");
                                string name = Console.ReadLine() ?? "";
                                customer = Customers.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                                if (customer != null)
                                {
                                    Console.WriteLine("Locating Reservation...");
                                    var reservation = reservationManager.GetReservationByCustomer(customer.Name);

                                    if (reservation != null)
                                    {
                                        _table = reservation.Table;
                                    }
                                    else
                                    {
                                        Console.WriteLine("No reservation found. We'll need to reserve a table first.");
                                        // continue to table reservation process
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Customer not found. Proceeding to table reservation...");
                                }
                            }

                            if (customer == null || _table == null)
                            {
                                Console.WriteLine("Reserving Table...");
                                Console.Write("Enter number of guests: ");

                                if (int.TryParse(Console.ReadLine(), out guest))
                                {
                                    _table = tableManager.FindAvailableTable(guest, DateTime.Today);

                                    if (_table != null)
                                    {
                                        Console.WriteLine($"Table {_table.TableID} Reserved");

                                        if (customer == null)
                                        {
                                            Console.WriteLine("Enter your name: ");
                                            string name = Console.ReadLine() ?? "";

                                            Console.WriteLine("Enter your contact number: ");
                                            string contact = Console.ReadLine() ?? "";

                                            customer = new Customer(name, contact, "No requirements");
                                            Customers.Add(customer);
                                        }

                                        reservationManager.OccupyTable(customer, tableManager, DateTime.Now, guest);
                                    }
                                    else
                                    {
                                        Console.WriteLine("No tables available, Returning to Menu");
                                        break; // exit the order process
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input for number of guests.");
                                    return; // exit the order process
                                }
                            }
                            // Take order
                            Order order = new Order(customer.CustomerId);
                            bool ordering = true;

                            while (ordering)
                            {
                                Console.Clear();
                                menu.DisplayMenu();

                                Console.Write("Enter the Menu Item ID to add to order, or 'done' to finish: ");
                                string menuItemInput = Console.ReadLine() ?? "";

                                if (menuItemInput.ToLower() == "done")
                                {
                                    customer.AddOrder(order);
                                    Kitchen.Update(order);
                                    ordering = false;
                                    Console.WriteLine("Order completed and sent to the kitchen.");
                                }
                                else if (int.TryParse(menuItemInput, out int menuItemId))
                                {
                                    MenuItem? menuItem = menu.MenuItems.FirstOrDefault(item => item.MenuItemId == menuItemId);

                                    if (menuItem != null)
                                    {
                                        order.AddItem(menuItem);
                                        Console.WriteLine($"{menuItem.Name} added to order.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Menu Item ID.");
                                    }

                                    Console.Write("Press any key to continue...");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid Menu Item ID or 'done' to finish.");
                                }
                            }
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "4":
                        tableManager.DisplayTables();
                        Console.Write("Press any key to continue...");
                        Console.Read();
                        break;
                    case "5":
                        {
                            var availableTables = tableManager.ListAvailableTables();
                            if (availableTables.Count > 0)
                            {
                                Console.WriteLine("Available Tables:");
                                foreach (var availableTable in availableTables)
                                {
                                    Console.WriteLine($"Table ID: {availableTable.TableID}, Capacity: {availableTable.Capacity}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No tables available");
                            }
                            Console.Write("Press any key to continue...");
                            Console.Read();
                            break;
                        }
                    case "6":
                        {
                            Console.Write("Enter number of guests: ");
                            int guests = int.Parse(Console.ReadLine() ?? "");
                            Table? availableTable = tableManager.FindAvailableTable(guests, DateTime.Today);
                            if (availableTable != null)
                            {
                                Console.WriteLine($"Table ID: {availableTable.TableID}, Capacity: {availableTable.Capacity}");
                            }
                            else
                            {
                                Console.WriteLine("No tables available");
                            }
                            Console.Write("Press any key to continue...");
                            Console.Read();
                            break;
                        }
                    case "7":
                        {
                            Console.WriteLine($"Select a customer to pay for an order: {String.Join(", ", Customers.Select(c => c.Name))}");
                            string customerName = Console.ReadLine() ?? "";
                            Customer? customer = Customers.FirstOrDefault(c => c.Name.Equals(customerName, StringComparison.OrdinalIgnoreCase));
                            if (customer != null)
                            {
                                string result = customer.DisplayUnpaidOrders();
                                if (result == "")
                                {
                                    Console.WriteLine("No unpaid orders found.");
                                    Console.Write("Press any key to continue...");
                                    Console.Read();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine(result);
                                }
                                Console.WriteLine("Please select the order number you wish to pay for:");
                                if (int.TryParse(Console.ReadLine() ?? "0", out int selectedOrder))
                                {
                                    Order? requiredOrder = customer.Orders.FirstOrDefault(order => order.OrderId == selectedOrder);

                                    if (requiredOrder == null)
                                    {
                                        Console.WriteLine("That order could not be found");
                                    }
                                    else
                                    {
                                        //Allows Order, Invoice and Payment to update status and complete payment process.
                                        customer.Pay(requiredOrder);
                                        //Frees table after payment
                                        reservationManager.FreeTable(requiredOrder.CustomerId);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("A valid order ID was not submitted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Customer not found");
                            }
                            Console.Write("Press any key to continue...");
                            Console.Read();
                            break;
                        }
                    case "8":
                        {
                            menu.DisplayMenu();
                            Console.Write("Press any key to continue...");
                            Console.Read();
                            break;
                        }
                    case "9":
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid option");
                            Console.Write("Press any key to continue...");
                            Console.Read();
                            break;
                        }
                }
            }
        }
    }
}

