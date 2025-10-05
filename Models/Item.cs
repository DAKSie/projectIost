

namespace projectIost.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string SKU { get; set; } = null!;
        public string Name { get; set; } = null;
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }
}