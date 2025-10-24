using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectIost.Models
{
    public class Supply_Details
    {
        [Key]
        public int Supply_Detail_id { get; set; }
        public int Supply_id { get; set; }

        [ForeignKey("Supply_id")]
        public Supply Supply { get; set; } = null!;

        [ForeignKey("Item_id")]
        public Item Item { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
    }
}
