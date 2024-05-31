using Newtonsoft.Json;

namespace RelaxingKoala
{
    public class MenuItem
    {
        private static int _lastMenuItemId = 0;
        public enum FoodCategory { Appetizer, MainCourse, Dessert, Drink };

        public MenuItem(int price, string name, FoodCategory category)
        {
            MenuItemId = ++_lastMenuItemId;
            Category = category;
            Name = name;
            Price = price;
            
        }

        public int MenuItemId { get; private set; }
        public int Price { get; set; }
        public string Name { get; set; }
        private FoodCategory Category { get; set; }
    }
}
