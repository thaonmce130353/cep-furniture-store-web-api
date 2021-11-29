using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cep_furniture_store.Models
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int status { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
