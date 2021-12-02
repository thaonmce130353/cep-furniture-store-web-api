using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OO.CEP.ReceiveEndPoint.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public int status { get; set; }

    }
}
