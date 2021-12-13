using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.MediatR.Practice.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string material { get; set; }
        public string color { get; set; }
        public string dimention { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public int status { get; set; }
        public int categoryId { get; set; }
    }
}
