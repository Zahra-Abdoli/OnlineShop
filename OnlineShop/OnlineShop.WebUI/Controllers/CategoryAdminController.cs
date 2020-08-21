using OnlineShop.Core;
using OnlineShop.Core.Contarcts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
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
            List<Category> categories = context.Collection().ToList();
            return View(categories);
        }
        public ActionResult Create()
        {
            Category category = new Category();
            return View(category);
        }
        [HttpPost]
        public ActionResult Create(Category category, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            else
            {
                if (file != null)
                {
                    category.Image = category.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//CategoryImages//") + category.Image);
         
                }
                context.Insert(category);
                context.Commit();
                return RedirectToAction("Index");

            }
        }
        public ActionResult Edit(string Id)
        {
            Category category = context.Find(Id);
            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(category);
            }
        }
        [HttpPost]
        public ActionResult Edit(Category category, string Id)
        {
           Category categoryToEdit = context.Find(Id);
            if (categoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }
                categoryToEdit.CategoryName = category.CategoryName;
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(string id)

        {
           Category category = context.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(category);
            }
        }
        public ActionResult Delete(string Id)
        {
            Category categoryToDelete = context.Find(Id);

            if (categoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoryToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Category categoryToDelete = context.Find(Id);
            if (categoryToDelete == null)
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
