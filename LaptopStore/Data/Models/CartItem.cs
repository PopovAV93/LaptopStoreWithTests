namespace LaptopStore.Data.Models
{
    public class CartItem
    {
        public long id { get; set; }
        public Laptop laptop { get; set; }
        public ulong price { get; set; }
        public string CartId { get; set; }
    }
}
