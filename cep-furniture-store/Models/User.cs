using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace cep_furniture_store.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        [JsonIgnore]
        public string password { get; set; }
        public int status { get; set; }

        [ForeignKey("User")]
        public int roleId { get; set; }
        public Role Role { get; set; }
    }
}
