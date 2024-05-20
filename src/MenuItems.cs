namespace RelaxingKoala
{
    public class MenuItem
    {
        //Create temporary file
        string path = Path.GetTempFileName();
        
        public int MenuItemId { get; set; }
        public decimal Price { get; set; }
        public required string Name { get; set; }
    }
}