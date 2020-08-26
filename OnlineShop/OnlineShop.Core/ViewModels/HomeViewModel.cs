using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> products { set; get; }
        public IEnumerable<Category> productCategories { set; get; }
    }
}
