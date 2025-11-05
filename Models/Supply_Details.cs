using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectIost.Models
{
    [Table("supply_details")] // ADD THIS
    public class Supply_Details
    {
        [Key]
        public int Supply_Detail_id { get; set; }
        public int Supply_id { get; set; }

        [ForeignKey("Supply_id")]
        public Supply Supply { get; set; } = null!;

        public int Item_id { get; set; } // ADD THIS - Foreign key property was missing!

        [ForeignKey("Item_id")]
        public Item Item { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
    }
}