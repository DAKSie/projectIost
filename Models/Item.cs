using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace projectIost.Models
{
    [Table("item")] // Make sure your table is actually called "item" (singular)
    public class Item
    {
        [Key]
        public int Item_id { get; set; }
        public string Item_SKU { get; set; } = null!;
        public string Item_name { get; set; } = null!;
        public int Item_quantity { get; set; }
        public decimal Item_cost { get; set; }
        public decimal Item_price { get; set; }

        // public ICollection<Order_Details> OrderDetails { get; set; } = new List<Order_Details>();
        // public ICollection<Supply_Details> SuppliesDetails { get; set; } = new List<Supply_Details>();
    }
}