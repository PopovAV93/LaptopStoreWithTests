using System.ComponentModel.DataAnnotations;

namespace LaptopStore.Data.Models
{
    public class Profile
    {
        public long id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        [MaxLength(250, ErrorMessage = "The maximum length must be less than 250 characters")]
        public string address { get; set; }

        [Range(0, 150, ErrorMessage = "Age range must be between 0 and 150")]
        public short? age { get; set; }

        public long userId { get; set; }
        
        public User user { get; set; }
    }
}