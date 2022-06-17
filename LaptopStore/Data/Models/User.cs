using LaptopStore.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace LaptopStore.Data.Models
{
    public class User
    {
        public long id { get; set; }

        [Required]
        public string email { get; set; }
        [Required]
        public string loginName { get; set; }
        [Required]
        public string password { get; set; }

        public Role role { get; set; }
        
        public Profile profile { get; set; }
    }
}