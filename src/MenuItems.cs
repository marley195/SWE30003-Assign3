using Newtonsoft.Json;

namespace RelaxingKoala
{
    public class MenuItem
    {
        private static int _lastMenuItemId = 0;

        public MenuItem(int price, string name)
        {
            MenuItemId = ++_lastMenuItemId;
            Price = price;
            Name = name;
        }

        public int MenuItemId { get; private set; }
        public int Price { get; set; }
        public string Name { get; set; }
    }
}
