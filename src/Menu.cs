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
            Console.WriteLine("Menu Items:");
            foreach (var menuItem in MenuItems)
            {
                Console.WriteLine($"ID: {menuItem.MenuItemId}, Name: {menuItem.Name}, Price: ${menuItem.Price}");
            }
        }
    }
}