using OnlineShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.ViewModels
{
     public class ProductAdminViewModel
    {
        public Product product { set; get; }
        public IEnumerable<Product> products { set; get; }
        public IEnumerable<Category> productCategories { set; get; }

        public IEnumerable<Comment> comments { get; set; }

        public Comment comment { get; set; }
    }
}
