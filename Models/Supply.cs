using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectIost.Models
{
    public class Supply
    {
        [Key]
        public int Supply_id { get; set; }

        public DateTime Date { get; set; }
        public string Supplier { get; set; } = null!;
        public decimal Total {  get; set; }

        public int User_id { get; set; }

        [ForeignKey("User_id")]
        public User User { get; set; }
        public ICollection<Supply_Details> SupplyDetails { get; set; } = new List<Supply_Details>();
    }
}
