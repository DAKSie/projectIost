using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace projectIost.Models
{
    [Table("order_details")]
    public class Order_Details
    {
        [Key]
        [Column("order_detail_id")]
        public int Order_Detail_id { get; set; }

        [Column("order_id")]
        public int Order_id { get; set; }

        [ForeignKey("Order_id")]
        public Order Order { get; set; } = null!;

        [Column("item_id")]
        public int Item_id { get; set; }

        [ForeignKey("Item_id")]
        public Item Item { get; set; } = null!;

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("subtotal")]
        public decimal Subtotal { get; set; }
    }
}
