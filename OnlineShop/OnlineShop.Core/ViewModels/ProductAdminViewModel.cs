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
        public IEnumerable<Category> productCategories { set; get; }
    }
}
