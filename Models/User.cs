using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectIost.Models
{
    [Table("user")] // ADD THIS
    public class User
    {
        [Key]
        public int User_id { get; set; }
        public string User_name { get; set; } = null!; // Fixed: null! instead of null
        public string User_password { get; set; } = null!; // Fixed: null! instead of null
        public Boolean user_isAdmin { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>(); // Fixed: Orders instead of OrderDetails
        public ICollection<Supply> Supplies { get; set; } = new List<Supply>();
    }
}