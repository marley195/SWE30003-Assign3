namespace RelaxingKoala
{
        public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
        public decimal TotalAmount => Items.Sum(item => item.Price);

        public void AddItem(MenuItem item)
        {
            Items.Add(item);
        }
    }
}
