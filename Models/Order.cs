using projectIost.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectIost.Models
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("order_number")] // Map to the correct column name
        public int Order_id { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("customer")]
        public string Customer { get; set; } = null!;

        [Column("total")]
        public decimal Total { get; set; }

        [Column("user_id")]
        public int User_id { get; set; }

        [ForeignKey("User_id")]
        public User User { get; set; } = null!;

        public ICollection<Order_Details> OrderDetails { get; set; } = new List<Order_Details>();
    }
}
   