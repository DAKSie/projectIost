using System.ComponentModel.DataAnnotations;

namespace projectIost.Models
{
    public class Item
    {
        [Key]
        public int Item_id { get; set; }
        public string Item_SKU { get; set; } = null!;
        public string Item_name { get; set; } = null;
        public int Item_quantity { get; set; }
        public decimal Item_cost { get; set; }
        public decimal Item_price { get; set; }
    }
}