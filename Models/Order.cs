using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectIost.Models
{
    public class Order
    {
        [Key]
        public int Order_id { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; } = null!;
        public decimal Total { get; set; }

        [ForeignKey("User_id")]
        public User User { get; set; } = null!;

        public ICollection<Order_Details> OrderDetails { get; set; } = new List<Order_Details>();
    }
}
