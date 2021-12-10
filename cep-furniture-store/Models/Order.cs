using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cep_furniture_store.Models
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        public Guid OrderId { get; set; }
        public string OrderDay { get; set; }
        public string status { get; set; }
    }
}
