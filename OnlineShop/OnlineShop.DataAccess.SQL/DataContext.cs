﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Core;

namespace OnlineShop.DataAccess.SQL
{
        public class DataContext : DbContext
        {
            public DataContext()
                : base("DefaultConnection")
            {

            }
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> ProductCategories { get; set; }
          
    }
}
