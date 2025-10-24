using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace projectIost.Models
{
    public class User
    {
        [Key]
        public int User_id { get; set; }
        public string User_name { get; set; }
        public string User_password { get; set; }
        public Boolean user_isAdmin { get; set; }
    }

}
