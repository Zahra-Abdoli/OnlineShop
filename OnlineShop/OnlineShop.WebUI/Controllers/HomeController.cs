using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Core.Contarcts;

using OnlineShop.Core;

namespace Myshop.WebUI.Controllers
{

    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<Category> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<Category> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }
        public ActionResult Index()
        {
            List<Category> categories = productCategories.Collection().ToList();
            return View(categories);
        }
        public ActionResult Sort(string categoryName)
        {
            List<Product> products = context.Collection().Where(c => c.Category == categoryName).ToList();
            return View(products);

        }
        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}