using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectIost.Models
{
    [Table("supply")] // ADD THIS
    public class Supply
    {
        [Key]
        public int Supply_id { get; set; }

        public DateTime Date { get; set; }
        public string Supplier { get; set; } = null!;
        public decimal Total { get; set; }

        public int User_id { get; set; }

        [ForeignKey("User_id")]
        public User User { get; set; } = null!; // Fixed nullability

        public ICollection<Supply_Details> SupplyDetails { get; set; } = new List<Supply_Details>();
    }
}