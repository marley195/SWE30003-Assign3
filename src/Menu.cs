namespace RelaxingKoala
{
    public class Menu
    {
        public List<MenuItem> MenuItems { get; set; }

        public Menu()
        {
            MenuItems = new List<MenuItem>();
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            MenuItems.Add(menuItem);
        }

        public void DisplayMenu()
        {
            Console.WriteLine("Which items would you to see\n1. Appetizer\n2. Main Course\n3. Dessert\n4. Drink\n5. All Items\n6. Exit");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                Console.WriteLine("Appetizers:");
                foreach (var menuItem in MenuItems)
                {
                    if (menuItem.Category == MenuItem.FoodCategory.Appetizer)
                    {
                        Console.WriteLine($"Name: {menuItem.Name}, Price: ${menuItem.Price}. Category: {menuItem.Category}");
                    }
                }
                    break;
                case "2":
                    Console.WriteLine("Main Course:");
                    foreach (var menuItem in MenuItems)
                    {
                        if (menuItem.Category == MenuItem.FoodCategory.MainCourse)
                        {
                            Console.WriteLine($"Name: {menuItem.Name}, Price: ${menuItem.Price}.");
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine("Desserts:");
                    foreach (var menuItem in MenuItems)
                    {
                        if (menuItem.Category == MenuItem.FoodCategory.Dessert)
                        {
                            Console.WriteLine($"Name: {menuItem.Name}, Price: ${menuItem.Price}");
                        }
                    }
                    break;
                case "4":
                    Console.WriteLine("Drinks:");
                    foreach (var menuItem in MenuItems)
                    {
                        if (menuItem.Category == MenuItem.FoodCategory.Drink)
                        {
                            Console.WriteLine($"Name: {menuItem.Name}, Price: ${menuItem.Price}.");
                        }
                    }
                    break;
                case "5":
                    Console.WriteLine("All Items:");
                    foreach (var menuItem in MenuItems)
                    {
                        Console.WriteLine($" Category: {menuItem.Category}, Name: {menuItem.Name}, Price: ${menuItem.Price}.");
                    }
                    break;
                case "6":
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}