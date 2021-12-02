using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cep.Backend.ReceiveEndPoint.Models
{
    class Category
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public int status { get; set; }
    }
}
