namespace LaptopStore.Data.Models
{
    public class OrderDetail
    {
        public long id { get; set; }
        public long orderId { get; set; }
        public long laptopId { get; set; }
        public ulong price { get; set; }
        public virtual Laptop laptop { get; set; }
        public virtual Order order { get; set; }
    }
}