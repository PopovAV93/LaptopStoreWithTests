namespace LaptopStore.Data.Models
{
    public class Laptop
    {
        public long id { get; set; }
        public string name { get; set; }
        public string shortDesc { get; set; }
        public string longDesc { get; set; }
        public ulong price { get; set; } //USD dollar
        public string imgUrl { get; set; }
        public bool isFavorite { get; set; }
        public bool isAvailable { get; set; }
        public long categoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
