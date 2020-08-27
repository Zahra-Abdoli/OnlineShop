using OnlineShop.Core;
using OnlineShop.Core.Contarcts;
using OnlineShop.Core.Models;
using OnlineShop.Core.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Unity.Injection;

namespace OnlineShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
      //[Authorize]
    public class ProductAdminController : Controller
    {
        IRepository<Product> context;
        IRepository<Category> productCategories;
        IRepository<Comment> comments;
        public ProductAdminController(IRepository<Product> productContext, IRepository<Category> productCategoriesContext, IRepository<Comment> commentsContext)
        {
            context = productContext;
            productCategories = productCategoriesContext;
            comments = commentsContext;

        }

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);

        }

        //public ActionResult ShowComment(string Id)
        //{
        //    //IEnumerable<CommentViewModel> listOfCommentViewModels = (from comment in comments.Collection()
        //    CommentViewModel viewModel = new CommentViewModel();
        //    viewModel.Comments = new Comment();
        //    var cat = from c in comments.Collection()
        //              where c.ProductId== Id select c;

        //    var model = cat.FirstOrDefault();

        //    return View(model);
        //}

        public ActionResult Create()
        {

            ProductAdminViewModel viewModel = new ProductAdminViewModel();
            viewModel.product = new Product();
            viewModel.productCategories = productCategories.Collection();
            

                return View(viewModel);

        }
        [HttpPost]
  
        public ActionResult Create(Product product,HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductAdminViewModel viewModel = new ProductAdminViewModel();
                viewModel.product = product;
                viewModel.productCategories = productCategories.Collection();
                return View(viewModel);
            }
        }
        [HttpPost]

        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(Id);
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
                if (file != null)
                {
                    productToEdit.Image = productToEdit.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
             
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;


                context.Commit();
               
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(string Id)

        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }

            //else
            //{
            //    return View(product);
            //}
            ViewBag.Id = Id;
            List<Comment> usercomments = comments.Collection().Where(c => c.ProductId == Id).ToList();
            ViewBag.Comments = comments;
            var ratings = comments.Collection().Where(c => c.ProductId == Id).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }


            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            int x = 0;
            var comment = form["Comment"].ToString();
            var Id = int.TryParse(form["Id"], out x);
            var rating = int.Parse(form["Rating"]);

            Comment userComment = new Comment()
            {
                ProductId = Id.ToString(),
               CommentDescritption = comment,
                Rating = rating,
               CommentedOn = DateTime.Now
            };
            comments.Insert(userComment);
            comments.Commit();


           
            return RedirectToAction("ShowRating", "ProductAdmin");
        }

        public ActionResult ShowRating()

        {

            
            return View();

        }


        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);

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
            Product productToDelete = context.Find(Id);
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