using OnlineShop.Core;
using OnlineShop.Core.Contarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.WebUI.Controllers
{
    public class CategoryAdminController : Controller
    {
        IRepository<Category> context;
        public CategoryAdminController(IRepository<Category> productCategoryContext)
        {
            context = productCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Category> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            Category product = new Category();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(Category product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");

            }
        }
        public ActionResult Edit(string Id)
        {
            Category product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(Category product, string Id)
        {
           Category productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit.CategoryName = product.CategoryName;
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(string id)

        {
           Category product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        public ActionResult Delete(string Id)
        {
            Category productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Category productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}
