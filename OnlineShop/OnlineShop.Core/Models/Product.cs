using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core
{
  public  class Product:BaseEntity
    {
        public Product()
        { if (Discount != 0)
                this.Price = this.Price * (100 - Discount) / 100;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
    }
}
