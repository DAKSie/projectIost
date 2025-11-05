using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectIost.Models
{
    [Table("order_details")] // ADD THIS
    public class Order_Details
    {
        [Key]
        public int Order_Detail_id { get; set; }
        public int Order_id { get; set; }

        [ForeignKey("Order_id")]
        public Order Order { get; set; } = null!;

        public int Item_id { get; set; }
        [ForeignKey("Item_id")]
        public Item Item { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
    }
}