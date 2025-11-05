using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectIost.Models
{
    [Table("orders")] // ADD THIS
    public class Order
    {
        [Key]
        public int Order_id { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; } = null!;
        public decimal Total { get; set; }

        public int User_id { get; set; } // ADD THIS - Foreign key property

        [ForeignKey("User_id")]
        public User User { get; set; } = null!;

        public ICollection<Order_Details> OrderDetails { get; set; } = new List<Order_Details>();
    }
}