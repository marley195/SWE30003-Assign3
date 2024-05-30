using Newtonsoft.Json;

namespace RelaxingKoala
{
    public class MenuItem
    {
        //Create temporary file
        string path = Path.GetTempFileName();
        
        public int MenuItemId { get; set; }
        public int Price { get; set; }
        public  string Name { get; set; }
    }
}